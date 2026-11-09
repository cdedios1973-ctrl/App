namespace ERP.Views.WinForms.UI
{
    partial class FrmLoginService
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
            gbLogin = new GroupBox();
            txtPass = new TextBox();
            txtUser = new TextBox();
            lblPassword = new Label();
            lblUser = new Label();
            gbActions = new GroupBox();
            btnAccept = new Button();
            pnlTitle = new Panel();
            btnClose = new Button();
            btnMinimize = new Button();
            gbLogin.SuspendLayout();
            gbActions.SuspendLayout();
            pnlTitle.SuspendLayout();
            SuspendLayout();
            // 
            // gbLogin
            // 
            gbLogin.Controls.Add(txtPass);
            gbLogin.Controls.Add(txtUser);
            gbLogin.Controls.Add(lblPassword);
            gbLogin.Controls.Add(lblUser);
            gbLogin.Font = new Font("DejaVu Sans Condensed", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbLogin.Location = new Point(14, 59);
            gbLogin.Margin = new Padding(4, 3, 4, 3);
            gbLogin.Name = "gbLogin";
            gbLogin.Padding = new Padding(4, 3, 4, 3);
            gbLogin.Size = new Size(370, 110);
            gbLogin.TabIndex = 0;
            gbLogin.TabStop = false;
            gbLogin.Text = "Introduzca Usuario y clave";
            // 
            // txtPass
            // 
            txtPass.Font = new Font("DejaVu Sans Condensed", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPass.Location = new Point(189, 67);
            txtPass.Margin = new Padding(4, 3, 4, 3);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(173, 25);
            txtPass.TabIndex = 3;
            // 
            // txtUser
            // 
            txtUser.Font = new Font("DejaVu Sans Condensed", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUser.Location = new Point(189, 30);
            txtUser.Margin = new Padding(4, 3, 4, 3);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(173, 25);
            txtUser.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("DejaVu Sans Condensed", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPassword.Location = new Point(21, 70);
            lblPassword.Margin = new Padding(4, 0, 4, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(120, 18);
            lblPassword.TabIndex = 1;
            lblPassword.Text = "Clave de acceso:";
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new Font("DejaVu Sans Condensed", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUser.Location = new Point(21, 38);
            lblUser.Margin = new Padding(4, 0, 4, 0);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(65, 18);
            lblUser.TabIndex = 0;
            lblUser.Text = "Usuario:";
            // 
            // gbActions
            // 
            gbActions.Controls.Add(btnAccept);
            gbActions.Font = new Font("DejaVu Sans Condensed", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbActions.Location = new Point(14, 175);
            gbActions.Margin = new Padding(4, 3, 4, 3);
            gbActions.Name = "gbActions";
            gbActions.Padding = new Padding(4, 3, 4, 3);
            gbActions.Size = new Size(370, 62);
            gbActions.TabIndex = 1;
            gbActions.TabStop = false;
            gbActions.Text = "Acciones";
            // 
            // btnAccept
            // 
            btnAccept.BackColor = Color.FromArgb(53, 132, 228);
            btnAccept.Font = new Font("DejaVu Sans Condensed", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAccept.Location = new Point(236, 22);
            btnAccept.Margin = new Padding(4, 3, 4, 3);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new Size(127, 33);
            btnAccept.TabIndex = 0;
            btnAccept.Text = "Continuar";
            btnAccept.UseVisualStyleBackColor = false;
            btnAccept.Click += btnAccept_Click;
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.FromArgb(45, 45, 45);
            pnlTitle.Controls.Add(btnClose);
            pnlTitle.Controls.Add(btnMinimize);
            pnlTitle.Location = new Point(14, 14);
            pnlTitle.Margin = new Padding(4, 3, 4, 3);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Size = new Size(370, 38);
            pnlTitle.TabIndex = 2;
            // 
            // btnClose
            // 
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Marlett", 9F, FontStyle.Bold, GraphicsUnit.Point, 2);
            btnClose.Location = new Point(343, 3);
            btnClose.Margin = new Padding(4, 3, 4, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(23, 23);
            btnClose.TabIndex = 4;
            btnClose.Text = "r";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // btnMinimize
            // 
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.Font = new Font("Marlett", 9F, FontStyle.Bold, GraphicsUnit.Point, 2);
            btnMinimize.Location = new Point(313, 3);
            btnMinimize.Margin = new Padding(4, 3, 4, 3);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(23, 23);
            btnMinimize.TabIndex = 3;
            btnMinimize.Text = "0";
            btnMinimize.UseVisualStyleBackColor = true;
            // 
            // FrmLoginService
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(246, 246, 246);
            ClientSize = new Size(397, 248);
            Controls.Add(pnlTitle);
            Controls.Add(gbActions);
            Controls.Add(gbLogin);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmLoginService";
            Text = "Servicio de Acceso a la Aplicación";
            FormClosing += FrmLoginService_FormClosing;
            Load += FrmLoginService_Load;
            gbLogin.ResumeLayout(false);
            gbLogin.PerformLayout();
            gbActions.ResumeLayout(false);
            pnlTitle.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLogin;
        private System.Windows.Forms.GroupBox gbActions;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnClose;
    }
}