namespace AppWF
{
    partial class frmMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbActions = new System.Windows.Forms.GroupBox();
            this.pnActions = new System.Windows.Forms.Panel();
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.gbActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbActions
            // 
            this.gbActions.Controls.Add(this.pnActions);
            this.gbActions.Location = new System.Drawing.Point(12, 448);
            this.gbActions.Name = "gbActions";
            this.gbActions.Size = new System.Drawing.Size(776, 60);
            this.gbActions.TabIndex = 0;
            this.gbActions.TabStop = false;
            this.gbActions.Text = "Actions";
            // 
            // pnActions
            // 
            this.pnActions.Location = new System.Drawing.Point(6, 20);
            this.pnActions.Name = "pnActions";
            this.pnActions.Size = new System.Drawing.Size(764, 34);
            this.pnActions.TabIndex = 0;
            // 
            // gbMain
            // 
            this.gbMain.Location = new System.Drawing.Point(12, 29);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(776, 413);
            this.gbMain.TabIndex = 1;
            this.gbMain.TabStop = false;
            this.gbMain.Text = "Main";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.gbMain);
            this.Controls.Add(this.gbActions);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.gbActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbActions;
        private System.Windows.Forms.GroupBox gbMain;
        private System.Windows.Forms.Panel pnActions;
    }
}

