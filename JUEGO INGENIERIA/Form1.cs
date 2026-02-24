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


namespace JUEGO_INGENIERIA
{
    public partial class Form1 : Form
    {
       
        private Vistas.Jugador jugadorActual;

       
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
        }

        
        private void Form1_Activated(object sender, EventArgs e)
        {
            // 1. Buscamos la ruta exacta de tu fuente usando la misma lógica de tus imágenes
            string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf");

            // 2. Cargamos la fuente en la colección privada
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(rutaFuente);

            // 3. Creamos el estilo de la fuente (Aquí puedes cambiar el 12f por el tamaño que prefieras)
            Font fuentePixel = new Font(pfc.Families[0], 9f);

            // 4. Se la asignamos a los textos (Asegúrate de que los nombres de los lbl coincidan con los tuyos)
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

        // --- CARGA DE IMÁGENES ---
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

            // 3. ¡Mira lo limpio que queda ahora! Llenamos las listas sin usar ni un solo if:

            // Abajo (Frente)
            animAbajo.Add(CargarSprite("frente1"));
            animAbajo.Add(CargarSprite("frente2"));
            animAbajo.Add(CargarSprite("frente3"));

            // Arriba (Espalda)
            animArriba.Add(CargarSprite("espalda1"));
            animArriba.Add(CargarSprite("espalda2"));
            animArriba.Add(CargarSprite("espalda3"));

            // Derecha
            animDerecha.Add(CargarSprite("ladoderecho1"));
            animDerecha.Add(CargarSprite("ladoderecho2"));
            animDerecha.Add(CargarSprite("ladoderecho3"));

            // Izquierda
            animIzquierda.Add(CargarSprite("ladoizquiedo1"));
            animIzquierda.Add(CargarSprite("ladoizquiedo2"));
            animIzquierda.Add(CargarSprite("ladoizquiedo3"));

            // --- DIAGONALES ---
            // Arriba + Derecha
            animArribaDerecha.Add(CargarSprite("inclinadaderechaespalda1"));
            animArribaDerecha.Add(CargarSprite("inclinadaderechaespalda2"));
            animArribaDerecha.Add(CargarSprite("inclinadaderechaespalda3"));

            // Arriba + Izquierda
            animArribaIzquierda.Add(CargarSprite("inclinadaizquiedaespalda1"));
            animArribaIzquierda.Add(CargarSprite("inclinadaizquiedaespalda2"));
            animArribaIzquierda.Add(CargarSprite("inclinadaizquiedaespalda3"));

            // Abajo + Derecha
            animAbajoDerecha.Add(CargarSprite("inclinadaderechafrente1"));
            animAbajoDerecha.Add(CargarSprite("inclinadaderechafrente2"));
            animAbajoDerecha.Add(CargarSprite("inclinadaderechafrente3"));

            // Abajo + Izquierda
            animAbajoIzquierda.Add(CargarSprite("inclinadaizquiedafrente1"));
            animAbajoIzquierda.Add(CargarSprite("inclinadaizquiedafrente2"));
            animAbajoIzquierda.Add(CargarSprite("inclinadaizquiedafrente3"));

            pbPersonaje.Image = CargarSprite("frente2"); // Imagen inicial

            animAbajoIzquierda.Add(CargarSprite("inclinadaizquiedafrente3"));

            pbPersonaje.Image = CargarSprite("frente2");
        }

        // --- TECLAS ---
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

