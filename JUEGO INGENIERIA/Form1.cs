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
using System.Security.Cryptography;

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

        public static Jugador? JugadorActual;
        private FormMovimiento motorMovimiento;

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
            if (motorMovimiento != null)
            {
                motorMovimiento.DibujarPersonaje(e.Graphics);
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

        private void MotorMovimiento_ColisionConObjeto(object sender, Control x)
        {
            if (x.Name == "pbPuertaNivel1")
            {
                motorMovimiento.Stop();
                motorMovimiento.EstaPausado = true;

                lblPreguntaNivel1.Text = "¿Estás listo para entrar a la clase del profesor Oswald (Nivel 1)?";
                nivelSeleccionado = 1;

                pnlConfirmacionNivel1.Visible = true;
                pnlConfirmacionNivel1.BringToFront();
            }

            else if (x.Name == "pbPuertaNivel5")
            {
                motorMovimiento.Stop();
                motorMovimiento.EstaPausado = true;
                musicaFondo.controls.stop();

                this.Hide();
                FormDecanato decanato = new FormDecanato();
                decanato.ShowDialog();

                this.Show();
                ReproducirMusicaMapa();

                this.Invalidate(pbPersonaje.Bounds);
                pbPersonaje.Top += 40;
                this.Invalidate(pbPersonaje.Bounds);

                // Reanudamos el movimiento
                motorMovimiento.Start();
                motorMovimiento.EstaPausado = false;
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

            this.Invalidate(pictureBox19.Bounds);
            this.Invalidate(pnlConfirmacionNivel1.Bounds);
            this.Invalidate(pbPersonaje.Bounds);
            pbPersonaje.Top += 40;
            this.Invalidate(pbPersonaje.Bounds);

            motorMovimiento.Start();
            motorMovimiento.EstaPausado = false;
        }

        private void btnNoNivel1_Click(object sender, EventArgs e)
        {
            pnlConfirmacionNivel1.Visible = false;
            this.Invalidate(pictureBox19.Bounds);
            this.Invalidate(pnlConfirmacionNivel1.Bounds);
            this.Invalidate(pbPersonaje.Bounds);
            pbPersonaje.Top += 40;
            this.Invalidate(pbPersonaje.Bounds);
            motorMovimiento.Start();
            motorMovimiento.EstaPausado = false;
        }
    }
}
