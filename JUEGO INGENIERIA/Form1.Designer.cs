namespace JUEGO_INGENIERIA
{
    partial class Form1
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
            lblNombreJugador = new Label();
            lblNivel = new Label();
            tmrMovimiento = new System.Windows.Forms.Timer(components);
            pbPersonaje = new PictureBox();
            nivelPersonaje = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox6 = new PictureBox();
            pictureBox7 = new PictureBox();
            pictureBox8 = new PictureBox();
            pictureBox9 = new PictureBox();
            pbPuertaNivel1 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox10 = new PictureBox();
            pictureBox11 = new PictureBox();
            pictureBox12 = new PictureBox();
            pictureBox13 = new PictureBox();
            pictureBox14 = new PictureBox();
            pictureBox15 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPuertaNivel1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox15).BeginInit();
            SuspendLayout();
            // 
            // lblNombreJugador
            // 
            lblNombreJugador.AutoSize = true;
            lblNombreJugador.Location = new Point(37, 88);
            lblNombreJugador.Name = "lblNombreJugador";
            lblNombreJugador.Size = new Size(50, 20);
            lblNombreJugador.TabIndex = 0;
            lblNombreJugador.Text = "label1";
            // 
            // lblNivel
            // 
            lblNivel.AutoSize = true;
            lblNivel.Location = new Point(37, 43);
            lblNivel.Name = "lblNivel";
            lblNivel.Size = new Size(50, 20);
            lblNivel.TabIndex = 1;
            lblNivel.Text = "label1";
            // 
            // tmrMovimiento
            // 
            tmrMovimiento.Enabled = true;
            tmrMovimiento.Interval = 20;
            tmrMovimiento.Tick += tmrMovimiento_Tick;
            // 
            // pbPersonaje
            // 
            pbPersonaje.BackColor = Color.Transparent;
            pbPersonaje.Image = Properties.Resources.gris_frente3;
            pbPersonaje.Location = new Point(1144, 255);
            pbPersonaje.Name = "pbPersonaje";
            pbPersonaje.Size = new Size(78, 200);
            pbPersonaje.SizeMode = PictureBoxSizeMode.CenterImage;
            pbPersonaje.TabIndex = 2;
            pbPersonaje.TabStop = false;
            // 
            // nivelPersonaje
            // 
            nivelPersonaje.AutoSize = true;
            nivelPersonaje.Location = new Point(1258, 50);
            nivelPersonaje.Name = "nivelPersonaje";
            nivelPersonaje.Size = new Size(50, 20);
            nivelPersonaje.TabIndex = 3;
            nivelPersonaje.Text = "label1";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Crimson;
            pictureBox1.Location = new Point(33, 779);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(629, 56);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Tag = "muro";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Crimson;
            pictureBox2.Location = new Point(531, 323);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(594, 71);
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            pictureBox2.Tag = "muro";
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Crimson;
            pictureBox3.Location = new Point(953, 779);
            pictureBox3.Margin = new Padding(3, 4, 3, 4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(688, 35);
            pictureBox3.TabIndex = 5;
            pictureBox3.TabStop = false;
            pictureBox3.Tag = "muro";
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Crimson;
            pictureBox4.Location = new Point(1356, 460);
            pictureBox4.Margin = new Padding(3, 4, 3, 4);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(195, 87);
            pictureBox4.TabIndex = 6;
            pictureBox4.TabStop = false;
            pictureBox4.Tag = "muro";
            // 
            // pictureBox6
            // 
            pictureBox6.BackColor = Color.Crimson;
            pictureBox6.Location = new Point(86, 151);
            pictureBox6.Margin = new Padding(3, 4, 3, 4);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(191, 76);
            pictureBox6.TabIndex = 8;
            pictureBox6.TabStop = false;
            pictureBox6.Tag = "muro";
            // 
            // pictureBox7
            // 
            pictureBox7.BackColor = Color.Crimson;
            pictureBox7.Location = new Point(1356, 113);
            pictureBox7.Margin = new Padding(3, 4, 3, 4);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(195, 76);
            pictureBox7.TabIndex = 9;
            pictureBox7.TabStop = false;
            pictureBox7.Tag = "muro";
            // 
            // pictureBox8
            // 
            pictureBox8.BackColor = Color.Crimson;
            pictureBox8.Location = new Point(1167, -3);
            pictureBox8.Margin = new Padding(3, 4, 3, 4);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(138, 47);
            pictureBox8.TabIndex = 10;
            pictureBox8.TabStop = false;
            pictureBox8.Tag = "muro";
            // 
            // pictureBox9
            // 
            pictureBox9.BackColor = Color.Crimson;
            pictureBox9.Location = new Point(263, -19);
            pictureBox9.Margin = new Padding(3, 4, 3, 4);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(277, 63);
            pictureBox9.TabIndex = 11;
            pictureBox9.TabStop = false;
            pictureBox9.Tag = "muro";
            // 
            // pbPuertaNivel1
            // 
            pbPuertaNivel1.Location = new Point(159, 597);
            pbPuertaNivel1.Margin = new Padding(3, 4, 3, 4);
            pbPuertaNivel1.Name = "pbPuertaNivel1";
            pbPuertaNivel1.Size = new Size(47, 62);
            pbPuertaNivel1.TabIndex = 12;
            pbPuertaNivel1.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.Crimson;
            pictureBox5.Location = new Point(1063, -3);
            pictureBox5.Margin = new Padding(3, 4, 3, 4);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(38, 337);
            pictureBox5.TabIndex = 13;
            pictureBox5.TabStop = false;
            pictureBox5.Tag = "muro";
            // 
            // pictureBox10
            // 
            pictureBox10.BackColor = Color.Crimson;
            pictureBox10.Location = new Point(953, 779);
            pictureBox10.Margin = new Padding(3, 4, 3, 4);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(29, 231);
            pictureBox10.TabIndex = 14;
            pictureBox10.TabStop = false;
            pictureBox10.Tag = "muro";
            // 
            // pictureBox11
            // 
            pictureBox11.BackColor = Color.Crimson;
            pictureBox11.Location = new Point(646, 779);
            pictureBox11.Margin = new Padding(3, 4, 3, 4);
            pictureBox11.Name = "pictureBox11";
            pictureBox11.Size = new Size(29, 240);
            pictureBox11.TabIndex = 15;
            pictureBox11.TabStop = false;
            pictureBox11.Tag = "muro";
            // 
            // pictureBox12
            // 
            pictureBox12.BackColor = Color.Crimson;
            pictureBox12.Location = new Point(531, -1);
            pictureBox12.Margin = new Padding(3, 4, 3, 4);
            pictureBox12.Name = "pictureBox12";
            pictureBox12.Size = new Size(38, 337);
            pictureBox12.TabIndex = 16;
            pictureBox12.TabStop = false;
            pictureBox12.Tag = "muro";
            // 
            // pictureBox13
            // 
            pictureBox13.BackColor = Color.Crimson;
            pictureBox13.Location = new Point(244, -48);
            pictureBox13.Margin = new Padding(3, 4, 3, 4);
            pictureBox13.Name = "pictureBox13";
            pictureBox13.Size = new Size(33, 237);
            pictureBox13.TabIndex = 17;
            pictureBox13.TabStop = false;
            pictureBox13.Tag = "muro";
            // 
            // pictureBox14
            // 
            pictureBox14.BackColor = Color.Crimson;
            pictureBox14.Location = new Point(47, 189);
            pictureBox14.Margin = new Padding(3, 4, 3, 4);
            pictureBox14.Name = "pictureBox14";
            pictureBox14.Size = new Size(33, 593);
            pictureBox14.TabIndex = 18;
            pictureBox14.TabStop = false;
            pictureBox14.Tag = "muro";
            // 
            // pictureBox15
            // 
            pictureBox15.BackColor = Color.Crimson;
            pictureBox15.Location = new Point(86, 483);
            pictureBox15.Margin = new Padding(3, 4, 3, 4);
            pictureBox15.Name = "pictureBox15";
            pictureBox15.Size = new Size(191, 86);
            pictureBox15.TabIndex = 19;
            pictureBox15.TabStop = false;
            pictureBox15.Tag = "muro";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Green;
            BackgroundImage = Properties.Resources.fondomapa;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1631, 1005);
            Controls.Add(pictureBox15);
            Controls.Add(pictureBox14);
            Controls.Add(pictureBox13);
            Controls.Add(pictureBox12);
            Controls.Add(pictureBox11);
            Controls.Add(pictureBox10);
            Controls.Add(pictureBox5);
            Controls.Add(pbPuertaNivel1);
            Controls.Add(pictureBox9);
            Controls.Add(pictureBox8);
            Controls.Add(pictureBox7);
            Controls.Add(pictureBox6);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(lblNivel);
            Controls.Add(lblNombreJugador);
            Controls.Add(pbPersonaje);
            DoubleBuffered = true;
            KeyPreview = true;
            Name = "Form1";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            Activated += Form1_Activated;
            Load += Form1_Load;
            Shown += Form1_Shown;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPuertaNivel1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox12).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox13).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox14).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox15).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombreJugador;
        private Label lblNivel;
        private System.Windows.Forms.Timer tmrMovimiento;
        private PictureBox pbPersonaje;
        private Label nivelPersonaje;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox6;
        private PictureBox pictureBox7;
        private PictureBox pictureBox8;
        private PictureBox pictureBox9;
        private PictureBox pbPuertaNivel1;
        private PictureBox pictureBox5;
        private PictureBox pictureBox10;
        private PictureBox pictureBox11;
        private PictureBox pictureBox12;
        private PictureBox pictureBox13;
        private PictureBox pictureBox14;
        private PictureBox pictureBox15;
    }
}