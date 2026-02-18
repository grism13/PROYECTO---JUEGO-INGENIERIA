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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            pictureBox9 = new PictureBox();
            pbPuertaNivel1 = new PictureBox();
            pictureBox10 = new PictureBox();
            pictureBox11 = new PictureBox();
            pictureBox12 = new PictureBox();
            pictureBox14 = new PictureBox();
            pictureBox15 = new PictureBox();
            pictureBox16 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox17 = new PictureBox();
            pictureBox8 = new PictureBox();
            pictureBox13 = new PictureBox();
            lblDinero = new Label();
            pictureBox18 = new PictureBox();
            pictureBox19 = new PictureBox();
            pictureBox20 = new PictureBox();
            pictureBox21 = new PictureBox();
            pictureBox22 = new PictureBox();
            pictureBox23 = new PictureBox();
            pictureBox26 = new PictureBox();
            pictureBox24 = new PictureBox();
            pictureBox25 = new PictureBox();
            pictureBox27 = new PictureBox();
            pictureBox28 = new PictureBox();
            pictureBox29 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPuertaNivel1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox17).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox18).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox19).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox20).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox23).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox26).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox24).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox25).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox27).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox28).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox29).BeginInit();
            SuspendLayout();
            // 
            // lblNombreJugador
            // 
            lblNombreJugador.AutoSize = true;
            lblNombreJugador.BackColor = Color.Transparent;
            lblNombreJugador.ForeColor = Color.Black;
            lblNombreJugador.Image = Properties.Resources.fondo_imagen_para_registro__1_;
            lblNombreJugador.Location = new Point(12, 45);
            lblNombreJugador.Name = "lblNombreJugador";
            lblNombreJugador.Size = new Size(38, 15);
            lblNombreJugador.TabIndex = 0;
            lblNombreJugador.Text = "label1";
            // 
            // lblNivel
            // 
            lblNivel.AutoSize = true;
            lblNivel.BackColor = Color.Transparent;
            lblNivel.ForeColor = Color.Black;
            lblNivel.Image = Properties.Resources.fondo_imagen_para_registro__1_;
            lblNivel.Location = new Point(12, 18);
            lblNivel.Name = "lblNivel";
            lblNivel.Size = new Size(38, 15);
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
            pbPersonaje.Location = new Point(560, 355);
            pbPersonaje.Margin = new Padding(3, 2, 3, 2);
            pbPersonaje.Name = "pbPersonaje";
            pbPersonaje.Size = new Size(88, 119);
            pbPersonaje.SizeMode = PictureBoxSizeMode.StretchImage;
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
            pictureBox1.Location = new Point(64, 767);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(811, 10);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Tag = "muro";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Crimson;
            pictureBox2.Location = new Point(407, 210);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(437, 25);
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            pictureBox2.Tag = "muro";
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Crimson;
            pictureBox3.Location = new Point(1025, 751);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(602, 18);
            pictureBox3.TabIndex = 5;
            pictureBox3.TabStop = false;
            pictureBox3.Tag = "muro";
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Crimson;
            pictureBox4.Location = new Point(1606, 32);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(21, 155);
            pictureBox4.TabIndex = 6;
            pictureBox4.TabStop = false;
            pictureBox4.Tag = "muro";
            // 
            // pictureBox6
            // 
            pictureBox6.BackColor = Color.Crimson;
            pictureBox6.Location = new Point(43, 87);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(208, 86);
            pictureBox6.TabIndex = 8;
            pictureBox6.TabStop = false;
            pictureBox6.Tag = "muro";
            // 
            // pictureBox7
            // 
            pictureBox7.BackColor = Color.Crimson;
            pictureBox7.Location = new Point(1606, 466);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(203, 34);
            pictureBox7.TabIndex = 9;
            pictureBox7.TabStop = false;
            pictureBox7.Tag = "muro";
            // 
            // pictureBox9
            // 
            pictureBox9.BackColor = Color.Crimson;
            pictureBox9.Location = new Point(96, 6);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(332, 27);
            pictureBox9.TabIndex = 11;
            pictureBox9.TabStop = false;
            pictureBox9.Tag = "muro";
            // 
            // pbPuertaNivel1
            // 
            pbPuertaNivel1.Location = new Point(125, 418);
            pbPuertaNivel1.Name = "pbPuertaNivel1";
            pbPuertaNivel1.Size = new Size(56, 16);
            pbPuertaNivel1.TabIndex = 12;
            pbPuertaNivel1.TabStop = false;
            // 
            // pictureBox10
            // 
            pictureBox10.BackColor = Color.Crimson;
            pictureBox10.Location = new Point(1025, 744);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(25, 166);
            pictureBox10.TabIndex = 14;
            pictureBox10.TabStop = false;
            pictureBox10.Tag = "muro";
            // 
            // pictureBox11
            // 
            pictureBox11.BackColor = Color.Crimson;
            pictureBox11.Location = new Point(850, 751);
            pictureBox11.Name = "pictureBox11";
            pictureBox11.Size = new Size(25, 173);
            pictureBox11.TabIndex = 15;
            pictureBox11.TabStop = false;
            pictureBox11.Tag = "muro";
            // 
            // pictureBox12
            // 
            pictureBox12.BackColor = Color.Crimson;
            pictureBox12.Location = new Point(407, -83);
            pictureBox12.Name = "pictureBox12";
            pictureBox12.Size = new Size(32, 318);
            pictureBox12.TabIndex = 16;
            pictureBox12.TabStop = false;
            pictureBox12.Tag = "muro";
            // 
            // pictureBox14
            // 
            pictureBox14.BackColor = Color.Crimson;
            pictureBox14.Location = new Point(43, 149);
            pictureBox14.Name = "pictureBox14";
            pictureBox14.Size = new Size(26, 614);
            pictureBox14.TabIndex = 18;
            pictureBox14.TabStop = false;
            pictureBox14.Tag = "muro";
            // 
            // pictureBox15
            // 
            pictureBox15.BackColor = Color.Crimson;
            pictureBox15.Location = new Point(1076, 299);
            pictureBox15.Name = "pictureBox15";
            pictureBox15.Size = new Size(101, 56);
            pictureBox15.TabIndex = 19;
            pictureBox15.TabStop = false;
            pictureBox15.Tag = "muro";
            // 
            // pictureBox16
            // 
            pictureBox16.BackColor = Color.Crimson;
            pictureBox16.Location = new Point(1617, 481);
            pictureBox16.Name = "pictureBox16";
            pictureBox16.Size = new Size(10, 282);
            pictureBox16.TabIndex = 20;
            pictureBox16.TabStop = false;
            pictureBox16.Tag = "muro";
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.Crimson;
            pictureBox5.Location = new Point(806, -57);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(38, 292);
            pictureBox5.TabIndex = 23;
            pictureBox5.TabStop = false;
            pictureBox5.Tag = "muro";
            // 
            // pictureBox17
            // 
            pictureBox17.BackColor = Color.Crimson;
            pictureBox17.Location = new Point(1606, 174);
            pictureBox17.Name = "pictureBox17";
            pictureBox17.Size = new Size(212, 27);
            pictureBox17.TabIndex = 24;
            pictureBox17.TabStop = false;
            pictureBox17.Tag = "muro";
            // 
            // pictureBox8
            // 
            pictureBox8.BackColor = Color.Crimson;
            pictureBox8.Location = new Point(791, 6);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(545, 30);
            pictureBox8.TabIndex = 25;
            pictureBox8.TabStop = false;
            pictureBox8.Tag = "muro";
            // 
            // pictureBox13
            // 
            pictureBox13.BackColor = Color.Crimson;
            pictureBox13.Location = new Point(226, 6);
            pictureBox13.Name = "pictureBox13";
            pictureBox13.Size = new Size(25, 166);
            pictureBox13.TabIndex = 26;
            pictureBox13.TabStop = false;
            pictureBox13.Tag = "muro";
            // 
            // lblDinero
            // 
            lblDinero.AutoSize = true;
            lblDinero.BackColor = Color.Transparent;
            lblDinero.ForeColor = Color.Black;
            lblDinero.Image = Properties.Resources.fondo_imagen_para_registro__1_;
            lblDinero.Location = new Point(12, 69);
            lblDinero.Name = "lblDinero";
            lblDinero.Size = new Size(38, 15);
            lblDinero.TabIndex = 27;
            lblDinero.Text = "label1";
            // 
            // pictureBox18
            // 
            pictureBox18.BackColor = Color.Transparent;
            pictureBox18.Image = Properties.Resources.fondo_imagen_para_registro__1_;
            pictureBox18.Location = new Point(-55, -143);
            pictureBox18.Name = "pictureBox18";
            pictureBox18.Size = new Size(306, 315);
            pictureBox18.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox18.TabIndex = 28;
            pictureBox18.TabStop = false;
            // 
            // pictureBox19
            // 
            pictureBox19.BackColor = Color.Transparent;
            pictureBox19.Image = Properties.Resources.nivel_1;
            pictureBox19.Location = new Point(52, 313);
            pictureBox19.Name = "pictureBox19";
            pictureBox19.Size = new Size(166, 78);
            pictureBox19.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox19.TabIndex = 29;
            pictureBox19.TabStop = false;
            pictureBox19.Tag = "";
            // 
            // pictureBox20
            // 
            pictureBox20.BackColor = Color.Crimson;
            pictureBox20.Location = new Point(703, 483);
            pictureBox20.Name = "pictureBox20";
            pictureBox20.Size = new Size(552, 25);
            pictureBox20.TabIndex = 30;
            pictureBox20.TabStop = false;
            pictureBox20.Tag = "muro";
            // 
            // pictureBox21
            // 
            pictureBox21.BackColor = Color.Crimson;
            pictureBox21.Location = new Point(743, 532);
            pictureBox21.Name = "pictureBox21";
            pictureBox21.Size = new Size(32, 112);
            pictureBox21.TabIndex = 31;
            pictureBox21.TabStop = false;
            pictureBox21.Tag = "muro";
            // 
            // pictureBox22
            // 
            pictureBox22.BackColor = Color.Crimson;
            pictureBox22.Location = new Point(492, 523);
            pictureBox22.Name = "pictureBox22";
            pictureBox22.Size = new Size(32, 112);
            pictureBox22.TabIndex = 32;
            pictureBox22.TabStop = false;
            pictureBox22.Tag = "muro";
            // 
            // pictureBox23
            // 
            pictureBox23.BackColor = Color.Crimson;
            pictureBox23.Location = new Point(43, 483);
            pictureBox23.Name = "pictureBox23";
            pictureBox23.Size = new Size(89, 25);
            pictureBox23.TabIndex = 33;
            pictureBox23.TabStop = false;
            pictureBox23.Tag = "muro";
            // 
            // pictureBox26
            // 
            pictureBox26.BackColor = Color.Crimson;
            pictureBox26.Location = new Point(1076, 79);
            pictureBox26.Name = "pictureBox26";
            pictureBox26.Size = new Size(129, 40);
            pictureBox26.TabIndex = 36;
            pictureBox26.TabStop = false;
            pictureBox26.Tag = "muro";
            // 
            // pictureBox24
            // 
            pictureBox24.BackColor = Color.Transparent;
            pictureBox24.Image = Properties.Resources.LOGO_DEL_JUEGO;
            pictureBox24.Location = new Point(585, 79);
            pictureBox24.Name = "pictureBox24";
            pictureBox24.Size = new Size(89, 78);
            pictureBox24.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox24.TabIndex = 37;
            pictureBox24.TabStop = false;
            // 
            // pictureBox25
            // 
            pictureBox25.BackColor = Color.Crimson;
            pictureBox25.Location = new Point(75, 339);
            pictureBox25.Name = "pictureBox25";
            pictureBox25.Size = new Size(38, 16);
            pictureBox25.TabIndex = 38;
            pictureBox25.TabStop = false;
            pictureBox25.Tag = "muro";
            // 
            // pictureBox27
            // 
            pictureBox27.BackColor = Color.Crimson;
            pictureBox27.Location = new Point(143, 339);
            pictureBox27.Name = "pictureBox27";
            pictureBox27.Size = new Size(38, 16);
            pictureBox27.TabIndex = 39;
            pictureBox27.TabStop = false;
            pictureBox27.Tag = "muro";
            // 
            // pictureBox28
            // 
            pictureBox28.BackColor = Color.Crimson;
            pictureBox28.Location = new Point(338, 492);
            pictureBox28.Name = "pictureBox28";
            pictureBox28.Size = new Size(161, 25);
            pictureBox28.TabIndex = 40;
            pictureBox28.TabStop = false;
            pictureBox28.Tag = "muro";
            // 
            // pictureBox29
            // 
            pictureBox29.BackColor = Color.Crimson;
            pictureBox29.Location = new Point(242, 546);
            pictureBox29.Name = "pictureBox29";
            pictureBox29.Size = new Size(89, 25);
            pictureBox29.TabIndex = 41;
            pictureBox29.TabStop = false;
            pictureBox29.Tag = "muro";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Green;
            BackgroundImage = Properties.Resources.fondomapa;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1264, 681);
            Controls.Add(pictureBox29);
            Controls.Add(pictureBox28);
            Controls.Add(pictureBox24);
            Controls.Add(pictureBox19);
            Controls.Add(pbPersonaje);
            Controls.Add(pictureBox26);
            Controls.Add(pictureBox23);
            Controls.Add(pictureBox22);
            Controls.Add(pictureBox21);
            Controls.Add(pictureBox20);
            Controls.Add(lblDinero);
            Controls.Add(pictureBox8);
            Controls.Add(pictureBox17);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox16);
            Controls.Add(pictureBox15);
            Controls.Add(pictureBox12);
            Controls.Add(pictureBox11);
            Controls.Add(pictureBox10);
            Controls.Add(pbPuertaNivel1);
            Controls.Add(pictureBox7);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(lblNivel);
            Controls.Add(lblNombreJugador);
            Controls.Add(pictureBox18);
            Controls.Add(pictureBox6);
            Controls.Add(pictureBox14);
            Controls.Add(pictureBox13);
            Controls.Add(pictureBox9);
            Controls.Add(pictureBox25);
            Controls.Add(pictureBox27);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MENU";
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
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPuertaNivel1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox12).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox14).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox15).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox17).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox13).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox18).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox19).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox20).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox21).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox22).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox23).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox26).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox24).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox25).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox27).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox28).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox29).EndInit();
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
        private PictureBox pictureBox9;
        private PictureBox pbPuertaNivel1;
        private PictureBox pictureBox10;
        private PictureBox pictureBox11;
        private PictureBox pictureBox12;
        private PictureBox pictureBox14;
        private PictureBox pictureBox15;
        private PictureBox pictureBox16;
        private PictureBox pictureBox5;
        private PictureBox pictureBox17;
        private PictureBox pictureBox8;
        private PictureBox pictureBox13;
        private Label lblDinero;
        private PictureBox pictureBox18;
        private PictureBox pictureBox19;
        private PictureBox pictureBox20;
        private PictureBox pictureBox21;
        private PictureBox pictureBox22;
        private PictureBox pictureBox23;
        private PictureBox pictureBox26;
        private PictureBox pictureBox24;
        private PictureBox pictureBox25;
        private PictureBox pictureBox27;
        private PictureBox pictureBox28;
        private PictureBox pictureBox29;
    }
}