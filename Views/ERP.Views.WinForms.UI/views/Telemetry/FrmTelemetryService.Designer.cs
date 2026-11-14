namespace ERP.Views.WinForms.UI.views.Telemetry
{
    partial class FrmTelemetryService
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlTitle = new Panel();
            lblTitle = new Label();
            btnClose = new Button();
            txtTelemetry = new TextBox();
            pnlMainContainer = new Panel();
            pnlTextBoxContainer = new Panel();
            pnlTitle.SuspendLayout();
            pnlMainContainer.SuspendLayout();
            pnlTextBoxContainer.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.FromArgb(45, 45, 45);
            pnlTitle.Controls.Add(lblTitle);
            pnlTitle.Controls.Add(btnClose);
            pnlTitle.Dock = DockStyle.Top;
            pnlTitle.Location = new Point(0, 0);
            pnlTitle.Margin = new Padding(4, 3, 4, 3);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Size = new Size(566, 26);
            pnlTitle.TabIndex = 3;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Aptos Mono", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(5, 4);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(171, 19);
            lblTitle.TabIndex = 5;
            lblTitle.Text = "MONITOR DE SISTEMA";
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Marlett", 9F, FontStyle.Bold, GraphicsUnit.Point, 2);
            btnClose.Location = new Point(540, 3);
            btnClose.Margin = new Padding(4, 3, 4, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(23, 23);
            btnClose.TabIndex = 4;
            btnClose.Text = "r";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // txtTelemetry
            // 
            txtTelemetry.BackColor = Color.FromArgb(0, 0, 170);
            txtTelemetry.BorderStyle = BorderStyle.None;
            txtTelemetry.Dock = DockStyle.Fill;
            txtTelemetry.Font = new Font("Lucida Console", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTelemetry.ForeColor = Color.FromArgb(170, 170, 170);
            txtTelemetry.Location = new Point(8, 8);
            txtTelemetry.Multiline = true;
            txtTelemetry.Name = "txtTelemetry";
            txtTelemetry.ReadOnly = true;
            txtTelemetry.ScrollBars = ScrollBars.Vertical;
            txtTelemetry.Size = new Size(550, 226);
            txtTelemetry.TabIndex = 4;
            txtTelemetry.TabStop = false;
            txtTelemetry.Text = "C:>";
            txtTelemetry.WordWrap = false;
            // 
            // pnlMainContainer
            // 
            pnlMainContainer.BackColor = Color.Black;
            pnlMainContainer.BorderStyle = BorderStyle.FixedSingle;
            pnlMainContainer.Controls.Add(pnlTextBoxContainer);
            pnlMainContainer.Controls.Add(pnlTitle);
            pnlMainContainer.Location = new Point(6, 4);
            pnlMainContainer.Name = "pnlMainContainer";
            pnlMainContainer.Size = new Size(568, 270);
            pnlMainContainer.TabIndex = 5;
            // 
            // pnlTextBoxContainer
            // 
            pnlTextBoxContainer.BackColor = Color.FromArgb(0, 0, 170);
            pnlTextBoxContainer.Controls.Add(txtTelemetry);
            pnlTextBoxContainer.Dock = DockStyle.Fill;
            pnlTextBoxContainer.Location = new Point(0, 26);
            pnlTextBoxContainer.Name = "pnlTextBoxContainer";
            pnlTextBoxContainer.Padding = new Padding(8);
            pnlTextBoxContainer.Size = new Size(566, 242);
            pnlTextBoxContainer.TabIndex = 6;
            // 
            // FrmTelemetryService
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(580, 280);
            ControlBox = false;
            Controls.Add(pnlMainContainer);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmTelemetryService";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmTelemetryService";
            TopMost = true;
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            pnlMainContainer.ResumeLayout(false);
            pnlTextBoxContainer.ResumeLayout(false);
            pnlTextBoxContainer.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTitle;
        private Button btnClose;
        private TextBox txtTelemetry;
        private Panel pnlMainContainer;
        private Label lblTitle;
        private Panel pnlTextBoxContainer;
    }
}