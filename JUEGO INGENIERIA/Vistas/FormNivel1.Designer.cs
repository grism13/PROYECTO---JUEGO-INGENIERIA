namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormNivel1
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
            pbOswald = new PictureBox();
            label1 = new Label();
            pnlEscenario = new Panel();
            pbxJugador = new PictureBox();
            lblTiempo = new Label();
            lblVidas = new Label();
            lblPuntos = new Label();
            tmrGameLoop = new System.Windows.Forms.Timer(components);
            tmrGenerador = new System.Windows.Forms.Timer(components);
            tmrReloj = new System.Windows.Forms.Timer(components);
            pnlIntro = new Panel();
            lblOswaldText = new Label();
            pictureBox1 = new PictureBox();
            timerEscritura = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pbOswald).BeginInit();
            pnlEscenario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxJugador).BeginInit();
            pnlIntro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pbOswald
            // 
            pbOswald.BackColor = Color.Transparent;
            pbOswald.Image = Properties.Resources.oswald_version_narrativa;
            pbOswald.Location = new Point(-59, -60);
            pbOswald.Margin = new Padding(3, 2, 3, 2);
            pbOswald.Name = "pbOswald";
            pbOswald.Size = new Size(464, 405);
            pbOswald.SizeMode = PictureBoxSizeMode.Zoom;
            pbOswald.TabIndex = 0;
            pbOswald.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(444, 480);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // pnlEscenario
            // 
            pnlEscenario.BackColor = Color.Transparent;
            pnlEscenario.Controls.Add(pbxJugador);
            pnlEscenario.Location = new Point(424, 12);
            pnlEscenario.Name = "pnlEscenario";
            pnlEscenario.Size = new Size(377, 696);
            pnlEscenario.TabIndex = 2;
            // 
            // pbxJugador
            // 
            pbxJugador.BackColor = Color.Blue;
            pbxJugador.Location = new Point(149, 582);
            pbxJugador.Name = "pbxJugador";
            pbxJugador.Size = new Size(70, 70);
            pbxJugador.TabIndex = 0;
            pbxJugador.TabStop = false;
            // 
            // lblTiempo
            // 
            lblTiempo.AutoSize = true;
            lblTiempo.BackColor = Color.Transparent;
            lblTiempo.ForeColor = Color.Black;
            lblTiempo.Location = new Point(1119, 169);
            lblTiempo.Name = "lblTiempo";
            lblTiempo.Size = new Size(68, 15);
            lblTiempo.TabIndex = 3;
            lblTiempo.Text = "TIEMPO: 30";
            // 
            // lblVidas
            // 
            lblVidas.AutoSize = true;
            lblVidas.BackColor = Color.Transparent;
            lblVidas.ForeColor = Color.FromArgb(255, 128, 128);
            lblVidas.Location = new Point(1123, 258);
            lblVidas.Name = "lblVidas";
            lblVidas.Size = new Size(51, 15);
            lblVidas.TabIndex = 4;
            lblVidas.Text = "VIDAS: 3";
            // 
            // lblPuntos
            // 
            lblPuntos.AutoSize = true;
            lblPuntos.BackColor = Color.Transparent;
            lblPuntos.ForeColor = Color.FromArgb(192, 255, 192);
            lblPuntos.Location = new Point(1123, 353);
            lblPuntos.Name = "lblPuntos";
            lblPuntos.Size = new Size(64, 15);
            lblPuntos.TabIndex = 5;
            lblPuntos.Text = "PUNTOS: 0";
            // 
            // tmrGameLoop
            // 
            tmrGameLoop.Interval = 20;
            tmrGameLoop.Tick += tmrGameLoop_Tick;
            // 
            // tmrGenerador
            // 
            tmrGenerador.Interval = 1000;
            tmrGenerador.Tick += tmrGenerador_Tick;
            // 
            // tmrReloj
            // 
            tmrReloj.Interval = 1000;
            tmrReloj.Tick += tmrReloj_Tick;
            // 
            // pnlIntro
            // 
            pnlIntro.BackColor = Color.Transparent;
            pnlIntro.Controls.Add(lblOswaldText);
            pnlIntro.Controls.Add(pictureBox1);
            pnlIntro.Controls.Add(pbOswald);
            pnlIntro.Location = new Point(33, 413);
            pnlIntro.Name = "pnlIntro";
            pnlIntro.Size = new Size(953, 313);
            pnlIntro.TabIndex = 6;
            // 
            // lblOswaldText
            // 
            lblOswaldText.AutoSize = true;
            lblOswaldText.BackColor = Color.White;
            lblOswaldText.ForeColor = Color.Black;
            lblOswaldText.Location = new Point(317, 141);
            lblOswaldText.Name = "lblOswaldText";
            lblOswaldText.Size = new Size(38, 15);
            lblOswaldText.TabIndex = 7;
            lblOswaldText.Text = "label2";
            lblOswaldText.Click += lblOswaldText_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.narrativa;
            pictureBox1.Location = new Point(263, 85);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(686, 210);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // timerEscritura
            // 
            timerEscritura.Interval = 50;
            timerEscritura.Tick += timerEscritura_Tick;
            // 
            // FormNivel1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = Properties.Resources.fondonivel1;
            ClientSize = new Size(1280, 720);
            Controls.Add(pnlIntro);
            Controls.Add(lblPuntos);
            Controls.Add(lblVidas);
            Controls.Add(lblTiempo);
            Controls.Add(pnlEscenario);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormNivel1";
            Text = "Clase Profesor Oswald";
            Load += FormNivel1_Load;
            KeyDown += FormNivel1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pbOswald).EndInit();
            pnlEscenario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbxJugador).EndInit();
            pnlIntro.ResumeLayout(false);
            pnlIntro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbOswald;
        private Label label1;
        private Panel pnlEscenario;
        private PictureBox pbxJugador;
        private Label lblTiempo;
        private Label lblVidas;
        private Label lblPuntos;
        private System.Windows.Forms.Timer tmrGameLoop;
        private System.Windows.Forms.Timer tmrGenerador;
        private System.Windows.Forms.Timer tmrReloj;
        private Panel pnlIntro;
        private Label lblOswaldText;
        private System.Windows.Forms.Timer timerEscritura;
        private PictureBox pictureBox1;
    }
}