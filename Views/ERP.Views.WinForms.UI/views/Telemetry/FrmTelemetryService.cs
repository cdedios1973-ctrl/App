using ERP.Domain.Events;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace ERP.Views.WinForms.UI.views.Telemetry
{
    public partial class FrmTelemetryService : Form
    {

        private readonly AppMessageHandler _messageHandler;
        private bool _instalacionCompletada = false;
        private bool _instalando = false;

        // Aquí le enchufas desde Program.cs la acción síncrona que quieras ejecutar al pulsar F2
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Action? AccionInstalacion { get; set; }


        public FrmTelemetryService(AppMessageHandler messageHandler)
        {
            InitializeComponent();

            this.KeyPreview = true;

            _messageHandler = messageHandler;
            // Suscripción al evento del dominio
            _messageHandler.UpdateStatus += OnAppStatusUpdated;
        }

        private void OnAppStatusUpdated(object? sender, EventArgs e)
        {
            if (e is not UpdateStatusEventArgs args) return;

            string hora = args.TimeReached.ToString("HH:mm:ss");
            EscribirLog($"[{hora}] {args.Sender}: {args.Message} [{args.Status}]");

            // Fuerza a Windows Forms a pintar la línea al instante en pantalla
            txtTelemetry.Refresh();

            if (args.Confirmation)
            {
                _instalacionCompletada = true;
                EscribirLog("");
                EscribirLog("Pulse [ENTER] para continuar o [F3] para salir...");
                txtTelemetry.Refresh();
            }
        }

        public void EscribirLog(string mensaje)
        {
            txtTelemetry.AppendText(mensaje + Environment.NewLine);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Al estar en ShowDialog(), asignar DialogResult cierra automáticamente el modal
            this.DialogResult = DialogResult.Cancel;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // F2: Lanzar instalación (solo si no se está ejecutando ni ha terminado ya)
            if (keyData == Keys.F2 && !_instalando && !_instalacionCompletada && AccionInstalacion != null)
            {
                _instalando = true;
                EscribirLog("");
                AccionInstalacion.Invoke();
                _instalando = false;
                return true;
            }

            // ENTER: Solo deja continuar si la instalación ya terminó
            if (keyData == Keys.Enter && _instalacionCompletada)
            {
                this.DialogResult = DialogResult.OK;
                return true;
            }

            // F3 o ESC: Abortar y salir
            if (keyData == Keys.F3 || keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _messageHandler.UpdateStatus -= OnAppStatusUpdated;
            base.OnFormClosed(e);
        }

    }
}
