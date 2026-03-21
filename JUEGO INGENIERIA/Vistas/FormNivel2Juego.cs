using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using System.Drawing.Text;

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

        public FormNivel2Juego()
        {
            InitializeComponent();

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
                if (nota.Y > pnlPistaBaile.Height)
                {
                    notasEnPantalla.RemoveAt(i);
                    // Ya no hay PictureBox que destruir ni remover del panel

                    faltas++; // <--- SUMAMOS UNA FALTA
                    lblFaltas.Text = "Faltas: " + faltas; 
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

                MessageBox.Show("¡Uff, coronaste la pista de baile!", "Nivel Completado");

                // Aquí en el futuro pones la lógica para volver al menú o pasar de nivel
                // this.Close(); 
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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (timerJuego.Enabled)
            {
                int direccionPulsada = 0;
                if (keyData == Keys.Up) direccionPulsada = 1;
                else if (keyData == Keys.Right) direccionPulsada = 3;
                else if (keyData == Keys.Down) direccionPulsada = 2;
                else if (keyData == Keys.Left) direccionPulsada = 4;

                if (direccionPulsada != 0)
                {
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

        // Evento de tu botón de empezar
        private void btnEmpezar_Click_1(object sender, EventArgs e)
        {
            conteo = 3;
            lblCuentaRegresiva.Text = conteo.ToString();
            lblCuentaRegresiva.Visible = true;
            btnEmpezar.Visible = false;

            timerCuenta.Start();
        }
    }
}