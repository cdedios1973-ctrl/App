using System;
using System.Drawing;
using System.Drawing.Drawing2D;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppWF
{
    public partial class FrmLoginService : Form
    {
        public FrmLoginService()
        {
            InitializeComponent();
        }

        private void FrmLoginService_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            ChangeButtonStyleRounded(btnMinimize);
            ChangeButtonStyleRounded(btnClose);

            this.ApplyLinuxMinusButtonStyle(ref this.btnMinimize);
            this.ApplyLinuxCloseButtonStyle(ref this.btnClose);

            this.btnMinimize.Click += BtnMinimize_Click;
            this.btnClose.Click += BtnClose_Click;
        }

        // Método mágico para transformar cualquier botón cuadrado en un círculo
        private void ChangeButtonStyleRounded(Button boton)
        {
            GraphicsPath rutaClip = new GraphicsPath();
            // Dibuja una elipse que ocupa todo el ancho y alto del botón
            rutaClip.AddEllipse(0, 0, boton.Width, boton.Height);
            boton.Region = new Region(rutaClip);
        }

        // 2. Colores estilo Linux (Ubuntu / Fedora) -> Minimizar: Gris oscuro o naranja apagado
        private void ApplyLinuxMinusButtonStyle(ref Button btn)
        {
            btn.BackColor = Color.FromArgb(212, 212, 212);
            btn.ForeColor = Color.FromArgb(60, 60, 60);
        }

        // Cerrar: El clásico rojo/naranja de Linux
        private void ApplyLinuxCloseButtonStyle(ref Button btn)
        {
            btn.BackColor = Color.FromArgb(232, 17, 35);
            btn.ForeColor = Color.White;
        }

        // Acciones de los botones
        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLoginService_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel) return;

            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas salir de la aplicación?", "Confirmar cierre",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.No)
                e.Cancel = true;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {

        }
    }
}
