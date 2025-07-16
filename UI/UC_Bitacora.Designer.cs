namespace Vista.UserControls.Backup
{
    partial class UC_Bitacora
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
            dgvBitacora = new DataGridView();
            label1 = new Label();
            rbTodos = new RadioButton();
            rbSoloBackups = new RadioButton();
            rbSoloRestores = new RadioButton();
            btnRecargarBitacora = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).BeginInit();
            SuspendLayout();
            // 
            // dgvBitacora
            // 
            dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBitacora.Location = new Point(95, 52);
            dgvBitacora.Name = "dgvBitacora";
            dgvBitacora.Size = new Size(372, 306);
            dgvBitacora.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(86, 54);
            label1.TabIndex = 1;
            label1.Text = "Bitácora";
            // 
            // rbTodos
            // 
            rbTodos.AutoSize = true;
            rbTodos.Location = new Point(501, 87);
            rbTodos.Name = "rbTodos";
            rbTodos.Size = new Size(83, 19);
            rbTodos.TabIndex = 2;
            rbTodos.TabStop = true;
            rbTodos.Text = "Listar Todo";
            rbTodos.UseVisualStyleBackColor = true;
            // 
            // rbSoloBackups
            // 
            rbSoloBackups.AutoSize = true;
            rbSoloBackups.Location = new Point(501, 121);
            rbSoloBackups.Name = "rbSoloBackups";
            rbSoloBackups.Size = new Size(95, 19);
            rbSoloBackups.TabIndex = 3;
            rbSoloBackups.TabStop = true;
            rbSoloBackups.Text = "Listar Backup";
            rbSoloBackups.UseVisualStyleBackColor = true;
            // 
            // rbSoloRestores
            // 
            rbSoloRestores.AutoSize = true;
            rbSoloRestores.Location = new Point(501, 156);
            rbSoloRestores.Name = "rbSoloRestores";
            rbSoloRestores.Size = new Size(100, 19);
            rbSoloRestores.TabIndex = 4;
            rbSoloRestores.TabStop = true;
            rbSoloRestores.Text = "Listar Restores";
            rbSoloRestores.UseVisualStyleBackColor = true;
            // 
            // btnRecargarBitacora
            // 
            btnRecargarBitacora.Location = new Point(502, 202);
            btnRecargarBitacora.Name = "btnRecargarBitacora";
            btnRecargarBitacora.Size = new Size(94, 46);
            btnRecargarBitacora.TabIndex = 5;
            btnRecargarBitacora.Text = "Recargar Bitácora";
            btnRecargarBitacora.UseVisualStyleBackColor = true;
            // 
            // UC_Bitacora
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnRecargarBitacora);
            Controls.Add(rbSoloRestores);
            Controls.Add(rbSoloBackups);
            Controls.Add(rbTodos);
            Controls.Add(label1);
            Controls.Add(dgvBitacora);
            Name = "UC_Bitacora";
            Size = new Size(800, 426);
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvBitacora;
        private Label label1;
        private RadioButton rbTodos;
        private RadioButton rbSoloBackups;
        private RadioButton rbSoloRestores;
        private Button btnRecargarBitacora;
    }
}
