using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
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
    // Variables para optimizar RAM cargando las imágenes solo una vez
    private Image imgFlavioHablando1;
    private Image imgFlavioHablando2;
    private Image imgFlavioHablando3;
    private Image imgFlavioTranquilo;
    private Image imgFlavioActual;
    public PictureBox pbPuertaSalida; // Nva puerta
    public FormDecanato(Jugador jugadorRecibido)
    {
        InitializeComponent();

        this.jugadorActual = jugadorRecibido;

        imgFlavioHablando1 = Properties.Resources.flavioHablando1;

        imgFlavioHablando1 = Properties.Resources.flavioHablando1;
        imgFlavioHablando2 = Properties.Resources.flavioHablando2;
        imgFlavioHablando3 = Properties.Resources.flavioHablando3;
        imgFlavioTranquilo = Properties.Resources.flavioTranquilo;
        imgFlavioActual = imgFlavioHablando2;
        // Ocultar el control y dibujarlo más eficientemente en OnPaint
        pictureBox1.Visible = false;
        timer1.Start();
        // Si pbPersonaje falta en el diseñador visual, lo inicializamos
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

        // Inicializar la puerta de salida
        pbPuertaSalida = new PictureBox();
        pbPuertaSalida.Name = "pbPuertaSalida";
        pbPuertaSalida.Size = new Size(50, 50); // Tamaño sugerido
                                                // Posicionar puerta en algún lugar visible o inferior (Ajustar Location a necesidad, ej: esquina inf-izq)
        pbPuertaSalida.Location = new Point(10, this.ClientSize.Height - 60);
        pbPuertaSalida.BackColor = Color.Brown; // Un color visible para distinguirla rápido
                                                // Atributos clave para que no parpadee si es transparente y un TAG o evento
        pbPuertaSalida.Tag = "puerta";
        this.Controls.Add(pbPuertaSalida);

        DoubleBuffered = true;
        motorMovimiento = new FormMovimiento(this, pbPersonaje);
        // Suscribir al evento de colision
        motorMovimiento.ColisionConObjeto += MotorMovimiento_ColisionConObjeto;

        motorMovimiento.Start();
        lienzo = new Bitmap(pbPizarra.Width, pbPizarra.Height);
        dibujante = Graphics.FromImage(lienzo);
        dibujante.Clear(Color.White);
        pbPizarra.Image = lienzo;
        pictureBox2.BorderStyle = BorderStyle.Fixed3D;
    }
    protected override void OnPaint(PaintEventArgs e)
    {

        e.Graphics.Clear(this.BackColor);

        base.OnPaint(e);

        e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
        // Dibujar a Flavio directamente
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
                if ((string)x.Tag != "muro" && x.Name.StartsWith("pictureBox"))
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
            // Evitamos que salte multiples veces seguidas
            motorMovimiento.Stop();
            this.Close();
        }
    }

    // Variables para animacion de telefono
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
                break;
            case 1:
                imgFlavioActual = imgFlavioHablando2;
                break;
            case 2:
                imgFlavioActual = imgFlavioHablando3;
                break;
            case 3:
                imgFlavioActual = imgFlavioTranquilo;
                break;
        }
        Invalidate(pictureBox1.Bounds); // Solo repintamos el área de Flavio
    }
    private void tmrRevisarZonas_Tick(object sender, EventArgs e)
    {

        if (pbPersonaje.Bounds.IntersectsWith(pbZonaActiva.Bounds))
        {
            if (panelInfo.Visible == false)
            {
                panelInfo.Visible = true;
                // 1. Pausamos el Timer que hace que Flavio se mueva
                timer1.Stop();
                imgFlavioActual = imgFlavioTranquilo;
                Invalidate(pictureBox1.Bounds);
            }
        }
        else
        {
            // Si sales de la zona y el panel estaba visible...
            if (panelInfo.Visible == true)
            {
                panelInfo.Visible = false;
                // 3. Volvemos a iniciar el Timer de Flavio para que continúe su animación normal
                timer1.Start();
            }
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
    // Aqui empiezan los marcadores de colores (aqui es el azul)
    private void pictureBox2_Click(object sender, EventArgs e)
    {
        marcador.Color = Color.RoyalBlue;
        ApagarBordes();
        pictureBox2.BorderStyle = BorderStyle.Fixed3D;
    }
    // Rojo
    private void pictureBox4_Click(object sender, EventArgs e)
    {
        marcador.Color = Color.Firebrick;
        ApagarBordes();
        pictureBox4.BorderStyle = BorderStyle.Fixed3D;
    }
    // Amarillo
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
            case 1:
                lblMensaje.Text = "Cambiate de carrera";
                break;
            case 2:
                lblMensaje.Text = "Báñate plis :)";
                break;
            case 3:
                lblMensaje.Text = "No te rindas eres increible";
                break;
            case 4:
                lblMensaje.Text = "Si te sientes mal,\nimaginate como se sentirán los de diseño";
                break;
            case 5:
                lblMensaje.Text = "Como dice una persona muy sabia: \nHay que comerse la hamburguesa por partes...";
                break;
            case 6:
                lblMensaje.Text = "Descansa un rato, tienes 1 semana sin dormir.\n Por Dios";
                break;
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
}

