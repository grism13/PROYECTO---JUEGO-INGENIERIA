using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace JUEGO_INGENIERIA.Vistas
{
    public static class IrisTransitions
    {
        private static Form overlay;
        private static float radioApertura;
        private static int maxRadio;
        private static float paso;
        public static Action OnIrisAbierto;

        private static void InitCore()
        {
            if (overlay == null)
            {
                overlay = new Form();
                overlay.FormBorderStyle = FormBorderStyle.None;
                overlay.WindowState = FormWindowState.Maximized;
                overlay.TopMost = true;
                overlay.BackColor = Color.Black;
                overlay.TransparencyKey = Color.Magenta;
                // DoubleBuffered es protegido, lo activamos por reflexión
                typeof(Form).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(overlay, true, null);
                overlay.ShowInTaskbar = false;
                // Ayuda a que los clics pasen al juego cuando está totalmente abierto (100% Magenta), 
                // pero ignorémoslo ya que de todos modos esconderemos o permitiremos el paso.

                int w = Screen.PrimaryScreen.Bounds.Width;
                int h = Screen.PrimaryScreen.Bounds.Height;
                maxRadio = (int)Math.Sqrt(w * w + h * h) / 2 + 100;
                paso = maxRadio / 25f; //  Aproximadamente unos 375ms para cerrar

                overlay.Paint += Overlay_Paint;
            }
        }

        private static void Overlay_Paint(object? sender, PaintEventArgs e)
        {
            if (radioApertura > 0)
            {
                // Es IMPORTANTE apagar el Anti-Aliasing. 
                // Si está prendido, Windows mezcla el borde negro con el magenta creando pixeles "morado oscuro" 
                // que la computadora ya no reconoce como transparentes, causando que se vea un halo en la pantalla.
                e.Graphics.SmoothingMode = SmoothingMode.None;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(overlay.Width / 2f - radioApertura, overlay.Height / 2f - radioApertura, radioApertura * 2, radioApertura * 2);
                    using (SolidBrush brush = new SolidBrush(Color.Magenta))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            }
        }

        public static void Transicion(Form nextForm, Action accionIntermedia = null, bool revertOnClose = true)
        {
            InitCore();
            
            if (!overlay.Visible || radioApertura > 0)
            {
                radioApertura = maxRadio;
                overlay.Show();
                overlay.BringToFront();
                Application.DoEvents();

                // Fase 1: Iris Out sobre la ventana actual
                while (radioApertura > 0)
                {
                    radioApertura -= paso;
                    if (radioApertura < 0) radioApertura = 0;
                    overlay.Invalidate();
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(15);
                }
            }

            // Cuando la próxima ventana cargue por debajo y esté lista, soltamos el círculo
            nextForm.Shown += (s, e) =>
            {
                System.Windows.Forms.Timer abrirTimer = new System.Windows.Forms.Timer();
                abrirTimer.Interval = 20;
                abrirTimer.Tick += (s2, e2) =>
                {
                    radioApertura += paso;
                    if (radioApertura >= maxRadio)
                    {
                        abrirTimer.Stop();
                        overlay.Hide();
                        OnIrisAbierto?.Invoke();
                        OnIrisAbierto = null;
                    }
                    else
                    {
                        overlay.Invalidate();
                    }
                };
                abrirTimer.Start();
            };

            // Fase 2: Mostrar el nuevo Formulario (Bloqueará el hilo principal pero correrán los Timers)
            nextForm.ShowDialog();

            accionIntermedia?.Invoke();

            // Fase 3: Iris In sobre este nivel base cuando volvimos. 
            // (La ventana destino llamó antes a CerrarIrisSync, por lo tanto radioApertura es 0 y el overlay está vivo y negro)
            if (revertOnClose && overlay.Visible && radioApertura <= 0)
            {
                while (radioApertura < maxRadio)
                {
                    radioApertura += paso;
                    if (radioApertura > maxRadio) radioApertura = maxRadio;
                    overlay.Invalidate();
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(15);
                }
                overlay.Hide();
            }
        }

        public static void CerrarIrisSync()
        {
            InitCore();
            // Evitamos saltos, lo ponemos en máximo para empezar a cerrar visualmente
            radioApertura = maxRadio;
            overlay.Show();
            overlay.BringToFront();
            Application.DoEvents();

            // Iris Out final
            while (radioApertura > 0)
            {
                radioApertura -= paso;
                if (radioApertura < 0) radioApertura = 0;
                overlay.Invalidate();
                Application.DoEvents();
                System.Threading.Thread.Sleep(15);
            }
        }

        public static void AbrirIrisSync()
        {
            InitCore();
            // Lo ponemos en negro visualmente
            radioApertura = 0;
            overlay.Show();
            overlay.BringToFront();
            Application.DoEvents();

            // Iris In final
            while (radioApertura < maxRadio)
            {
                radioApertura += paso;
                if (radioApertura > maxRadio) radioApertura = maxRadio;
                overlay.Invalidate();
                Application.DoEvents();
                System.Threading.Thread.Sleep(15);
            }
            overlay.Hide();
        }

        public static void OcultarSinc()
        {
            if (overlay != null)
            {
                overlay.Hide();
                radioApertura = maxRadio;
            }
        }
    }
}
