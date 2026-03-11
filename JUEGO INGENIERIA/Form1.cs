using JUEGO_INGENIERIA.Vistas;
using System.Drawing.Text;

namespace JUEGO_INGENIERIA
{
    public partial class Form1 : Form
    {
        private FormMovimiento motorMovimiento;
        public static Jugador JugadorActual { get; set; }
        private Jugador jugadorActual;

        private WMPLib.WindowsMediaPlayer musicaFondo;

        // Esta variable guardará a qué nivel estamos intentando entrar (1 o 3)
        private int nivelSeleccionado = 0;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            musicaFondo = new WMPLib.WindowsMediaPlayer();

            // Ocultamos el panel universal por defecto
            pnlConfirmacionNivel1.Visible = false;

            pbPersonaje.Visible = false;
            EsconderMuros();
        }

        // --- SISTEMA DE DIBUJADO Y EFECTO DE CAMUFLAJE DE ÁRBOLES EN 3D ---
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            Region regionDeRecorte = new Region(e.ClipRectangle);

            foreach (Control control in this.Controls)
            {
                if (control is PictureBox x && x != pbPersonaje)
                {
                    if ((string)x.Tag != "muro" && x.Name.StartsWith("pictureBox") && x.BackColor == Color.Transparent)
                    {
                        if (pbPersonaje.Bounds.IntersectsWith(x.Bounds))
                        {
                            if (pbPersonaje.Bottom <= x.Bottom)
                            {
                                regionDeRecorte.Exclude(x.Bounds);
                            }
                        }
                    }
                }
            }

            e.Graphics.Clip = regionDeRecorte;

            if (motorMovimiento != null)
            {
                motorMovimiento.DibujarPersonaje(e.Graphics);
            }

            e.Graphics.ResetClip();
            regionDeRecorte.Dispose();

            foreach (Control control in this.Controls)
            {
                if (control is PictureBox x && x != pbPersonaje)
                {
                    if ((string)x.Tag != "muro" && x.Name.StartsWith("pictureBox") && x.Image != null)
                    {
                        e.Graphics.DrawImage(x.Image, x.Left, x.Top, x.Width, x.Height);
                    }
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Hide();

            FormIntro intro = new FormIntro();
            intro.ShowDialog();

            FormAdmision registro = new FormAdmision();
            registro.ShowDialog();

            ElegirPersonaje seleccion = new ElegirPersonaje();
            seleccion.ShowDialog();

            jugadorActual = Form1.JugadorActual;

            motorMovimiento = new FormMovimiento(this, pbPersonaje);
            motorMovimiento.ColisionConObjeto += MotorMovimiento_ColisionConObjeto;
            motorMovimiento.Start();

            this.Show();
            this.Focus();

            ReproducirMusicaMapa();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf");
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(rutaFuente);
            Font fuentePixel = new Font(pfc.Families[0], 9f);
            Font fuentePanel = new Font(pfc.Families[0], 8f);

            lblNombreJugador.Font = fuentePixel;
            lblNivel.Font = fuentePixel;
            lblDinero.Font = fuentePixel;

            lblPreguntaNivel1.Font = fuentePanel;
            btnSiNivel1.Font = fuentePanel;
            btnNoNivel1.Font = fuentePanel;

            if (JugadorActual != null)
            {
                lblNombreJugador.Text = "Jugador: " + JugadorActual.Nombre;
                lblNivel.Text = "Nivel: " + JugadorActual.Nivel.ToString();
                lblDinero.Text = "Dinero: $" + JugadorActual.Billetera.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // --- MANEJO DE CHOQUES CON PUERTAS ---
        private void MotorMovimiento_ColisionConObjeto(object sender, Control x)
        {
            if (x.Name == "pbPuertaNivel1")
            {
                motorMovimiento.Stop();
                motorMovimiento.EstaPausado = true;

                // Configuramos el panel universal para que hable del Nivel 1
                lblPreguntaNivel1.Text = "¿Estás listo para entrar a la clase del profesor Oswald (Nivel 1)?";
                nivelSeleccionado = 1;

                pnlConfirmacionNivel1.Visible = true;
                pnlConfirmacionNivel1.BringToFront();
            }
            else if (x.Name == "pbPuertaNivel3") // <--- COLISIÓN DE TU NUEVA PUERTA
            {
                motorMovimiento.Stop();
                motorMovimiento.EstaPausado = true;

                // Configuramos EL MISMO panel universal para que ahora hable del Nivel 3
                lblPreguntaNivel1.Text = "¿Estás listo para entrar al Nivel 3?";
                nivelSeleccionado = 3;

                pnlConfirmacionNivel1.Visible = true;
                pnlConfirmacionNivel1.BringToFront();
            }
            else if (x.Name == "pbPuertaNivel5") // Decanato
            {
                motorMovimiento.Stop();
                motorMovimiento.EstaPausado = true;
                musicaFondo.controls.stop();

                this.Hide();

                if (jugadorActual == null)
                {
                    jugadorActual = new Vistas.Jugador();
                    jugadorActual.Billetera = 0;
                    jugadorActual.Nivel = 1;
                    jugadorActual.Nombre = "Prueba";
                }

                FormDecanato decanato = new FormDecanato(jugadorActual);
                decanato.ShowDialog();

                this.Show();
                ReproducirMusicaMapa();

                this.Invalidate(pbPersonaje.Bounds);
                pbPersonaje.Top += 40;
                this.Invalidate(pbPersonaje.Bounds);

                motorMovimiento.Start();
                motorMovimiento.EstaPausado = false;
            }
        }

        // --- HACER INVISIBLES LOS MUROS ---
        private void EsconderMuros()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "muro" || x.Name == "pbPuertaNivel1" || x.Name == "pbPuertaNivel3")
                {
                    x.BackColor = Color.Transparent;
                }
            }
        }

