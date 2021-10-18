using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VivasGramm
{
    class Chat
    {
        private List<Socket> usuarios = new List<Socket>();
        public Object l = new object();

        public int ComprobarPuerto()
        {
            Console.WriteLine("entrar");
            try
            {
                using (StreamReader sr = new StreamReader("C:\\puerto.txt")) 
                {
                    string linea = sr.ReadLine();
                    if (linea != null)
                    {
                        try
                        {
                            int numPuerto = Convert.ToInt32(linea);
                            if (numPuerto > 10000 && numPuerto <= IPEndPoint.MaxPort)
                            {
                                Console.WriteLine("Valor válido");
                                return numPuerto;
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Error");
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No hay archivo");
            }
            return 10000;
        }

        public void EnvioMensaje(string m, IPEndPoint ie)
        {
            IPEndPoint info;
            lock (l)
            {
                for (int i = usuarios.Count; i > 0; i--)
                {
                    info = (IPEndPoint)usuarios[i - 1].RemoteEndPoint;
                    using (NetworkStream ns = new NetworkStream(usuarios[i - 1]))
                    using (StreamWriter sw = new StreamWriter(ns))
                    {
                        try
                        {
                            if (ie.Port != info.Port)
                            {
                                //Usuarios conectados en el momento -> usuarios.Count
                                sw.WriteLine("Ip: {0}, Puerto:{1}: {2}", ie.Address, ie.Port, m);
                                sw.Flush();
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message + "");
                        }
                    }
                }
            }
        }

        public void IniciarServer()
        {
            bool puertoCorrecto = false;
            int puerto = ComprobarPuerto();
            while (!puertoCorrecto)
            {
                try
                {
                    IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), puerto);
                    puertoCorrecto = true;

                    Socket socketConexion = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socketConexion.Bind(endpoint);
                    socketConexion.Listen(2);

                    while (true)
                    {
                        Socket socketCliente = socketConexion.Accept();
                        Thread hilos = new Thread(HiloCliente);
                        hilos.Start(socketCliente);
                    }
                }
                catch (SocketException e) when (e.ErrorCode == (int)SocketError.AddressAlreadyInUse)
                {

                    if (puerto < IPEndPoint.MaxPort)
                    {
                        puerto++;
                    }
                    else
                    {
                        puerto = 10000;
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        public void HiloCliente(object Socket)
        {
            Socket socketusuario = (Socket)Socket;
            string mensaje;
            bool cerrarChat = false;

            lock (l)
            {
                usuarios.Add(socketusuario);
            }

            IPEndPoint info = (IPEndPoint)socketusuario.RemoteEndPoint;

            using (NetworkStream ns = new NetworkStream(socketusuario))
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns))
            {
                sw.WriteLine("Bienvenido a VivasGram Puerto:{0} || IP:{1}", info.Port, info.Address);
                lock (l)
                {
                    sw.WriteLine("Usuarios conectados en este momento:{0} ", usuarios.Count);
                }
                sw.Flush();

                while (!cerrarChat)
                {
                    try
                    {
                        mensaje = sr.ReadLine();
                        if (mensaje != null)
                        {
                            if (mensaje == "salir")
                            {
                                cerrarChat = true;
                                EnvioMensaje("Se ha desconectado", info);
                            }
                            else
                            {
                                EnvioMensaje(mensaje, info);
                            }
                        }

                    }
                    catch (IOException)
                    {
                        Console.WriteLine("Desconexión: " + info.Port);
                        socketusuario.Close();
                        lock (l)
                        {
                            usuarios.Remove(socketusuario);
                        }
                        cerrarChat = true;
                    }


                }

            }
        }
    }
}
