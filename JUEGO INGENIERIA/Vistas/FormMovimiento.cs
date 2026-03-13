using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace JUEGO_INGENIERIA.Vistas
{
    public class FormMovimiento
    {
        private Form frm;
        public PictureBox pbPersonaje;
        private System.Windows.Forms.Timer tmrMovimiento;
        public bool goArriba, goAbajo, goIzquierda, goDerecha;
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);
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
        public bool esNivelEspacial = false;
        public FormMovimiento(Form frm, PictureBox pbPersonaje, bool esNivelEspacial = false)
        {
            this.frm = frm;
            this.pbPersonaje = pbPersonaje;
            this.esNivelEspacial = esNivelEspacial;
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
            Image CargarSpriteEspacial(string nombreExacto)
            {
                object obj = Properties.Resources.ResourceManager.GetObject(nombreExacto);
                if (obj == null)
                {
                    string fallback = nombreExacto.Replace(p, "gris");
                    obj = Properties.Resources.ResourceManager.GetObject(fallback);
                }
                return (Image)obj;
            }
            if (esNivelEspacial)
            {
                if (p == "gris")
                {
                    animAbajo.Add(CargarSpriteEspacial("gris-estrella-abajo1"));
                    animAbajo.Add(CargarSpriteEspacial("gris-estrella-abajo2"));
                    animAbajo.Add(CargarSpriteEspacial("gris-estrella-abajo3"));
                    animAbajo.Add(CargarSpriteEspacial("gris-estrella-abajo4"));
                    animArriba.Add(CargarSpriteEspacial("gris-estrella-arriba1"));
                    animArriba.Add(CargarSpriteEspacial("gris-estrella-arriba2"));
                    animIzquierda.Add(CargarSpriteEspacial("gris-estrella-izquierda1"));
                    animIzquierda.Add(CargarSpriteEspacial("gris-estrella-izquierda2"));
                    animIzquierda.Add(CargarSpriteEspacial("gris-estrella-izquierda3"));
                    animIzquierda.Add(CargarSpriteEspacial("gris-estrella-izquierda4"));
                    animDerecha.Add(CargarSpriteEspacial("gris-estrella-izquierda1"));
                    animDerecha.Add(CargarSpriteEspacial("gris-estrella-izquierda2"));
                    animDerecha.Add(CargarSpriteEspacial("gris-estrella-izquierda3"));
                    animDerecha.Add(CargarSpriteEspacial("gris-estrella-izquierda4"));
                    animArribaDerecha = animDerecha;
                    animArribaIzquierda = animIzquierda;
                    animAbajoDerecha = animDerecha;
                    animAbajoIzquierda = animIzquierda;
                }
                else
                {
                    string accionEspacial = p == "roand" ? "roand-pirula" : "eliezer-iguana";
                    List<Image> animFrames = new List<Image>();
                    animFrames.Add(CargarSpriteEspacial($"{accionEspacial}1"));
                    animFrames.Add(CargarSpriteEspacial($"{accionEspacial}2"));
                    animFrames.Add(CargarSpriteEspacial($"{accionEspacial}3"));
                    animFrames.Add(CargarSpriteEspacial($"{accionEspacial}4"));
                    animAbajo = animFrames;
                    animArriba = animFrames;
                    animDerecha = animFrames;
                    animIzquierda = animFrames;
                    animArribaDerecha = animFrames;
                    animArribaIzquierda = animFrames;
                    animAbajoDerecha = animFrames;
                    animAbajoIzquierda = animFrames;
                }
                pbPersonaje.Image = animDerecha.Count > 1 ? animDerecha[1] : null;
                ultimaAnimacion = animDerecha;
            }
            else
            {
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
        }
        // --- SE ELIMINARON LOS EVENTOS KEYDOWN/KEYUP EN FAVOR DE GETASYNCKEYSTATE ---
        private void Frm_KeyDown(object sender, KeyEventArgs e) { }
        private void Frm_KeyUp(object sender, KeyEventArgs e) { }
        private void TmrMovimiento_Tick(object sender, EventArgs e)
        {
            if (EstaPausado) return;
            // Uso de GetAsyncKeyState para evitar "Ghosting" de las flechas cuando hay muchas teclas pisadas (Space + Alt + Abajo)
            // Agregamos WASD como alternativa porque el hardware de muchos teclados bloquea físicamente la combinación (Alt + Space + Flecha Abajo)
            goArriba = (GetAsyncKeyState(Keys.Up) & 0x8000) != 0 || (GetAsyncKeyState(Keys.W) & 0x8000) != 0;
            goAbajo = (GetAsyncKeyState(Keys.Down) & 0x8000) != 0 || (GetAsyncKeyState(Keys.S) & 0x8000) != 0;
            goIzquierda = (GetAsyncKeyState(Keys.Left) & 0x8000) != 0 || (GetAsyncKeyState(Keys.A) & 0x8000) != 0;
            goDerecha = (GetAsyncKeyState(Keys.Right) & 0x8000) != 0 || (GetAsyncKeyState(Keys.D) & 0x8000) != 0;
            int xAnterior = pbPersonaje.Left;
            int yAnterior = pbPersonaje.Top;
            int vNormal = esNivelEspacial ? 15 : 5;
            int vDiag = esNivelEspacial ? 11 : 3;

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
                if (esNivelEspacial)
                {
                    // En el espacio siempre estamos "flotando", animamos el frame actual
                    Animar(ultimaAnimacion);
                }
                else
                {
                    frameActual = 0;
                    contadorLentitud = 10;
                }
            }
            if (!esNivelEspacial)
            {
                Rectangle areaAnterior = new Rectangle(xAnterior, yAnterior, pbPersonaje.Width, pbPersonaje.Height);
                Rectangle areaNueva = new Rectangle(pbPersonaje.Left, pbPersonaje.Top, pbPersonaje.Width, pbPersonaje.Height);
                frm.Invalidate(areaAnterior);
                frm.Invalidate(areaNueva);
            }
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
                // NUEVO SALVAVIDAS: Verificamos que el contenido de ese frame no esté vacío
                if (ultimaAnimacion[frameActual] != null)
                {
                    g.DrawImage(ultimaAnimacion[frameActual], pbPersonaje.Left, pbPersonaje.Top, pbPersonaje.Width, pbPersonaje.Height);
                }
            }
            else if (pbPersonaje.Image != null)
            {
                g.DrawImage(pbPersonaje.Image, pbPersonaje.Left, pbPersonaje.Top, pbPersonaje.Width, pbPersonaje.Height);
            }
        }
    }
}