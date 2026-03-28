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
            btnConsejo = new Label();
            btnOno = new Label();
            btnTrabajo = new Label();
            pbMensaje = new Panel();
            pictureBox15 = new PictureBox();
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
            pictureBox7 = new PictureBox();
            pictureBox8 = new PictureBox();
            pictureBox9 = new PictureBox();
            pictureBox10 = new PictureBox();
            pictureBox11 = new PictureBox();
            pictureBox12 = new PictureBox();
            pictureBox13 = new PictureBox();
            pictureBox14 = new PictureBox();
            panelInfo = new Panel();
            pbPuertaSalida = new PictureBox();
            pictureBox16 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pbMensaje.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox15).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox14).BeginInit();
            panelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbPuertaSalida).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.flavioHablando2;
            pictureBox1.Location = new Point(839, 257);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(158, 253);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // btnConsejo
            // 
            btnConsejo.BackColor = Color.Transparent;
            btnConsejo.Cursor = Cursors.Hand;
            btnConsejo.ForeColor = Color.Gainsboro;
            btnConsejo.Image = Properties.Resources.botonazul;
            btnConsejo.Location = new Point(45, 125);
            btnConsejo.Margin = new Padding(24, 20, 3, 0);
            btnConsejo.Name = "btnConsejo";
            btnConsejo.Size = new Size(163, 49);
            btnConsejo.TabIndex = 12;
            btnConsejo.Text = "CONSEJO";
            btnConsejo.TextAlign = ContentAlignment.MiddleCenter;
            btnConsejo.Click += btnConsejo_Click;
            // 
            // btnOno
            // 
            btnOno.AllowDrop = true;
            btnOno.BackColor = Color.Transparent;
            btnOno.Cursor = Cursors.Hand;
            btnOno.ForeColor = Color.White;
            btnOno.Image = Properties.Resources.botonrosa;
            btnOno.Location = new Point(221, 125);
            btnOno.Margin = new Padding(24, 20, 3, 0);
            btnOno.Name = "btnOno";
            btnOno.Size = new Size(168, 49);
            btnOno.TabIndex = 13;
            btnOno.Text = "LEER ONO";
            btnOno.TextAlign = ContentAlignment.MiddleCenter;
            btnOno.Click += btnOno_Click;
            // 
            // btnTrabajo
            // 
            btnTrabajo.BackColor = Color.Transparent;
            btnTrabajo.Cursor = Cursors.Hand;
            btnTrabajo.ForeColor = Color.White;
            btnTrabajo.Image = Properties.Resources.botonverde1;
            btnTrabajo.Location = new Point(398, 125);
            btnTrabajo.Margin = new Padding(24, 20, 3, 0);
            btnTrabajo.Name = "btnTrabajo";
            btnTrabajo.Size = new Size(170, 49);
            btnTrabajo.TabIndex = 14;
            btnTrabajo.Text = "TRABAJO";
            btnTrabajo.TextAlign = ContentAlignment.MiddleCenter;
            btnTrabajo.Click += btnTrabajo_Click;
            // 
            // pbMensaje
            // 
            pbMensaje.BackColor = Color.Transparent;
            pbMensaje.Controls.Add(pictureBox15);
            pbMensaje.Controls.Add(x);
            pbMensaje.Controls.Add(lblMensaje);
            pbMensaje.Controls.Add(pictureBox6);
            pbMensaje.Location = new Point(990, 141);
            pbMensaje.Name = "pbMensaje";
            pbMensaje.Size = new Size(371, 245);
            pbMensaje.TabIndex = 10;
            pbMensaje.Visible = false;
            // 
            // pictureBox15
            // 
            pictureBox15.BackColor = Color.Transparent;
            pictureBox15.Image = Properties.Resources.consejosicon;
            pictureBox15.Location = new Point(3, 177);
            pictureBox15.Margin = new Padding(3, 4, 3, 4);
            pictureBox15.Name = "pictureBox15";
            pictureBox15.Size = new Size(34, 69);
            pictureBox15.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox15.TabIndex = 23;
            pictureBox15.TabStop = false;
            // 
            // x
            // 
            x.BackColor = Color.Transparent;
            x.Cursor = Cursors.Hand;
            x.Image = Properties.Resources.botondecerrar;
            x.Location = new Point(303, 177);
            x.Name = "x";
            x.Size = new Size(45, 52);
            x.SizeMode = PictureBoxSizeMode.Zoom;
            x.TabIndex = 11;
            x.TabStop = false;
            x.Click += x_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.BackColor = Color.White;
            lblMensaje.ForeColor = Color.Black;
            lblMensaje.Location = new Point(37, 40);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(262, 128);
            lblMensaje.TabIndex = 0;
            lblMensaje.Text = "label2";
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox6
            // 
            pictureBox6.BackColor = Color.Transparent;
            pictureBox6.Image = Properties.Resources.nubedetexto;
            pictureBox6.Location = new Point(-13, 0);
            pictureBox6.Margin = new Padding(3, 4, 3, 4);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(360, 229);
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
            pbZonaActiva.Location = new Point(647, 552);
            pbZonaActiva.Margin = new Padding(3, 4, 3, 4);
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
            pbPizarra.BackColor = Color.White;
            pbPizarra.Location = new Point(392, 168);
            pbPizarra.Margin = new Padding(3, 4, 3, 4);
            pbPizarra.Name = "pbPizarra";
            pbPizarra.Size = new Size(213, 131);
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
            pictureBox2.Location = new Point(407, 308);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(26, 15);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Gold;
            pictureBox3.Location = new Point(470, 307);
            pictureBox3.Margin = new Padding(3, 4, 3, 4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(26, 15);
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Firebrick;
            pictureBox4.Location = new Point(439, 308);
            pictureBox4.Margin = new Padding(3, 4, 3, 4);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(25, 15);
            pictureBox4.TabIndex = 9;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // pbPersonaje
            // 
            pbPersonaje.BackColor = Color.Transparent;
            pbPersonaje.Image = Properties.Resources.gris_frente3;
            pbPersonaje.Location = new Point(661, 628);
            pbPersonaje.Margin = new Padding(3, 4, 3, 4);
            pbPersonaje.Name = "pbPersonaje";
            pbPersonaje.Size = new Size(134, 215);
            pbPersonaje.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPersonaje.TabIndex = 11;
            pbPersonaje.TabStop = false;
            // 
            // luna
            // 
            luna.BackColor = Color.Transparent;
            luna.Image = Properties.Resources.lunaechada;
            luna.Location = new Point(707, 371);
            luna.Margin = new Padding(3, 5, 3, 5);
            luna.Name = "luna";
            luna.Size = new Size(111, 92);
            luna.SizeMode = PictureBoxSizeMode.Zoom;
            luna.TabIndex = 12;
            luna.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.Transparent;
            pictureBox5.Image = Properties.Resources.pizarra;
            pictureBox5.Location = new Point(328, 144);
            pictureBox5.Margin = new Padding(3, 4, 3, 4);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(321, 179);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 13;
            pictureBox5.TabStop = false;
            // 
            // pictureBox7
            // 
            pictureBox7.BackColor = Color.Red;
            pictureBox7.Location = new Point(446, 489);
            pictureBox7.Margin = new Padding(3, 4, 3, 4);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(111, 35);
            pictureBox7.TabIndex = 14;
            pictureBox7.TabStop = false;
            pictureBox7.Tag = "muro";
            pictureBox7.Visible = false;
            // 
            // pictureBox8
            // 
            pictureBox8.BackColor = Color.Red;
            pictureBox8.Location = new Point(928, 519);
            pictureBox8.Margin = new Padding(3, 4, 3, 4);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(566, 35);
            pictureBox8.TabIndex = 15;
            pictureBox8.TabStop = false;
            pictureBox8.Tag = "muro";
            pictureBox8.Visible = false;
            // 
            // pictureBox9
            // 
            pictureBox9.BackColor = Color.Red;
            pictureBox9.Location = new Point(551, 509);
            pictureBox9.Margin = new Padding(3, 4, 3, 4);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(384, 35);
            pictureBox9.TabIndex = 16;
            pictureBox9.TabStop = false;
            pictureBox9.Tag = "muro";
            pictureBox9.Visible = false;
            // 
            // pictureBox10
            // 
            pictureBox10.BackColor = Color.Red;
            pictureBox10.Location = new Point(563, 628);
            pictureBox10.Margin = new Padding(3, 4, 3, 4);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(51, 21);
            pictureBox10.TabIndex = 17;
            pictureBox10.TabStop = false;
            pictureBox10.Tag = "muro";
            pictureBox10.Visible = false;
            // 
            // pictureBox11
            // 
            pictureBox11.BackColor = Color.Red;
            pictureBox11.Location = new Point(839, 628);
            pictureBox11.Margin = new Padding(3, 4, 3, 4);
            pictureBox11.Name = "pictureBox11";
            pictureBox11.Size = new Size(39, 21);
            pictureBox11.TabIndex = 18;
            pictureBox11.TabStop = false;
            pictureBox11.Tag = "muro";
            pictureBox11.Visible = false;
            // 
            // pictureBox12
            // 
            pictureBox12.BackColor = Color.Red;
            pictureBox12.Location = new Point(243, 519);
            pictureBox12.Margin = new Padding(3, 4, 3, 4);
            pictureBox12.Name = "pictureBox12";
            pictureBox12.Size = new Size(111, 35);
            pictureBox12.TabIndex = 19;
            pictureBox12.TabStop = false;
            pictureBox12.Tag = "muro";
            pictureBox12.Visible = false;
            // 
            // pictureBox13
            // 
            pictureBox13.BackColor = Color.Red;
            pictureBox13.Location = new Point(117, 419);
            pictureBox13.Margin = new Padding(3, 4, 3, 4);
            pictureBox13.Name = "pictureBox13";
            pictureBox13.Size = new Size(35, 336);
            pictureBox13.TabIndex = 20;
            pictureBox13.TabStop = false;
            pictureBox13.Tag = "muro";
            pictureBox13.Visible = false;
            // 
            // pictureBox14
            // 
            pictureBox14.BackColor = Color.Red;
            pictureBox14.Location = new Point(1335, 561);
            pictureBox14.Margin = new Padding(3, 4, 3, 4);
            pictureBox14.Name = "pictureBox14";
            pictureBox14.Size = new Size(39, 247);
            pictureBox14.TabIndex = 21;
            pictureBox14.TabStop = false;
            pictureBox14.Tag = "muro";
            pictureBox14.Visible = false;
            // 
            // panelInfo
            // 
            panelInfo.BackColor = Color.Transparent;
            panelInfo.BackgroundImage = Properties.Resources.paneldecanato1;
            panelInfo.BackgroundImageLayout = ImageLayout.Stretch;
            panelInfo.Controls.Add(btnConsejo);
            panelInfo.Controls.Add(btnTrabajo);
            panelInfo.Controls.Add(btnOno);
            panelInfo.Location = new Point(289, 226);
            panelInfo.Margin = new Padding(3, 4, 3, 4);
            panelInfo.Name = "panelInfo";
            panelInfo.Size = new Size(600, 237);
            panelInfo.TabIndex = 22;
            // 
            // pbPuertaSalida
            // 
            pbPuertaSalida.BackColor = Color.Transparent;
            pbPuertaSalida.Location = new Point(367, 888);
            pbPuertaSalida.Margin = new Padding(3, 4, 3, 4);
            pbPuertaSalida.Name = "pbPuertaSalida";
            pbPuertaSalida.Size = new Size(690, 31);
            pbPuertaSalida.TabIndex = 23;
            pbPuertaSalida.TabStop = false;
            pbPuertaSalida.Tag = "puerta";
            // 
            // pictureBox16
            // 
            pictureBox16.BackColor = Color.Transparent;
            pictureBox16.Image = Properties.Resources.cesar1;
            pictureBox16.Location = new Point(265, 549);
            pictureBox16.Margin = new Padding(3, 4, 3, 4);
            pictureBox16.Name = "pictureBox16";
            pictureBox16.Size = new Size(111, 206);
            pictureBox16.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox16.TabIndex = 24;
            pictureBox16.TabStop = false;
            // 
            // FormDecanato
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondodecanato;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1445, 908);
            Controls.Add(pictureBox16);
            Controls.Add(pbPuertaSalida);
            Controls.Add(panelInfo);
            Controls.Add(pictureBox14);
            Controls.Add(pictureBox13);
            Controls.Add(pictureBox12);
            Controls.Add(pictureBox11);
            Controls.Add(pictureBox10);
            Controls.Add(pictureBox8);
            Controls.Add(pictureBox7);
            Controls.Add(pbPersonaje);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pbPizarra);
            Controls.Add(pbZonaActiva);
            Controls.Add(pbMensaje);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox5);
            Controls.Add(luna);
            Controls.Add(pictureBox9);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "FormDecanato";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DECANATO";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pbMensaje.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox15).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox12).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox13).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox14).EndInit();
            panelInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbPuertaSalida).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
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
        private PictureBox pictureBox7;
        private PictureBox pictureBox8;
        private PictureBox pictureBox9;
        private PictureBox pictureBox10;
        private PictureBox pictureBox11;
        private PictureBox pictureBox12;
        private PictureBox pictureBox13;
        private PictureBox pictureBox14;
        private Panel panelInfo;
        private PictureBox pictureBox15;
        private PictureBox pbPuertaSalida;
        private PictureBox pictureBox16;
    }
}