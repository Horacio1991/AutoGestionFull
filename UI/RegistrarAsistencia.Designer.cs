namespace AutoGestion.UI
{
    partial class RegistrarAsistencia
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
            label2 = new Label();
            dgvTurnos = new DataGridView();
            label3 = new Label();
            label4 = new Label();
            cmbEstado = new ComboBox();
            txtObservaciones = new TextBox();
            btnGuardar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvTurnos).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(191, 54);
            label1.TabIndex = 0;
            label1.Text = "Registrar Asistencia";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 72);
            label2.Name = "label2";
            label2.Size = new Size(134, 15);
            label2.TabIndex = 1;
            label2.Text = "Lista de turnos vencidos";
            // 
            // dgvTurnos
            // 
            dgvTurnos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTurnos.Location = new Point(17, 90);
            dgvTurnos.Name = "dgvTurnos";
            dgvTurnos.Size = new Size(411, 190);
            dgvTurnos.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(451, 90);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 3;
            label3.Text = "Estado:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(451, 120);
            label4.Name = "label4";
            label4.Size = new Size(87, 15);
            label4.TabIndex = 4;
            label4.Text = "Observaciones:";
            // 
            // cmbEstado
            // 
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(521, 82);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(175, 23);
            cmbEstado.TabIndex = 5;
            // 
            // txtObservaciones
            // 
            txtObservaciones.Location = new Point(451, 138);
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.Size = new Size(306, 72);
            txtObservaciones.TabIndex = 6;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(510, 240);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(175, 40);
            btnGuardar.TabIndex = 7;
            btnGuardar.Text = "Guardar Estado";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // RegistrarAsistencia
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnGuardar);
            Controls.Add(txtObservaciones);
            Controls.Add(cmbEstado);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(dgvTurnos);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "RegistrarAsistencia";
            Size = new Size(800, 426);
            ((System.ComponentModel.ISupportInitialize)dgvTurnos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private DataGridView dgvTurnos;
        private Label label3;
        private Label label4;
        private ComboBox cmbEstado;
        private TextBox txtObservaciones;
        private Button btnGuardar;
    }
}
