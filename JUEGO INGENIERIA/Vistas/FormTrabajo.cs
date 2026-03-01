using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormTrabajo : Form
    {
        private FormMovimiento movimientoJugador;
        private int tiempoRestante = 200;
        private int documentos = 0;
        private int documentosEntregados = 0;
        private int mesaIndicada = 0;
        private Random generadorAleatorio = new Random();

        
        private DateTime ultimoToqueGenerador = DateTime.Now;
        private DateTime ultimoToqueMesa = DateTime.Now;

        public FormTrabajo()
        {
            InitializeComponent();

            
            this.DoubleBuffered = true;

            this.Load += FormTrabajo_Load;
            this.Paint += FormTrabajo_Paint;
        }

        private void FormTrabajo_Load(object sender, EventArgs e)
        {
            
            movimientoJugador = new FormMovimiento(this, pbPersonaje);

            
            movimientoJugador.ColisionConObjeto += MovimientoJugador_ColisionConObjeto;
            movimientoJugador.Start();

            
            timer1.Start();

            
            AsignarNuevaMesa();
            ActualizarMarcadores();
        }


        private void AsignarNuevaMesa()
        {

            mesaIndicada = generadorAleatorio.Next(1, 5);

            pbMesa1.BackColor = Color.Blue;
            pbMesa2.BackColor = Color.Blue;
            pbMesa3.BackColor = Color.Blue;
            pbMesa4.BackColor = Color.Blue;

            if (panel2 != null)
            {
                lblMesaDestino.Text = $"¡Lleva el documento a la MESA {mesaIndicada}!";
            }
        }


        private void MovimientoJugador_ColisionConObjeto(object sender, Control objetoTocado)
        {
           
            if (objetoTocado.Name == "pbGenerador")
            {
                
                if (documentos == 0)
                {
                    documentos = 1; 
                    ActualizarMarcadores();
                }
            }

           
            string nombreMesaCorrecta = "pbMesa" + mesaIndicada;

            if (objetoTocado.Name == nombreMesaCorrecta)
            {
                
                if (documentos == 1)
                {
                    documentos = 0; 
                    documentosEntregados++;

                    AsignarNuevaMesa(); 
                    ActualizarMarcadores();
                }
            }
        }

        
        private void ActualizarMarcadores()
        {
            if (lblTiempo != null) lblTiempo.Text = $"Tiempo: {tiempoRestante}s";
            if (lblDocumentos != null) lblDocumentos.Text = $"En mano: {documentos}";
            if (lblEntregados != null) lblEntregados.Text = $"Entregados: {documentosEntregados}";
        }

        
        private void FormTrabajo_Paint(object sender, PaintEventArgs e)
        {
            if (movimientoJugador != null)
            {
                movimientoJugador.DibujarPersonaje(e.Graphics);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tiempoRestante--;
            ActualizarMarcadores();

            
            if (tiempoRestante <= 0)
            {
                timer1.Stop();
                movimientoJugador.Stop();

                MessageBox.Show($"¡Se acabó el tiempo!\nLograste entregar {documentosEntregados} documentos.", "Fin del Turno");

                
                this.Close();
            }
        }
    }
}