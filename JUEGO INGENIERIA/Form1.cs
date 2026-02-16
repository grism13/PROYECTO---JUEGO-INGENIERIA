using JUEGO_INGENIERIA.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JUEGO_INGENIERIA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //Aqui se oculta inicio y abre FormNarrativa.cs
            this.Hide();
            FormIntro intro = new FormIntro();
            intro.ShowDialog();

            //Aqui deberia comenzar el registro del usuario :( Tercera evaluacion de c# y sigo odiandolo
            FormAdmision registro = new FormAdmision();
            registro.ShowDialog();

            this.Show();
        }
    }
}
