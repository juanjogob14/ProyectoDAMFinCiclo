using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BienvenidaLogin
{
    public partial class Form1 : Form
    {
        // https://www.youtube.com/watch?v=IfJfaDtaFpQ&ab_channel=C%C3%B3digosdeProgramaci%C3%B3n-MR
        static string cadenaConexion = "Database = vivasgram; Data Source=localhost; Port = 3306; User id=root ; ";
        
        MySqlConnection conectbd = new MySqlConnection(cadenaConexion);
        MySqlConnection conectbd2 = new MySqlConnection(cadenaConexion);
        MySqlDataReader reader = null;
        MySqlDataReader reader2 = null;
        bool usuarioRegistrado = false;
        List<string> nombresUsers = new List<string>();

        const string IP_SERVER = "127.0.0.1";

        public Form1()
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;

        }
        IPEndPoint ie = new IPEndPoint(IPAddress.Parse(IP_SERVER), 15001);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private void btnConectar_Click(object sender, EventArgs e)
        {
                try
                {
                    string consultaComprueba = "Select nombreusuario from usuarios";
                    MySqlCommand comando2 = new MySqlCommand(consultaComprueba);
                    comando2.Connection = conectbd2;
                    conectbd2.Open();
                    reader2 = comando2.ExecuteReader();

                    while (reader2.Read())
                    {
                        nombresUsers.Add(reader2.GetString(0));

                        for (int i = 0; i < nombresUsers.Count; i++)
                        {
                            if (txtNombre.Text == nombresUsers[i])
                            {
                                usuarioRegistrado = true;
                            }
                            if (txtNombre.Text != nombresUsers[i])
                            {
                                usuarioRegistrado = false;
                            }
                        }
                    }

                    lblAviso.Text = "";

                    if (txtNombre.Text=="" || usuarioRegistrado)
                    {
                        lblAviso.Text = "INTRODUZCA UN USUARIO CORRECTO";
                    }
                    else
                    {
                        Form2 f = new Form2();
                        f.nombreUsuario = this.txtNombre.Text;
                        f.Conectar();
                        string consulta = "Insert into usuarios (nombreusuario) values " + "('" + txtNombre.Text + "');";
                        MySqlCommand comando = new MySqlCommand(consulta);
                        comando.Connection = conectbd;
                        conectbd.Open();
                        reader = comando.ExecuteReader();

                        f.ShowDialog();
                        this.Close();
                    }
         
                }
                catch (SocketException se)
                {
                    Console.WriteLine("Error de conexion {0}", se.ErrorCode);
                }
                finally
                {
                conectbd.Close();
                conectbd2.Close();
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            
        }
    }
}
