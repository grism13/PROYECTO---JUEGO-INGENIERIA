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
            pictureBox1 = new PictureBox();
            lblOswaldText = new Label();
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
            pbOswald.Location = new Point(0, 194);
            pbOswald.Margin = new Padding(3, 2, 3, 2);
            pbOswald.Name = "pbOswald";
            pbOswald.Size = new Size(346, 298);
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
            pnlEscenario.BackColor = Color.DimGray;
            pnlEscenario.Controls.Add(pbxJugador);
            pnlEscenario.Location = new Point(361, 60);
            pnlEscenario.Name = "pnlEscenario";
            pnlEscenario.Size = new Size(800, 608);
            pnlEscenario.TabIndex = 2;
            // 
            // pbxJugador
            // 
            pbxJugador.BackColor = Color.Blue;
            pbxJugador.Location = new Point(220, 349);
            pbxJugador.Name = "pbxJugador";
            pbxJugador.Size = new Size(70, 70);
            pbxJugador.TabIndex = 0;
            pbxJugador.TabStop = false;
            // 
            // lblTiempo
            // 
            lblTiempo.AutoSize = true;
            lblTiempo.BackColor = Color.White;
            lblTiempo.Location = new Point(1187, 184);
            lblTiempo.Name = "lblTiempo";
            lblTiempo.Size = new Size(67, 15);
            lblTiempo.TabIndex = 3;
            lblTiempo.Text = "TIEMPO: 30";
            // 
            // lblVidas
            // 
            lblVidas.AutoSize = true;
            lblVidas.BackColor = Color.Red;
            lblVidas.Location = new Point(1187, 234);
            lblVidas.Name = "lblVidas";
            lblVidas.Size = new Size(51, 15);
            lblVidas.TabIndex = 4;
            lblVidas.Text = "VIDAS: 3";
            // 
            // lblPuntos
            // 
            lblPuntos.AutoSize = true;
            lblPuntos.BackColor = Color.GreenYellow;
            lblPuntos.Location = new Point(1191, 284);
            lblPuntos.Name = "lblPuntos";
            lblPuntos.Size = new Size(63, 15);
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
            pnlIntro.BackColor = Color.DarkGray;
            pnlIntro.Controls.Add(pictureBox1);
            pnlIntro.Location = new Point(146, 523);
            pnlIntro.Name = "pnlIntro";
            pnlIntro.Size = new Size(200, 100);
            pnlIntro.TabIndex = 6;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(71, 35);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 50);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // lblOswaldText
            // 
            lblOswaldText.AutoSize = true;
            lblOswaldText.BackColor = Color.DarkGray;
            lblOswaldText.Location = new Point(156, 653);
            lblOswaldText.Name = "lblOswaldText";
            lblOswaldText.Size = new Size(38, 15);
            lblOswaldText.TabIndex = 7;
            lblOswaldText.Text = "label2";
            lblOswaldText.Click += lblOswaldText_Click;
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
            BackColor = Color.Black;
            ClientSize = new Size(1280, 720);
            Controls.Add(lblOswaldText);
            Controls.Add(pnlIntro);
            Controls.Add(lblPuntos);
            Controls.Add(lblVidas);
            Controls.Add(lblTiempo);
            Controls.Add(pnlEscenario);
            Controls.Add(label1);
            Controls.Add(pbOswald);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormNivel1";
            Text = "Clase Profesor Oswald";
            WindowState = FormWindowState.Maximized;
            Load += FormNivel1_Load;
            KeyDown += FormNivel1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pbOswald).EndInit();
            pnlEscenario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbxJugador).EndInit();
            pnlIntro.ResumeLayout(false);
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
        private PictureBox pictureBox1;
        private Label lblOswaldText;
        private System.Windows.Forms.Timer timerEscritura;
    }
}