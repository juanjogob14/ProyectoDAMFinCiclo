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

namespace VivasGramm
{
    class Chat
    {
        private TcpListener server;
        private TcpClient client = new TcpClient();
        private IPEndPoint ipendpoint = new IPEndPoint(IPAddress.Any, 15001);
        private List<Connection> list = new List<Connection>();


        private struct Connection
        {
            public NetworkStream stream;
            public StreamWriter streamw;
            public StreamReader streamr;
            public string nick;
        }

        Connection con;

        public Chat()
        {
            
        }

        string cadenaConexion = "Database = vivasgram; Data Source=localhost; Port = 3306; User id=root ; ";
        public void Inicio()
        {
            Console.WriteLine("Servidor en marcha");
            server = new TcpListener(ipendpoint);
            server.Start();


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

            while (true)
            {
                client = server.AcceptTcpClient();
                con = new Connection();
                con.stream = client.GetStream();
                con.streamr = new StreamReader(con.stream);
                con.streamw = new StreamWriter(con.stream);

                con.nick = con.streamr.ReadLine();

                list.Add(con);
                Console.WriteLine(con.nick + " se ha conectado.");

                Thread t = new Thread(Escuchar_conexion);

                t.Start();
            }


        }

        void Escuchar_conexion()
        {
            Connection usercon = con;

            do
            {
                try
                {
                    string mensaje = usercon.streamr.ReadLine();
                    Console.WriteLine(usercon.nick + ": " + mensaje);
                    foreach (Connection c in list)
                    {
                        try
                        {
                            c.streamw.WriteLine(usercon.nick + ": " + mensaje);
                            c.streamw.Flush();                            
                        }
                        catch
                        {
                            Console.WriteLine("se ha detenido la conexion");
                        }
                    }
                    
                }
                catch
                {
                    list.Remove(usercon);
                    MySqlConnection conectbd2 = new MySqlConnection(cadenaConexion);
                    MySqlDataReader reader2 = null;

                    Console.WriteLine(usercon.nick + " se ha desconectado. Usuarios conectados: "+list.Count);
                    string consulta = "Delete from usuarios where nombreusuario like '" + usercon.nick + "'";
                    MySqlCommand comando = new MySqlCommand(consulta);
                    comando.Connection = conectbd2;
                    conectbd2.Open();
                    reader2 = comando.ExecuteReader();
                    conectbd2.Close();

                    break;
                }
            } while (true);
        }
    }
}
