namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormOno
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
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            lblTexto = new Label();
            cmbOpcionesNPC = new ComboBox();
            pictureBox7 = new PictureBox();
            timerAnimacion = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.MediumSeaGreen;
            pictureBox1.Location = new Point(203, 210);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(113, 142);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.MediumSeaGreen;
            pictureBox2.Location = new Point(368, 210);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(113, 142);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.MediumSeaGreen;
            pictureBox3.Location = new Point(538, 210);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(113, 142);
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.MediumSeaGreen;
            pictureBox4.Location = new Point(203, 401);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(113, 142);
            pictureBox4.TabIndex = 3;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.MediumSeaGreen;
            pictureBox5.Location = new Point(368, 401);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(113, 142);
            pictureBox5.TabIndex = 4;
            pictureBox5.TabStop = false;
            pictureBox5.Click += pictureBox5_Click;
            // 
            // pictureBox6
            // 
            pictureBox6.BackColor = Color.MediumSeaGreen;
            pictureBox6.Location = new Point(538, 401);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(113, 142);
            pictureBox6.TabIndex = 5;
            pictureBox6.TabStop = false;
            pictureBox6.Click += pictureBox6_Click;
            // 
            // lblTexto
            // 
            lblTexto.AutoSize = true;
            lblTexto.Location = new Point(190, 41);
            lblTexto.Name = "lblTexto";
            lblTexto.Size = new Size(484, 20);
            lblTexto.TabIndex = 6;
            lblTexto.Text = "Cuéntame, futuro ingeniero, ¿qué quieres saber? Selecciona tu pregunta";
            // 
            // cmbOpcionesNPC
            // 
            cmbOpcionesNPC.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOpcionesNPC.FormattingEnabled = true;
            cmbOpcionesNPC.Location = new Point(203, 99);
            cmbOpcionesNPC.Name = "cmbOpcionesNPC";
            cmbOpcionesNPC.Size = new Size(448, 28);
            cmbOpcionesNPC.TabIndex = 9;
            // 
            // pictureBox7
            // 
            pictureBox7.BackColor = Color.Transparent;
            pictureBox7.Image = Properties.Resources.flavioMago;
            pictureBox7.Location = new Point(51, 32);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(133, 156);
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox7.TabIndex = 10;
            pictureBox7.TabStop = false;
            // 
            // timerAnimacion
            // 
            timerAnimacion.Interval = 40;
            timerAnimacion.Tick += timerAnimacion_Tick;
            // 
            // FormOno
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 589);
            Controls.Add(pictureBox7);
            Controls.Add(cmbOpcionesNPC);
            Controls.Add(lblTexto);
            Controls.Add(pictureBox6);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Name = "FormOno";
            Text = "FormOno";
            Load += FormOno_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
        private Label lblTexto;
        private ComboBox cmbOpcionesNPC;
        private PictureBox pictureBox7;
        private System.Windows.Forms.Timer timerAnimacion;
    }
}