        // --- EL CAMBIO MAGISTRAL AQUÍ (Lógica corregida) ---
        private void tmrMovimiento_Tick(object sender, EventArgs e)
        {
            //aqui se guarda la posicion para q pueda detectar los muros
            int xAnterior = pbPersonaje.Left;
            int yAnterior = pbPersonaje.Top;
            // Definimos dos velocidades locales
            int vNormal = 5;
            int vDiag = 3; // Más lento para diagonales (evita el efecto turbo)

            bool seMueve = false;

            // --- 1. PRIORIDAD: DIAGONALES ---
            // Revisamos primero si hay DOS teclas presionadas

            // ARRIBA + DERECHA
            if (goArriba && goDerecha)
            {
                if (pbPersonaje.Top > 0 && pbPersonaje.Left + pbPersonaje.Width < this.ClientSize.Width)
                {
                    pbPersonaje.Top -= vDiag;
                    pbPersonaje.Left += vDiag;
                    Animar(animArribaDerecha); // ¡Usa tu lista diagonal!
                    seMueve = true;
                }
            }
            // ARRIBA + IZQUIERDA
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
            // ABAJO + DERECHA
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
            // ABAJO + IZQUIERDA
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

            // --- 2. SECUNDARIO: CARDINALES (Una sola tecla) ---

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
                // Chocar contra los muros (Bloques rojos)
                if (x is PictureBox && (string)x.Tag == "muro")
                {
                    if (pbPersonaje.Bounds.IntersectsWith(x.Bounds))
                    {
                        // ¡Pisó el muro! Lo devolvemos a donde estaba al inicio
                        pbPersonaje.Left = xAnterior;
                        pbPersonaje.Top = yAnterior;
                    }
                }

                // aqui es cuando la muñeca toca la puerta de entrada al nivel 1
                if (x is PictureBox && x.Name == "pbPuertaNivel1")
                {
                    if (pbPersonaje.Bounds.IntersectsWith(x.Bounds))
                    {
                        tmrMovimiento.Stop(); // pausamos el mapa para que el personaje no siga caminando de fondo
                        goArriba = goAbajo = goIzquierda = goDerecha = false; // Reseteamos las teclas

                        // aquí te pregunta si quieres iniciar o no
                        DialogResult respuesta = MessageBox.Show(
                            "¿Estás listo para entrar a la clase del profesor Oswald?",
                            "Entrada al Nivel 1",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (respuesta == DialogResult.Yes)
                        {
                            // si el jugador dice que si se abre el nivel 1
                            FormNivel1 nivel1 = new FormNivel1(JugadorActual);
                            nivel1.ShowDialog(); // Usamos ShowDialog para pausar Form1 hasta que se cierre el nivel

                            // AL CERRAR EL NIVEL 1: Reactivamos el mapa y damos un pasito atrás para evitar el bucle
                            tmrMovimiento.Start();
                            pbPersonaje.Top += 40;
                        }
                        else
                        {
                            // si dice que no lo rebotamos un poquito hacia atrás para sacarlo de la zona verde
                            pbPersonaje.Left = xAnterior;
                            pbPersonaje.Top = yAnterior + 40; // Lo empujamos 40 píxeles hacia abajo

                            tmrMovimiento.Start(); // Volvemos a encender el motor del mapa
                        }
                    }
                }
            }

           


            // --- 3. RESETEO ---
            if (seMueve == false)
            {
                frameActual = 0;
                contadorLentitud = 10; // Truco: esto hace que al arrancar no tenga retraso la primera vez
            }
        }



        // --- FUNCIÓN DE ANIMAR ---
        private void Animar(List<Image> animacionNueva)
        {
            if (animacionNueva != ultimaAnimacion)
            {
                frameActual = 0;        // Reiniciamos la animación al primer frame
                contadorLentitud = 10;  // Forzamos que se dibuje inmediatamente
                ultimaAnimacion = animacionNueva; // Guardamos la nueva como actual
            }

            if (animacionNueva.Count > 0)
            {
                contadorLentitud++;

                if (contadorLentitud > 3) // Ajusta este número si va muy rápido
                {
                    // Importante: Asignar la imagen ANTES de incrementar para asegurar que vemos el frame 0
                    pbPersonaje.Image = animacionNueva[frameActual];

                    frameActual++;
                    if (frameActual >= animacionNueva.Count) frameActual = 0;

                    contadorLentitud = 0;
                }
                else if (contadorLentitud == 0)
                {
                    // Parche de seguridad: Si acabamos de resetear (contador 0), forzamos pintar la imagen
                    pbPersonaje.Image = animacionNueva[frameActual];
                }
            }
        }
        // Función para ocultar los bloques de muro al arrancar
        private void EsconderMuros()
        {
            foreach (Control x in this.Controls)
            {
                // Oculta todo lo que tenga el Tag "muro" o sea la puerta
                if (x is PictureBox && (string)x.Tag == "muro" || x.Name == "pbPuertaNivel1")
                {
                    x.BackColor = Color.Transparent;
                }
            }
        }


    }
}