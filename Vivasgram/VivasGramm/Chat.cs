using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BienvenidaLogin;

namespace VivasGramm
{
    public class Chat
    {
        private List<Socket> clientes = new List<Socket>();
        public List<string> nombres = new List<string>();
        public List<string> nombresBase = new List<string>();

        public Object llave = new object();
        static string cadenaConexion = "Database = vivasgram; Data Source=localhost; Port = 3306; User id=root; ";
        
        MySqlConnection conectbd1 = new MySqlConnection(cadenaConexion);
        MySqlDataReader reader1 = null;
        
        MySqlConnection conectbd2 = new MySqlConnection(cadenaConexion);
        MySqlDataReader reader2 = null;
        
        MySqlConnection conectbd3 = new MySqlConnection(cadenaConexion);
        MySqlDataReader reader3 = null;
        
        public bool usuarioRegistrado = false;
        bool correcto = true;
        string nombre;

        BienvenidaLogin.Form1 f1 = new Form1();

        public Chat()
        {
            
        }

        public struct Usuario
        {
            public string nickuser;
            public int puerto;
            public NetworkStream stream;
            public StreamWriter streamwriter;
            public StreamReader streamreader;
            
        }

        Usuario user;

        public void InicioChat()
        {
            bool puertoCorrecto = false;
            usuarioRegistrado = false;
            correcto = true;

            MySqlConnection conectbd = new MySqlConnection(cadenaConexion);
            MySqlDataReader reader = null;

            try
            {
                string consulta = "Delete from usuarios";
                MySqlCommand comando = new MySqlCommand(consulta);
                comando.Connection = conectbd;
                conectbd.Open();
                reader = comando.ExecuteReader();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.ErrorCode);

            }
            finally
            {
                conectbd.Close();
            }

            int puerto = 15001;

            while (!puertoCorrecto)
                try
                {
                    IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), puerto);
                    puertoCorrecto = true;

                    Socket socketConexion = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socketConexion.Bind(endpoint);
                    socketConexion.Listen(2);

                    while (correcto)
                    {
                       
                            Socket socketCliente = socketConexion.Accept();
                            Thread hilos = new Thread(HiloCliente);

                            user = new Usuario();
                            user.stream = new NetworkStream(socketCliente);
                            user.streamwriter = new StreamWriter(user.stream);
                            user.streamreader = new StreamReader(user.stream);
                            user.nickuser = user.streamreader.ReadLine();
                            nombre = user.nickuser.ToLower();

                            nombres.Add(nombre);

                            try
                            {
                                nombresBase.Clear();
                                string consultaSiEsta = "Select nombreusuario from usuarios";
                                MySqlCommand comando2 = new MySqlCommand(consultaSiEsta);
                                comando2.Connection = conectbd2;
                                conectbd2.Open();
                                reader2 = comando2.ExecuteReader();
                                while (reader2.Read())
                                {
                                    nombresBase.Add(reader2.GetString(0));
                                }

                                for (int i = 0; i < nombres.Count; i++)
                                {
                                    for (int j = 0; j < nombresBase.Count; j++)
                                    { 
                                        if (nombresBase.Contains(nombres[i]))
                                        {
                                            
                                            user.streamwriter.WriteLine("si");
                                            usuarioRegistrado = true;
                                        }
                                        else
                                        {
                                            usuarioRegistrado = false;
                                        }
                                    }
                                }
                            }
                            catch (MySqlException e)
                            {
                                Console.WriteLine("Error: " + e.ErrorCode);

                            }
                            finally
                            {
                                conectbd2.Close();
                            }

                            if (!usuarioRegistrado)
                            {
                                hilos.Start(socketCliente);
                            }
                            else
                            {
                                
                                user.streamwriter.WriteLine("si");
                            }
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



        public void HiloCliente(object Socket)
        {
            Socket socketCliente = (Socket)Socket;
            string mensaje;
            bool cerrarChat = false;
            IPEndPoint info = (IPEndPoint)socketCliente.RemoteEndPoint;


            lock (llave)
            {
                if (!usuarioRegistrado)
                {
                    clientes.Add(socketCliente);
                    Console.WriteLine("Añadido");
                    try
                    {
                        string consulta1 = "Insert into usuarios (nombreusuario, puerto) values " + "('" + nombre + "', '" + info.Port + "');";
                        MySqlCommand comando1 = new MySqlCommand(consulta1);
                        comando1.Connection = conectbd1;
                        conectbd1.Open();
                        reader1 = comando1.ExecuteReader();
                    }
                    catch (MySqlException e)
                    {
                        Console.WriteLine("Error: " + e.ErrorCode);

                    }
                    finally
                    {
                        conectbd1.Close();
                    }

                }
            }

            using (NetworkStream ns = new NetworkStream(socketCliente))
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns))
            {
                sw.WriteLine("Bienvenido a VivasGram {0}! \nConexion al puerto:{1}", user.nickuser, info.Port);
                lock (llave)
                {
                    sw.WriteLine("Personas conectadas en el momento de tu conexión:{0} ", clientes.Count);
                }
                sw.Flush();

                while (!cerrarChat)
                {
                    try
                    {
                        mensaje = sr.ReadLine();
                        if (mensaje != null)
                        {    
                            EnvioMensaje(mensaje, info);   
                        }

                    }
                    catch (IOException)
                    {
                        Console.WriteLine("Se ha desconectado " + info.Port);
                        socketCliente.Close();
                        lock (llave)
                        {
                            try
                            {
                                string consultaDelete = "delete from usuarios where puerto = ('" + info.Port + "');";
                                MySqlCommand comando3 = new MySqlCommand(consultaDelete);
                                comando3.Connection = conectbd3;
                                conectbd3.Open();
                                reader3 = comando3.ExecuteReader();
                            }
                            catch (MySqlException e)
                            {
                                Console.WriteLine("Error: " + e.ErrorCode);

                            }
                            finally
                            {
                                conectbd3.Close();
                            }
                            nombres.RemoveAt(clientes.IndexOf(socketCliente));
                            clientes.Remove(socketCliente);
                        }
                        cerrarChat = true;

                    }


                }

            }
        }

        public void EnvioMensaje(string m, IPEndPoint ie)
        {
            IPEndPoint info;
            Usuario usermensaje;


            lock (llave)
            {
                for (int i = clientes.Count; i > 0; i--)
                {

                    {
                        info = (IPEndPoint)clientes[i - 1].RemoteEndPoint;
                        usermensaje.stream = new NetworkStream(clientes[i - 1]);
                        usermensaje.streamwriter = new StreamWriter(usermensaje.stream);
                        try
                        {

                            usermensaje.streamwriter.WriteLine(user.nickuser + " : " + m);
                            usermensaje.streamwriter.Flush();

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("" + e.Message);
                        }
                    }

                }
            }
        }

    }
}
