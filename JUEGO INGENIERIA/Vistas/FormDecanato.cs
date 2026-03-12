using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;

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

    // NUEVA variable para que no te atrape infinitamente
    private bool menuAperturaBloqueada = false;

    public FormDecanato(Jugador jugadorRecibido)
    {
        InitializeComponent();
        AplicarFuente();

        this.jugadorActual = jugadorRecibido;

        imgFlavioHablando1 = Properties.Resources.flavioHablando1;
        imgFlavioHablando2 = Properties.Resources.flavioHablando2;
        imgFlavioHablando3 = Properties.Resources.flavioHablando3;
        imgFlavioTranquilo = Properties.Resources.flavioTranquilo;
        imgFlavioActual = imgFlavioHablando2;

        pictureBox1.Visible = false;
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
        if (motorMovimiento != null)
        {
            motorMovimiento.DibujarPersonaje(e.Graphics);
        }
        foreach (Control control in this.Controls)
        {
            if (control is PictureBox x && x != pictureBox1)
            {
                if (x.Visible && (string)x.Tag != "muro" && x.Name.StartsWith("pictureBox"))
                {
                    if (x.Image != null && pbPersonaje.Bounds.IntersectsWith(x.Bounds))
                    {
                        e.Graphics.DrawImage(x.Image, x.Left, x.Top, x.Width, x.Height);
                    }
                }
            }
        }
    }

    private void MotorMovimiento_ColisionConObjeto(object sender, Control objetoColisionado)
    {
        if (objetoColisionado == pbPuertaSalida)
        {
            motorMovimiento.Stop();
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
            case 0: imgFlavioActual = imgFlavioHablando1; break;
            case 1: imgFlavioActual = imgFlavioHablando2; break;
            case 2: imgFlavioActual = imgFlavioHablando3; break;
            case 3: imgFlavioActual = imgFlavioTranquilo; break;
        }
        Invalidate(pictureBox1.Bounds);
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
        FormOno formOno = new FormOno();
        formOno.ShowDialog();
    }
    private void btnTrabajo_Click(object sender, EventArgs e)
    {
        FormTrabajo trabajo = new FormTrabajo(jugadorActual);
        trabajo.ShowDialog();
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
