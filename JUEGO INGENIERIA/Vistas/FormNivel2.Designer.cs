namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormNivel2
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
            pnlEscenario.BackColor = Color.Black;
            pnlEscenario.Location = new Point(1, -3);
            pnlEscenario.Name = "pnlEscenario";
            pnlEscenario.Size = new Size(844, 543);
            pnlEscenario.TabIndex = 0;
            // 
            // tmrGameLoop
            // 
            tmrGameLoop.Interval = 20;
            tmrGameLoop.Tick += tmrGameLoop_Tick;
            // 
            // FormNivel2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(845, 541);
            Controls.Add(pnlEscenario);
            KeyPreview = true;
            Name = "FormNivel2";
            Text = "FormNivel2";
            Load += FormNivel2_Load;
            KeyDown += FormNivel2_KeyDown;
            KeyUp += FormNivel2_KeyUp;
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEscenario;
        private System.Windows.Forms.Timer tmrGameLoop;
    }
}