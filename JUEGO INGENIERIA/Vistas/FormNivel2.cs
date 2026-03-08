using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormNivel2 : Form
    {
        List<int> listaComandos = new List<int>();
        int indiceActual = 0;
        List<NotaVisual> flechasActivas = new List<NotaVisual>();
        int velocidadCaida = 10;

        public FormNivel2()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        // Este es el molde de los picturebox que no son picturebox
        public class NotaVisual
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Color ColorNota { get; set; }
        }

        private void CargarPartitura()
        {
            try
            {
                // Asegúrate de que el nombre del archivo sea exacto
                string rutaArchivo = "cancion.txt";

                // Lee todas las líneas de golpe
                string[] lineas = File.ReadAllLines(rutaArchivo);

                foreach (string linea in lineas)
                {
                    // Convertimos el texto a número y lo metemos en la lista
                    if (int.TryParse(linea.Trim(), out int comando))
                    {
                        listaComandos.Add(comando);
                    }
                }

                timerMusica.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error al leer la partitura: " + ex.Message);
            }
        }

        private void FormNivel2_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(FormNivel2_Paint);

            CargarPartitura();
            timerMusica.Start();
            timerAnimacion.Start();
        }



        private void timerMusica_Tick(object sender, EventArgs e)
        {
            if (indiceActual < listaComandos.Count)
            {
                int nota = listaComandos[indiceActual];

                // Si hay una nota (1, 2, 3, 4), creamos el cuadro visual
                if (nota != 0)
                {
                    GenerarFlechaVisual(nota);
                }

                indiceActual++;
            }
            else
            {
                timerMusica.Stop();
            }
        }

        private void GenerarFlechaVisual(int direccion)
        {
            // Creamos los datos de la nota, NO un PictureBox
            NotaVisual nuevaNota = new NotaVisual();
            nuevaNota.Y = 0; // Nace arriba

            if (direccion == 4) { nuevaNota.X = 50; nuevaNota.ColorNota = Color.Purple; } // Izq
            else if (direccion == 2) { nuevaNota.X = 120; nuevaNota.ColorNota = Color.Blue; }  // Abajo
            else if (direccion == 1) { nuevaNota.X = 190; nuevaNota.ColorNota = Color.Green; } // Arriba
            else if (direccion == 3) { nuevaNota.X = 260; nuevaNota.ColorNota = Color.Red; }   // Der

            flechasActivas.Add(nuevaNota);
        }


        private void timerAnimacion_Tick(object sender, EventArgs e)
        {
            for (int i = flechasActivas.Count - 1; i >= 0; i--)
            {
                // Movemos la posición Y (matemáticamente)
                flechasActivas[i].Y += velocidadCaida;

                // Si sale de la pantalla, la borramos
                if (flechasActivas[i].Y > this.Height)
                {
                    flechasActivas.RemoveAt(i);
                }
            }

            // ESTO ES CLAVE: Le avisa a Windows que los datos cambiaron y debe redibujar la pantalla
            this.Invalidate();
        }

        private void FormNivel2_Paint(object sender, PaintEventArgs e)
        {
            // Recorremos nuestra lista ligera y dibujamos un cuadrado por cada una
            foreach (NotaVisual nota in flechasActivas)
            {
                // Usamos una "brocha" del color que guardamos
                using (SolidBrush brocha = new SolidBrush(nota.ColorNota))
                {
                    // Dibujamos directamente en la pantalla (Brocha, X, Y, Ancho, Alto)
                    e.Graphics.FillRectangle(brocha, nota.X, nota.Y, 50, 50);
                }
            }
        }

    }
}
