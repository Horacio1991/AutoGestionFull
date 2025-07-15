namespace Vista.UserControls.Backup
{
    partial class UC_Backup
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
            dgvBackup = new DataGridView();
            btnBackup = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvBackup).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(10, 9);
            label1.Name = "label1";
            label1.Size = new Size(79, 54);
            label1.TabIndex = 0;
            label1.Text = "Backup";
            // 
            // dgvBackup
            // 
            dgvBackup.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBackup.Location = new Point(435, 58);
            dgvBackup.Name = "dgvBackup";
            dgvBackup.Size = new Size(427, 418);
            dgvBackup.TabIndex = 1;
            // 
            // btnBackup
            // 
            btnBackup.Location = new Point(571, 517);
            btnBackup.Name = "btnBackup";
            btnBackup.Size = new Size(161, 62);
            btnBackup.TabIndex = 2;
            btnBackup.Text = "Realizar Backup";
            btnBackup.UseVisualStyleBackColor = true;
            // 
            // UC_Backup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnBackup);
            Controls.Add(dgvBackup);
            Controls.Add(label1);
            Name = "UC_Backup";
            Size = new Size(1280, 720);
            ((System.ComponentModel.ISupportInitialize)dgvBackup).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dgvBackup;
        private Button btnBackup;
    }
}
