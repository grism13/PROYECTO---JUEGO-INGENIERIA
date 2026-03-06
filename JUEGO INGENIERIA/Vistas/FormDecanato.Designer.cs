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
            timer1 = new System.Windows.Forms.Timer(components);
            pbZonaActiva = new PictureBox();
            tmrRevisarZonas = new System.Windows.Forms.Timer(components);
            pbPizarra = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pbPersonaje = new PictureBox();
            nubedetexto = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
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
            ((System.ComponentModel.ISupportInitialize)nubedetexto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.flavioHablando2;
            pictureBox1.Location = new Point(233, 169);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(120, 163);
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
            panelInfo.Location = new Point(194, 326);
            panelInfo.Margin = new Padding(3, 2, 3, 2);
            panelInfo.Name = "panelInfo";
            panelInfo.Size = new Size(259, 79);
            panelInfo.TabIndex = 4;
            panelInfo.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 8);
            label1.Margin = new Padding(18, 8, 3, 0);
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
            btnTrabajo.Image = Properties.Resources.botonazul;
            btnTrabajo.Location = new Point(18, 34);
            btnTrabajo.Margin = new Padding(18, 11, 3, 0);
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
            btnConsejo.Location = new Point(96, 34);
            btnConsejo.Margin = new Padding(18, 11, 3, 0);
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
            btnOno.Location = new Point(175, 34);
            btnOno.Margin = new Padding(18, 11, 3, 0);
            btnOno.Name = "btnOno";
            btnOno.Size = new Size(62, 15);
            btnOno.TabIndex = 13;
            btnOno.Text = "LEER ONO";
            btnOno.Click += btnOno_Click;
            // 
            // pbMensaje
            // 
            pbMensaje.BackColor = Color.White;
            pbMensaje.Controls.Add(x);
            pbMensaje.Controls.Add(lblMensaje);
            pbMensaje.Location = new Point(381, 200);
            pbMensaje.Margin = new Padding(3, 2, 3, 2);
            pbMensaje.Name = "pbMensaje";
            pbMensaje.Size = new Size(168, 62);
            pbMensaje.TabIndex = 10;
            pbMensaje.Visible = false;
            // 
            // x
            // 
            x.BackColor = Color.Firebrick;
            x.Location = new Point(147, 0);
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
            lblMensaje.Location = new Point(8, 10);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(38, 15);
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
            pbZonaActiva.Location = new Point(247, 419);
            pbZonaActiva.Margin = new Padding(3, 2, 3, 2);
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
            pbPizarra.Location = new Point(73, 119);
            pbPizarra.Margin = new Padding(3, 2, 3, 2);
            pbPizarra.Name = "pbPizarra";
            pbPizarra.Size = new Size(169, 106);
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
            pictureBox2.Location = new Point(121, 232);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(17, 18);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Gold;
            pictureBox3.Location = new Point(96, 233);
            pictureBox3.Margin = new Padding(3, 2, 3, 2);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(19, 17);
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Firebrick;
            pictureBox4.Location = new Point(73, 233);
            pictureBox4.Margin = new Padding(3, 2, 3, 2);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(17, 17);
            pictureBox4.TabIndex = 9;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // pbPersonaje
            // 
            pbPersonaje.BackColor = Color.Transparent;
            pbPersonaje.Image = Properties.Resources.gris_frente3;
            pbPersonaje.Location = new Point(268, 465);
            pbPersonaje.Margin = new Padding(3, 2, 3, 2);
            pbPersonaje.Name = "pbPersonaje";
            pbPersonaje.Size = new Size(104, 132);
            pbPersonaje.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPersonaje.TabIndex = 11;
            pbPersonaje.TabStop = false;
            // 
            // nubedetexto
            // 
            nubedetexto.BackColor = Color.Transparent;
            nubedetexto.Image = Properties.Resources.nubedetexto;
            nubedetexto.Location = new Point(351, 169);
            nubedetexto.Name = "nubedetexto";
            nubedetexto.Size = new Size(229, 134);
            nubedetexto.SizeMode = PictureBoxSizeMode.Zoom;
            nubedetexto.TabIndex = 12;
            nubedetexto.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.Transparent;
            pictureBox5.Image = Properties.Resources.pizarra;
            pictureBox5.Location = new Point(46, 98);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(201, 152);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 13;
            pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            pictureBox6.BackColor = Color.Transparent;
            pictureBox6.Image = Properties.Resources.lunaechada;
            pictureBox6.Location = new Point(127, 255);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(115, 88);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 14;
            pictureBox6.TabStop = false;
            // 
            // FormDecanato
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackgroundImage = Properties.Resources.fondo_decanato1;
            ClientSize = new Size(642, 595);
            Controls.Add(pbPersonaje);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pbPizarra);
            Controls.Add(pbMensaje);
            Controls.Add(pbZonaActiva);
            Controls.Add(panelInfo);
            Controls.Add(nubedetexto);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox6);
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
            ((System.ComponentModel.ISupportInitialize)pbZonaActiva).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPizarra).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).EndInit();
            ((System.ComponentModel.ISupportInitialize)nubedetexto).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
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
        private PictureBox nubedetexto;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
    }
}