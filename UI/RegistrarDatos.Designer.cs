namespace AutoGestion.Vista
{
    partial class RegistrarDatos
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
            label6 = new Label();
            txtDominio = new TextBox();
            txtEvaluacion = new TextBox();
            cmbEstadoStock = new ComboBox();
            btnBuscarOferta = new Button();
            btnRegistrar = new Button();
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
            label1.Text = "Registrar Datos";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(184, 75);
            label2.Name = "label2";
            label2.Size = new Size(123, 15);
            label2.TabIndex = 1;
            label2.Text = "Domínio del vehículo:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(184, 111);
            label3.Name = "label3";
            label3.Size = new Size(171, 15);
            label3.TabIndex = 2;
            label3.Text = "Motor, chasis, documentación:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(184, 215);
            label6.Name = "label6";
            label6.Size = new Size(104, 15);
            label6.TabIndex = 5;
            label6.Text = "Estado en el stock:";
            // 
            // txtDominio
            // 
            txtDominio.Location = new Point(358, 67);
            txtDominio.Name = "txtDominio";
            txtDominio.Size = new Size(172, 23);
            txtDominio.TabIndex = 6;
            // 
            // txtEvaluacion
            // 
            txtEvaluacion.Location = new Point(358, 103);
            txtEvaluacion.Multiline = true;
            txtEvaluacion.Name = "txtEvaluacion";
            txtEvaluacion.Size = new Size(172, 94);
            txtEvaluacion.TabIndex = 7;
            // 
            // cmbEstadoStock
            // 
            cmbEstadoStock.FormattingEnabled = true;
            cmbEstadoStock.Location = new Point(358, 212);
            cmbEstadoStock.Name = "cmbEstadoStock";
            cmbEstadoStock.Size = new Size(172, 23);
            cmbEstadoStock.TabIndex = 11;
            // 
            // btnBuscarOferta
            // 
            btnBuscarOferta.Location = new Point(542, 67);
            btnBuscarOferta.Name = "btnBuscarOferta";
            btnBuscarOferta.Size = new Size(138, 23);
            btnBuscarOferta.TabIndex = 12;
            btnBuscarOferta.Text = "Buscar Oferta";
            btnBuscarOferta.UseVisualStyleBackColor = true;
            btnBuscarOferta.Click += btnBuscarOferta_Click;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(358, 285);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(172, 41);
            btnRegistrar.TabIndex = 13;
            btnRegistrar.Text = "Registrar Datos";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // RegistrarDatos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnRegistrar);
            Controls.Add(btnBuscarOferta);
            Controls.Add(cmbEstadoStock);
            Controls.Add(txtEvaluacion);
            Controls.Add(txtDominio);
            Controls.Add(label6);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "RegistrarDatos";
            Size = new Size(800, 426);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtDominio;
        private TextBox txtEvaluacion;
        private TextBox txtDetallesPago;
        private ComboBox cmbTipoPago;
        private ComboBox cmbEstadoStock;
        private Button btnBuscarOferta;
        private Button btnRegistrar;
    }
}
