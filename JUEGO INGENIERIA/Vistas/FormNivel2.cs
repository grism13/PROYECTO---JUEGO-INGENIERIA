using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using JUEGO_INGENIERIA.Modelos;
using System.Drawing;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class formNivel2 : Form
    {

        //"C:\Users\eliez\Desktop\cancion-formNivel2.wav"
        private string rutaArchivo = "C:\\Users\\eliez\\Desktop\\cancion.txt";
        private Stopwatch cronometro = new Stopwatch();
        private SoundPlayer reproductor;
        private List<string> notasGrabadas = new List<string>();
        private bool estaGrabando = false;

        // Variable para controlar la cuenta regresiva
        private int conteo = 3;

        public formNivel2()
        {
            InitializeComponent();
            reproductor = new SoundPlayer("C:\\Users\\eliez\\Desktop\\cancion-formNivel2.wav");
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


        // Evento cuando PRESIONAS la tecla (Ilumina el panel y graba)
        // Esta función intercepta las teclas ANTES de que Windows robe el foco
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (estaGrabando)
            {
                long tiempoActual = cronometro.ElapsedMilliseconds;
                int numeroFlecha = 0;
                Control panelActivo = null;

                // Si presiona ESC, terminamos y guardamos
                if (keyData == Keys.Escape)
                {
                    TerminarYGuardar();
                    return true;
                }

                // Asignamos números y encendemos el panel correspondiente
                if (keyData == Keys.Up) { numeroFlecha = 1; panelActivo = panelArriba; panelActivo.BackColor = Color.Cyan; }
                else if (keyData == Keys.Right) { numeroFlecha = 2; panelActivo = panelDer; panelActivo.BackColor = Color.Red; }
                else if (keyData == Keys.Down) { numeroFlecha = 3; panelActivo = panelAbajo; panelActivo.BackColor = Color.Lime; }
                else if (keyData == Keys.Left) { numeroFlecha = 4; panelActivo = panelIzq; panelActivo.BackColor = Color.Magenta; }

                // Si presionó una flecha, guardamos el dato y hacemos el efecto visual
                if (numeroFlecha != 0)
                {
                    notasGrabadas.Add($"{numeroFlecha},{tiempoActual}");

                    ApagarPanel(panelActivo); // Llama al efecto de apagado rápido

                    return true; // Le dice a Windows: "Ya usé esta tecla, no cambies de botón"
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Truco moderno: Espera 150 milisegundos y apaga el panel automáticamente
        private async void ApagarPanel(Control p)
        {
            await System.Threading.Tasks.Task.Delay(150);
            if (p != null) p.BackColor = Color.Gray;
        }

        private void TerminarYGuardar()
        {
            estaGrabando = false;
            cronometro.Stop();
            reproductor.Stop();
            File.WriteAllLines(rutaArchivo, notasGrabadas);
            MessageBox.Show("¡Nivel guardado con éxito!");
        }

        private void timerCuenta_Tick_1(object sender, EventArgs e)
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
    }
}