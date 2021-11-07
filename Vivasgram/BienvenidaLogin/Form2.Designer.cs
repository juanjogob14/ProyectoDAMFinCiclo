
namespace BienvenidaLogin
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.txtRedaccion = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.lblConexiones = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtRedaccion
            // 
            this.txtRedaccion.Location = new System.Drawing.Point(22, 388);
            this.txtRedaccion.Margin = new System.Windows.Forms.Padding(2);
            this.txtRedaccion.Name = "txtRedaccion";
            this.txtRedaccion.Size = new System.Drawing.Size(261, 20);
            this.txtRedaccion.TabIndex = 0;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(322, 388);
            this.btnEnviar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(79, 19);
            this.btnEnviar.TabIndex = 2;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // lblConexiones
            // 
            this.lblConexiones.AutoSize = true;
            this.lblConexiones.Location = new System.Drawing.Point(322, 95);
            this.lblConexiones.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblConexiones.Name = "lblConexiones";
            this.lblConexiones.Size = new System.Drawing.Size(0, 13);
            this.lblConexiones.TabIndex = 3;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(22, 38);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(261, 316);
            this.listBox1.TabIndex = 4;
            // 
            // listBox2
            // 
            this.listBox2.BackColor = System.Drawing.Color.White;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(304, 95);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(120, 251);
            this.listBox2.TabIndex = 5;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(301, 59);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(110, 13);
            this.lblInfo.TabIndex = 6;
            this.lblInfo.Text = "Usuarios conectados:";
            // 
            // Form2
            // 
            this.AcceptButton = this.btnEnviar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(436, 435);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.lblConexiones);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.txtRedaccion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form2";
            this.Text = "Vivasgram";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRedaccion;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Label lblConexiones;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label lblInfo;
        public System.Windows.Forms.ListBox listBox2;
    }
}