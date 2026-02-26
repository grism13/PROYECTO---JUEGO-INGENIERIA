namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormMenuPrincipal
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
            btnJugar = new Button();
            btnSalir = new Button();
            SuspendLayout();
            // 
            // btnJugar
            // 
            btnJugar.Location = new Point(725, 423);
            btnJugar.Name = "btnJugar";
            btnJugar.Size = new Size(210, 52);
            btnJugar.TabIndex = 0;
            btnJugar.Text = "button1";
            btnJugar.UseVisualStyleBackColor = true;
            btnJugar.Click += btnJugar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(351, 423);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(210, 52);
            btnSalir.TabIndex = 1;
            btnSalir.Text = "button2";
            btnSalir.UseVisualStyleBackColor = true;
            // 
            // FormMenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1264, 681);
            Controls.Add(btnSalir);
            Controls.Add(btnJugar);
            Name = "FormMenuPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormMenuPrincipal";
            ResumeLayout(false);
        }

        #endregion

        private Button btnJugar;
        private Button btnSalir;
    }
}