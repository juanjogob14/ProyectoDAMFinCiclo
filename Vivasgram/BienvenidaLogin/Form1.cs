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
using VivasGramm;
using MySql.Data.MySqlClient;

namespace BienvenidaLogin
{
    public partial class Form1 : Form
    {
        private NetworkStream stream;
        private StreamWriter streamwriter;
        private StreamReader streamreader;


        const string IP_SERVER = "127.0.0.1";
        public Object l = new object();
        public string nombreUsuario, contrasenha;
        public List<string> nombres = new List<string>();
        public bool usuarioRegistrado = false;

        IPEndPoint ie = new IPEndPoint(IPAddress.Parse(IP_SERVER), 15001);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public Form1()
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackgroundImageLayout = ImageLayout.Stretch;



        }
        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                Thread hiloListen = new Thread(EsucharConexion);
                hiloListen.Start();
                lblAviso.Text = "";
                //if (ie.)
                //{
                    if (txtNombre.Text.Contains(" ") || txtNombre.Text == "" || txtPass.Text.Contains(" ") || txtPass.Text == "")
                    {
                        lblAviso.Text = "INTRODUZCA UN USUARIO CORRECTO \nSIN ESPACIOS";
                    }
                    else
                    {
                        //for (int  i = 0;  i < c.nombresBase.Count;  i++)
                        //{
                        //    Console.WriteLine(c.nombresBase.Count); 
                        //    //if (!c.nombresBase[i].Contains(txtNombre.Text))
                        //    //{
                        //    //    lblAviso.Text = "USUARIO NO REGISTRADO , POR FAVOR INTRODUZ";
                        //    //}

                        //}
                        this.lblNombre.Visible = false;
                        this.lblAviso.Visible = false;
                        this.txtNombre.Visible = false;
                        this.txtPass.Visible = false;
                        this.btnRegistro.Visible = false;
                        this.lblPass.Visible = false;
                        this.btnCancelar.Visible = false;
                        this.btnConectar.Visible = false;

                        this.listBoxMensajes.Visible = true;
                        this.btnEnviar.Visible = true;
                        this.txtRedac.Visible = true;

                        this.Text = "VivasGram";

                        this.AcceptButton = this.btnEnviar;

                        this.BackgroundImage = BienvenidaLogin.Properties.Resources.Fondo_chat_vegetales_20_bananas;

                        nombreUsuario = this.txtNombre.Text;
                        contrasenha = this.txtPass.Text;

                        server.Connect(ie);
                        stream = new NetworkStream(server);
                        streamwriter = new StreamWriter(stream);
                        streamreader = new StreamReader(stream);

                        streamwriter.WriteLine(nombreUsuario);
                        streamwriter.WriteLine(contrasenha);

                        //Thread hiloListen = new Thread(EsucharConexion);
                        //hiloListen.Start();

                        streamwriter.Flush();

                        

                    }
                //}
                //else
                //{
                //    lblAviso.Text = "El servidor no está operativo";
                //}


            }
            catch (SocketException se)
            {
                Console.WriteLine("Error de conexion {0}", se.ErrorCode);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private delegate void DAddItem(String s);

        private void AddItem(String s)
        {
            listBoxMensajes.Items.Add(s);
        }

        void EsucharConexion()
        {
            while (server.Connected)
            {
                lock (l)
                {
                    if (server.Connected)
                    {
                        try
                        {
                            this.Invoke(new DAddItem(AddItem), streamreader.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Application.Exit();
                        }
                    }
                }

            }

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (txtRedac.Text == "" || txtRedac.Text[0] == ' ')
            {
                txtRedac.Text = ("Escribe un mensaje válido");
            }
            else
            {
                streamwriter.WriteLine(txtRedac.Text);
                streamwriter.Flush();
                txtRedac.Clear();
            }


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Close();
        }

        private void lblNombre_Click(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            

            try
            {
                //this.txtContraseñaNuev.Visible = false;
                //this.txtNombreNuevo.Visible = false;
                //this.lblNombreRegistro.Visible = false;
                //this.lblPassNueva.Visible = false;

                this.listBoxMensajes.Visible = true;
                this.txtRedac.Visible = true;
                this.btnEnviar.Visible = true;

                //this.btnAceptar.Visible = false;
                this.btnCancelarRegistro.Visible = false;

                this.AcceptButton = btnEnviar;

                this.BackgroundImage = BienvenidaLogin.Properties.Resources.Fondo_chat_vegetales_20_bananas;


                using (NetworkStream ns = new NetworkStream(server))
                using (StreamReader sr = new StreamReader(ns))
                using (StreamWriter sw = new StreamWriter(ns))
                {
                    sw.WriteLine("registro");
                    //sw.WriteLine(this.txtNombreNuevo.Text);
                    //sw.WriteLine(this.txtContraseñaNuev.Text);
                }
                Thread hiloListen = new Thread(EsucharConexion);
                hiloListen.Start();
            }
            catch (Exception)
            {

                label1.Text = "No ";
            }
            

        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                // EN caso de que el usuario no este registrado se le registra pulsando aqui 
                lblAviso.Text = "";

                //this.lblNombre.Visible = false;
                //this.lblAviso.Visible = false;
                //this.txtNombre.Visible = false;
                //this.btnCancelar.Visible = false;
                //this.btnConectar.Visible = false;
                //this.btnRegistro.Visible = false;
                //this.lblPass.Visible = false;
                //this.txtPass.Visible = false;

                //this.listBoxMensajes.Visible = false;
                //this.btnEnviar.Visible = false;
                //this.txtRedac.Visible = false;

                //this.txtNombreNuevo.Visible = true;
                //this.txtContraseñaNuev.Visible = true;
                //this.lblNombreRegistro.Visible = true;
                //this.lblPassNueva.Visible = true;
                //this.btnAceptar.Visible = true;
                //this.btnCancelarRegistro.Visible = true;

                this.Text = "VivasGram";

                //this.AcceptButton = this.btnAceptar;


            }
            catch (SocketException se)
            {
                Console.WriteLine("Error de conexion {0}", se.ErrorCode);
            }
        }

    }
}