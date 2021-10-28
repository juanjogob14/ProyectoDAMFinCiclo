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
using System.Data.SqlClient;

namespace BienvenidaLogin
{
    public partial class Form1 : Form
    {
        // https://www.youtube.com/watch?v=Ku81JmshDuQ&ab_channel=C%C3%B3digosdeProgramaci%C3%B3n-MR
        SqlConnection conexion = new SqlConnection("server= localhost; database = vivasgram; integrated security = true");
        const string IP_SERVER = "127.0.0.1";
        string msg;
        string userMsg;
        public Form1()
        {
            InitializeComponent();
            conexion.Open();

        }
        IPEndPoint ie = new IPEndPoint(IPAddress.Parse(IP_SERVER), 15001);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private void btnConectar_Click(object sender, EventArgs e)
        {

            try
            {
                Console.WriteLine("Bieeeeen");
                string consulta = "Insert into usuarios (Nombre) values " + "('" + txtNombre.Text + "');";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
                server.Connect(ie);
                Form2 f = new Form2();
                f.ShowDialog();
                this.Close();
                
            }
            catch (SocketException se)
            {
                Console.WriteLine("Error de conexion {0}", se.ErrorCode);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            txtContrasena.PasswordChar = '*';
            
        }
    }
}
