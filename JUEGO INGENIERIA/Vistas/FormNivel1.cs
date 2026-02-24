using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JUEGO_INGENIERIA.Vistas;
using System.IO;
using System.Drawing.Text;
using System.Text.Json; // Para usar el archivo .json de los jugadores


using JUEGO_INGENIERIA.Modelos;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormNivel1 : Form
    {
        Jugador jugadorActual;

        // --- CONFIGURACIÓN ---
        int velocidadCaida = 25; // Un poco más rápido porque el dibujo es más fluido
        int tiempoLimite = 30;

        // --- IMÁGENES ---
        Image lobueno;
        Image lomalo;

        // --- VARIABLES INTERNAS ---
        int xIzquierda, xCentro, xDerecha;
        int carrilActual = 1;

        // --- CAMBIO IMPORTANTE: Usamos tu clase ObjetoJuego, NO PictureBox ---
        List<ObjetoJuego> objetosLogicos = new List<ObjetoJuego>();

        Random rnd = new Random();
        int puntos = 0;
        int vidas = 3;
        int tiempoRestante;

        // --- VARIABLES DIÁLOGO ---
        int indiceFrase = 0;
        int indiceLetra = 0;
        string[] discursoOswald = {
            "Oswald: Error 404: Talento no encontrado. Soy Oswald v1.0.",
            "Bienvenido a tu primer 'Hola Mundo' del dolor.",
            "He llenado la sala de excepciones y bugs. Usa tus flechas para esquivarlos.",
            "Si tocas uno... CRASH. El sistema se cae. ¿Listo para compilar o vas a dar error?"
        };

        public FormNivel1(Jugador jugadorRecibido)
        {
            InitializeComponent();
            this.jugadorActual = jugadorRecibido;
        }

        private void FormNivel1_Load(object sender, EventArgs e)
        {
            // 1. CARGA DE FUENTES (Tu código original)
            string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf");
            PrivateFontCollection pfc = new PrivateFontCollection();
            try
            {
                pfc.AddFontFile(rutaFuente);
                Font fuentePixel = new Font(pfc.Families[0], 11f);
                lblOswaldText.Font = fuentePixel;
                lblTiempo.Font = fuentePixel;
                lblPuntos.Font = fuentePixel;
            }
            catch { } // Si falla la fuente, no pasa nada

            // 2. VALIDACIÓN
            if (jugadorActual.Billetera < 100)
            {
                MessageBox.Show("No tienes los $100 necesarios.", "Sin Fondos");
                this.Close();
                return;
            }

            // 3. CONFIGURACIÓN VISUAL
            tiempoRestante = tiempoLimite;
            lblTiempo.Text = "TIEMPO: " + tiempoRestante;
            lblPuntos.Text = "PUNTOS: " + puntos;
            ActualizarVidasVisuales();

            // 4. CARGAR IMÁGENES
            lobueno = Properties.Resources.lobueno;
            lomalo = Properties.Resources.lomalo;

            // 5. MOTORES
            tmrGameLoop.Interval = 20;
            tmrGenerador.Interval = 700;
            tmrReloj.Interval = 1000;
            timerEscritura.Interval = 50;

            // 6. CARRILES
            int anchoPista = pnlEscenario.Width;
            int anchoJugador = pbxJugador.Width;
            xIzquierda = (anchoPista / 6) - (anchoJugador / 2);
            xCentro = (anchoPista / 2) - (anchoJugador / 2);
            xDerecha = (anchoPista * 5 / 6) - (anchoJugador / 2);
            carrilActual = 1;
            ActualizarPosicion();

            // --- 7. ACTIVAR EL DIBUJADO RÁPIDO (NUEVO) ---
            // Esto conecta el código de dibujo al panel
            pnlEscenario.Paint += new PaintEventHandler(pnlEscenario_Paint);

            // Truco "Double Buffer" para que no parpadee
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, pnlEscenario, new object[] { true });

            // 8. INTRO
            pnlIntro.Visible = true;
            pnlIntro.BringToFront();
            lblOswaldText.Text = "";
            timerEscritura.Start();
        }

        // --- ESTE ES EL EVENTO MÁGICO QUE DIBUJA TODO (NUEVO) ---
        private void pnlEscenario_Paint(object sender, PaintEventArgs e)
        {
            // Dibuja cada objeto de la lista directamente en la pantalla
            // Esto es 100 veces más rápido que usar PictureBox
            foreach (ObjetoJuego obj in objetosLogicos)
            {
                if (obj.Imagen != null)
                {
                    e.Graphics.DrawImage(obj.Imagen, obj.X, obj.Y, 50, 50);
                }
            }
        }

        // --- GENERADOR (SIN PICTUREBOX) ---
        private void tmrGenerador_Tick(object sender, EventArgs e)
        {
            // 1. Crear DATOS (Tu clase nueva)
            ObjetoJuego nuevo = new ObjetoJuego();

            // 2. Calcular posición
            int r = rnd.Next(0, 3);
            int posX = (r == 0) ? xIzquierda : (r == 1) ? xCentro : xDerecha;

            nuevo.X = posX;
            nuevo.Y = -70; // Nace arriba

            // 3. Asignar imagen y tag
            if (rnd.Next(0, 100) < 60)
            {
                nuevo.Imagen = lobueno;
                nuevo.Tag = "bueno";
            }
            else
            {
                nuevo.Imagen = lomalo;
                nuevo.Tag = "malo";
            }

            // 4. Guardar en la lista y PEDIR DIBUJO
            objetosLogicos.Add(nuevo);

            // ¡IMPORTANTE! Esto le dice al panel: "Borra lo viejo y pinta lo nuevo"
            pnlEscenario.Invalidate();
        }

        // --- GAME LOOP (MATEMÁTICAS PURAS) ---
        private void tmrGameLoop_Tick(object sender, EventArgs e)
        {
            // Recorremos al revés
            for (int i = objetosLogicos.Count - 1; i >= 0; i--)
            {
                ObjetoJuego obj = objetosLogicos[i];

                // 1. Mover (Solo cambiamos el número Y)
                obj.Y += velocidadCaida;

                // 2. Choques (Usamos obj.Area que creaste en tu clase)
                if (obj.Area.IntersectsWith(pbxJugador.Bounds))
                {
                    if (obj.Tag == "bueno")
                    {
                        if (puntos < 20)
                        {
                            puntos++;
                            lblPuntos.Text = "PUNTOS: " + puntos;
                        }
                    }
                    else if (obj.Tag == "malo")
                    {
                        vidas--;
                        ActualizarVidasVisuales();
                        if (vidas <= 0) { DetenerJuego(); PerderNivel("Sin vidas"); return; }
                    }

                    objetosLogicos.RemoveAt(i); // Lo borramos de la lista
                    continue;
                }

                // 3. Salir de pantalla
                if (obj.Y > pnlEscenario.Height)
                {
                    objetosLogicos.RemoveAt(i);
                }
            }

            // OBLIGATORIO: Actualizar el dibujo
            pnlEscenario.Invalidate();
        }

        // --- EL RESTO DE TU CÓDIGO (NO CAMBIA MUCHO) ---

        private void ActualizarPosicion()
        {
            int nuevaX = 0;
            if (carrilActual == 0) nuevaX = xIzquierda;
            else if (carrilActual == 1) nuevaX = xCentro;
            else if (carrilActual == 2) nuevaX = xDerecha;
            pbxJugador.Location = new Point(nuevaX, pbxJugador.Location.Y);

            // Redibujamos por si acaso el jugador se mueve
            pnlEscenario.Invalidate();
        }

        private void ActualizarVidasVisuales()
        {
            if (vidas == 3) { pbVida1.Visible = false; pbVida2.Visible = false; pbVida3.Visible = true; }
            else if (vidas == 2) { pbVida1.Visible = false; pbVida2.Visible = true; pbVida3.Visible = false; }
            else if (vidas == 1) { pbVida1.Visible = true; pbVida2.Visible = false; pbVida3.Visible = false; }
            else { pbVida1.Visible = false; pbVida2.Visible = false; pbVida3.Visible = false; }
        }

        private void tmrReloj_Tick(object sender, EventArgs e)
        {
            tiempoRestante--;
            lblTiempo.Text = "TIEMPO: " + tiempoRestante;
            if (tiempoRestante <= 0)
            {
                DetenerJuego();
                if (puntos >= 10) GanarNivel();
                else PerderNivel("Tiempo agotado");
            }
        }

        private void timerEscritura_Tick(object sender, EventArgs e)
        {
            string fraseCompleta = discursoOswald[indiceFrase];
            if (indiceLetra < fraseCompleta.Length)
            {
                lblOswaldText.Text += fraseCompleta[indiceLetra];
                indiceLetra++;
            }
            else
            {
                timerEscritura.Stop();
            }
        }

        private void lblOswaldText_Click(object sender, EventArgs e)
        {
            if (timerEscritura.Enabled)
            {
                timerEscritura.Stop();
                lblOswaldText.Text = discursoOswald[indiceFrase];
            }
            else
            {
                indiceFrase++;
                if (indiceFrase < discursoOswald.Length)
                {
                    lblOswaldText.Text = "";
                    indiceLetra = 0;
                    timerEscritura.Start();
                }
                else
                {
                    EmpezarJuegoReal();
                }
            }
        }

        private void EmpezarJuegoReal()
        {
            timerEscritura.Stop();
            pnlIntro.Visible = false;
            lblOswaldText.Visible = false;
            pictureBox1.Visible = false;
            pnlIntro.SendToBack();
            tmrGameLoop.Start();
            tmrGenerador.Start();
            tmrReloj.Start();
            this.Focus();
        }

        private void FormNivel1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && carrilActual > 0)
            {
                carrilActual--;
                ActualizarPosicion();
            }
            else if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && carrilActual < 2)
            {
                carrilActual++;
                ActualizarPosicion();
            }
        }

        private void DetenerJuego()
        {
            tmrGameLoop.Stop();
            tmrGenerador.Stop();
            tmrReloj.Stop();
        }

        private void GanarNivel()
        {
            if (jugadorActual.Nivel == 1)
            {
                jugadorActual.Nivel = 2;
                MessageBox.Show($"¡FELICIDADES!\nNota: {puntos}/20", "Nivel Completado");
            }
            else
            {
                MessageBox.Show($"¡Bien hecho!", "Repaso Completado");
            }

            ActualizarDatos(); // Guardamos el progreso en el .json
            this.Close();

        }

        private void PerderNivel(string motivo)
        {
            jugadorActual.Billetera -= 100;
            MessageBox.Show($"REPROBADO ({motivo}).\nMulta: $100", "Game Over");

            ActualizarDatos(); // Guardamos el progreso en el .json
            this.Close();
        }

        // Esto es para actualizar los datos en el .json

        private void ActualizarDatos()
        {
            string rutaArchivo = "jugadores.json";

            // Aqui se verifica que el archivo exista para que no haya errores
            if (!File.Exists(rutaArchivo)) return;

            // Se lee la linea completa de jugadores desde el .json
            string TextoJson = File.ReadAllText(rutaArchivo);
            List<Jugador> listaDeJugadores = JsonSerializer.Deserialize<List<Jugador>>(TextoJson) ?? new List<Jugador>();

            for (int i = 0; i < listaDeJugadores.Count; i++)
            {
                if (listaDeJugadores[i].IdJugador == jugadorActual.IdJugador)
                {
                    listaDeJugadores[i].Nivel = jugadorActual.Nivel;
                    listaDeJugadores[i].Billetera = jugadorActual.Billetera;
                    break;
                }
            }

            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string nuevoJson = JsonSerializer.Serialize(listaDeJugadores, opciones);
            File.WriteAllText(rutaArchivo, nuevoJson);
        }
    }
}