using JUEGO_INGENIERIA.Vistas;
using JUEGO_INGENIERIA.Properties;
using System;
using System.Collections.Generic;
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

        // --- VARIABLE DE MÚSICA ---
        WindowsMediaPlayer musicaFondo = new WindowsMediaPlayer();

        // --- VARIABLE PARA RECORDAR QUÉ NIVEL SE VA A ABRIR ---
        int nivelSeleccionado = 0;

        public Form1(Vistas.Jugador jugadorRecibido)
        {
            InitializeComponent();
            jugadorActual = jugadorRecibido;
            ConfigurarGraficos();
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
            ConfigurarGraficos();
        }

        // --- FUNCIÓN QUE ARREGLA EL PROBLEMA DEL FONDO CORTADO ---
        private void ConfigurarGraficos()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            // Ocultamos el PictureBox para que no corte el fondo
            pbPersonaje.Visible = false;
            EsconderMuros();
        }

        // --- DIBUJAMOS EL PERSONAJE DIRECTO EN EL MAPA ---
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // 1. DIBUJAMOS AL PERSONAJE PRIMERO
            if (ultimaAnimacion != null && ultimaAnimacion.Count > 0)
            {
                e.Graphics.DrawImage(ultimaAnimacion[frameActual], pbPersonaje.Left, pbPersonaje.Top, pbPersonaje.Width, pbPersonaje.Height);
            }
            else if (pbPersonaje.Image != null)
            {
                e.Graphics.DrawImage(pbPersonaje.Image, pbPersonaje.Left, pbPersonaje.Top, pbPersonaje.Width, pbPersonaje.Height);
            }
            // 2. DIBUJAMOS LA DECORACIÓN AUTOMÁTICAMENTE Y PENSANDO EN EL FUTURO
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox x)
                {
                    // REGLAS PARA SABER QUÉ ES UN ÁRBOL O TECHO:
                    // 1. NO tiene el tag "muro"
                    // 2. Su nombre EMPIEZA por "pictureBox" (así descartamos puertas que se llamen "pbPuerta1", "Cofre2", etc)
                    if ((string)x.Tag != "muro" && x.Name.StartsWith("pictureBox"))
                    {
                        if (x.Image != null && pbPersonaje.Bounds.IntersectsWith(x.Bounds))
                        {
                            e.Graphics.DrawImage(x.Image, x.Left, x.Top, x.Width, x.Height);
                        }
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

            CargarSpritesPersonaje();

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

            // FUENTE PARA EL PANEL PERSONALIZADO
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
            ultimaAnimacion = animAbajo; // Animación por defecto
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Solo nos movemos si el cartel no está mostrándose
            if (pnlConfirmacionNivel1 != null && !pnlConfirmacionNivel1.Visible)
            {
                if (e.KeyCode == Keys.Up) goArriba = true;
                if (e.KeyCode == Keys.Down) goAbajo = true;
                if (e.KeyCode == Keys.Left) goIzquierda = true;
                if (e.KeyCode == Keys.Right) goDerecha = true;
            }
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
                    pbPersonaje.Top -= vDiag; pbPersonaje.Left += vDiag;
                    Animar(animArribaDerecha); seMueve = true;
                }
            }
            else if (goArriba && goIzquierda)
            {
                if (pbPersonaje.Top > 0 && pbPersonaje.Left > 0)
                {
                    pbPersonaje.Top -= vDiag; pbPersonaje.Left -= vDiag;
                    Animar(animArribaIzquierda); seMueve = true;
                }
            }
            else if (goAbajo && goDerecha)
            {
                if (pbPersonaje.Top + pbPersonaje.Height < this.ClientSize.Height && pbPersonaje.Left + pbPersonaje.Width < this.ClientSize.Width)
                {
                    pbPersonaje.Top += vDiag; pbPersonaje.Left += vDiag;
                    Animar(animAbajoDerecha); seMueve = true;
                }
            }
            else if (goAbajo && goIzquierda)
            {
                if (pbPersonaje.Top + pbPersonaje.Height < this.ClientSize.Height && pbPersonaje.Left > 0)
                {
                    pbPersonaje.Top += vDiag; pbPersonaje.Left -= vDiag;
                    Animar(animAbajoIzquierda); seMueve = true;
                }
            }
            else if (goArriba)
            {
                if (pbPersonaje.Top > 0) { pbPersonaje.Top -= vNormal; Animar(animArriba); seMueve = true; }
            }
            else if (goAbajo)
            {
                if (pbPersonaje.Top + pbPersonaje.Height < this.ClientSize.Height) { pbPersonaje.Top += vNormal; Animar(animAbajo); seMueve = true; }
            }
            else if (goIzquierda)
            {
                if (pbPersonaje.Left > 0) { pbPersonaje.Left -= vNormal; Animar(animIzquierda); seMueve = true; }
            }
            else if (goDerecha)
            {
                if (pbPersonaje.Left + pbPersonaje.Width < this.ClientSize.Width) { pbPersonaje.Left += vNormal; Animar(animDerecha); seMueve = true; }
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

                        lblPreguntaNivel1.Text = "¿Estás listo para entrar a la clase del profesor Oswald (Nivel 1)?";
                        nivelSeleccionado = 1;

                        pnlConfirmacionNivel1.Visible = true;
                        pnlConfirmacionNivel1.BringToFront();
                    }
                }
            }

            if (seMueve == false)
            {
                frameActual = 0;
                contadorLentitud = 10;
            }

            // OBLIGAMOS AL MAPA A REPINTAR *SOLO* LA ZONA DONDE ESTÁ Y DONDE ESTUVO
            Rectangle areaAnterior = new Rectangle(xAnterior, yAnterior, pbPersonaje.Width, pbPersonaje.Height);
            Rectangle areaNueva = new Rectangle(pbPersonaje.Left, pbPersonaje.Top, pbPersonaje.Width, pbPersonaje.Height);

            // Limpiamos la pisada vieja y dibujamos la nueva, ahorrando un 99% de rendimiento
            this.Invalidate(areaAnterior);
            this.Invalidate(areaNueva);

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
                    frameActual++;
                    if (frameActual >= animacionNueva.Count) frameActual = 0;
                    contadorLentitud = 0;
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

        private void btnSiNivel1_Click(object sender, EventArgs e)
        {
            pnlConfirmacionNivel1.Visible = false;
            musicaFondo.controls.stop();

            if (nivelSeleccionado == 1)
            {
                FormNivel1 nivel1 = new FormNivel1(JugadorActual);
                nivel1.ShowDialog();
            }

            ReproducirMusicaMapa();

            pbPersonaje.Top += 40;
            tmrMovimiento.Start();
        }

        private void btnNoNivel1_Click(object sender, EventArgs e)
        {
            pnlConfirmacionNivel1.Visible = false;
            pbPersonaje.Top += 40;
            tmrMovimiento.Start();
        }
    }
}
