using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormMenuPrincipal : Form
    {
        public FormMenuPrincipal()
        {
            InitializeComponent();
        }

        private void btnJugar_Click(object sender, EventArgs e)
        {
            FormAdmision admision = new FormAdmision();
            this.Hide(); // Ocultamos el menú principa
            admision.ShowDialog();
            this.Show(); // Al salir del juego, volvemos a mostrar el menú
        }
    }
}
