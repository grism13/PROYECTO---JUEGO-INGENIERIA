using System;
using System.Windows.Forms;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormVictoria : Form
    {
        public FormVictoria(string mensajePrincipal)
        {
            InitializeComponent();

            // Solo inyectamos el mensaje (el título ya viene en tu imagen)
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

        // El parámetro 'tituloOpcional' está de adorno para que no te dé error al reemplazar MessageBox viejos
        public static DialogResult Mostrar(string mensaje, string tituloOpcional = "IGNORADO", Action accionIntermedia = null)
        {
            using (FormVictoria form = new FormVictoria(mensaje)) // Solo le pasamos el mensaje al From
            {
                IrisTransitions.Transicion(form, accionIntermedia);
                return form.DialogResult;
            }
        }
    }
}
