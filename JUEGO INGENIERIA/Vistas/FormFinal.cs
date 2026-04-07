using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormFinal : Form
    {
        List<string> textos = new List<string>();
        List<Image> imagenes = new List<Image>();
        int posicion = 0;

        PrivateFontCollection pfc = new PrivateFontCollection();

        public FormFinal()
        {
            InitializeComponent();
            System.Media.SoundPlayer sonidoFondoIntro = new System.Media.SoundPlayer(Properties.Resources.intro_juegoINGENERIA__1_);
            sonidoFondoIntro.PlayLooping();
            ResolucionPantalla.ForzarResolucionJuego();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            CargarFuente();
            CargarDatos();
            ActualizarPantalla();
            NavegacionConsola.Configurar(this, btnSkip, btnSiguiente);
        }

        private void CargarFuente()
        {
            string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf");

            if (File.Exists(rutaFuente))
            {
                pfc.AddFontFile(rutaFuente);

                Font fuentePokemonTexto = new Font(pfc.Families[0], 12f);
                Font fuentePokemonBoton = new Font(pfc.Families[0], 10f);

                lblTexto.Font = fuentePokemonTexto;
                btnSiguiente.Font = fuentePokemonBoton;
                btnSkip.Font = fuentePokemonBoton;
            }
            else
            {
                MessageBox.Show("Ojo: No se encontró la fuente en la ruta: " + rutaFuente);
            }
        }

        private void CargarDatos()
        {
            // Escena 1
            textos.Add("El silencio inundó el auditorio tras el último eco de sus voces. Los temibles jurados se miraron, cerraron sus libretas y asintieron. ¡Habían derrotado al jefe final y aprobado con la máxima nota!!");
            imagenes.Add(Properties.Resources.fondoFinal1);

            // Escena 2
            textos.Add("La inmensa mazmorra de la UNIMAR y sus trampas quedaron atrás. Con sus togas y títulos, el viento de Margarita los recibió como héroes. El inmenso peso del mundo por fin había desaparecido de sus hombros.");
            imagenes.Add(Properties.Resources.fondoFinal2_2);

            // Escena 3
            textos.Add("Gris, Roand y Eliezer se detuvieron en la plaza central. Juntos, levantaron la mirada hacia el cielo despejado. Allí recordaron la leyenda que los impulsó desde el inicio.");
            imagenes.Add(Properties.Resources.fondoFinal3);

            // Escena 4
            textos.Add("Sabían que el gran Flavio habitaba en una dimensión inalcanzable. No eran dioses, pero al ver el atardecer, sonrieron con satisfacción. Habían superado la prueba, dando el primer paso para seguir su ejemplo.");
            imagenes.Add(Properties.Resources.fondoFinal4);

            // Escena 5
            textos.Add("La larga y exigente etapa del tutorial había llegado a su fin. Ahora, con el título equipado en sus manos, estaban listos. La verdadera aventura de construir su propio mundo acababa de comenzar.");
            imagenes.Add(Properties.Resources.fondoFinal5);
        }

        private void ActualizarPantalla()
        {
            if (posicion < textos.Count)
            {
                lblTexto.Text = textos[posicion];
                pbImagen.Image = imagenes[posicion];
            }

            if (posicion > 4)
            {
                System.Media.SoundPlayer sonidoCaja = new System.Media.SoundPlayer(Properties.Resources.SONIDO_DE_CAJA_COBRANDO);
                sonidoCaja.Play();

                this.Close();
            }
        }

        private void btnSiguiente_Click_1(object sender, EventArgs e)
        {
            posicion++;
            ActualizarPantalla();
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            // Salta directamente a la última posición para cerrar o avanzar a la siguiente pantalla
            posicion = 5;
            ActualizarPantalla();
        }

        private void FormFinal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (posicion < 5)
            {
                e.Cancel = true;
                MessageBox.Show("¡Espera! Debes terminar la historia para continuar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FormFinal_Load(object sender, EventArgs e)
        {
        }

        private void pbImagen_Click(object sender, EventArgs e)
        {
        }
    }
}

