namespace AutoGestion.UI
{
    partial class AutorizarVenta
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
            btnAutorizar = new Button();
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
            label1.Size = new Size(155, 54);
            label1.TabIndex = 0;
            label1.Text = "Autorizar Venta";
            // 
            // dgvVentas
            // 
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.AllowUserToDeleteRows = false;
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentas.Location = new Point(3, 57);
            dgvVentas.MultiSelect = false;
            dgvVentas.Name = "dgvVentas";
            dgvVentas.ReadOnly = true;
            dgvVentas.RowHeadersVisible = false;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.Size = new Size(794, 225);
            dgvVentas.TabIndex = 1;
            // 
            // btnAutorizar
            // 
            btnAutorizar.Location = new Point(114, 307);
            btnAutorizar.Name = "btnAutorizar";
            btnAutorizar.Size = new Size(170, 42);
            btnAutorizar.TabIndex = 2;
            btnAutorizar.Text = "Autorizar Venta";
            btnAutorizar.UseVisualStyleBackColor = true;
            btnAutorizar.Click += btnAutorizar_Click;
            // 
            // btnRechazar
            // 
            btnRechazar.Location = new Point(307, 307);
            btnRechazar.Name = "btnRechazar";
            btnRechazar.Size = new Size(170, 42);
            btnRechazar.TabIndex = 3;
            btnRechazar.Text = "Rechazar Venta";
            btnRechazar.UseVisualStyleBackColor = true;
            btnRechazar.Click += btnRechazar_Click;
            // 
            // txtMotivoRechazo
            // 
            txtMotivoRechazo.Location = new Point(496, 307);
            txtMotivoRechazo.Multiline = true;
            txtMotivoRechazo.Name = "txtMotivoRechazo";
            txtMotivoRechazo.Size = new Size(287, 104);
            txtMotivoRechazo.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(495, 283);
            label2.Name = "label2";
            label2.Size = new Size(108, 15);
            label2.TabIndex = 5;
            label2.Text = "Motivo de Rechazo";
            // 
            // AutorizarVenta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(txtMotivoRechazo);
            Controls.Add(btnRechazar);
            Controls.Add(btnAutorizar);
            Controls.Add(dgvVentas);
            Controls.Add(label1);
            Name = "AutorizarVenta";
            Size = new Size(800, 426);
            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dgvVentas;
        private Button btnAutorizar;
        private Button btnRechazar;
        private TextBox txtMotivoRechazo;
        private Label label2;
    }
}
