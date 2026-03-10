namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormTrabajo
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
            pbPersonaje = new PictureBox();
            pbMesa1 = new PictureBox();
            pbMesa2 = new PictureBox();
            pbMesa3 = new PictureBox();
            pbMesa4 = new PictureBox();
            panel1 = new Panel();
            lblEntregados = new Label();
            lblDocumentos = new Label();
            lblTiempo = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            pbGenerador = new PictureBox();
            panel2 = new Panel();
            lblMesaDestino = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            pictureBox7 = new PictureBox();
            pictureBox8 = new PictureBox();
            pictureBox9 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMesa1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMesa2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMesa3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMesa4).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbGenerador).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            SuspendLayout();
            // 
            // pbPersonaje
            // 
            pbPersonaje.BackColor = Color.Transparent;
            pbPersonaje.Image = Properties.Resources.gris_frente3;
            pbPersonaje.Location = new Point(657, 281);
            pbPersonaje.Margin = new Padding(3, 2, 3, 2);
            pbPersonaje.Name = "pbPersonaje";
            pbPersonaje.Size = new Size(100, 125);
            pbPersonaje.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPersonaje.TabIndex = 4;
            pbPersonaje.TabStop = false;
            // 
            // pbMesa1
            // 
            pbMesa1.BackColor = Color.Transparent;
            pbMesa1.BackgroundImage = Properties.Resources.mesa2;
            pbMesa1.BackgroundImageLayout = ImageLayout.Stretch;
            pbMesa1.Location = new Point(185, 238);
            pbMesa1.Margin = new Padding(3, 2, 3, 2);
            pbMesa1.Name = "pbMesa1";
            pbMesa1.Size = new Size(158, 89);
            pbMesa1.SizeMode = PictureBoxSizeMode.StretchImage;
            pbMesa1.TabIndex = 6;
            pbMesa1.TabStop = false;
            // 
            // pbMesa2
            // 
            pbMesa2.BackColor = Color.Transparent;
            pbMesa2.BackgroundImage = Properties.Resources.mesa1;
            pbMesa2.BackgroundImageLayout = ImageLayout.Stretch;
            pbMesa2.Location = new Point(185, 440);
            pbMesa2.Margin = new Padding(3, 2, 3, 2);
            pbMesa2.Name = "pbMesa2";
            pbMesa2.Size = new Size(158, 89);
            pbMesa2.SizeMode = PictureBoxSizeMode.StretchImage;
            pbMesa2.TabIndex = 7;
            pbMesa2.TabStop = false;
            // 
            // pbMesa3
            // 
            pbMesa3.BackColor = Color.Transparent;
            pbMesa3.BackgroundImage = Properties.Resources.mesa;
            pbMesa3.BackgroundImageLayout = ImageLayout.Stretch;
            pbMesa3.Location = new Point(906, 238);
            pbMesa3.Margin = new Padding(3, 2, 3, 2);
            pbMesa3.Name = "pbMesa3";
            pbMesa3.Size = new Size(158, 91);
            pbMesa3.SizeMode = PictureBoxSizeMode.StretchImage;
            pbMesa3.TabIndex = 8;
            pbMesa3.TabStop = false;
            // 
            // pbMesa4
            // 
            pbMesa4.BackColor = Color.Transparent;
            pbMesa4.BackgroundImage = Properties.Resources.mesa1;
            pbMesa4.BackgroundImageLayout = ImageLayout.Stretch;
            pbMesa4.Location = new Point(906, 439);
            pbMesa4.Margin = new Padding(3, 2, 3, 2);
            pbMesa4.Name = "pbMesa4";
            pbMesa4.Size = new Size(158, 90);
            pbMesa4.SizeMode = PictureBoxSizeMode.StretchImage;
            pbMesa4.TabIndex = 9;
            pbMesa4.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblEntregados);
            panel1.Controls.Add(lblDocumentos);
            panel1.Controls.Add(lblTiempo);
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(434, 131);
            panel1.TabIndex = 10;
            // 
            // lblEntregados
            // 
            lblEntregados.Location = new Point(14, 85);
            lblEntregados.Name = "lblEntregados";
            lblEntregados.Size = new Size(381, 22);
            lblEntregados.TabIndex = 2;
            lblEntregados.Text = "label3";
            lblEntregados.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDocumentos
            // 
            lblDocumentos.Location = new Point(13, 46);
            lblDocumentos.Name = "lblDocumentos";
            lblDocumentos.Size = new Size(380, 23);
            lblDocumentos.TabIndex = 1;
            lblDocumentos.Text = "label2";
            lblDocumentos.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTiempo
            // 
            lblTiempo.Location = new Point(14, 11);
            lblTiempo.Name = "lblTiempo";
            lblTiempo.Size = new Size(368, 22);
            lblTiempo.TabIndex = 0;
            lblTiempo.Text = "label1";
            lblTiempo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // pbGenerador
            // 
            pbGenerador.BackColor = Color.Transparent;
            pbGenerador.BackgroundImage = Properties.Resources.libreria;
            pbGenerador.BackgroundImageLayout = ImageLayout.Stretch;
            pbGenerador.Location = new Point(486, 36);
            pbGenerador.Margin = new Padding(3, 2, 3, 2);
            pbGenerador.Name = "pbGenerador";
            pbGenerador.Size = new Size(298, 241);
            pbGenerador.SizeMode = PictureBoxSizeMode.StretchImage;
            pbGenerador.TabIndex = 11;
            pbGenerador.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(lblMesaDestino);
            panel2.Location = new Point(443, 552);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(400, 130);
            panel2.TabIndex = 12;
            // 
            // lblMesaDestino
            // 
            lblMesaDestino.Location = new Point(29, 20);
            lblMesaDestino.Name = "lblMesaDestino";
            lblMesaDestino.Size = new Size(343, 61);
            lblMesaDestino.TabIndex = 0;
            lblMesaDestino.Text = "label1";
            lblMesaDestino.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.White;
            label1.Location = new Point(247, 329);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 1;
            label1.Text = "Mesa1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.ForeColor = Color.White;
            label2.Location = new Point(247, 540);
            label2.Name = "label2";
            label2.Size = new Size(41, 15);
            label2.TabIndex = 13;
            label2.Text = "Mesa2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.ForeColor = Color.White;
            label3.Location = new Point(964, 331);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 14;
            label3.Text = "Mesa3";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.ForeColor = Color.White;
            label4.Location = new Point(964, 540);
            label4.Name = "label4";
            label4.Size = new Size(41, 15);
            label4.TabIndex = 15;
            label4.Text = "Mesa4";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Red;
            pictureBox1.Location = new Point(51, 36);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(25, 655);
            pictureBox1.TabIndex = 16;
            pictureBox1.TabStop = false;
            pictureBox1.Tag = "muro";
            pictureBox1.Visible = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Red;
            pictureBox2.Location = new Point(1227, 36);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(25, 655);
            pictureBox2.TabIndex = 17;
            pictureBox2.TabStop = false;
            pictureBox2.Tag = "muro";
            pictureBox2.Visible = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Red;
            pictureBox3.Location = new Point(27, 162);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(1254, 31);
            pictureBox3.TabIndex = 18;
            pictureBox3.TabStop = false;
            pictureBox3.Tag = "muro";
            pictureBox3.Visible = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Red;
            pictureBox4.Location = new Point(51, 629);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(1185, 30);
            pictureBox4.TabIndex = 19;
            pictureBox4.TabStop = false;
            pictureBox4.Tag = "muro";
            pictureBox4.Visible = false;
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.Transparent;
            pictureBox5.Image = Properties.Resources.alexis;
            pictureBox5.Location = new Point(747, 185);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(68, 100);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.TabIndex = 20;
            pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            pictureBox6.BackColor = Color.Transparent;
            pictureBox6.Image = Properties.Resources.gacielita;
            pictureBox6.Location = new Point(116, 346);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(82, 108);
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.TabIndex = 21;
            pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            pictureBox7.BackColor = Color.Red;
            pictureBox7.Location = new Point(758, 199);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(26, 31);
            pictureBox7.TabIndex = 22;
            pictureBox7.TabStop = false;
            pictureBox7.Tag = "muro";
            pictureBox7.Visible = false;
            // 
            // pictureBox8
            // 
            pictureBox8.BackColor = Color.Red;
            pictureBox8.Location = new Point(143, 368);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(26, 31);
            pictureBox8.TabIndex = 23;
            pictureBox8.TabStop = false;
            pictureBox8.Tag = "muro";
            pictureBox8.Visible = false;
            // 
            // pictureBox9
            // 
            pictureBox9.BackColor = Color.Red;
            pictureBox9.Location = new Point(526, 209);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(215, 31);
            pictureBox9.TabIndex = 24;
            pictureBox9.TabStop = false;
            pictureBox9.Tag = "muro";
            pictureBox9.Visible = false;
            // 
            // FormTrabajo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondoformtrabajo;
            ClientSize = new Size(1264, 681);
            Controls.Add(pbPersonaje);
            Controls.Add(pictureBox9);
            Controls.Add(pictureBox8);
            Controls.Add(pictureBox7);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(panel2);
            Controls.Add(pbGenerador);
            Controls.Add(panel1);
            Controls.Add(pbMesa4);
            Controls.Add(pbMesa3);
            Controls.Add(pbMesa2);
            Controls.Add(pbMesa1);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox6);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormTrabajo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormTrabajo";
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMesa1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMesa2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMesa3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMesa4).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbGenerador).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbPersonaje;
        private PictureBox pbMesa1;
        private PictureBox pbMesa2;
        private PictureBox pbMesa3;
        private PictureBox pbMesa4;
        private Panel panel1;
        private Label lblEntregados;
        private Label lblDocumentos;
        private Label lblTiempo;
        private System.Windows.Forms.Timer timer1;
        private PictureBox pbGenerador;
        private Panel panel2;
        private Label lblMesaDestino;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
        private PictureBox pictureBox7;
        private PictureBox pictureBox8;
        private PictureBox pictureBox9;
    }
}