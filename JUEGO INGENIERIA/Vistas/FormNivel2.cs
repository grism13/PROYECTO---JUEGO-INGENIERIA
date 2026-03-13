using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class formNivel2 : Form
    {
        private string rutaArchivo = "mapa_gary_vs_david.txt";
        private Stopwatch cronometro = new Stopwatch();
        private SoundPlayer reproductor;
        private List<string> notasGrabadas = new List<string>();
        private bool estaGrabando = false;
        
        // Variable para controlar la cuenta regresiva
        private int conteo = 3; 

        public formNivel2()
        {
            InitializeComponent();
            reproductor = new SoundPlayer("ruta_a_tu_cancion.wav");
            this.KeyPreview = true; // Súper importante para que detecte el teclado
        }

        // Este botón ahora solo inicia la cuenta regresiva, NO la música
        private void btnEmpezar_Click(object sender, EventArgs e)
        {
            conteo = 3;
            lblCuentaRegresiva.Text = conteo.ToString();
            lblCuentaRegresiva.Visible = true;
            timerCuenta.Start(); // Arranca el timer de 1 segundo
        }

        // Da doble clic en tu timerCuenta en el diseñador para crear este evento
        private void timerCuenta_Tick(object sender, EventArgs e)
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
                // Cuando baja de 0, apagamos el timer y ¡ARRANCAMOS A GRABAR!
                timerCuenta.Stop();
                lblCuentaRegresiva.Visible = false;
                
                notasGrabadas.Clear();
                estaGrabando = true;
                
                reproductor.Play();
                cronometro.Start();
            }
        }

        // Evento cuando PRESIONAS la tecla (Ilumina el panel y graba)
        private void formNivel2_KeyDown(object sender, KeyEventArgs e)
        {
            if (!estaGrabando) return;

            if (e.KeyCode == Keys.Escape)
            {
                TerminarYGuardar();
                return;
            }

            long tiempoActual = cronometro.ElapsedMilliseconds;
            int numeroFlecha = 0;

            // Iluminamos el panel correspondiente y asignamos el número
            switch (e.KeyCode)
            {
                case Keys.Up:
                    numeroFlecha = 1;
                    panelArriba.BackColor = Color.Cyan; // Color de "encendido"
                    break;
                case Keys.Right:
                    numeroFlecha = 2;
                    panelDer.BackColor = Color.Red;
                    break;
                case Keys.Down:
                    numeroFlecha = 3;
                    panelAbajo.BackColor = Color.Lime;
                    break;
                case Keys.Left:
                    numeroFlecha = 4;
                    panelIzq.BackColor = Color.Magenta;
                    break;
            }

            if (numeroFlecha != 0)
            {
                notasGrabadas.Add($"{numeroFlecha},{tiempoActual}");
            }
        }

        // Evento cuando SUELTAS la tecla (Apaga el panel)
        private void formNivel2_KeyUp(object sender, KeyEventArgs e)
        {
            if (!estaGrabando) return;

            // Regresamos el color al estado "apagado" (gris)
            switch (e.KeyCode)
            {
                case Keys.Up: panelArriba.BackColor = Color.Gray; break;
                case Keys.Right: panelDer.BackColor = Color.Gray; break;
                case Keys.Down: panelAbajo.BackColor = Color.Gray; break;
                case Keys.Left: panelIzq.BackColor = Color.Gray; break;
            }
        }

        private void TerminarYGuardar()
        {
            estaGrabando = false;
            cronometro.Stop();
            reproductor.Stop();
            File.WriteAllLines(rutaArchivo, notasGrabadas);
            MessageBox.Show("¡Nivel guardado con éxito!");
        }
    }
}