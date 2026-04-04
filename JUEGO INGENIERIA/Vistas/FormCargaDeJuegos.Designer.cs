namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormCargaDeJuegos
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
            RelojDeArena = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)RelojDeArena).BeginInit();
            SuspendLayout();
            // 
            // RelojDeArena
            // 
            RelojDeArena.Image = Properties.Resources.relojArena1;
            RelojDeArena.Location = new Point(1155, 427);
            RelojDeArena.Name = "RelojDeArena";
            RelojDeArena.Size = new Size(175, 185);
            RelojDeArena.SizeMode = PictureBoxSizeMode.StretchImage;
            RelojDeArena.TabIndex = 0;
            RelojDeArena.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(712, 539);
            label1.Name = "label1";
            label1.Size = new Size(404, 73);
            label1.TabIndex = 1;
            label1.Text = "CARGANDO.";
            // 
            // FormCargaDeJuegos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1359, 647);
            Controls.Add(label1);
            Controls.Add(RelojDeArena);
            Name = "FormCargaDeJuegos";
            Text = "FormCargaDeJuegos";
            ((System.ComponentModel.ISupportInitialize)RelojDeArena).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox RelojDeArena;
        private Label label1;
    }
}