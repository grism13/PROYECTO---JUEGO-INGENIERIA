using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;
using WMPLib;

namespace JUEGO_INGENIERIA.Vistas;

using JUEGO_INGENIERIA.Modelos;

public partial class FormDecanato : Form
{
    private Jugador jugadorActual;
    Bitmap lienzo;
    Graphics dibujante;
    Pen marcador = new Pen(Color.RoyalBlue, 4);
    bool estaDibujando = false;
    Point puntoAnterior;
    private FormMovimiento motorMovimiento;
    Random generadorAleatorio = new Random();

    private Image imgFlavioHablando1;
    private Image imgFlavioHablando2;
    private Image imgFlavioHablando3;
    private Image imgFlavioTranquilo;
    private Image imgFlavioActual;

    private Image imgCesar1;
    private Image imgCesar2;
    private Image imgCesar3;
    private Image imgCesarActual;

    // NUEVA variable para que no te atrape infinitamente
    private bool menuAperturaBloqueada = false;

    // --- AUDIO ---
    private WindowsMediaPlayer reproductorMusica = new WindowsMediaPlayer();

    public FormDecanato(Jugador jugadorRecibido)
    {
        InitializeComponent();
        AplicarFuente();

        this.jugadorActual = jugadorRecibido;

        // --- INICIAR MÚSICA ---
        try
        {
            string rutaAudio = Path.Combine(Application.StartupPath, "Resources", "decanato_musica.mp3");

            // Trampa: Revisamos si el archivo existe realmente en esa ruta
            if (!System.IO.File.Exists(rutaAudio))
            {
                MessageBox.Show("¡Alerta! Steam no encuentra el archivo en esta ruta:\n" + rutaAudio);
                return; // Cortamos aquí para que no intente reproducir la nada
            }

            // Si pasa la trampa, el archivo sí existe. Intentamos reproducir.
            reproductorMusica.URL = rutaAudio;
            reproductorMusica.settings.setMode("loop", true);
            reproductorMusica.settings.volume = 20;
            reproductorMusica.controls.play();
        }
        catch (Exception ex)
        {
            MessageBox.Show("El archivo existe, pero WMPLib falló: " + ex.Message);
        }
        // --- OPTIMIZACIÓN EXTREMA DE FONDO (BYPASS STRETCH LAG) ---
        if (this.BackgroundImage != null)
        {
            this.BackgroundImageLayout = ImageLayout.None; // Apagamos el pesado recalculador estirado de Windows
            Bitmap fondoOptimizado = new Bitmap(this.ClientSize.Width, this.ClientSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            using (Graphics g = Graphics.FromImage(fondoOptimizado))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                // FormDecanato has specific bounds, let's make sure it matches the form's display size
                g.DrawImage(this.BackgroundImage, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
            }
            this.BackgroundImage = fondoOptimizado;
        }

        imgFlavioHablando1 = Properties.Resources.flavioHablando1;
        imgFlavioHablando2 = Properties.Resources.flavioHablando2;
        imgFlavioHablando3 = Properties.Resources.flavioHablando3;
        imgFlavioTranquilo = Properties.Resources.flavioTranquilo;
        imgFlavioActual = imgFlavioHablando2;
        
        imgCesar1 = Properties.Resources.cesar1;
        imgCesar2 = Properties.Resources.cesar2;
        imgCesar3 = Properties.Resources.cesar3;
        imgCesarActual = imgCesar1;

        pictureBox1.Visible = false;
        pictureBox16.Visible = false;
        timer1.Start();

        if (pbPersonaje == null)
        {
            pbPersonaje = new PictureBox();
            pbPersonaje.Name = "pbPersonaje";
            pbPersonaje.Size = new Size(88, 119);
            pbPersonaje.Location = new Point(100, 100);
            pbPersonaje.BackColor = Color.Transparent;
            pbPersonaje.Visible = false;
            this.Controls.Add(pbPersonaje);
        }

        // ESCONDER NATIVAMENTE LAS DECORACIONES PARA CORTAR EL "INVALIDATE COSTOSO"
        foreach (Control control in this.Controls)
        {
            if (control is PictureBox x && x != pictureBox1 && x != pbPersonaje && x != pbPizarra && x != pictureBox2 && x != pictureBox3 && x != pictureBox4 && x != pictureBox16)
            {
                if (x.Name.StartsWith("pictureBox"))
                {
                    // Guardamos una tag mental por si queremos saber si "debería" haber sido visible
                    x.Tag = x.Visible ? x.Tag : "oculto_intencional";
                    x.Visible = false;
                }
            }
        }

        DoubleBuffered = true;
        motorMovimiento = new FormMovimiento(this, pbPersonaje);
        motorMovimiento.ColisionConObjeto += MotorMovimiento_ColisionConObjeto;

        motorMovimiento.Start();
        lienzo = new Bitmap(pbPizarra.Width, pbPizarra.Height);
        dibujante = Graphics.FromImage(lienzo);
        dibujante.Clear(Color.White);
        pbPizarra.Image = lienzo;
        pictureBox2.BorderStyle = BorderStyle.Fixed3D;

        panelInfo.Visible = false;
        pbMensaje.Visible = false;

        // NUEVO: Permite salir del menú al presionar la flecha ABAJO, ESCAPE o S
        this.KeyDown += (s, ev) => {
            if (panelInfo.Visible && (ev.KeyCode == Keys.Down || ev.KeyCode == Keys.Escape || ev.KeyCode == Keys.S))
            {
                panelInfo.Visible = false;
                pbMensaje.Visible = false;
                menuAperturaBloqueada = true; // Activar el seguro! No abrir más hasta que se aleje
                NavegacionConsola.LimpiarFoco(this);
                if (motorMovimiento != null) motorMovimiento.EstaPausado = false;
                timer1.Start();
            }
        };

        this.FormClosing += (s, ev) => {
            if (reproductorMusica != null)
            {
                reproductorMusica.controls.stop();
            }
        };
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

        if (imgFlavioActual != null)
        {
            e.Graphics.DrawImage(imgFlavioActual, pictureBox1.Bounds);
        }
        if (imgCesarActual != null)
        {
            e.Graphics.DrawImage(imgCesarActual, pictureBox16.Bounds);
        }

        // Dividido en Frente y Fondo para Z-Index, como en Form1 puro
        List<PictureBox> capaFondo = new List<PictureBox>();
        List<PictureBox> capaFrente = new List<PictureBox>();

        foreach (Control control in this.Controls)
        {
            if (control is PictureBox x && x != pictureBox1 && x != pbPersonaje && x != pbPizarra && x != pictureBox16)
            {
                // Solo renderizamos si NO tiene la tag que indica que estaba oculto a propósito antes en el designer
                if ((string)x.Tag != "muro" && x.Name.StartsWith("pictureBox") && (string)x.Tag != "oculto_intencional")
                {
                    if (x.Image != null)
                    {
                        if (x.Bottom <= pbPersonaje.Bottom)
                            capaFondo.Add(x);
                        else
                            capaFrente.Add(x);
                    }
                }
            }
        }

        // Dibujar lo que va atrás
        foreach (var pFondo in capaFondo)
        {
            e.Graphics.DrawImage(pFondo.Image, pFondo.Left, pFondo.Top, pFondo.Width, pFondo.Height);
        }

        // Dibujar personaje
        if (motorMovimiento != null)
        {
            motorMovimiento.DibujarPersonaje(e.Graphics);
        }

        // Dibujar cosas frente
        foreach (var pFrente in capaFrente)
        {
            e.Graphics.DrawImage(pFrente.Image, pFrente.Left, pFrente.Top, pFrente.Width, pFrente.Height);
        }
    }

    private void MotorMovimiento_ColisionConObjeto(object sender, Control objetoColisionado)
    {
        if (objetoColisionado == pbPuertaSalida)
        {
            motorMovimiento.Stop();
            if (reproductorMusica != null) reproductorMusica.controls.stop();
            this.Close();
        }
    }

    private int pasoAnimacion = 0;
    private void timer1_Tick(object sender, EventArgs e)
    {
        pasoAnimacion++;
        if (pasoAnimacion > 3)
        {
            pasoAnimacion = 0;
        }
        switch (pasoAnimacion)
        {
            case 0: 
                imgFlavioActual = imgFlavioHablando1; 
                imgCesarActual = imgCesar1;
                break;
            case 1: 
                imgFlavioActual = imgFlavioHablando2; 
                imgCesarActual = imgCesar2;
                break;
            case 2: 
                imgFlavioActual = imgFlavioHablando3; 
                imgCesarActual = imgCesar3;
                break;
            case 3: 
                imgFlavioActual = imgFlavioTranquilo; 
                imgCesarActual = imgCesar2;
                break;
        }
        Invalidate(pictureBox1.Bounds);
        Invalidate(pictureBox16.Bounds);
    }

    private void tmrRevisarZonas_Tick(object sender, EventArgs e)
    {
        if (pbPersonaje.Bounds.IntersectsWith(pbZonaActiva.Bounds))
        {
            // Solo abrimos el menú si el Seguro NO está activado
            if (panelInfo.Visible == false && !menuAperturaBloqueada)
            {
                panelInfo.Visible = true;
                NavegacionConsola.Configurar(this, btnConsejo, btnOno, btnTrabajo);

                timer1.Stop();
                imgFlavioActual = imgFlavioTranquilo;
                Invalidate(pictureBox1.Bounds);

                // Pausar al personaje
                if (motorMovimiento != null)
                {
                    motorMovimiento.goArriba = false;
                    motorMovimiento.goAbajo = false;
                    motorMovimiento.goIzquierda = false;
                    motorMovimiento.goDerecha = false;
                    motorMovimiento.EstaPausado = true;
                }
            }
        }
        else
        {
            // El jugador salió de la zona por fin. Quitamos el seguro por si quiere volver a entrar luego.
            menuAperturaBloqueada = false;
        }
    }

    private void pbPizarra_MouseDown(object sender, MouseEventArgs e)
    {
        estaDibujando = true;
        puntoAnterior = e.Location;
    }
    private void pbPizarra_MouseMove(object sender, MouseEventArgs e)
    {
        if (estaDibujando)
        {
            dibujante.DrawLine(marcador, puntoAnterior, e.Location);
            puntoAnterior = e.Location;
            pbPizarra.Invalidate();
        }
    }
    private void pbPizarra_MouseUp(object sender, MouseEventArgs e)
    {
        estaDibujando = false;
    }
    private void pictureBox2_Click(object sender, EventArgs e)
    {
        marcador.Color = Color.RoyalBlue;
        ApagarBordes();
        pictureBox2.BorderStyle = BorderStyle.Fixed3D;
    }
    private void pictureBox4_Click(object sender, EventArgs e)
    {
        marcador.Color = Color.Firebrick;
        ApagarBordes();
        pictureBox4.BorderStyle = BorderStyle.Fixed3D;
    }
    private void pictureBox3_Click(object sender, EventArgs e)
    {
        marcador.Color = Color.Gold;
        ApagarBordes();
        pictureBox3.BorderStyle = BorderStyle.Fixed3D;
    }
    private void ApagarBordes()
    {
        pictureBox2.BorderStyle = BorderStyle.None;
        pictureBox4.BorderStyle = BorderStyle.None;
        pictureBox3.BorderStyle = BorderStyle.None;
    }
    private void x_Click(object sender, EventArgs e)
    {
        pbMensaje.Visible = false;
    }
    private void btnConsejo_Click(object sender, EventArgs e)
    {
        pbMensaje.Visible = true;
        int resultado = generadorAleatorio.Next(1, 7);
        switch (resultado)
        {
            case 1: lblMensaje.Text = "Cambiate de carrera"; break;
            case 2: lblMensaje.Text = "Báñate plis :)"; break;
            case 3: lblMensaje.Text = "No te rindas eres increible"; break;
            case 4: lblMensaje.Text = "Si te sientes mal,\nimaginate como se sentirán los de diseño"; break;
            case 5: lblMensaje.Text = "Como dice una persona muy sabia: \nHay que comerse la hamburguesa por partes..."; break;
            case 6: lblMensaje.Text = "Descansa un rato, tienes 1 semana sin dormir.\n Por Dios"; break;
        }
    }
    private void btnOno_Click(object sender, EventArgs e)
    {
        if (reproductorMusica != null) reproductorMusica.controls.pause(); // Pausar Decanato
        FormOno formOno = new FormOno();
        formOno.ShowDialog();
        if (reproductorMusica != null) reproductorMusica.controls.play(); // Reanudar Decanato al salir del ONO
    }
    private void btnTrabajo_Click(object sender, EventArgs e)
    {
        if (reproductorMusica != null) reproductorMusica.controls.pause(); // Pausar Decanato
        FormTrabajo trabajo = new FormTrabajo(jugadorActual);
        trabajo.ShowDialog();
        if (reproductorMusica != null) reproductorMusica.controls.play(); // Reanudar Decanato al salir del Trabajo
    }
    private void AplicarFuente()
    {
        try
        {
            string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf");
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(rutaFuente);

            Font fuenteMensaje = new Font(pfc.Families[0], 10f);
            Font fuenteBoton = new Font(pfc.Families[0], 8f);

            lblMensaje.Font = fuenteMensaje;
            btnConsejo.Font = fuenteBoton;
            btnOno.Font = fuenteBoton;
            btnTrabajo.Font = fuenteBoton;
        }
        catch { }
    }

    private void pbPizarra_Click(object sender, EventArgs e) { }
    private void label1_Click(object sender, EventArgs e) { }
    private void pictureBox5_Click(object sender, EventArgs e) { }
    private void pbPersonaje_Click(object sender, EventArgs e) { }
}
