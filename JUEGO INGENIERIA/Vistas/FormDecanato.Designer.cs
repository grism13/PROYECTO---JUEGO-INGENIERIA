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
            pictureBox1 = new PictureBox();
            pbPersonaje = new PictureBox();
            panelInfo = new FlowLayoutPanel();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            pbZonaActiva = new PictureBox();
            tmrRevisarZonas = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).BeginInit();
            panelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbZonaActiva).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.flavioHablando2;
            pictureBox1.Location = new Point(473, 82);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(121, 143);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pbPersonaje
            // 
            pbPersonaje.BackColor = Color.Transparent;
            pbPersonaje.Image = Properties.Resources.gris_frente3;
            pbPersonaje.Location = new Point(274, 340);
            pbPersonaje.Name = "pbPersonaje";
            pbPersonaje.Size = new Size(88, 119);
            pbPersonaje.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPersonaje.TabIndex = 3;
            pbPersonaje.TabStop = false;
            // 
            // panelInfo
            // 
            panelInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelInfo.Controls.Add(label1);
            panelInfo.Controls.Add(button1);
            panelInfo.Controls.Add(button2);
            panelInfo.Controls.Add(button3);
            panelInfo.Location = new Point(627, 82);
            panelInfo.Name = "panelInfo";
            panelInfo.Size = new Size(320, 82);
            panelInfo.TabIndex = 4;
            panelInfo.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(262, 20);
            label1.TabIndex = 0;
            label1.Text = "Hola futuro ingeniero!! Que necesitas?";
            // 
            // button1
            // 
            button1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button1.Location = new Point(3, 23);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "CONSEJO";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button2.Location = new Point(103, 23);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 2;
            button2.Text = "LEER ONO";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button3.Location = new Point(203, 23);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 3;
            button3.Text = "TRABAJO";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // timer1
            // 
            timer1.Interval = 400;
            timer1.Tick += timer1_Tick;
            // 
            // pbZonaActiva
            // 
            pbZonaActiva.Location = new Point(446, 222);
            pbZonaActiva.Name = "pbZonaActiva";
            pbZonaActiva.Size = new Size(171, 62);
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
            // FormDecanato
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1058, 635);
            Controls.Add(pbZonaActiva);
            Controls.Add(panelInfo);
            Controls.Add(pbPersonaje);
            Controls.Add(pictureBox1);
            KeyPreview = true;
            Name = "FormDecanato";
            Text = "FormDecanato";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).EndInit();
            panelInfo.ResumeLayout(false);
            panelInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbZonaActiva).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pbPersonaje;
        private FlowLayoutPanel panelInfo;
        private Label label1;
        private Button button1;
        private Button button2;
        private Button button3;
        private System.Windows.Forms.Timer timer1;
        private PictureBox pbZonaActiva;
        private System.Windows.Forms.Timer tmrRevisarZonas;
    }
}