namespace Vista.UserControls.Backup
{
    partial class UC_Restore
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            lstBackups = new ListBox();
            btnRestaurarSeleccionado = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(82, 54);
            label1.TabIndex = 0;
            label1.Text = "Restore";
            // 
            // lstBackups
            // 
            lstBackups.FormattingEnabled = true;
            lstBackups.ItemHeight = 15;
            lstBackups.Location = new Point(203, 32);
            lstBackups.Name = "lstBackups";
            lstBackups.Size = new Size(244, 274);
            lstBackups.TabIndex = 1;
            // 
            // btnRestaurarSeleccionado
            // 
            btnRestaurarSeleccionado.Location = new Point(262, 312);
            btnRestaurarSeleccionado.Name = "btnRestaurarSeleccionado";
            btnRestaurarSeleccionado.Size = new Size(117, 43);
            btnRestaurarSeleccionado.TabIndex = 2;
            btnRestaurarSeleccionado.Text = "Restaurar";
            btnRestaurarSeleccionado.UseVisualStyleBackColor = true;
            btnRestaurarSeleccionado.Click += btnRestaurarSeleccionado_Click;
            // 
            // UC_Restore
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnRestaurarSeleccionado);
            Controls.Add(lstBackups);
            Controls.Add(label1);
            Name = "UC_Restore";
            Size = new Size(800, 426);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListBox lstBackups;
        private Button btnRestaurarSeleccionado;
    }
}
