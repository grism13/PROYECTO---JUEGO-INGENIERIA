namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormIntro
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
            pbImagen = new PictureBox();
            lblTexto = new Label();
            btnSiguiente = new Button();
            ((System.ComponentModel.ISupportInitialize)pbImagen).BeginInit();
            SuspendLayout();
            // 
            // pbImagen
            // 
            pbImagen.Location = new Point(26, 53);
            pbImagen.Name = "pbImagen";
            pbImagen.Size = new Size(675, 536);
            pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            pbImagen.TabIndex = 0;
            pbImagen.TabStop = false;
            // 
            // lblTexto
            // 
            lblTexto.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTexto.Location = new Point(743, 108);
            lblTexto.Name = "lblTexto";
            lblTexto.Size = new Size(615, 245);
            lblTexto.TabIndex = 1;
            lblTexto.Text = "label1";
            lblTexto.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Location = new Point(976, 377);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(164, 52);
            btnSiguiente.TabIndex = 2;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click_1;
            // 
            // FormIntro
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1424, 635);
            Controls.Add(btnSiguiente);
            Controls.Add(lblTexto);
            Controls.Add(pbImagen);
            Name = "FormIntro";
            Text = "FormIntro";
            FormClosing += FormIntro_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pbImagen).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbImagen;
        private Label lblTexto;
        private Button btnSiguiente;
    }
}