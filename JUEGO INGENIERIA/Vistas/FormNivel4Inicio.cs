using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormNivel4Inicio : Form
    {
        private FormMovimiento movimiento;
        private System.Windows.Forms.Timer tmrPersecucion;
        private bool esFaseExterior = false;

        public FormNivel4Inicio()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Load += FormNivel4Inicio_Load;
            this.FormClosing += FormNivel4Inicio_FormClosing;
            this.Paint += FormNivel4Inicio_Paint;
        }
        private void FormNivel4Inicio_Load(object sender, EventArgs e)
        {
            if (pbPersonaje != null)
            {
                // Iniciar FormMovimiento con pbPersonaje
                movimiento = new FormMovimiento(this, pbPersonaje, false);
                movimiento.Start();
                
                // Cargar la imagen de la cabeza dinámicamente desde los recursos para el personaje interior
                CargarCabezaPersonaje(pbPersonaje);

                // Restaurar visibilidad para usar una sola imagen estática
                pbPersonaje.Visible = true;
                pbPersonaje.SizeMode = PictureBoxSizeMode.Zoom;
            }
            // ---- AQUÍ SE CREA EL TIMER POR CÓDIGO ----
            tmrPersecucion = new System.Windows.Forms.Timer();
            tmrPersecucion.Interval = 40; // 25 fps aprox para la actualización de la IA
            tmrPersecucion.Tick += TmrPersecucion_Tick;
            tmrPersecucion.Start();
        }
        private void TmrPersecucion_Tick(object sender, EventArgs e)
        {
            if (pbPersonaje == null || pbMalo == null || pbPuerta == null) return;
            // 1. Lógica para que el enemigo persiga al personaje
            int velocidadEnemigo = 3; // Velocidad de pbMalo
            if (pbMalo.Left < pbPersonaje.Left) pbMalo.Left += velocidadEnemigo;
            else if (pbMalo.Left > pbPersonaje.Left) pbMalo.Left -= velocidadEnemigo;
            if (pbMalo.Top < pbPersonaje.Top) pbMalo.Top += velocidadEnemigo;
            else if (pbMalo.Top > pbPersonaje.Top) pbMalo.Top -= velocidadEnemigo;
            // 2. Colisión Jugador - Enemigo (Game Over / Reinicio)
            if (pbPersonaje.Bounds.IntersectsWith(pbMalo.Bounds))
            {
                tmrPersecucion.Stop();
                if (movimiento != null) movimiento.Stop();
                MessageBox.Show("¡El enemigo te ha atrapado! Inténtalo de nuevo.");
                ReiniciarNivel();
            }
            // 3. Colisión Jugador - Salida (Completar laberinto y pasar a explorar el Form)
            if (pbPersonaje.Bounds.IntersectsWith(pbPuerta.Bounds))
            {
                tmrPersecucion.Stop();
                if (movimiento != null) movimiento.Stop();

                // Ocultar al personaje original para simular que salió del laberinto
                pbPersonaje.Visible = false;

                // Buscar pbPersonaje2 (o pictureBox1 si aún no le cambiaste el nombre en las propiedades)
                PictureBox pbPersonajeFuera = this.Controls.Find("pbPersonaje2", true).FirstOrDefault() as PictureBox;
                if (pbPersonajeFuera == null)
                {
                    // Si no se encuentra como pbPersonaje2, asumimos que es pictureBox1
                    pbPersonajeFuera = this.Controls.Find("pictureBox1", true).FirstOrDefault() as PictureBox;
                }

                if (pbPersonajeFuera != null)
                {
                    // Transferir el control conectando FormMovimiento al nuevo personaje outside
                    movimiento = new FormMovimiento(this, pbPersonajeFuera, false);
                    movimiento.Start();
                    
                    // Activamos la fase exterior para que OnPaint comience a registrar los gráficos animados
                    esFaseExterior = true;
                    
                    if (pbLaverinto != null) pbLaverinto.Visible = false;
                }
            }
        }

        private void FormNivel4Inicio_Paint(object sender, PaintEventArgs e)
        {
            // Dibujar al personaje animado solo si estamos en la fase exterior (ya fuera del laberinto)
            if (esFaseExterior && movimiento != null)
            {
                movimiento.DibujarPersonaje(e.Graphics);
            }
        }
        private void ReiniciarNivel()
        {
            if (pbPersonaje != null)
            {
                pbPersonaje.Left = 50;
                pbPersonaje.Top = 50;
            }
            if (pbLaverinto != null && pbMalo != null)
            {
                pbMalo.Left = pbLaverinto.Width - 100;
                pbMalo.Top = pbLaverinto.Height - 100;
            }
            if (movimiento != null) movimiento.Start();
            if (tmrPersecucion != null) tmrPersecucion.Start();
        }
        private void CargarCabezaPersonaje(PictureBox pb)
        {
            try
            {
                string p = "gris";
                if (!string.IsNullOrEmpty(DatosJuego.PersonajeElegido))
                {
                    p = DatosJuego.PersonajeElegido.ToLower();
                }

                // Buscar la imagen de la cabeza (soporta nombre exacto o con guión bajo si VS lo renombra)
                object obj = Properties.Resources.ResourceManager.GetObject($"{p}-cabeza");
                if (obj == null) obj = Properties.Resources.ResourceManager.GetObject($"{p}_cabeza");
                
                // Si no tiene, por defecto cargamos a gris-cabeza
                if (obj == null) obj = Properties.Resources.ResourceManager.GetObject("gris-cabeza");
                if (obj == null) obj = Properties.Resources.ResourceManager.GetObject("gris_cabeza");

                if (obj != null)
                {
                    pb.Image = (Image)obj;
                }
            }
            catch (Exception) { }
        }

        private void FormNivel4Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (movimiento != null) movimiento.Stop();
            if (tmrPersecucion != null) tmrPersecucion.Stop();
        }
    }
}