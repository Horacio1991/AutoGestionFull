namespace AutoGestion.UI
{
    partial class RealizarEntrega
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
            btnConfirmarEntrega = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(161, 54);
            label1.TabIndex = 0;
            label1.Text = "Realizar Entrega";
            // 
            // dgvVentas
            // 
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentas.Location = new Point(3, 46);
            dgvVentas.Name = "dgvVentas";
            dgvVentas.Size = new Size(794, 278);
            dgvVentas.TabIndex = 1;
            // 
            // btnConfirmarEntrega
            // 
            btnConfirmarEntrega.Location = new Point(326, 348);
            btnConfirmarEntrega.Name = "btnConfirmarEntrega";
            btnConfirmarEntrega.Size = new Size(148, 45);
            btnConfirmarEntrega.TabIndex = 2;
            btnConfirmarEntrega.Text = "Confirmar Entrega";
            btnConfirmarEntrega.UseVisualStyleBackColor = true;
            // 
            // RealizarEntrega
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnConfirmarEntrega);
            Controls.Add(dgvVentas);
            Controls.Add(label1);
            Name = "RealizarEntrega";
            Size = new Size(800, 426);
            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dgvVentas;
        private Button btnConfirmarEntrega;
    }
}
