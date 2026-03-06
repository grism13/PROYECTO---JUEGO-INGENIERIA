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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDecanato));
            pictureBox1 = new PictureBox();
            panelInfo = new FlowLayoutPanel();
            label1 = new Label();
            btnTrabajo = new Label();
            btnConsejo = new Label();
            btnOno = new Label();
            pbMensaje = new Panel();
            x = new PictureBox();
            lblMensaje = new Label();
            pictureBox6 = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            pbZonaActiva = new PictureBox();
            tmrRevisarZonas = new System.Windows.Forms.Timer(components);
            pbPizarra = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pbPersonaje = new PictureBox();
            luna = new PictureBox();
            pictureBox5 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelInfo.SuspendLayout();
            pbMensaje.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)x).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbZonaActiva).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPizarra).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).BeginInit();
            ((System.ComponentModel.ISupportInitialize)luna).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.flavioHablando2;
            pictureBox1.Location = new Point(734, 193);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(138, 190);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panelInfo
            // 
            panelInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelInfo.BackColor = Color.White;
            panelInfo.Controls.Add(label1);
            panelInfo.Controls.Add(btnTrabajo);
            panelInfo.Controls.Add(btnConsejo);
            panelInfo.Controls.Add(btnOno);
            panelInfo.Location = new Point(562, 358);
            panelInfo.Name = "panelInfo";
            panelInfo.Size = new Size(298, 83);
            panelInfo.TabIndex = 4;
            panelInfo.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 11);
            label1.Margin = new Padding(21, 11, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(208, 15);
            label1.TabIndex = 0;
            label1.Text = "Hola futuro ingeniero!! Que necesitas?";
            label1.Click += label1_Click;
            // 
            // btnTrabajo
            // 
            btnTrabajo.AutoSize = true;
            btnTrabajo.BackColor = Color.CornflowerBlue;
            btnTrabajo.ForeColor = Color.White;
            btnTrabajo.Location = new Point(21, 41);
            btnTrabajo.Margin = new Padding(21, 15, 3, 0);
            btnTrabajo.Name = "btnTrabajo";
            btnTrabajo.Size = new Size(57, 15);
            btnTrabajo.TabIndex = 14;
            btnTrabajo.Text = "TRABAJO";
            btnTrabajo.Click += btnTrabajo_Click;
            // 
            // btnConsejo
            // 
            btnConsejo.AutoSize = true;
            btnConsejo.BackColor = Color.CornflowerBlue;
            btnConsejo.ForeColor = Color.White;
            btnConsejo.Location = new Point(102, 41);
            btnConsejo.Margin = new Padding(21, 15, 3, 0);
            btnConsejo.Name = "btnConsejo";
            btnConsejo.Size = new Size(58, 15);
            btnConsejo.TabIndex = 12;
            btnConsejo.Text = "CONSEJO";
            btnConsejo.Click += btnConsejo_Click;
            // 
            // btnOno
            // 
            btnOno.AutoSize = true;
            btnOno.BackColor = Color.CornflowerBlue;
            btnOno.ForeColor = Color.White;
            btnOno.Location = new Point(184, 41);
            btnOno.Margin = new Padding(21, 15, 3, 0);
            btnOno.Name = "btnOno";
            btnOno.Size = new Size(62, 15);
            btnOno.TabIndex = 13;
            btnOno.Text = "LEER ONO";
            btnOno.Click += btnOno_Click;
            // 
            // pbMensaje
            // 
            pbMensaje.BackColor = Color.Transparent;
            pbMensaje.Controls.Add(x);
            pbMensaje.Controls.Add(lblMensaje);
            pbMensaje.Controls.Add(pictureBox6);
            pbMensaje.Location = new Point(861, 91);
            pbMensaje.Margin = new Padding(3, 2, 3, 2);
            pbMensaje.Name = "pbMensaje";
            pbMensaje.Size = new Size(292, 154);
            pbMensaje.TabIndex = 10;
            pbMensaje.Visible = false;
            // 
            // x
            // 
            x.BackColor = Color.Firebrick;
            x.Location = new Point(235, 0);
            x.Margin = new Padding(3, 2, 3, 2);
            x.Name = "x";
            x.Size = new Size(21, 23);
            x.TabIndex = 11;
            x.TabStop = false;
            x.Click += x_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.BackColor = Color.White;
            lblMensaje.Location = new Point(34, 35);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(38, 15);
            lblMensaje.TabIndex = 0;
            lblMensaje.Text = "label2";
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox6
            // 
            pictureBox6.BackColor = Color.Transparent;
            pictureBox6.Image = Properties.Resources.nubedetexto;
            pictureBox6.Location = new Point(-11, 0);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(267, 137);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 14;
            pictureBox6.TabStop = false;
            // 
            // timer1
            // 
            timer1.Interval = 400;
            timer1.Tick += timer1_Tick;
            // 
            // pbZonaActiva
            // 
            pbZonaActiva.Location = new Point(562, 532);
            pbZonaActiva.Name = "pbZonaActiva";
            pbZonaActiva.Size = new Size(150, 19);
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
            pbPizarra.BackColor = Color.White;
            pbPizarra.Location = new Point(345, 136);
            pbPizarra.Name = "pbPizarra";
            pbPizarra.Size = new Size(186, 124);
            pbPizarra.TabIndex = 6;
            pbPizarra.TabStop = false;
            pbPizarra.Click += pbPizarra_Click;
            pbPizarra.MouseDown += pbPizarra_MouseDown;
            pbPizarra.MouseMove += pbPizarra_MouseMove;
            pbPizarra.MouseUp += pbPizarra_MouseUp;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.RoyalBlue;
            pictureBox2.Location = new Point(345, 266);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(23, 24);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Gold;
            pictureBox3.Location = new Point(402, 266);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(23, 24);
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Firebrick;
            pictureBox4.Location = new Point(374, 266);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(22, 24);
            pictureBox4.TabIndex = 9;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // pbPersonaje
            // 
            pbPersonaje.BackColor = Color.Transparent;
            pbPersonaje.Image = Properties.Resources.gris_frente3;
            pbPersonaje.Location = new Point(297, 655);
            pbPersonaje.Name = "pbPersonaje";
            pbPersonaje.Size = new Size(126, 181);
            pbPersonaje.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPersonaje.TabIndex = 11;
            pbPersonaje.TabStop = false;
            // 
            // luna
            // 
            luna.BackColor = Color.Transparent;
            luna.Image = Properties.Resources.lunaechada;
            luna.Location = new Point(965, 469);
            luna.Margin = new Padding(3, 4, 3, 4);
            luna.Name = "luna";
            luna.Size = new Size(173, 127);
            luna.SizeMode = PictureBoxSizeMode.Zoom;
            luna.TabIndex = 12;
            luna.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.Transparent;
            pictureBox5.Image = Properties.Resources.pizarra;
            pictureBox5.Location = new Point(286, 115);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(281, 175);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 13;
            pictureBox5.TabStop = false;
            // 
            // FormDecanato
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondodecanato;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1264, 681);
            Controls.Add(luna);
            Controls.Add(pbPersonaje);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pbPizarra);
            Controls.Add(pbZonaActiva);
            Controls.Add(panelInfo);
            Controls.Add(pbMensaje);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox5);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormDecanato";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormDecanato";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelInfo.ResumeLayout(false);
            panelInfo.PerformLayout();
            pbMensaje.ResumeLayout(false);
            pbMensaje.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)x).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbZonaActiva).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPizarra).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).EndInit();
            ((System.ComponentModel.ISupportInitialize)luna).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
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
        private PictureBox luna;
        private PictureBox pictureBox6;
        private PictureBox pictureBox5;
    }
}