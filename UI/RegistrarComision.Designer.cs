namespace AutoGestion.Vista
{
    partial class RegistrarComision
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
            dgvVentas = new DataGridView();
            label = new Label();
            txtComisionFinal = new TextBox();
            btnConfirmar = new Button();
            btnRechazar = new Button();
            txtMotivoRechazo = new TextBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(184, 54);
            label1.TabIndex = 0;
            label1.Text = "Registrar Comisión";
            // 
            // dgvVentas
            // 
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentas.Location = new Point(11, 40);
            dgvVentas.Name = "dgvVentas";
            dgvVentas.Size = new Size(774, 270);
            dgvVentas.TabIndex = 1;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(161, 368);
            label.Name = "label";
            label.Size = new Size(89, 15);
            label.TabIndex = 2;
            label.Text = "Comision Final:";
            // 
            // txtComisionFinal
            // 
            txtComisionFinal.Location = new Point(256, 360);
            txtComisionFinal.Name = "txtComisionFinal";
            txtComisionFinal.Size = new Size(138, 23);
            txtComisionFinal.TabIndex = 4;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(182, 319);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(146, 28);
            btnConfirmar.TabIndex = 6;
            btnConfirmar.Text = "Aprobar Comisión";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click_1;
            // 
            // btnRechazar
            // 
            btnRechazar.Location = new Point(478, 319);
            btnRechazar.Name = "btnRechazar";
            btnRechazar.Size = new Size(139, 28);
            btnRechazar.TabIndex = 7;
            btnRechazar.Text = "Rechazar Comisión";
            btnRechazar.UseVisualStyleBackColor = true;
            btnRechazar.Click += btnRechazar_Click;
            // 
            // txtMotivoRechazo
            // 
            txtMotivoRechazo.Location = new Point(424, 350);
            txtMotivoRechazo.Multiline = true;
            txtMotivoRechazo.Name = "txtMotivoRechazo";
            txtMotivoRechazo.Size = new Size(194, 70);
            txtMotivoRechazo.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(424, 332);
            label2.Name = "label2";
            label2.Size = new Size(48, 15);
            label2.TabIndex = 9;
            label2.Text = "Motivo:";
            // 
            // RegistrarComision
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(txtMotivoRechazo);
            Controls.Add(btnRechazar);
            Controls.Add(btnConfirmar);
            Controls.Add(txtComisionFinal);
            Controls.Add(label);
            Controls.Add(dgvVentas);
            Controls.Add(label1);
            Name = "RegistrarComision";
            Size = new Size(800, 426);
            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dgvVentas;
        private Label label;
        private Label label3;
        private TextBox txtComisionFinal;
        private ComboBox cmbEstado;
        private Button btnConfirmar;
        private Button btnRechazar;
        private TextBox txtMotivoRechazo;
        private Label label2;
    }
}
