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

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

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

            FormCargaDeJuegos carga = new FormCargaDeJuegos();
            carga.Show();

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 2500;
            t.Tick += (s, e) => {
                t.Stop();
                carga.Close();
            };
            t.Start();

            this.Close();
        }

        public static DialogResult Mostrar(string mensaje, string tituloOpcional = "IGNORADO", Action accionIntermedia = null)
        {
            using (FormDerrota form = new FormDerrota(mensaje))
            {
                IrisTransitions.Transicion(form, accionIntermedia);
                return form.DialogResult;
            }
        }
    }
}
