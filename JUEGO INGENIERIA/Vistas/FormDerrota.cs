using System;
using System.Windows.Forms;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormDerrota : Form
    {
        public FormDerrota(string mensajePrincipal)
        {
            InitializeComponent();

            // Solo inyectamos el mensaje
            if (lblMensaje != null) lblMensaje.Text = mensajePrincipal;

            // Magia del Joystick
            NavegacionConsola.Configurar(this, btnAceptar);
            this.KeyPreview = true;
            this.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space || e.KeyCode == Keys.Escape) CerrarPantalla();
            };
            btnAceptar.Click += (s, e) => CerrarPantalla();
        }

        private void CerrarPantalla()
        {
            NavegacionConsola.LimpiarFoco(this);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public static DialogResult Mostrar(string mensaje, string tituloOpcional = "IGNORADO")
        {
            using (FormDerrota form = new FormDerrota(mensaje))
            {
                return form.ShowDialog();
            }
        }
    }
}
