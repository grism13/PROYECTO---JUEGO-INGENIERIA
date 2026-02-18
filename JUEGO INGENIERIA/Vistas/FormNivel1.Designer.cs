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
            pbOswald = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pbOswald).BeginInit();
            SuspendLayout();
            // 
            // pbOswald
            // 
            pbOswald.Location = new Point(469, 134);
            pbOswald.Name = "pbOswald";
            pbOswald.Size = new Size(396, 397);
            pbOswald.SizeMode = PictureBoxSizeMode.Zoom;
            pbOswald.TabIndex = 0;
            pbOswald.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(507, 640);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // FormNivel1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1552, 790);
            Controls.Add(label1);
            Controls.Add(pbOswald);
            Name = "FormNivel1";
            Text = "FormNivel1";
            ((System.ComponentModel.ISupportInitialize)pbOswald).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbOswald;
        private Label label1;
    }
}