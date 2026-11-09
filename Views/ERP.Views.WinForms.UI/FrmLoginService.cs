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

using ERP.Domain.DTO;
using ERP.Domain.Interfaces.Controller;
using ERP.Domain.Interfaces.Login;
using ERP.Controller.Login;

namespace ERP.Views.WinForms.UI
{
    public partial class FrmLoginService : Form
    {
        private readonly ILoginController _loginController;

        // El contenedor de .NET 9 inyectará el controlador aquí automáticamente al arrancar
        public FrmLoginService(ILoginController loginController)
        {
            InitializeComponent();
            _loginController = loginController ?? throw new ArgumentNullException(nameof(loginController));
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

        #region Buttons Style

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

        #endregion

        #region Buttons Actions

        // Acciones de los botones
        private void BtnMinimize_Click(object? sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnClose_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            // ¡Ya no usas el 'new'! Usas la instancia limpia guardada en el campo privado
            List<string> errores = _loginController.Login(new UserLoginQueryDto(this.txtUser.Text, this.txtPass.Text));

            if (errores.Count == 0)
            {
                // ¡Olé! No hay errores, el usuario es válido

                DialogResult resultado = MessageBox.Show("pues no hay errores", "...",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                // Si FrmMain también necesitara servicios, lo ideal sería pedirlo al contenedor, 
                // pero para arrancar, esto te vale perfectamente.
                //var mainForm = new FrmMain();
                //mainForm.Show();
            }
            else
            {
                // Concatenamos los errores y se los mostramos al usuario de golpe
                string mensajeError = string.Join(Environment.NewLine, errores);
                MessageBox.Show(mensajeError, "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

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
    }
}
