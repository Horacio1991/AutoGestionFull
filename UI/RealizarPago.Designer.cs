namespace AutoGestion.Vista
{
    partial class RealizarPago
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
            lblRealizarPago = new Label();
            cmbTipoPago = new ComboBox();
            txtMonto = new TextBox();
            txtCuotas = new TextBox();
            txtOtrosDatos = new TextBox();
            btnRegistrarPago = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            txtDni = new TextBox();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtContacto = new TextBox();
            btnBuscarCliente = new Button();
            dgvVehiculos = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvVehiculos).BeginInit();
            SuspendLayout();
            // 
            // lblRealizarPago
            // 
            lblRealizarPago.AutoSize = true;
            lblRealizarPago.Font = new Font("Sans Serif Collection", 15.7499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRealizarPago.Location = new Point(0, 0);
            lblRealizarPago.Name = "lblRealizarPago";
            lblRealizarPago.Size = new Size(175, 72);
            lblRealizarPago.TabIndex = 0;
            lblRealizarPago.Text = "Realizar Pago";
            // 
            // cmbTipoPago
            // 
            cmbTipoPago.FormattingEnabled = true;
            cmbTipoPago.Location = new Point(507, 74);
            cmbTipoPago.Name = "cmbTipoPago";
            cmbTipoPago.Size = new Size(267, 23);
            cmbTipoPago.TabIndex = 1;
            // 
            // txtMonto
            // 
            txtMonto.Location = new Point(507, 120);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(267, 23);
            txtMonto.TabIndex = 2;
            // 
            // txtCuotas
            // 
            txtCuotas.Location = new Point(507, 163);
            txtCuotas.Name = "txtCuotas";
            txtCuotas.Size = new Size(267, 23);
            txtCuotas.TabIndex = 3;
            // 
            // txtOtrosDatos
            // 
            txtOtrosDatos.Location = new Point(514, 210);
            txtOtrosDatos.Multiline = true;
            txtOtrosDatos.Name = "txtOtrosDatos";
            txtOtrosDatos.Size = new Size(260, 68);
            txtOtrosDatos.TabIndex = 4;
            // 
            // btnRegistrarPago
            // 
            btnRegistrarPago.Location = new Point(514, 298);
            btnRegistrarPago.Name = "btnRegistrarPago";
            btnRegistrarPago.Size = new Size(260, 42);
            btnRegistrarPago.TabIndex = 5;
            btnRegistrarPago.Text = "Registrar Pago";
            btnRegistrarPago.UseVisualStyleBackColor = true;
            btnRegistrarPago.Click += btnRegistrarPago_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(390, 74);
            label1.Name = "label1";
            label1.Size = new Size(81, 15);
            label1.TabIndex = 6;
            label1.Text = "Tipo De Pago:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(390, 120);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 7;
            label2.Text = "Monto:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(390, 163);
            label3.Name = "label3";
            label3.Size = new Size(114, 15);
            label3.TabIndex = 8;
            label3.Text = "Cantidad de Cuotas:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(390, 210);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 9;
            label4.Text = "Detalles:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(37, 61);
            label5.Name = "label5";
            label5.Size = new Size(84, 41);
            label5.TabIndex = 10;
            label5.Text = "DNI Cliente:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(38, 123);
            label6.Name = "label6";
            label6.Size = new Size(66, 41);
            label6.TabIndex = 11;
            label6.Text = "Apellido:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(37, 94);
            label7.Name = "label7";
            label7.Size = new Size(67, 41);
            label7.TabIndex = 12;
            label7.Text = "Nombre:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Sans Serif Collection", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(37, 152);
            label8.Name = "label8";
            label8.Size = new Size(71, 41);
            label8.TabIndex = 13;
            label8.Text = "Contacto:";
            // 
            // txtDni
            // 
            txtDni.Location = new Point(114, 66);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(149, 23);
            txtDni.TabIndex = 14;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(114, 99);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(149, 23);
            txtNombre.TabIndex = 15;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(114, 128);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(149, 23);
            txtApellido.TabIndex = 16;
            // 
            // txtContacto
            // 
            txtContacto.Location = new Point(114, 157);
            txtContacto.Name = "txtContacto";
            txtContacto.Size = new Size(149, 23);
            txtContacto.TabIndex = 17;
            // 
            // btnBuscarCliente
            // 
            btnBuscarCliente.Location = new Point(269, 65);
            btnBuscarCliente.Name = "btnBuscarCliente";
            btnBuscarCliente.Size = new Size(75, 23);
            btnBuscarCliente.TabIndex = 18;
            btnBuscarCliente.Text = "Buscar";
            btnBuscarCliente.UseVisualStyleBackColor = true;
            btnBuscarCliente.Click += btnBuscarCliente_Click_1;
            // 
            // dgvVehiculos
            // 
            dgvVehiculos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVehiculos.Location = new Point(38, 210);
            dgvVehiculos.Name = "dgvVehiculos";
            dgvVehiculos.Size = new Size(306, 213);
            dgvVehiculos.TabIndex = 19;
            dgvVehiculos.SelectionChanged += dgvVehiculos_SelectionChanged;
            // 
            // RealizarPago
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvVehiculos);
            Controls.Add(btnBuscarCliente);
            Controls.Add(txtContacto);
            Controls.Add(txtApellido);
            Controls.Add(txtNombre);
            Controls.Add(txtDni);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnRegistrarPago);
            Controls.Add(txtOtrosDatos);
            Controls.Add(txtCuotas);
            Controls.Add(txtMonto);
            Controls.Add(cmbTipoPago);
            Controls.Add(lblRealizarPago);
            Name = "RealizarPago";
            Size = new Size(800, 426);
            ((System.ComponentModel.ISupportInitialize)dgvVehiculos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblRealizarPago;
        private ComboBox cmbTipoPago;
        private TextBox txtMonto;
        private TextBox txtCuotas;
        private TextBox txtOtrosDatos;
        private Button btnRegistrarPago;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox txtDni;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtContacto;
        private Button btnBuscarCliente;
        private DataGridView dgvVehiculos;
    }
}
