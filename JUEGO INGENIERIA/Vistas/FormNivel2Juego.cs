using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

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

        public FormNivel2Juego()
        {
            InitializeComponent();
            reproductor = new SoundPlayer(rutaCancion);
            ConfigurarTimers();
            CargarMapaDeNotas();
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

            // Debug visual para ti
            lblCuentaRegresiva.Visible = true;
            lblCuentaRegresiva.Font = new Font("Arial", 12);
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

                // Si la nota sale de la pantalla por abajo, se borra (El jugador falló)
                if (pic.Top > this.Height)
                {
                    this.Controls.Remove(pic);
                    notasEnPantalla.RemoveAt(i);
                }
            }
        }

        // EL GENERADOR DE CUADROS
        private void GenerarNotaVisual(int direccion, long tiempoObjetivo)
        {
            PictureBox nuevaNota = new PictureBox();
            nuevaNota.Size = new Size(50, 50);
            nuevaNota.Top = -50; // Nacen un poco más arriba para que la caída sea limpia

            // TRUCO: Guardamos en el cuadro de qué dirección es y en qué milisegundo exacto debía tocarse
            nuevaNota.Tag = $"{direccion},{tiempoObjetivo}";

            switch (direccion)
            {
                case 1: nuevaNota.BackColor = Color.Cyan; nuevaNota.Left = 100; break;     // Arriba
                case 2: nuevaNota.BackColor = Color.Red; nuevaNota.Left = 160; break;      // Derecha
                case 3: nuevaNota.BackColor = Color.Lime; nuevaNota.Left = 220; break;     // Abajo
                case 4: nuevaNota.BackColor = Color.Magenta; nuevaNota.Left = 40; break;   // Izquierda
            }

            this.Controls.Add(nuevaNota);
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
        private void VerificarGolpe(int direccionPulsada)
        {
            long tiempoActual = cronometro.ElapsedMilliseconds;

            for (int i = 0; i < notasEnPantalla.Count; i++)
            {
                PictureBox pic = notasEnPantalla[i];
                string[] datos = pic.Tag.ToString().Split(',');

                int direccionNota = int.Parse(datos[0]);
                long tiempoNota = long.Parse(datos[1]);

                // Si tocaste la flecha correcta para este color...
                if (direccionNota == direccionPulsada)
                {
                    // Calculamos la diferencia matemática (si te adelantaste o te atrasaste)
                    long diferencia = Math.Abs(tiempoActual - tiempoNota);

                    // Si estás dentro de los 250ms de tolerancia...
                    if (diferencia <= margenError)
                    {
                        // ¡ACIERTO! Eliminamos el cuadro de la pantalla
                        this.Controls.Remove(pic);
                        notasEnPantalla.RemoveAt(i);

                        // Aquí es donde en el futuro sumaremos puntos

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