using System;
using System.Windows.Forms;
using WMPLib;
using System.IO;
using System.Drawing;
using System.Drawing.Text;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormDerrota : Form
    {
        WindowsMediaPlayer bgmDerrota = new WindowsMediaPlayer();
        PrivateFontCollection pfc = new PrivateFontCollection();

        public FormDerrota(string mensajePrincipal)
        {
            InitializeComponent();

            // Cargar fuente Pokemon
            try
            {
                string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf");
                if (File.Exists(rutaFuente))
                {
                    pfc.AddFontFile(rutaFuente);
                    if (lblMensaje != null) lblMensaje.Font = new Font(pfc.Families[0], 18f, FontStyle.Bold);
                    if (btnAceptar != null) btnAceptar.Font = new Font(pfc.Families[0], 12f, FontStyle.Bold);
                }
            }
            catch { }

            // Cargar sonido global de derrota
            try
            {
                string rutaMp3 = Path.Combine(Application.StartupPath, "Resources", "sonidoDerrota.mp3");
                string rutaWav = Path.Combine(Application.StartupPath, "Resources", "sonidoDerrota.wav");

                if (File.Exists(rutaMp3))
                    bgmDerrota.URL = rutaMp3;
                else if (File.Exists(rutaWav))
                    bgmDerrota.URL = rutaWav;

                if (!string.IsNullOrEmpty(bgmDerrota.URL))
                    bgmDerrota.controls.play();
            }
            catch { }

            // Solo inyectamos el mensaje y lo centramos dinámicamente
            if (lblMensaje != null)
            {
                lblMensaje.Text = mensajePrincipal;
                lblMensaje.Left = (Screen.PrimaryScreen.Bounds.Width - lblMensaje.Width) / 2;
            }

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            try
            {
                if (pictureBox2 != null) pictureBox2.Dock = DockStyle.Top;
                if (pictureBox1 != null) pictureBox1.Dock = DockStyle.Bottom;

                PictureBox pbGif = new PictureBox();
                pbGif.Image = Properties.Resources.gifDerrota;
                pbGif.Dock = DockStyle.Fill;
                pbGif.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(pbGif);
                pbGif.SendToBack(); // Que el label y botón queden encima
                if (lblMensaje != null) lblMensaje.BringToFront();
                if (btnAceptar != null) btnAceptar.BringToFront();
                int frames = pbGif.Image.GetFrameCount(System.Drawing.Imaging.FrameDimension.Time);
                byte[] times = pbGif.Image.GetPropertyItem(0x5100).Value;
                int totalDuration = 0;
                for (int i = 0; i < frames; i++)
                {
                    int delay = BitConverter.ToInt32(times, 4 * i);
                    if (delay == 0) delay = 10; // prevencion
                    totalDuration += delay * 10;
                }

                pbGif.Enabled = false; // Empezamos en pausa

                System.Windows.Forms.Timer freezeTimer = new System.Windows.Forms.Timer();
                freezeTimer.Interval = totalDuration > 0 ? totalDuration : 3000;
                freezeTimer.Tick += (senderGif, argsGif) => {
                    freezeTimer.Stop();
                    pbGif.Enabled = false; // Congela el GIF
                };

                // Esperamos al gatillo del Iris
                IrisTransitions.OnIrisAbierto += () =>
                {
                    if (pbGif != null && !pbGif.IsDisposed)
                    {
                        pbGif.Enabled = true; // Inicia la animación visual
                        freezeTimer.Start(); // Empieza el contador para el fin
                    }
                };
            }
            catch { }

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
            if (bgmDerrota != null) bgmDerrota.controls.stop(); // Calla el gemido si siguen dándole enter rápido
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            pfc?.Dispose();
            base.OnFormClosed(e);
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
