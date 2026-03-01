namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormDecanato
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
            components = new System.ComponentModel.Container();
            pictureBox1 = new PictureBox();
            panelInfo = new FlowLayoutPanel();
            label1 = new Label();
            btnConsejo = new Label();
            btnOno = new Label();
            btnTrabajo = new Label();
            pbMensaje = new Panel();
            x = new PictureBox();
            lblMensaje = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            pbZonaActiva = new PictureBox();
            tmrRevisarZonas = new System.Windows.Forms.Timer(components);
            pbPizarra = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pbPersonaje = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelInfo.SuspendLayout();
            pbMensaje.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)x).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbZonaActiva).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPizarra).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.flavioHablando2;
            pictureBox1.Location = new Point(473, 82);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(121, 143);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panelInfo
            // 
            panelInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelInfo.BackColor = Color.White;
            panelInfo.Controls.Add(label1);
            panelInfo.Controls.Add(btnConsejo);
            panelInfo.Controls.Add(btnOno);
            panelInfo.Controls.Add(btnTrabajo);
            panelInfo.Location = new Point(627, 82);
            panelInfo.Name = "panelInfo";
            panelInfo.Size = new Size(320, 82);
            panelInfo.TabIndex = 4;
            panelInfo.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 10);
            label1.Margin = new Padding(20, 10, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(262, 20);
            label1.TabIndex = 0;
            label1.Text = "Hola futuro ingeniero!! Que necesitas?";
            // 
            // btnConsejo
            // 
            btnConsejo.AutoSize = true;
            btnConsejo.BackColor = Color.CornflowerBlue;
            btnConsejo.ForeColor = Color.White;
            btnConsejo.Location = new Point(20, 45);
            btnConsejo.Margin = new Padding(20, 15, 3, 0);
            btnConsejo.Name = "btnConsejo";
            btnConsejo.Size = new Size(72, 20);
            btnConsejo.TabIndex = 12;
            btnConsejo.Text = "CONSEJO";
            btnConsejo.Click += btnConsejo_Click;
            // 
            // btnOno
            // 
            btnOno.AutoSize = true;
            btnOno.BackColor = Color.CornflowerBlue;
            btnOno.ForeColor = Color.White;
            btnOno.Location = new Point(115, 45);
            btnOno.Margin = new Padding(20, 15, 3, 0);
            btnOno.Name = "btnOno";
            btnOno.Size = new Size(78, 20);
            btnOno.TabIndex = 13;
            btnOno.Text = "LEER ONO";
            btnOno.Click += btnOno_Click;
            // 
            // btnTrabajo
            // 
            btnTrabajo.AutoSize = true;
            btnTrabajo.BackColor = Color.CornflowerBlue;
            btnTrabajo.ForeColor = Color.White;
            btnTrabajo.Location = new Point(216, 45);
            btnTrabajo.Margin = new Padding(20, 15, 3, 0);
            btnTrabajo.Name = "btnTrabajo";
            btnTrabajo.Size = new Size(72, 20);
            btnTrabajo.TabIndex = 14;
            btnTrabajo.Text = "TRABAJO";
            btnTrabajo.Click += btnTrabajo_Click;
            // 
            // pbMensaje
            // 
            pbMensaje.BackColor = Color.White;
            pbMensaje.Controls.Add(x);
            pbMensaje.Controls.Add(lblMensaje);
            pbMensaje.Location = new Point(627, 82);
            pbMensaje.Name = "pbMensaje";
            pbMensaje.Size = new Size(320, 82);
            pbMensaje.TabIndex = 10;
            pbMensaje.Visible = false;
            // 
            // x
            // 
            x.BackColor = Color.Firebrick;
            x.Location = new Point(293, 0);
            x.Name = "x";
            x.Size = new Size(27, 17);
            x.TabIndex = 11;
            x.TabStop = false;
            x.Click += x_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.Location = new Point(12, 10);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(50, 20);
            lblMensaje.TabIndex = 0;
            lblMensaje.Text = "label2";
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            timer1.Interval = 400;
            timer1.Tick += timer1_Tick;
            // 
            // pbZonaActiva
            // 
            pbZonaActiva.Location = new Point(446, 212);
            pbZonaActiva.Name = "pbZonaActiva";
            pbZonaActiva.Size = new Size(171, 25);
            pbZonaActiva.TabIndex = 5;
            pbZonaActiva.TabStop = false;
            pbZonaActiva.Visible = false;
            // 
            // tmrRevisarZonas
            // 
            tmrRevisarZonas.Enabled = true;
            tmrRevisarZonas.Interval = 50;
            tmrRevisarZonas.Tick += tmrRevisarZonas_Tick;
            // 
            // pbPizarra
            // 
            pbPizarra.Location = new Point(136, 67);
            pbPizarra.Name = "pbPizarra";
            pbPizarra.Size = new Size(235, 141);
            pbPizarra.TabIndex = 6;
            pbPizarra.TabStop = false;
            pbPizarra.MouseDown += pbPizarra_MouseDown;
            pbPizarra.MouseMove += pbPizarra_MouseMove;
            pbPizarra.MouseUp += pbPizarra_MouseUp;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.RoyalBlue;
            pictureBox2.Location = new Point(215, 214);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(17, 14);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Gold;
            pictureBox3.Location = new Point(286, 214);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(17, 14);
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Firebrick;
            pictureBox4.Location = new Point(251, 214);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(17, 14);
            pictureBox4.TabIndex = 9;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // pbPersonaje
            // 
            pbPersonaje.BackColor = Color.Transparent;
            pbPersonaje.Image = Properties.Resources.gris_frente3;
            pbPersonaje.Location = new Point(491, 477);
            pbPersonaje.Name = "pbPersonaje";
            pbPersonaje.Size = new Size(88, 119);
            pbPersonaje.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPersonaje.TabIndex = 11;
            pbPersonaje.TabStop = false;
            // 
            // FormDecanato
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1058, 635);
            Controls.Add(pbPersonaje);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pbPizarra);
            Controls.Add(pbMensaje);
            Controls.Add(pbZonaActiva);
            Controls.Add(panelInfo);
            Controls.Add(pictureBox1);
            KeyPreview = true;
            Name = "FormDecanato";
            Text = "FormDecanato";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelInfo.ResumeLayout(false);
            panelInfo.PerformLayout();
            pbMensaje.ResumeLayout(false);
            pbMensaje.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)x).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbZonaActiva).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPizarra).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private FlowLayoutPanel panelInfo;
        private Label label1;
        private System.Windows.Forms.Timer timer1;
        private PictureBox pbZonaActiva;
        private System.Windows.Forms.Timer tmrRevisarZonas;
        private PictureBox pbPizarra;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private Panel pbMensaje;
        private PictureBox x;
        private Label lblMensaje;
        private Label btnConsejo;
        private Label btnOno;
        private Label btnTrabajo;
        private PictureBox pbPersonaje;
    }
}