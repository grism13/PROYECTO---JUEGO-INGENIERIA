namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormNivel4
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
            tmrGravedad = new System.Windows.Forms.Timer(components);
            pbFondo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbFondo).BeginInit();
            SuspendLayout();
            // 
            // tmrGravedad
            // 
            tmrGravedad.Enabled = true;
            tmrGravedad.Interval = 20;
            // 
            // pbFondo
            // 
            pbFondo.Location = new Point(0, 0);
            pbFondo.Name = "pbFondo";
            pbFondo.Size = new Size(100, 50);
            pbFondo.SizeMode = PictureBoxSizeMode.AutoSize;
            pbFondo.TabIndex = 0;
            pbFondo.TabStop = false;
            // 
            // FormNivel4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 681);
            Controls.Add(pbFondo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormNivel4";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormNivel4";
            ((System.ComponentModel.ISupportInitialize)pbFondo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer tmrGravedad;
        private PictureBox pbFondo;
    }
}