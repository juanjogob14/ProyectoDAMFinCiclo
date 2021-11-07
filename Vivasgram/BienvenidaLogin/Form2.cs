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
        static private NetworkStream stream;
        static private StreamWriter streamw;
        static private StreamReader streamr;
        public string nombreUsuario = "";
        static private TcpClient client = new TcpClient();
        const string IP_SERVER = "127.0.0.1";
        private List<Socket> usuarios = new List<Socket>();
        public Object l = new object();


        IPEndPoint ie = new IPEndPoint(IPAddress.Parse(IP_SERVER), 15001);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public Form2()
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void AddItem(String s)
        {
            listBox1.Items.Add(s);
        }

        private delegate void DAddItem(String s);

        public void Conectar()
        {
            try
            {
                client.Connect("127.0.0.1", 15001);
                if (client.Connected)
                {
                    Thread t = new Thread(Listen);

                    stream = client.GetStream();
                    streamw = new StreamWriter(stream);
                    streamr = new StreamReader(stream);

                    streamw.WriteLine(nombreUsuario);
                    streamw.Flush();

                    t.Start();
                }
                else
                {
                    Application.Exit();
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de conexion");
                Application.Exit();
            }
        }


        void Listen()
        {
            while (client.Connected)
            {
                try
                {
                    this.Invoke(new DAddItem(AddItem), streamr.ReadLine());

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Application.Exit();
                }
                
            }
            
          
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
           
            streamw.WriteLine(txtRedaccion.Text);
            streamw.Flush();
            txtRedaccion.Clear();

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine(nombreUsuario+" se ha desconectado");
            client.Close();
        }
    }
}
