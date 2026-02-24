using JUEGO_INGENIERIA.Vistas;
using JUEGO_INGENIERIA.Properties; // NECESARIO para las imágenes
using System;
using System.Collections.Generic; // NECESARIO para las Listas
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;
using WMPLib;

namespace JUEGO_INGENIERIA
{
    public partial class Form1 : Form
    {
        private Vistas.Jugador jugadorActual;

        // --- VARIABLE DE MÚSICA (NUEVO) ---
        WindowsMediaPlayer musicaFondo = new WindowsMediaPlayer();

        public Form1(Vistas.Jugador jugadorRecibido)
        {
            InitializeComponent();
            jugadorActual = jugadorRecibido;
        }

        bool goArriba, goAbajo, goIzquierda, goDerecha;
        int velocidad = 5;

        public static Jugador? JugadorActual;

        List<Image> animAbajo = new List<Image>();
        List<Image> animArriba = new List<Image>();
        List<Image> animIzquierda = new List<Image>();
        List<Image> animDerecha = new List<Image>();

        List<Image> animArribaDerecha = new List<Image>();
        List<Image> animArribaIzquierda = new List<Image>();
        List<Image> animAbajoDerecha = new List<Image>();
        List<Image> animAbajoIzquierda = new List<Image>();

        int frameActual = 0;
        int contadorLentitud = 0;
        List<Image> ultimaAnimacion = null;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            EsconderMuros();
        }

        // --- TU CÓDIGO IMPORTANTE: INTRO Y REGISTRO ---
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Hide();

            FormIntro intro = new FormIntro();
            intro.ShowDialog();

            FormAdmision registro = new FormAdmision();
            registro.ShowDialog();

            ElegirPersonaje seleccion = new ElegirPersonaje();
            seleccion.ShowDialog();

            CargarSpritesPersonaje();

            this.Show();
            this.Focus();

            // --- INICIAR MÚSICA (NUEVO) ---
            ReproducirMusicaMapa();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf");
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(rutaFuente);
            Font fuentePixel = new Font(pfc.Families[0], 9f);

            lblNombreJugador.Font = fuentePixel;
            lblNivel.Font = fuentePixel;
            lblDinero.Font = fuentePixel;

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

