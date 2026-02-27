using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace JUEGO_INGENIERIA.Vistas
{
    public class FormMovimiento
    {
        private Form frm;
        public PictureBox pbPersonaje;
        private System.Windows.Forms.Timer tmrMovimiento;

        public bool goArriba, goAbajo, goIzquierda, goDerecha;

        List<Image> animAbajo = new List<Image>();
        List<Image> animArriba = new List<Image>();
        List<Image> animIzquierda = new List<Image>();
        List<Image> animDerecha = new List<Image>();

        List<Image> animArribaDerecha = new List<Image>();
        List<Image> animArribaIzquierda = new List<Image>();
        List<Image> animAbajoDerecha = new List<Image>();
        List<Image> animAbajoIzquierda = new List<Image>();

        int frameActual = 0;
        int contadorLentitud = 0;
        List<Image> ultimaAnimacion = null;

        public event EventHandler<Control> ColisionConObjeto;
        public bool EstaPausado { get; set; } = false;

        public FormMovimiento(Form frm, PictureBox pbPersonaje)
        {
            this.frm = frm;
            this.pbPersonaje = pbPersonaje;
            this.pbPersonaje.Visible = false; // Ocultamos para double buffer, se dibuja manual

            // Configurar double buffering en el Formulario si no lo está
            //frm.DoubleBuffered = true;

            CargarSpritesPersonaje();

            tmrMovimiento = new System.Windows.Forms.Timer();
            tmrMovimiento.Interval = 20; // Aproximadamente 50 FPS
            tmrMovimiento.Tick += TmrMovimiento_Tick;

            frm.KeyDown += Frm_KeyDown;
            frm.KeyUp += Frm_KeyUp;
        }

        public void Start()
        {
            tmrMovimiento.Start();
        }

        public void Stop()
        {
            tmrMovimiento.Stop();
            goArriba = goAbajo = goIzquierda = goDerecha = false;
        }

        private void CargarSpritesPersonaje()
        {
            string p = "gris"; // Valor por defecto
            if (!string.IsNullOrEmpty(DatosJuego.PersonajeElegido))
            {
                p = DatosJuego.PersonajeElegido.ToLower();
            }

            Image CargarSprite(string accion)
            {
                object obj = Properties.Resources.ResourceManager.GetObject($"{p}_{accion}");
                if (obj == null)
                {
                    obj = Properties.Resources.ResourceManager.GetObject($"gris_{accion}");
                }
                return (Image)obj;
            }

            animAbajo.Add(CargarSprite("frente1"));
            animAbajo.Add(CargarSprite("frente2"));
            animAbajo.Add(CargarSprite("frente3"));

            animArriba.Add(CargarSprite("espalda1"));
            animArriba.Add(CargarSprite("espalda2"));
            animArriba.Add(CargarSprite("espalda3"));

            animDerecha.Add(CargarSprite("ladoderecho1"));
            animDerecha.Add(CargarSprite("ladoderecho2"));
            animDerecha.Add(CargarSprite("ladoderecho3"));

            animIzquierda.Add(CargarSprite("ladoizquiedo1"));
            animIzquierda.Add(CargarSprite("ladoizquiedo2"));
            animIzquierda.Add(CargarSprite("ladoizquiedo3"));

            animArribaDerecha.Add(CargarSprite("inclinadaderechaespalda1"));
            animArribaDerecha.Add(CargarSprite("inclinadaderechaespalda2"));
            animArribaDerecha.Add(CargarSprite("inclinadaderechaespalda3"));

            animArribaIzquierda.Add(CargarSprite("inclinadaizquiedaespalda1"));
            animArribaIzquierda.Add(CargarSprite("inclinadaizquiedaespalda2"));
            animArribaIzquierda.Add(CargarSprite("inclinadaizquiedaespalda3"));

            animAbajoDerecha.Add(CargarSprite("inclinadaderechafrente1"));
            animAbajoDerecha.Add(CargarSprite("inclinadaderechafrente2"));
            animAbajoDerecha.Add(CargarSprite("inclinadaderechafrente3"));

            animAbajoIzquierda.Add(CargarSprite("inclinadaizquiedafrente1"));
            animAbajoIzquierda.Add(CargarSprite("inclinadaizquiedafrente2"));
            animAbajoIzquierda.Add(CargarSprite("inclinadaizquiedafrente3"));

            pbPersonaje.Image = CargarSprite("frente2");
            ultimaAnimacion = animAbajo;
        }

        private void Frm_KeyDown(object sender, KeyEventArgs e)
        {
            if (EstaPausado) return;

            if (e.KeyCode == Keys.Up) goArriba = true;
            if (e.KeyCode == Keys.Down) goAbajo = true;
            if (e.KeyCode == Keys.Left) goIzquierda = true;
            if (e.KeyCode == Keys.Right) goDerecha = true;
        }

        private void Frm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) goArriba = false;
            if (e.KeyCode == Keys.Down) goAbajo = false;
            if (e.KeyCode == Keys.Left) goIzquierda = false;
            if (e.KeyCode == Keys.Right) goDerecha = false;
        }

        private void TmrMovimiento_Tick(object sender, EventArgs e)
        {
            if (EstaPausado) return;

            int xAnterior = pbPersonaje.Left;
            int yAnterior = pbPersonaje.Top;
            int vNormal = 5;
            int vDiag = 3;

            bool seMueve = false;

            if (goArriba && goDerecha)
            {
                if (pbPersonaje.Top > 0 && pbPersonaje.Left + pbPersonaje.Width < frm.ClientSize.Width)
                {
                    pbPersonaje.Top -= vDiag; pbPersonaje.Left += vDiag;
                    Animar(animArribaDerecha); seMueve = true;
                }
            }
            else if (goArriba && goIzquierda)
            {
                if (pbPersonaje.Top > 0 && pbPersonaje.Left > 0)
                {
                    pbPersonaje.Top -= vDiag; pbPersonaje.Left -= vDiag;
                    Animar(animArribaIzquierda); seMueve = true;
                }
            }
            else if (goAbajo && goDerecha)
            {
                if (pbPersonaje.Top + pbPersonaje.Height < frm.ClientSize.Height && pbPersonaje.Left + pbPersonaje.Width < frm.ClientSize.Width)
                {
                    pbPersonaje.Top += vDiag; pbPersonaje.Left += vDiag;
                    Animar(animAbajoDerecha); seMueve = true;
                }
            }
            else if (goAbajo && goIzquierda)
            {
                if (pbPersonaje.Top + pbPersonaje.Height < frm.ClientSize.Height && pbPersonaje.Left > 0)
                {
                    pbPersonaje.Top += vDiag; pbPersonaje.Left -= vDiag;
                    Animar(animAbajoIzquierda); seMueve = true;
                }
            }
            else if (goArriba)
            {
                if (pbPersonaje.Top > 0) { pbPersonaje.Top -= vNormal; Animar(animArriba); seMueve = true; }
            }
            else if (goAbajo)
            {
                if (pbPersonaje.Top + pbPersonaje.Height < frm.ClientSize.Height) { pbPersonaje.Top += vNormal; Animar(animAbajo); seMueve = true; }
            }
            else if (goIzquierda)
            {
                if (pbPersonaje.Left > 0) { pbPersonaje.Left -= vNormal; Animar(animIzquierda); seMueve = true; }
            }
            else if (goDerecha)
            {
                if (pbPersonaje.Left + pbPersonaje.Width < frm.ClientSize.Width) { pbPersonaje.Left += vNormal; Animar(animDerecha); seMueve = true; }
            }

            foreach (Control x in frm.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "muro")
                {
                    if (pbPersonaje.Bounds.IntersectsWith(x.Bounds))
                    {
                        pbPersonaje.Left = xAnterior;
                        pbPersonaje.Top = yAnterior;
                    }
                }
                else if (x is PictureBox && (string)x.Tag != "muro")
                {
                    if (pbPersonaje.Bounds.IntersectsWith(x.Bounds))
                    {
                        ColisionConObjeto?.Invoke(this, x);
                    }
                }
            }

            if (seMueve == false)
            {
                frameActual = 0;
                contadorLentitud = 10;
            }

            Rectangle areaAnterior = new Rectangle(xAnterior, yAnterior, pbPersonaje.Width, pbPersonaje.Height);
            Rectangle areaNueva = new Rectangle(pbPersonaje.Left, pbPersonaje.Top, pbPersonaje.Width, pbPersonaje.Height);

            frm.Invalidate(areaAnterior);
            frm.Invalidate(areaNueva);
        }

        private void Animar(List<Image> animacionNueva)
        {
            if (animacionNueva != ultimaAnimacion)
            {
                frameActual = 0;
                contadorLentitud = 10;
                ultimaAnimacion = animacionNueva;
            }

            if (animacionNueva.Count > 0)
            {
                contadorLentitud++;
                if (contadorLentitud > 3)
                {
                    frameActual++;
                    if (frameActual >= animacionNueva.Count) frameActual = 0;
                    contadorLentitud = 0;
                }
            }
        }

        public void DibujarPersonaje(Graphics g)
        {
            if (ultimaAnimacion != null && ultimaAnimacion.Count > 0 && frameActual < ultimaAnimacion.Count)
            {
                g.DrawImage(ultimaAnimacion[frameActual], pbPersonaje.Left, pbPersonaje.Top, pbPersonaje.Width, pbPersonaje.Height);
            }
            else if (pbPersonaje.Image != null)
            {
                g.DrawImage(pbPersonaje.Image, pbPersonaje.Left, pbPersonaje.Top, pbPersonaje.Width, pbPersonaje.Height);
            }
        }


    }
}
