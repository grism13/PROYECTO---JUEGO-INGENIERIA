namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormFinal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFinal));
            lblTexto = new Label();
            btnSiguiente = new Button();
            btnSkip = new Button();
            pbImagen = new PictureBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbImagen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblTexto
            // 
            lblTexto.BackColor = Color.Transparent;
            lblTexto.Font = new Font("Times New Roman", 10F);
            lblTexto.ForeColor = Color.Black;
            lblTexto.Location = new Point(924, 225);
            lblTexto.Name = "lblTexto";
            lblTexto.Size = new Size(500, 380);
            lblTexto.TabIndex = 1;
            lblTexto.Text = "label1";
            lblTexto.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Cursor = Cursors.Hand;
            btnSiguiente.FlatStyle = FlatStyle.Flat;
            btnSiguiente.Font = new Font("Impact", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSiguiente.ForeColor = Color.White;
            btnSiguiente.Image = Properties.Resources.boton_de_siguiente;
            btnSiguiente.Location = new Point(1022, 675);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(301, 81);
            btnSiguiente.TabIndex = 2;
            btnSiguiente.Text = "SIGUIENTE";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click_1;
            // 
            // btnSkip
            // 
            btnSkip.Cursor = Cursors.Hand;
            btnSkip.FlatStyle = FlatStyle.Flat;
            btnSkip.Font = new Font("Impact", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSkip.ForeColor = Color.White;
            btnSkip.Image = Properties.Resources.boton_de_siguiente;
            btnSkip.Location = new Point(106, 811);
            btnSkip.Name = "btnSkip";
            btnSkip.Size = new Size(287, 71);
            btnSkip.TabIndex = 4;
            btnSkip.Text = "SALTAR HISTORIA";
            btnSkip.UseVisualStyleBackColor = true;
            btnSkip.Click += btnSkip_Click;
            // 
            // pbImagen
            // 
            pbImagen.Image = Properties.Resources.fondoFinal1;
            pbImagen.Location = new Point(151, 168);
            pbImagen.Name = "pbImagen";
            pbImagen.Size = new Size(547, 499);
            pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            pbImagen.TabIndex = 0;
            pbImagen.TabStop = false;
            pbImagen.Click += pbImagen_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.fondo_tipo_televisor_para_intro;
            pictureBox1.Location = new Point(-147, 16);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1144, 1017);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // FormFinal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1445, 908);
            Controls.Add(pbImagen);
            Controls.Add(btnSiguiente);
            Controls.Add(btnSkip);
            Controls.Add(lblTexto);
            Controls.Add(pictureBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormFinal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HISTORIA";
            FormClosing += FormFinal_FormClosing;
            Load += FormFinal_Load;
            ((System.ComponentModel.ISupportInitialize)pbImagen).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label lblTexto;
        private Button btnSiguiente;
        private Button btnSkip;
        private PictureBox pbImagen;
        private PictureBox pictureBox1;
    }
}

