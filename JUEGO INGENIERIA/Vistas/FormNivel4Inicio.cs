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

        public FormNivel4Inicio()
        {
            InitializeComponent();
            this.Load += FormNivel4Inicio_Load;
            this.FormClosing += FormNivel4Inicio_FormClosing;
        }

        private void FormNivel4Inicio_Load(object sender, EventArgs e)
        {
            if (pbPersonaje != null)
            {
                // Iniciar FormMovimiento con pbPersonaje
                movimiento = new FormMovimiento(this, pbPersonaje, false);
                movimiento.Start();

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

            // 3. Colisión Jugador - Salida (Completar laberinto y pasar al Nivel 4)
            if (pbPersonaje.Bounds.IntersectsWith(pbPuerta.Bounds))
            {
                tmrPersecucion.Stop();
                if (movimiento != null) movimiento.Stop();
                MessageBox.Show("¡Escape Exitoso!");

                // FormNivel4 frmNivel4 = new FormNivel4();
                // frmNivel4.Show();
                this.Close(); // O ocultar este form y abrir el otro según prefieras configurar el Nivel 4 real
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

        private void FormNivel4Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (movimiento != null) movimiento.Stop();
            if (tmrPersecucion != null) tmrPersecucion.Stop();
        }
    }
}
