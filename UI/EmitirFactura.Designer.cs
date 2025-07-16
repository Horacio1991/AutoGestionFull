namespace AutoGestion.UI
{
    partial class EmitirFactura
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
            btnEmitir = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(141, 54);
            label1.TabIndex = 0;
            label1.Text = "Emitir Factura";
            // 
            // dgvVentas
            // 
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentas.Location = new Point(3, 46);
            dgvVentas.Name = "dgvVentas";
            dgvVentas.Size = new Size(794, 238);
            dgvVentas.TabIndex = 1;
            // 
            // btnEmitir
            // 
            btnEmitir.Location = new Point(319, 317);
            btnEmitir.Name = "btnEmitir";
            btnEmitir.Size = new Size(149, 47);
            btnEmitir.TabIndex = 2;
            btnEmitir.Text = "Emitir Factura";
            btnEmitir.UseVisualStyleBackColor = true;
            btnEmitir.Click += btnEmitir_Click_1;
            // 
            // EmitirFactura
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnEmitir);
            Controls.Add(dgvVentas);
            Controls.Add(label1);
            Name = "EmitirFactura";
            Size = new Size(800, 426);
            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dgvVentas;
        private Button btnEmitir;
    }
}
