using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BienvenidaLogin
{
    public partial class Form2 : Form
    {
        const string IP_SERVER = "127.0.0.1";
        string msg;
        string userMsg;
        private List<Socket> usuarios = new List<Socket>();
        public Object l = new object();


        IPEndPoint ie = new IPEndPoint(IPAddress.Parse(IP_SERVER), 15001);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            IPEndPoint info = null;

            Console.WriteLine(ie.Port);
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
                            Console.WriteLine(info.Port);
                            if (ie.Port != info.Port)
                            {
                                //Usuarios conectados en el momento -> usuarios.Count
                                txtMensajes.Text+=("Ip: {0}, Puerto:{1}: {2}", ie.Address, ie.Port, txtRedaccion.Text);
                                sw.Flush();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message + "");
                        }
                    }
                }
            }
            //txtMensajes.Text += (txtRedaccion.Text + ""+ie.Address) + Environment.NewLine;
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
                    Console.WriteLine("ie-> " + ie.Port + " info-> " + info.Port);
                        
                        try
                        {
                            if (ie.Port != info.Port)
                            {
                                //Usuarios conectados en el momento -> usuarios.Count
                                txtMensajes.Text += ("Ip: {0}, Puerto:{1}: {2}", ie.Address, ie.Port, m) + Environment.NewLine;
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
    }
}
