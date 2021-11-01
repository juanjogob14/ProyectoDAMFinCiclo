
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
            this.txtMensajes = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.lblConexiones = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtRedaccion
            // 
            this.txtRedaccion.Location = new System.Drawing.Point(30, 478);
            this.txtRedaccion.Name = "txtRedaccion";
            this.txtRedaccion.Size = new System.Drawing.Size(347, 22);
            this.txtRedaccion.TabIndex = 0;
            // 
            // txtMensajes
            // 
            this.txtMensajes.Location = new System.Drawing.Point(30, 31);
            this.txtMensajes.Multiline = true;
            this.txtMensajes.Name = "txtMensajes";
            this.txtMensajes.Size = new System.Drawing.Size(347, 410);
            this.txtMensajes.TabIndex = 1;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(430, 478);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(105, 23);
            this.btnEnviar.TabIndex = 2;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // lblConexiones
            // 
            this.lblConexiones.AutoSize = true;
            this.lblConexiones.Location = new System.Drawing.Point(430, 117);
            this.lblConexiones.Name = "lblConexiones";
            this.lblConexiones.Size = new System.Drawing.Size(0, 17);
            this.lblConexiones.TabIndex = 3;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(582, 535);
            this.Controls.Add(this.lblConexiones);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.txtMensajes);
            this.Controls.Add(this.txtRedaccion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "Vivasgram";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRedaccion;
        private System.Windows.Forms.TextBox txtMensajes;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Label lblConexiones;
    }
}