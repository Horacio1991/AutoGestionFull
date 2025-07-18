namespace AutoGestion.UI
{
    partial class ConsultarComisiones
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
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtVendedor = new TextBox();
            dtpDesde = new DateTimePicker();
            dtpHasta = new DateTimePicker();
            cmbEstado = new ComboBox();
            dgvComisiones = new DataGridView();
            btnDetalle = new Button();
            btnFiltrar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvComisiones).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(207, 54);
            label1.TabIndex = 0;
            label1.Text = "Consultar Comisiones";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 56);
            label2.Name = "label2";
            label2.Size = new Size(107, 15);
            label2.TabIndex = 1;
            label2.Text = "Nombre Vendedor:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 88);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 2;
            label3.Text = "Fecha desde:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(312, 48);
            label4.Name = "label4";
            label4.Size = new Size(99, 15);
            label4.TabIndex = 3;
            label4.Text = "Filtrar por estado:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(312, 88);
            label5.Name = "label5";
            label5.Size = new Size(72, 15);
            label5.TabIndex = 4;
            label5.Text = "Fecha hasta:";
            // 
            // txtVendedor
            // 
            txtVendedor.Location = new Point(126, 48);
            txtVendedor.Name = "txtVendedor";
            txtVendedor.Size = new Size(147, 23);
            txtVendedor.TabIndex = 5;
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(123, 80);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(150, 23);
            dtpDesde.TabIndex = 6;
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(418, 80);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(150, 23);
            dtpHasta.TabIndex = 7;
            // 
            // cmbEstado
            // 
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(418, 40);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(150, 23);
            cmbEstado.TabIndex = 8;
            // 
            // dgvComisiones
            // 
            dgvComisiones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvComisiones.Location = new Point(12, 118);
            dgvComisiones.Name = "dgvComisiones";
            dgvComisiones.Size = new Size(785, 231);
            dgvComisiones.TabIndex = 9;
            // 
            // btnDetalle
            // 
            btnDetalle.Location = new Point(312, 355);
            btnDetalle.Name = "btnDetalle";
            btnDetalle.Size = new Size(159, 49);
            btnDetalle.TabIndex = 10;
            btnDetalle.Text = "Ver detalle";
            btnDetalle.UseVisualStyleBackColor = true;
            btnDetalle.Click += btnDetalle_Click;
            // 
            // btnFiltrar
            // 
            btnFiltrar.Location = new Point(570, 80);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(102, 23);
            btnFiltrar.TabIndex = 11;
            btnFiltrar.Text = "Aplicar Filtros";
            btnFiltrar.UseVisualStyleBackColor = true;
            // 
            // ConsultarComisiones
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnFiltrar);
            Controls.Add(btnDetalle);
            Controls.Add(dgvComisiones);
            Controls.Add(cmbEstado);
            Controls.Add(dtpHasta);
            Controls.Add(dtpDesde);
            Controls.Add(txtVendedor);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ConsultarComisiones";
            Size = new Size(800, 426);
            ((System.ComponentModel.ISupportInitialize)dgvComisiones).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtVendedor;
        private DateTimePicker dtpDesde;
        private DateTimePicker dtpHasta;
        private ComboBox cmbEstado;
        private DataGridView dgvComisiones;
        private Button btnDetalle;
        private Button btnFiltrar;
    }
}