        private void ReproducirMusicaMapa()
        {
            try
            {
                string rutaMusica = Path.Combine(Application.StartupPath, "Resources", "musica mapa", "musicaMapa.mp3");
                if (File.Exists(rutaMusica))
                {
                    musicaFondo.URL = rutaMusica;
                    musicaFondo.settings.setMode("loop", true);
                    musicaFondo.controls.play();
                }
            }
            catch (Exception)
            {
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (musicaFondo != null)
            {
                musicaFondo.controls.stop();
            }
        }

        private void pbPuertaNivel1_Click(object sender, EventArgs e)
        {
        }

        // --- BOTÓN SÍ UNIVERSAL ---
        private void btnSiNivel1_Click(object sender, EventArgs e)
        {
            // Ocultamos el panel
            pnlConfirmacionNivel1.Visible = false;
            musicaFondo.controls.stop();

            // Verificamos qué nivel estaba guardado en memoria cuando chocamos
            if (nivelSeleccionado == 1)
            {
                FormNivel1 nivel1 = new FormNivel1(jugadorActual);
                nivel1.ShowDialog();
            }
            else if (nivelSeleccionado == 3)
            {
                // Entramos al formulario Nivel2
                FormNivel3 nivel3 = new FormNivel3();
                nivel3.ShowDialog();
            }

            // Al salir del respectivo nivel, reactivamos música y alejamos al pj de la puerta
            ReproducirMusicaMapa();

            this.Invalidate(pnlConfirmacionNivel1.Bounds);
            this.Invalidate(pbPersonaje.Bounds);
            pbPersonaje.Top += 40;
            this.Invalidate(pbPersonaje.Bounds);

            motorMovimiento.Start();
            motorMovimiento.EstaPausado = false;
        }

        // --- BOTÓN NO UNIVERSAL ---
        private void btnNoNivel1_Click(object sender, EventArgs e)
        {
            // Sin importar a cuál íbamos a entrar, simplemente quitamos el panel
            pnlConfirmacionNivel1.Visible = false;

            this.Invalidate(pnlConfirmacionNivel1.Bounds);
            this.Invalidate(pbPersonaje.Bounds);
            pbPersonaje.Top += 40;
            this.Invalidate(pbPersonaje.Bounds);

            motorMovimiento.Start();
            motorMovimiento.EstaPausado = false;
        }

      
    }
}
