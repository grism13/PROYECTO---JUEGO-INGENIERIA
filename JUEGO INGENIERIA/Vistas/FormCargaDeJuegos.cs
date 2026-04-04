using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormCargaDeJuegos : Form
    {
        private System.Windows.Forms.Timer animTimer;
        private int currentFrame = 1;
        private float currentAngle = 0f;
        private bool isRotating = false;
        private Image? frame4Image;
        private Func<Form>? formFactory; // Almacena qué nivel vamos a cargar después
        
        // Variables para la animación de puntos
        private System.Windows.Forms.Timer textTimer;
        private int cantidadPuntos = 1;

        // Constructor estándar (ideal por si lo corres directo desde Program.cs para probar)
        public FormCargaDeJuegos()
        {
            InitBase();
        }

        // Constructor puente: Aquí le pasas el nivel al que vas: "new FormCargaDeJuegos(() => new FormTrabajo())"
        public FormCargaDeJuegos(Func<Form> factoryDestino)
        {
            InitBase();
            this.formFactory = factoryDestino;

            // Creamos un temporizador general que determine el tiempo inicial de la animación pura
            System.Windows.Forms.Timer transicionTimer = new System.Windows.Forms.Timer();
            transicionTimer.Interval = 2000; // 2 segundos de animación garantizada antes de empezar a cargar la memoria
            transicionTimer.Tick += (s, e) =>
            {
                transicionTimer.Stop(); // Paramos de esperar
                if (formFactory != null)
                {
                    Form proximoNivel = formFactory(); // Aquí se procesa toda la carga pesada

                    // Evento clave: Una vez que tu nivel ya terminó de inicializarse e intenta mostrarse
                    proximoNivel.Shown += (senderForm, args) => 
                    {
                        // Le damos 1 segundo de gracia (1000ms) para que Windows dibuje todo perfectamente bajo las cortinas
                        System.Windows.Forms.Timer revelarTimer = new System.Windows.Forms.Timer();
                        revelarTimer.Interval = 1000; 
                        revelarTimer.Tick += (s2, e2) => 
                        {
                            revelarTimer.Stop();
                            this.Hide(); // ¡Quitamos la cortina de carga! Descubriendo el juego fluído
                        };
                        revelarTimer.Start();
                    };

                    // Mostramos tu nivel. Al ser Dialog, el juego lo espera. 
                    proximoNivel.ShowDialog(); 
                    
                    // Esta línea solo se ejecuta cuando sales del nivel actual y quieres volver al mapa principal
                    this.Close(); 
                }
            };
            transicionTimer.Start();
        }

        // Concentramos la inicialización aquí para no repetir código
        private void InitBase()
        {
            InitializeComponent();

            // Asegurarnos de que cubra absolutamente todo como pantalla de carga
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;

            // Guardamos la imagen original 4 para dibujarla girando
            frame4Image = Properties.Resources.relojArena4;

            // Activar doble buffer en el PictureBox para rotación suave
            typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
               ?.SetValue(RelojDeArena, true, null);

            RelojDeArena.Paint += RelojDeArena_Paint;

            animTimer = new System.Windows.Forms.Timer();
            animTimer.Interval = 200; // Velocidad de la animación por cuadro en milisegundos
            animTimer.Tick += AnimTimer_Tick;
            animTimer.Start();

            // Configuramos animación de los puntos suspensivos ("CARGANDO.")
            textTimer = new System.Windows.Forms.Timer();
            textTimer.Interval = 500; // Medio segundo para agregar un punto
            textTimer.Tick += (s, e) =>
            {
                cantidadPuntos++;
                if (cantidadPuntos > 3) cantidadPuntos = 1; // Si llega a 4, volvemos a 1
                
                label1.Text = "CARGANDO" + new string('.', cantidadPuntos);
            };
            textTimer.Start();
        }

        private void AnimTimer_Tick(object? sender, EventArgs e)
        {
            if (!isRotating)
            {
                currentFrame++;
                if (currentFrame > 4)
                {
                    // Empezar a rotar la imagen 4
                    isRotating = true;
                    currentAngle = 0f;
                    animTimer.Interval = 15; // Intervalo más rápido para que el giro se vea suave
                    RelojDeArena.Image = null; // Quitamos la imagen nativa para dibujarla manualmente rotando
                    return;
                }

                UpdateFrameImage();
            }
            else
            {
                currentAngle += 12f; // Grados que gira por cada tick
                if (currentAngle >= 180f)
                {
                    // Terminar rotación y reiniciar la secuencia de arenas
                    currentAngle = 180f;
                    isRotating = false;
                    currentFrame = 1;
                    animTimer.Interval = 200; // Volver a la velocidad normal entre cuadros
                    UpdateFrameImage();
                }
                else
                {
                    RelojDeArena.Invalidate(); // Pedir que se dibuje el nuevo ángulo
                }
            }
        }

        private void UpdateFrameImage()
        {
            switch (currentFrame)
            {
                case 1: RelojDeArena.Image = Properties.Resources.relojArena1; break;
                case 2: RelojDeArena.Image = Properties.Resources.relojArena2; break;
                case 3: RelojDeArena.Image = Properties.Resources.relojArena3; break;
                case 4: RelojDeArena.Image = Properties.Resources.relojArena4; break;
            }
        }

        private void RelojDeArena_Paint(object? sender, PaintEventArgs e)
        {
            // Solo dibujamos la imagen manualmente cuando estamos en el estado de rotación
            if (isRotating && frame4Image != null)
            {
                e.Graphics.Clear(RelojDeArena.BackColor);

                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Mover el punto de rotación al centro del PictureBox
                e.Graphics.TranslateTransform(RelojDeArena.Width / 2f, RelojDeArena.Height / 2f);
                e.Graphics.RotateTransform(currentAngle);
                // Moverlo de vuelta
                e.Graphics.TranslateTransform(-RelojDeArena.Width / 2f, -RelojDeArena.Height / 2f);

                // Dibujar la imagen centrada desde el punto 0,0 real del PictureBox
                e.Graphics.DrawImage(frame4Image, new Rectangle(0, 0, RelojDeArena.Width, RelojDeArena.Height));
            }
        }
    }
}
