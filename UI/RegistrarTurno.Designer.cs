namespace AutoGestion.Vista
{
    partial class RegistrarTurno
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
            txtDniCliente = new TextBox();
            txtDominio = new TextBox();
            dtpFecha = new DateTimePicker();
            dtpHora = new DateTimePicker();
            btnRegistrar = new Button();
            dgvVehiculos = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvVehiculos).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(155, 54);
            label1.TabIndex = 0;
            label1.Text = "Registrar Turno";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(52, 79);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 1;
            label2.Text = "DNI Cliente:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(52, 126);
            label3.Name = "label3";
            label3.Size = new Size(101, 15);
            label3.TabIndex = 2;
            label3.Text = "Dominio Vehiculo";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(52, 175);
            label4.Name = "label4";
            label4.Size = new Size(92, 15);
            label4.TabIndex = 3;
            label4.Text = "Fecha del turno:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(52, 218);
            label5.Name = "label5";
            label5.Size = new Size(87, 15);
            label5.TabIndex = 4;
            label5.Text = "Hora del turno:";
            // 
            // txtDniCliente
            // 
            txtDniCliente.Location = new Point(181, 71);
            txtDniCliente.Name = "txtDniCliente";
            txtDniCliente.Size = new Size(185, 23);
            txtDniCliente.TabIndex = 5;
            // 
            // txtDominio
            // 
            txtDominio.Location = new Point(181, 118);
            txtDominio.Name = "txtDominio";
            txtDominio.Size = new Size(185, 23);
            txtDominio.TabIndex = 6;
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(181, 169);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(185, 23);
            dtpFecha.TabIndex = 7;
            // 
            // dtpHora
            // 
            dtpHora.Format = DateTimePickerFormat.Time;
            dtpHora.Location = new Point(181, 210);
            dtpHora.Name = "dtpHora";
            dtpHora.Size = new Size(185, 23);
            dtpHora.TabIndex = 8;
            dtpHora.ValueChanged += dtpHora_ValueChanged_1;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(143, 265);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(155, 42);
            btnRegistrar.TabIndex = 9;
            btnRegistrar.Text = "Registrar Turno";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // dgvVehiculos
            // 
            dgvVehiculos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVehiculos.Location = new Point(403, 71);
            dgvVehiculos.Name = "dgvVehiculos";
            dgvVehiculos.Size = new Size(332, 162);
            dgvVehiculos.TabIndex = 10;
            dgvVehiculos.CellClick += dgvVehiculos_CellClick_1;
            // 
            // RegistrarTurno
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvVehiculos);
            Controls.Add(btnRegistrar);
            Controls.Add(dtpHora);
            Controls.Add(dtpFecha);
            Controls.Add(txtDominio);
            Controls.Add(txtDniCliente);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "RegistrarTurno";
            Size = new Size(800, 426);
            Load += RegistrarTurno_Load_1;
            ((System.ComponentModel.ISupportInitialize)dgvVehiculos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtDniCliente;
        private TextBox txtDominio;
        private DateTimePicker dtpFecha;
        private DateTimePicker dtpHora;
        private Button btnRegistrar;
        private DataGridView dgvVehiculos;
    }
}