        private void CargarSpritesPersonaje()
        {
            string p = DatosJuego.PersonajeElegido.ToLower();

            Image CargarSprite(string accion)
            {
                return (Image)Properties.Resources.ResourceManager.GetObject($"{p}_{accion}");
            }

            animAbajo.Add(CargarSprite("frente1"));
            animAbajo.Add(CargarSprite("frente2"));
            animAbajo.Add(CargarSprite("frente3"));

            animArriba.Add(CargarSprite("espalda1"));
            animArriba.Add(CargarSprite("espalda2"));
            animArriba.Add(CargarSprite("espalda3"));

            animDerecha.Add(CargarSprite("ladoderecho1"));
            animDerecha.Add(CargarSprite("ladoderecho2"));
            animDerecha.Add(CargarSprite("ladoderecho3"));

            animIzquierda.Add(CargarSprite("ladoizquiedo1"));
            animIzquierda.Add(CargarSprite("ladoizquiedo2"));
            animIzquierda.Add(CargarSprite("ladoizquiedo3"));

            animArribaDerecha.Add(CargarSprite("inclinadaderechaespalda1"));
            animArribaDerecha.Add(CargarSprite("inclinadaderechaespalda2"));
            animArribaDerecha.Add(CargarSprite("inclinadaderechaespalda3"));

            animArribaIzquierda.Add(CargarSprite("inclinadaizquiedaespalda1"));
            animArribaIzquierda.Add(CargarSprite("inclinadaizquiedaespalda2"));
            animArribaIzquierda.Add(CargarSprite("inclinadaizquiedaespalda3"));

            animAbajoDerecha.Add(CargarSprite("inclinadaderechafrente1"));
            animAbajoDerecha.Add(CargarSprite("inclinadaderechafrente2"));
            animAbajoDerecha.Add(CargarSprite("inclinadaderechafrente3"));

            animAbajoIzquierda.Add(CargarSprite("inclinadaizquiedafrente1"));
            animAbajoIzquierda.Add(CargarSprite("inclinadaizquiedafrente2"));
            animAbajoIzquierda.Add(CargarSprite("inclinadaizquiedafrente3"));

            pbPersonaje.Image = CargarSprite("frente2");
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) goArriba = true;
            if (e.KeyCode == Keys.Down) goAbajo = true;
            if (e.KeyCode == Keys.Left) goIzquierda = true;
            if (e.KeyCode == Keys.Right) goDerecha = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) goArriba = false;
            if (e.KeyCode == Keys.Down) goAbajo = false;
            if (e.KeyCode == Keys.Left) goIzquierda = false;
            if (e.KeyCode == Keys.Right) goDerecha = false;
        }

        private void tmrMovimiento_Tick(object sender, EventArgs e)
        {
            int xAnterior = pbPersonaje.Left;
            int yAnterior = pbPersonaje.Top;
            int vNormal = 5;
            int vDiag = 3;

            bool seMueve = false;

            if (goArriba && goDerecha)
            {
                if (pbPersonaje.Top > 0 && pbPersonaje.Left + pbPersonaje.Width < this.ClientSize.Width)
                {
                    pbPersonaje.Top -= vDiag;
                    pbPersonaje.Left += vDiag;
                    Animar(animArribaDerecha);
                    seMueve = true;
                }
            }
            else if (goArriba && goIzquierda)
            {
                if (pbPersonaje.Top > 0 && pbPersonaje.Left > 0)
                {
                    pbPersonaje.Top -= vDiag;
                    pbPersonaje.Left -= vDiag;
                    Animar(animArribaIzquierda);
                    seMueve = true;
                }
            }
            else if (goAbajo && goDerecha)
            {
                if (pbPersonaje.Top + pbPersonaje.Height < this.ClientSize.Height &&
                    pbPersonaje.Left + pbPersonaje.Width < this.ClientSize.Width)
                {
                    pbPersonaje.Top += vDiag;
                    pbPersonaje.Left += vDiag;
                    Animar(animAbajoDerecha);
                    seMueve = true;
                }
            }
            else if (goAbajo && goIzquierda)
            {
                if (pbPersonaje.Top + pbPersonaje.Height < this.ClientSize.Height && pbPersonaje.Left > 0)
                {
                    pbPersonaje.Top += vDiag;
                    pbPersonaje.Left -= vDiag;
                    Animar(animAbajoIzquierda);
                    seMueve = true;
                }
            }
            else if (goArriba)
            {
                if (pbPersonaje.Top > 0)
                {
                    pbPersonaje.Top -= vNormal;
                    Animar(animArriba);
                    seMueve = true;
                }
            }
            else if (goAbajo)
            {
                if (pbPersonaje.Top + pbPersonaje.Height < this.ClientSize.Height)
                {
                    pbPersonaje.Top += vNormal;
                    Animar(animAbajo);
                    seMueve = true;
                }
            }
            else if (goIzquierda)
            {
                if (pbPersonaje.Left > 0)
                {
                    pbPersonaje.Left -= vNormal;
                    Animar(animIzquierda);
                    seMueve = true;
                }
            }
            else if (goDerecha)
            {
                if (pbPersonaje.Left + pbPersonaje.Width < this.ClientSize.Width)
                {
                    pbPersonaje.Left += vNormal;
                    Animar(animDerecha);
                    seMueve = true;
                }
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "muro")
                {
                    if (pbPersonaje.Bounds.IntersectsWith(x.Bounds))
                    {
                        pbPersonaje.Left = xAnterior;
                        pbPersonaje.Top = yAnterior;
                    }
                }

                if (x is PictureBox && x.Name == "pbPuertaNivel1")
                {
                    if (pbPersonaje.Bounds.IntersectsWith(x.Bounds))
                    {
                        tmrMovimiento.Stop();
                        goArriba = goAbajo = goIzquierda = goDerecha = false;

                        DialogResult respuesta = MessageBox.Show(
                            "¿Estás listo para entrar a la clase del profesor Oswald?",
                            "Entrada al Nivel 1",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (respuesta == DialogResult.Yes)
                        {
                            // --- DETENER MÚSICA DEL MAPA (NUEVO) ---
                            musicaFondo.controls.stop();

                            FormNivel1 nivel1 = new FormNivel1(JugadorActual);
                            nivel1.ShowDialog();

                            // --- REANUDAR MÚSICA AL VOLVER (NUEVO) ---
                            ReproducirMusicaMapa();

                            tmrMovimiento.Start();
                            pbPersonaje.Top += 40;
                        }
                        else
                        {
                            pbPersonaje.Left = xAnterior;
                            pbPersonaje.Top = yAnterior + 40;
                            tmrMovimiento.Start();
                        }
                    }
                }
            }

            if (seMueve == false)
            {
                frameActual = 0;
                contadorLentitud = 10;
            }
        }

        private void Animar(List<Image> animacionNueva)
        {
            if (animacionNueva != ultimaAnimacion)
            {
                frameActual = 0;
                contadorLentitud = 10;
                ultimaAnimacion = animacionNueva;
            }

            if (animacionNueva.Count > 0)
            {
                contadorLentitud++;
                if (contadorLentitud > 3)
                {
                    pbPersonaje.Image = animacionNueva[frameActual];
                    frameActual++;
                    if (frameActual >= animacionNueva.Count) frameActual = 0;
                    contadorLentitud = 0;
                }
                else if (contadorLentitud == 0)
                {
                    pbPersonaje.Image = animacionNueva[frameActual];
                }
            }
        }

        private void EsconderMuros()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "muro" || x.Name == "pbPuertaNivel1")
                {
                    x.BackColor = Color.Transparent;
                }
            }
        }

        // --- MÉTODO DE MÚSICA (NUEVO) ---
        private void ReproducirMusicaMapa()
        {
            try
            {
                string ruta = Path.Combine(Application.StartupPath, "Resources", "musica mapa", "musicaMapa.mp3");
                musicaFondo.URL = ruta;
                musicaFondo.settings.setMode("loop", true);
                musicaFondo.controls.play();
            }
            catch { }
        }
    }
}