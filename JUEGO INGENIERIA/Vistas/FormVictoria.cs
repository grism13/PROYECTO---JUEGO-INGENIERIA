using System;
using System.Windows.Forms;
using WMPLib;
using System.IO;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormVictoria : Form
    {
        WindowsMediaPlayer bgmVictoria = new WindowsMediaPlayer();

        public FormVictoria(string mensajePrincipal)
        {
            InitializeComponent();

            // Cargar sonido global de victoria
            try
            {
                string rutaMp3 = Path.Combine(Application.StartupPath, "Resources", "sonidoVictoria.mp3");
                string rutaWav = Path.Combine(Application.StartupPath, "Resources", "sonidoVictoria.wav");

                if (File.Exists(rutaMp3))
                    bgmVictoria.URL = rutaMp3;
                else if (File.Exists(rutaWav))
                    bgmVictoria.URL = rutaWav;

                if (!string.IsNullOrEmpty(bgmVictoria.URL))
                    bgmVictoria.controls.play();
            }
            catch { }

            // Solo inyectamos el mensaje y lo centramos dinámicamente
            if (label1 != null)
            {
                label1.Text = mensajePrincipal;
                label1.Left = (Screen.PrimaryScreen.Bounds.Width - label1.Width) / 2;
            }

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            try
            {
                if (pictureBox2 != null) pictureBox2.Dock = DockStyle.Top;
                if (pictureBox1 != null) pictureBox1.Dock = DockStyle.Bottom;

                PictureBox pbGif = new PictureBox();
                // Si el usuario quiere otro gif, aquí lo cambia (por ejemplo Properties.Resources.gifVictoria si existe).
                pbGif.Image = Properties.Resources.gifVictoria;
                pbGif.Dock = DockStyle.Fill;
                pbGif.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(pbGif);
                pbGif.SendToBack(); // Que el label y botón queden encima

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
            if (bgmVictoria != null) bgmVictoria.controls.stop(); // Corta el sonido al salir
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
