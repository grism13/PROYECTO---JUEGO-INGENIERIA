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
        private int velocidadCaida = 6; // Qué tan rápido caen los cuadros (ajusta esto si caen muy lento)
        // --- ANIMACIÓN DE JOSÉ JESÚS ---
        private Image[] framesJoseJesus;
        private int frameActualJJ = 0;
        private int contadorAnimacionJJ = 0;
        private int velocidadAnimacionJJ = 10; // A menor número, más rápido toca la guitarra

        // --- FUENTE PERSONALIZADA ---
        PrivateFontCollection coleccionFuentes = new PrivateFontCollection();
        Font fuenteJuegoNormal;
        Font fuenteJuegoGrande;
        // Estructura y Listas
        struct Nota
        {
            public int Direccion;
            public long Tiempo;
        }

        private List<Nota> listaNotas = new List<Nota>();
        private List<PictureBox> notasEnPantalla = new List<PictureBox>();
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

            // 2. MOVER NOTAS
            for (int i = notasEnPantalla.Count - 1; i >= 0; i--)
            {
                PictureBox pic = notasEnPantalla[i];
                pic.Top += velocidadCaida;

                // Si la nota sale del PANEL por abajo, se borra
                if (pic.Top > pnlPistaBaile.Height)
                {
                    pnlPistaBaile.Controls.Remove(pic); // BORRAR DEL PANEL
                    notasEnPantalla.RemoveAt(i);

                    faltas++; // <--- SUMAMOS UNA FALTA
                    lblFaltas.Text = "Faltas: " + faltas; 
                }
            }
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


        // EL GENERADOR DE CUADROS
        // EL GENERADOR DE CUADROS - Ahora con imágenes
        private void GenerarNotaVisual(int direccion, long tiempoObjetivo)
        {
            PictureBox nuevaNota = new PictureBox();
            nuevaNota.Size = new Size(80, 80); // Ajusta si tus imágenes son más grandes
            nuevaNota.Top = -50;

            // SUPER IMPORTANTE: Para que la imagen se adapte al cuadrito
            nuevaNota.SizeMode = PictureBoxSizeMode.Zoom;
            nuevaNota.BackColor = Color.Transparent; // Para que no tenga fondo feo

            nuevaNota.Tag = $"{direccion},{tiempoObjetivo}";

            // Asignamos la imagen correcta y alineamos con la meta
            switch (direccion)
            {
                case 1: // ARRIBA - Azul
                    nuevaNota.Image = Properties.Resources.flechaarrb; // <--- Tu imagen aquí
                    nuevaNota.Left = pbMetaArriba.Left;
                    break;
                case 2: // ABAJO - Rojo
                    nuevaNota.Image = Properties.Resources.flechaAbj; // <--- Tu imagen aquí
                    nuevaNota.Left = pbMetaAbajo.Left;
                    break;
                case 3: // DERECHA - Verde
                    nuevaNota.Image = Properties.Resources.flechaDer; // <--- Tu imagen aquí
                    nuevaNota.Left = pbMetaDer.Left;
                    break;
                case 4: // IZQUIERDA - Rosa
                    nuevaNota.Image = Properties.Resources.flechaIzq; // <--- Tu imagen aquí
                    nuevaNota.Left = pbMetaIzq.Left;
                    break;
            }

            pnlPistaBaile.Controls.Add(nuevaNota);
            nuevaNota.BringToFront();
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
                PictureBox notaVisual = notasEnPantalla[i];
                string[] datos = notaVisual.Tag.ToString().Split(',');
                int direccionNota = int.Parse(datos[0]);

                if (direccionNota == direccionPulsada)
                {
                    // --- LA LÓGICA MÁGICA DE COLISIÓN ---
                    // Verificamos matemáticamente si los rectángulos se superponen
                    if (notaVisual.Bounds.IntersectsWith(pbMetaObjetivo.Bounds))
                    {
                        // ¡ACIERTO VISUAL!
                        pnlPistaBaile.Controls.Remove(notaVisual);
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