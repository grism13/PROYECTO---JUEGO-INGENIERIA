using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Text.Json;
using JUEGO_INGENIERIA.Modelos;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormNivel2Juego : Form
    {
        // 1. Rutas de prueba
        private string rutaArchivo = Application.StartupPath + @"\cancion.txt";
        private string rutaCancion = Application.StartupPath + @"\cancion-formNivel2.wav";

        // 2. Variables Mágicas del Gameplay
        private int tiempoAnticipacion = 1500; // La nota nace 1.5 seg antes para darle tiempo de caer
        private int margenError = 250; // +/- 250 milisegundos de perdón para acertar la tecla
        private int velocidadCaida = 7; // Qué tan rápido caen los cuadros (ajusta esto si caen muy lento)
        // --- ANIMACIÓN DE JOSÉ JESÚS ---
        private Image[] framesJoseJesus;
        private int frameActualJJ = 0;
        private int contadorAnimacionJJ = 0;
        private int velocidadAnimacionJJ = 10; // A menor número, más rápido toca la guitarra

        // --- FUENTE PERSONALIZADA ---
        PrivateFontCollection coleccionFuentes = new PrivateFontCollection();
        Font fuenteJuegoNormal;
        Font fuenteJuegoGrande;

        // --- IMAGENES PRECARGADAS ---
        private Image imgFlechaArriba;
        private Image imgFlechaAbajo;
        private Image imgFlechaDerecha;
        private Image imgFlechaIzquierda;

        // IMÁGENES DE LOS BOTONES PRESIONADOS (LOS DE ABAJO) ---
        private Image imgBtnPresionadoArriba;
        private Image imgBtnPresionadoAbajo;
        private Image imgBtnPresionadoDer;
        private Image imgBtnPresionadoIzq;

        // --- SISTEMA DE DIÁLOGO (NARRATIVA) ---
        private string[] dialogosJJ;      // Aquí guardaremos las partes de la historia
        private int indiceDialogo = 0;    // En qué parte vamos (0, 1, 2...)
        private int indiceCaracter = 0;   // Por cuál letra vamos
        private bool escribiendo = false; // Para saber si el texto se está moviendo
        private System.Windows.Forms.Timer timerEscribir = new System.Windows.Forms.Timer();
        // Estructura y Listas
        struct Nota
        {
            public int Direccion;
            public long Tiempo;
        }

        class NotaVisual
        {
            public float X;
            public float Y;
            public int Direccion;
            public Image Imagen;
            public long TiempoObjetivo; // Para sincronización de tiempo absoluto
            public Rectangle Bounds => new Rectangle((int)X, (int)Y, 80, 80);
        }

        private List<Nota> listaNotas = new List<Nota>();
        private List<NotaVisual> notasEnPantalla = new List<NotaVisual>();
        private int indiceNotaActual = 0;

        // Herramientas del sistema
        private Stopwatch cronometro = new Stopwatch();
        private SoundPlayer reproductor;
        private System.Windows.Forms.Timer timerCuenta = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerJuego = new System.Windows.Forms.Timer();

        private int conteo = 3;
        private int puntuacion = 0;
        private int faltas = 0;
        private Jugador jugadorActual;

        public FormNivel2Juego(Jugador jugadorRecibido)
        {
            InitializeComponent();
            this.jugadorActual = jugadorRecibido;

            if (jugadorActual != null && jugadorActual.Billetera < 100)
            {
                MessageBox.Show("No tienes los $100 necesarios.", "Sin Fondos");
                this.Close();
                return;
            }

            // Evitar parpadeos al dibujar manualmente sobre el panel
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, pnlPistaBaile, new object[] { true });

            pnlPistaBaile.Paint += PnlPistaBaile_Paint;

            // Precargamos las imágenes para no saturar la memoria accediendo a Properties.Resources a cada rato
            imgFlechaArriba = Properties.Resources.flechaarrb;
            imgFlechaAbajo = Properties.Resources.flechaAbj;
            imgFlechaDerecha = Properties.Resources.flechaDer;
            imgFlechaIzquierda = Properties.Resources.flechaIzq;

            

            // OJO: Cambia estos nombres por los reales de tus imágenes presionadas
            imgBtnPresionadoArriba = Properties.Resources.verde_presionado;
            imgBtnPresionadoAbajo = Properties.Resources.azul_presionado;
            imgBtnPresionadoDer = Properties.Resources.naranja_presionado;
            imgBtnPresionadoIzq = Properties.Resources.rosa_presionado;


            reproductor = new SoundPlayer(rutaCancion);
            ConfigurarTimers();
            CargarMapaDeNotas();
            lblPuntuacion.Text = "Puntos: 0";

            framesJoseJesus = new Image[]
            {
                Properties.Resources.josejesus1,
                Properties.Resources.josejesus2,
                Properties.Resources.josejesus3,
            };
            pbJoseJesus.Image = framesJoseJesus[0];

            try
            {

                string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf"); // <--- PON TU NOMBRE AQUÍ


                coleccionFuentes.AddFontFile(rutaFuente);


                fuenteJuegoNormal = new Font(coleccionFuentes.Families[0], 12f);
                fuenteJuegoGrande = new Font(coleccionFuentes.Families[0], 16f);


                lblPuntuacion.Font = fuenteJuegoNormal;
                lblCuentaRegresiva.Font = fuenteJuegoGrande;
                lblFaltas.Font = fuenteJuegoNormal;
                btnEmpezar.Font = fuenteJuegoNormal;
                btnEmpezar.Font = fuenteJuegoNormal;

            }
            catch (Exception ex)
            {
                // Si por alguna razón no encuentra el archivo, no se crashea el juego, solo muestra un aviso
                MessageBox.Show("Aviso: No se pudo cargar la fuente personalizada.");
            }

            // --- LÓGICA DE INICIO CON NARRATIVA (EFECTO RPG) ---
            pnlNarrativaIntro.Visible = true;
            pnlNarrativaIntro.BringToFront();
            pnlPistaBaile.Visible = false;
            lblPuntuacion.Visible = false;
            lblFaltas.Visible = false;
            pbJoseJesus.Visible = false;

            // pbJoseJesusIntro.Image = Properties.Resources.jj_intro_estatico; // Tu imagen de JJ parado
            lblTextoNarrativa.Font = fuenteJuegoNormal;
            lblTextoNarrativa.Text = ""; // Arranca vacío

            // 1. Dividimos la historia en las partes que quieras
            dialogosJJ = new string[]
            {
                "¡Saludos, ingenieros rítmicos! Soy José Jesús.",
                "Para aprobar Ingeniería Rítmica 2, deben demostrar\r\nque sus dedos tienen un 'flow' matemático.",
                "Alineen sus mentes, sientan el ritmo...\r\ny pulsen las teclas exactas.",
                "¿Están listos para el examen final?"
            };

            // 2. Configuramos la velocidad de la máquina de escribir
            timerEscribir.Interval = 30; // 30 milisegundos por letra (cámbialo si lo quieres más rápido o lento)
            timerEscribir.Tick += TimerEscribir_Tick;

            // 3. Arrancamos la primera parte
            EmpezarAEscribir();
        }

        // Método que resetea el label y empieza a escupir letras
        private void EmpezarAEscribir()
        {
            lblTextoNarrativa.Text = "";
            indiceCaracter = 0;
            escribiendo = true;
            timerEscribir.Start();
        }

        // El motor que se ejecuta cada 30 milisegundos
        private void TimerEscribir_Tick(object sender, EventArgs e)
        {
            // Si todavía faltan letras en la frase actual...
            if (indiceCaracter < dialogosJJ[indiceDialogo].Length)
            {
                lblTextoNarrativa.Text += dialogosJJ[indiceDialogo][indiceCaracter];
                indiceCaracter++;
            }
            else
            {
                // Si ya terminó la frase, apagamos el motor para esperar el clic
                timerEscribir.Stop();
                escribiendo = false;
            }
        }

        private void ConfigurarTimers()
        {
            timerCuenta.Interval = 1000;
            timerCuenta.Tick += TimerCuenta_Tick;

            timerJuego.Interval = 16; // 60 FPS aprox
            timerJuego.Tick += TimerJuego_Tick;
        }

        private void CargarMapaDeNotas()
        {
            if (File.Exists(rutaArchivo))
            {
                string[] lineas = File.ReadAllLines(rutaArchivo);
                foreach (string linea in lineas)
                {
                    string[] partes = linea.Split(',');
                    if (partes.Length == 2)
                    {
                        Nota n = new Nota();
                        n.Direccion = int.Parse(partes[0]);
                        n.Tiempo = long.Parse(partes[1]);
                        listaNotas.Add(n);
                    }
                }
            }
            else
            {
                MessageBox.Show("No se encontró el archivo cancion.txt en el escritorio.");
            }
        }

        private void TimerCuenta_Tick(object sender, EventArgs e)
        {
            conteo--;
            if (conteo > 0)
            {
                lblCuentaRegresiva.Text = conteo.ToString();
            }
            else if (conteo == 0)
            {
                lblCuentaRegresiva.Text = "¡YA!";
            }
            else
            {
                timerCuenta.Stop();
                lblCuentaRegresiva.Visible = false;

                reproductor.Play();
                cronometro.Start();
                timerJuego.Start();
            }
        }

        // EL MOTOR DEL JUEGO
        private void TimerJuego_Tick(object sender, EventArgs e)
        {
            long tiempoActual = cronometro.ElapsedMilliseconds;
            // --- ANIMACIÓN DE JOSÉ JESÚS ---
            contadorAnimacionJJ++;
            if (contadorAnimacionJJ >= velocidadAnimacionJJ)
            {
                frameActualJJ++;

                // Si llegó al último dibujo, vuelve a empezar (ciclo infinito)
                if (frameActualJJ >= framesJoseJesus.Length)
                {
                    frameActualJJ = 0;
                }

                // Actualizamos la imagen que se ve en la pantalla
                pbJoseJesus.Image = framesJoseJesus[frameActualJJ];
                contadorAnimacionJJ = 0; // Reiniciamos el relojito
            }

            // Debug visual para ti
            lblCuentaRegresiva.Visible = true;
            lblCuentaRegresiva.Font = fuenteJuegoNormal;
            lblCuentaRegresiva.Text = $"Reloj: {tiempoActual}ms | Faltan: {listaNotas.Count - indiceNotaActual}";

            // 1. GENERAR NOTAS (Con la anticipación aplicada)
            while (indiceNotaActual < listaNotas.Count &&
                   tiempoActual >= listaNotas[indiceNotaActual].Tiempo - tiempoAnticipacion)
            {
                GenerarNotaVisual(listaNotas[indiceNotaActual].Direccion, listaNotas[indiceNotaActual].Tiempo);
                indiceNotaActual++;
            }

            // 2. MOVER NOTAS (Sincronización Perfecta Basada en Tiempo, No en Frames)
            // Asumimos que pbMetaArriba.Top es la línea de meta oficial para las 4 flechas
            float posY_Meta = pbMetaArriba.Top;
            float posY_Inicio = -50f;
            float distanciaTotal = posY_Meta - posY_Inicio;

            for (int i = notasEnPantalla.Count - 1; i >= 0; i--)
            {
                NotaVisual nota = notasEnPantalla[i];

                // ¿Cuántos milisegundos faltan para que llegue el tiempo exacto de esta nota?
                long tiempoFaltante = nota.TiempoObjetivo - tiempoActual;

                // Fracción matemática: 1 (recién nace), 0 (está sobre la meta), Negativo (ya pasó la meta)
                float fraccionRecorrido = (float)tiempoFaltante / tiempoAnticipacion;

                // Ubicación calculada milimétricamente con el audio. Así, si el juego se traba, 
                // la flecha "se teletransporta" a la posición en la que DEBE estar. NUNCA pierde el ritmo de la canción.
                nota.Y = posY_Meta - (distanciaTotal * fraccionRecorrido);

                // Si la nota sale del PANEL por abajo, se borra
                // Si la nota sale del PANEL por abajo, se borra
                if (nota.Y > pnlPistaBaile.Height)
                {
                    notasEnPantalla.RemoveAt(i);

                    faltas++; // <--- SUMAMOS UNA FALTA
                    lblFaltas.Text = "Faltas: " + faltas;

                    // --- LÓGICA DE DERROTA (GAME OVER) ---
                    if (faltas > 5)
                    {
                        // 1. Detenemos todos los motores y la música
                        timerJuego.Stop();
                        cronometro.Stop();
                        reproductor.Stop();

                        if (jugadorActual != null)
                        {
                            jugadorActual.Billetera -= 100;
                            ActualizarDatos();
                        }

                        // 2. Mostramos el mensaje de derrota
                        FormDerrota.Mostrar("¡Te pelaste muchos pasos! Has reprobado el nivel.\nMulta: $100", "¡GAME OVER!", () => this.Close());

                        // 4. IMPORTANTE: Salimos del método para que no siga calculando nada más
                        return;
                    }
                }
            }
            // Refrescar el panel para que OnPaint dibuje los nuevos cuadros
            pnlPistaBaile.Invalidate();
            // --- 3. VERIFICAR FIN DEL JUEGO ---
            if (indiceNotaActual >= listaNotas.Count && notasEnPantalla.Count == 0)
            {
                timerJuego.Stop();
                cronometro.Stop();
                reproductor.Stop();

                lblCuentaRegresiva.Visible = true;
                lblCuentaRegresiva.Text = "¡CANCION TERMINADA!";

                if (jugadorActual != null && jugadorActual.Nivel < 2)
                {
                    jugadorActual.Nivel = 2;
                }
                ActualizarDatos();

                FormVictoria.Mostrar("¡Uff, coronaste la pista de baile!", "Nivel Completado", () => this.Close());

                // Aquí en el futuro pones la lógica para volver al menú o pasar de nivel
            }
        }


        // EL EVENTO PAINT QUE DIBUJA TODO DE UNA SOLA VEZ
        private void PnlPistaBaile_Paint(object sender, PaintEventArgs e)
        {
            // Opcional: mejora la calidad si las imágenes se ven pixeladas
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Dibujar cada nota almacenada en la lista
            foreach (var nota in notasEnPantalla)
            {
                e.Graphics.DrawImage(nota.Imagen, nota.X, nota.Y, 80, 80);
            }
        }

        // EL GENERADOR DE CUADROS - Ahora con imágenes
        private void GenerarNotaVisual(int direccion, long tiempoObjetivo)
        {
            NotaVisual nuevaNota = new NotaVisual();
            nuevaNota.Y = -50;
            nuevaNota.Direccion = direccion;
            nuevaNota.TiempoObjetivo = tiempoObjetivo; // Sincronización absoluta

            // Asignamos la imagen correcta y alineamos con la meta
            switch (direccion)
            {
                case 1: // ARRIBA - Azul
                    nuevaNota.Imagen = imgFlechaArriba; // Imágen precargada
                    nuevaNota.X = pbMetaArriba.Left;
                    break;
                case 2: // ABAJO - Rojo
                    nuevaNota.Imagen = imgFlechaAbajo; // Imágen precargada
                    nuevaNota.X = pbMetaAbajo.Left;
                    break;
                case 3: // DERECHA - Verde
                    nuevaNota.Imagen = imgFlechaDerecha; // Imágen precargada
                    nuevaNota.X = pbMetaDer.Left;
                    break;
                case 4: // IZQUIERDA - Rosa
                    nuevaNota.Imagen = imgFlechaIzquierda; // Imágen precargada
                    nuevaNota.X = pbMetaIzq.Left;
                    break;
            }

            notasEnPantalla.Add(nuevaNota);
        }

        // EL ESCUCHADOR DEL TECLADO
        // EL ESCUCHADOR DEL TECLADO ACTUALIZADO
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (timerJuego.Enabled)
            {
                int direccionPulsada = 0;
                PictureBox botonAAnimar = null;
                Image imagenHundida = null;

                // Identificamos qué flecha se tocó, qué botón animar y con qué imagen
                if (keyData == Keys.Up)
                {
                    direccionPulsada = 1;
                    botonAAnimar = pbMetaArriba;
                    imagenHundida = imgBtnPresionadoArriba;
                }
                else if (keyData == Keys.Right)
                {
                    direccionPulsada = 3;
                    botonAAnimar = pbMetaDer;
                    imagenHundida = imgBtnPresionadoDer;
                }
                else if (keyData == Keys.Down)
                {
                    direccionPulsada = 2;
                    botonAAnimar = pbMetaAbajo;
                    imagenHundida = imgBtnPresionadoAbajo;
                }
                else if (keyData == Keys.Left)
                {
                    direccionPulsada = 4;
                    botonAAnimar = pbMetaIzq;
                    imagenHundida = imgBtnPresionadoIzq;
                }

                if (direccionPulsada != 0)
                {
                    // 1. Disparamos la animación visual del botón abajo
                    AnimarBotonPresionado(botonAAnimar, imagenHundida);

                    // 2. Evaluamos si le diste a una nota que venía cayendo
                    VerificarGolpe(direccionPulsada);

                    return true; // Le dice a Windows que no intente mover el foco a otro botón
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // EL CEREBRO DEL HIT Y EL RANGO DE ACEPTACIÓN
        // EL CEREBRO DEL HIT POR COLISIÓN VISUAL
        private void VerificarGolpe(int direccionPulsada)
        {
            // 1. Identificamos cuál meta PictureBox corresponde a la tecla pulsada
            PictureBox pbMetaObjetivo = null;
            switch (direccionPulsada)
            {
                case 1: pbMetaObjetivo = pbMetaArriba; break;
                case 2: pbMetaObjetivo = pbMetaAbajo; break;
                case 3: pbMetaObjetivo = pbMetaDer; break;
                case 4: pbMetaObjetivo = pbMetaIzq; break;
            }

            if (pbMetaObjetivo == null) return;

            // 2. Buscamos en la pantalla notas que sean de esa misma dirección
            for (int i = 0; i < notasEnPantalla.Count; i++)
            {
                NotaVisual notaVisual = notasEnPantalla[i];

                if (notaVisual.Direccion == direccionPulsada)
                {
                    // --- LA LÓGICA MÁGICA DE COLISIÓN ---
                    // Verificamos matemáticamente si los rectángulos se superponen
                    if (notaVisual.Bounds.IntersectsWith(pbMetaObjetivo.Bounds))
                    {
                        // ¡ACIERTO VISUAL!
                        notasEnPantalla.RemoveAt(i);

                        // Sumamos puntos
                        puntuacion += 10;
                        lblPuntuacion.Text = "Puntos: " + puntuacion;

                        return; // Rompemos el ciclo para destruir solo una nota por cada teclazo
                    }
                }
            }
        }

        // TRUCO MODERNO: Animación rápida de botón presionado
        private async void AnimarBotonPresionado(PictureBox pbBotonMeta, Image imagenPresionada)
        {
            if (pbBotonMeta == null || imagenPresionada == null) return;

            // 1. Guardamos la imagen normal (la que está sin presionar)
            Image imagenNormal = pbBotonMeta.Image;

            // 2. Le ponemos la imagen "hundida" o "iluminada"
            pbBotonMeta.Image = imagenPresionada;

            // 3. Esperamos 100 milisegundos (puedes bajarlo a 80 o subirlo a 150 a tu gusto)
            await System.Threading.Tasks.Task.Delay(100);

            // 4. Lo devolvemos a su estado normal
            pbBotonMeta.Image = imagenNormal;
        }

        // Evento de tu botón de empezar
        // Este es tu botón "Saltar/Empezar"
        private void btnEmpezar_Click_1(object sender, EventArgs e)
        {
            IniciarJuegoDesdeIntro();
        }

        // Lógica maestra para apagar la intro y prender el juego
        private void IniciarJuegoDesdeIntro()
        {
            // 1. Apagamos todo lo de la historia
            timerEscribir.Stop();
            pnlNarrativaIntro.Visible = false;

            // 2. Prendemos la pista de baile
            pnlPistaBaile.Visible = true;
            lblPuntuacion.Visible = true;
            lblFaltas.Visible = true;
            pbJoseJesus.Visible = true;

            // 3. Arrancamos el contador 3, 2, 1...
            conteo = 3;
            lblCuentaRegresiva.Text = conteo.ToString();
            lblCuentaRegresiva.Font = fuenteJuegoGrande;
            lblCuentaRegresiva.Visible = true;
            btnEmpezar.Visible = false; // Ocultar botón (si lo dejaste afuera)

            timerCuenta.Start();
        }

        private void lblTextoNarrativa_Click(object sender, EventArgs e)
        {
            if (escribiendo)
            {
                // Si el jugador hace clic MIENTRAS se está escribiendo, autocompletamos la frase de golpe
                timerEscribir.Stop();
                lblTextoNarrativa.Text = dialogosJJ[indiceDialogo];
                escribiendo = false;
            }
            else
            {
                // Si ya terminó de escribir, pasamos a la siguiente frase
                indiceDialogo++;

                if (indiceDialogo < dialogosJJ.Length)
                {
                    EmpezarAEscribir(); // Arranca la siguiente parte
                }
                else
                {
                    // Si ya no hay más diálogos, iniciamos el juego automáticamente
                    IniciarJuegoDesdeIntro();
                }
            }
        }

        private void ActualizarDatos()
        {
            string rutaArchivo = "jugadores.json";

            if (!File.Exists(rutaArchivo)) return;

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