namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormDerrota
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
            btnAceptar = new Button();
            lblMensaje = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(751, 852);
            btnAceptar.Margin = new Padding(3, 4, 3, 4);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(86, 31);
            btnAceptar.TabIndex = 0;
            btnAceptar.Text = "button1";
            btnAceptar.UseVisualStyleBackColor = true;
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.BackColor = Color.Black;
            lblMensaje.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMensaje.ForeColor = SystemColors.ButtonHighlight;
            lblMensaje.ImageAlign = ContentAlignment.TopCenter;
            lblMensaje.Location = new Point(696, 40);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(104, 41);
            lblMensaje.TabIndex = 1;
            lblMensaje.Text = "label1";
            lblMensaje.TextAlign = ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ActiveCaptionText;
            pictureBox1.Location = new Point(-3, 818);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1478, 114);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = SystemColors.ActiveCaptionText;
            pictureBox2.Location = new Point(-17, -7);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(1492, 114);
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // FormDerrota
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1473, 926);
            Controls.Add(lblMensaje);
            Controls.Add(btnAceptar);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormDerrota";
            Text = "FormDerrota";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAceptar;
        private Label lblMensaje;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}