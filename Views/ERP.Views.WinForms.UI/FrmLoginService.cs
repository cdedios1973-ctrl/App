using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Text;
//using System.Threading.Tasks;
using ERP.View.Presenter.Interfaces;

namespace ERP.Views.WinForms.UI;

public partial class FrmLoginService : Form, ILoginView
{
    public event EventHandler LoginClick;
    public event EventHandler CerrarClick;
    public event EventHandler MinimizarClick;

    public string Usuario => txtUser.Text;

    public string Password => txtPass.Text;

    // El contenedor de .NET 9 inyectará el controlador aquí automáticamente al arrancar
    public FrmLoginService()
    {
        InitializeComponent();

        // Conectar los botones de barra de título que ya creaste en el Designer
        btnClose.Click += (s, e) => CerrarClick?.Invoke(this, EventArgs.Empty);
        btnMinimize.Click += (s, e) => MinimizarClick?.Invoke(this, EventArgs.Empty);
        btnAccept.Click += (s, e) => MinimizarClick?.Invoke(this, EventArgs.Empty);

        this.btnAccept.Click += BtnAccept_Click;
        this.btnMinimize.Click += BtnMinimize_Click;
        this.btnClose.Click += BtnClose_Click;
    }

    /*
    private void Old_FrmLoginService_Load(object sender, EventArgs e)
    {
        this.CenterToScreen();

        this.ChangeButtonStyleRounded(btnMinimize);
        this.ChangeButtonStyleRounded(btnClose);

        this.ApplyLinuxMinusButtonStyle(ref this.btnMinimize);
        this.ApplyLinuxCloseButtonStyle(ref this.btnClose);

    }
    */

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
    private void BtnMinimize_Click(object? sender, EventArgs e)
    {
        this.WindowState = FormWindowState.Minimized;
    }

    private void BtnClose_Click(object? sender, EventArgs e)
    {
        this.Close();
    }

    private void BtnAccept_Click(object? sender, EventArgs e)
    {

        LoginClick?.Invoke(this, EventArgs.Empty);

        /*

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
        */

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

    public void MostrarErrores(List<string> errores)
    {
        MessageBox.Show(string.Join("\n", errores), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public void CerrarConExito()
    {
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    public void MinimizarVentana() => this.WindowState = FormWindowState.Minimized;
    public void CerrarAplicacion() => this.Close();

    public void Login()
    {
        LoginClick?.Invoke(this, EventArgs.Empty);
    }
}
