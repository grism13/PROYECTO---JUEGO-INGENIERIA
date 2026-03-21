namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormNivel4_Final
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
            pnlEscenario = new Panel();
            tmrGameLoop = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // pnlEscenario
            // 
            pnlEscenario.Dock = DockStyle.Fill;
            pnlEscenario.Location = new Point(0, 0);
            pnlEscenario.Name = "pnlEscenario";
            pnlEscenario.Size = new Size(1264, 681);
            pnlEscenario.TabIndex = 0;
            // 
            // FormNivel4_Final
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(pnlEscenario);
            Name = "FormNivel4_Final";
            Text = "FormNivel4_Final";
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEscenario;
        private System.Windows.Forms.Timer tmrGameLoop;
    }
}