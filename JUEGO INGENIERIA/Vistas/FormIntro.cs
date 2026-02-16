using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text; // NUEVO: Librería para fuentes personalizadas
using System.IO;           // NUEVO: Librería para buscar archivos en la PC

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormIntro : Form
    {
        List<string> textos = new List<string>();
        List<string> imagenes = new List<string>();
        int posicion = 0;

        // NUEVO: Creamos la "mochila" para la fuente al inicio de la clase
        PrivateFontCollection pfc = new PrivateFontCollection();

        public FormIntro()
        {
            InitializeComponent();

            // NUEVO: Llamamos a la función ANTES de cargar los textos para que se aplique de una vez
            CargarFuente();

            CargarDatos();
            ActualizarPantalla();
        }

        // NUEVO: La función que busca el archivo y cambia la letra
        private void CargarFuente()
        {
            // Busca la fuente en la carpeta Vistas -> Fuentes
            string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf");

            if (File.Exists(rutaFuente))
            {
                pfc.AddFontFile(rutaFuente);

                // Creamos los estilos (12 para el texto, 10 para el botón)
                Font fuentePokemonTexto = new Font(pfc.Families[0], 12f);
                Font fuentePokemonBoton = new Font(pfc.Families[0], 10f);

                // Se la aplicamos a los controles de tu diseño
                lblTexto.Font = fuentePokemonTexto;

                // NOTA: Asumiendo que tu botón se llama btnSiguiente en el diseño
                // Si tiene otro nombre, cámbialo aquí.
                // btnSiguiente.Font = fuentePokemonBoton; 
            }
            else
            {
                MessageBox.Show("Ojo: No se encontró la fuente en la ruta: " + rutaFuente);
            }
        }

        private void CargarDatos()
        {
            // Escena 1
            textos.Add("Hace mucho tiempo, antes de que el WiFi llegara a todos los rincones del campus y cuando los compiladores eran impiadosos con los mortales, existía el Caos. El código espagueti reinaba y los NullReferenceException aterrorizaban la tierra.");
            imagenes.Add("primera imagen de la intro.png");

            // Escena 2
            textos.Add("Pero de entre la oscuridad de la sintaxis errónea, surgió él: Flavio.\r\nNo nació siendo un Dios, no. Flavio comenzó desde abajo, enfrentándose a los demonios del Cálculo Diferencial y a las bestias de la Física Cuántica. Se cuenta que pasaba noches enteras sin dormir, alimentándose solo de café puro y determinación, picando código en lenguajes que ya han sido olvidados por la humanidad. Nivel tras nivel, semestre tras semestre, Flavio fue desbloqueando los secretos del universo digital. Dominó el C#, domó al Python y, con sus propias manos, estructuró la base de datos de la realidad misma. Al presentar su Tesis Final, el cielo se abrió y el compilador no arrojó ni un solo error. En ese momento, Flavio dejó de ser un simple estudiante y ascendió al panteón de los Ingenieros, convirtiéndose en el \"Ser de Luz del Sistema\", el guardián de la Lógica Perfecta.");
            imagenes.Add("segunda imagen de la intro (1).png");

            // Escena 3
            textos.Add("Capítulo 1: Los Herederos de la Voluntad\r\n\"La leyenda de Flavio quedó grabada en los muros de la UNIMAR. Se decía que aquel que siguiera sus pasos obtendría el poder de crear mundos con solo teclear. Años después, en las soleadas tierras de Margarita, tres jóvenes escucharon el llamado. No eran héroes... todavía. Eran soñadores.\r\n\r\nGris, la estratega, quien veía en la ingeniería el orden que el mundo necesitaba.\r\nRoand, el audaz, dispuesto a enfrentar cualquier algoritmo que se le cruzara.\r\nY Eliezer (tú), el visionario práctico, quien entendía que para construir el futuro, primero había que entender cómo funcionaban las cosas por dentro.");
            imagenes.Add("tercera imagen de la intro (1).png");

            // Escenea 4
            textos.Add("Inspirados por la historia del Gran Flavio, los tres se reunieron frente a las puertas de la universidad. Sus ojos brillaban con esperanza. Se miraron entre sí y prometieron que no descansarían hasta alcanzar el mismo título divino que su ídolo.\r\n\r\n—'Si Flavio pudo, nosotros también', dijo Eliezer con una sonrisa confiada. —'Seremos Ingenieros. ¿Qué tan difícil puede ser?'\r\n\r\nPobres insensatos... No sabían que la universidad no era solo un templo de conocimiento, sino una mazmorra llena de pruebas, trampas y profesores que custodiaban la nota aprobatoria como si fuera oro.");
            imagenes.Add("cuarta imagen para la intro.png");

            // Escena 5
            textos.Add("Con el pecho inflado de orgullo y la billetera lista, dieron el primer paso hacia el campus.\r\n\r\nFue entonces cuando sonó el primer golpe de realidad...");
            imagenes.Add("quinta imagen para la intro.png");
        }

        //Funcion de actuaizar pantalla
        private void ActualizarPantalla()
        {
            // Verificamos que los controles existan con estos nombres en el diseño
            if (posicion < textos.Count)
            {
                lblTexto.Text = textos[posicion];

                string ruta = Path.Combine(Application.StartupPath, "Vistas", "imagenes", imagenes[posicion]);

                if (File.Exists(ruta))
                {
                    // Liberamos la imagen anterior si existe para no saturar la RAM
                    if (pbImagen.Image != null) pbImagen.Image.Dispose();
                    pbImagen.Image = Image.FromFile(ruta);
                }
            }

            if (posicion > 4)
            {
                this.Close();
            }

        }

        private void btnSiguiente_Click_1(object sender, EventArgs e)
        {
            posicion++;
            ActualizarPantalla();
        }

        private void FormIntro_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (posicion < 5)
            {
                // AHORA SÍ FUNCIONARÁ EL CANCEL
                e.Cancel = true;
                MessageBox.Show("¡Espera! Debes terminar la historia para continuar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FormIntro_Load(object sender, EventArgs e)
        {

        }

        private void pbImagen_Click(object sender, EventArgs e)
        {

        }
    }
}
