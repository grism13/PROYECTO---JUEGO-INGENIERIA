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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNivel1));
            pbOswald = new PictureBox();
            label1 = new Label();
            pnlEscenario = new Panel();
            pbxJugador = new PictureBox();
            lblTiempo = new Label();
            lblPuntos = new Label();
            tmrGameLoop = new System.Windows.Forms.Timer(components);
            tmrGenerador = new System.Windows.Forms.Timer(components);
            tmrReloj = new System.Windows.Forms.Timer(components);
            pnlIntro = new Panel();
            lblOswaldText = new Label();
            pictureBox1 = new PictureBox();
            timerEscritura = new System.Windows.Forms.Timer(components);
            pbVida1 = new PictureBox();
            pbVida3 = new PictureBox();
            pbVida2 = new PictureBox();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbOswald).BeginInit();
            pnlEscenario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxJugador).BeginInit();
            pnlIntro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbVida1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbVida3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbVida2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pbOswald
            // 
            pbOswald.BackColor = Color.Transparent;
            pbOswald.Image = Properties.Resources.oswald_version_narrativa;
            pbOswald.Location = new Point(-76, -54);
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
            pnlEscenario.Location = new Point(415, 12);
            pnlEscenario.Name = "pnlEscenario";
            pnlEscenario.Size = new Size(391, 696);
            pnlEscenario.TabIndex = 2;
            // 
            // pbxJugador
            // 
            pbxJugador.BackColor = Color.Transparent;
            pbxJugador.Image = Properties.Resources.gris_espalda2;
            pbxJugador.Location = new Point(157, 468);
            pbxJugador.Name = "pbxJugador";
            pbxJugador.Size = new Size(86, 177);
            pbxJugador.SizeMode = PictureBoxSizeMode.CenterImage;
            pbxJugador.TabIndex = 0;
            pbxJugador.TabStop = false;
            // 
            // lblTiempo
            // 
            lblTiempo.AutoSize = true;
            lblTiempo.BackColor = Color.Transparent;
            lblTiempo.ForeColor = Color.Black;
            lblTiempo.Location = new Point(1123, 164);
            lblTiempo.Name = "lblTiempo";
            lblTiempo.Size = new Size(67, 15);
            lblTiempo.TabIndex = 3;
            lblTiempo.Text = "TIEMPO: 30";
            // 
            // lblPuntos
            // 
            lblPuntos.AutoSize = true;
            lblPuntos.BackColor = Color.Transparent;
            lblPuntos.ForeColor = Color.FromArgb(192, 255, 192);
            lblPuntos.Location = new Point(1127, 346);
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
            pnlIntro.BackColor = Color.Transparent;
            pnlIntro.Controls.Add(lblOswaldText);
            pnlIntro.Controls.Add(pictureBox1);
            pnlIntro.Controls.Add(pbOswald);
            pnlIntro.Location = new Point(43, 400);
            pnlIntro.Name = "pnlIntro";
            pnlIntro.Size = new Size(953, 313);
            pnlIntro.TabIndex = 6;
            // 
            // lblOswaldText
            // 
            lblOswaldText.BackColor = Color.FromArgb(192, 192, 255);
            lblOswaldText.ForeColor = Color.Black;
            lblOswaldText.Image = Properties.Resources.narrativa;
            lblOswaldText.Location = new Point(309, 141);
            lblOswaldText.Name = "lblOswaldText";
            lblOswaldText.Size = new Size(590, 110);
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
            // pbVida1
            // 
            pbVida1.BackColor = Color.Transparent;
            pbVida1.Image = Properties.Resources._1vidas;
            pbVida1.Location = new Point(1074, 230);
            pbVida1.Name = "pbVida1";
            pbVida1.Size = new Size(165, 71);
            pbVida1.SizeMode = PictureBoxSizeMode.StretchImage;
            pbVida1.TabIndex = 1;
            pbVida1.TabStop = false;
            // 
            // pbVida3
            // 
            pbVida3.BackColor = Color.Transparent;
            pbVida3.Image = Properties.Resources._3vidas__1_;
            pbVida3.Location = new Point(1074, 230);
            pbVida3.Name = "pbVida3";
            pbVida3.Size = new Size(165, 71);
            pbVida3.SizeMode = PictureBoxSizeMode.StretchImage;
            pbVida3.TabIndex = 7;
            pbVida3.TabStop = false;
            // 
            // pbVida2
            // 
            pbVida2.BackColor = Color.Transparent;
            pbVida2.Image = Properties.Resources._2vidas;
            pbVida2.Location = new Point(1074, 230);
            pbVida2.Name = "pbVida2";
            pbVida2.Size = new Size(165, 71);
            pbVida2.SizeMode = PictureBoxSizeMode.StretchImage;
            pbVida2.TabIndex = 8;
            pbVida2.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = Properties.Resources.oswald_version_mini;
            pictureBox2.Location = new Point(64, 379);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(298, 334);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 9;
            pictureBox2.TabStop = false;
            // 
            // FormNivel1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = Properties.Resources.fondonivel1;
            ClientSize = new Size(1280, 720);
            Controls.Add(pbVida3);
            Controls.Add(pnlIntro);
            Controls.Add(lblPuntos);
            Controls.Add(pictureBox2);
            Controls.Add(lblTiempo);
            Controls.Add(pnlEscenario);
            Controls.Add(label1);
            Controls.Add(pbVida2);
            Controls.Add(pbVida1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormNivel1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Clase Profesor Oswald";
            Load += FormNivel1_Load;
            KeyDown += FormNivel1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pbOswald).EndInit();
            pnlEscenario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbxJugador).EndInit();
            pnlIntro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbVida1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbVida3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbVida2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbOswald;
        private Label label1;
        private Panel pnlEscenario;
        private PictureBox pbxJugador;
        private Label lblTiempo;
        private Label lblPuntos;
        private System.Windows.Forms.Timer tmrGameLoop;
        private System.Windows.Forms.Timer tmrGenerador;
        private System.Windows.Forms.Timer tmrReloj;
        private Panel pnlIntro;
        private Label lblOswaldText;
        private System.Windows.Forms.Timer timerEscritura;
        private PictureBox pictureBox1;
        private PictureBox pbVida1;
        private PictureBox pbVida3;
        private PictureBox pbVida2;
        private PictureBox pictureBox2;
    }
}