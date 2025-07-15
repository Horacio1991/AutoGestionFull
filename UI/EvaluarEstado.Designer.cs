namespace AutoGestion.UI
{
    partial class EvaluarEstado
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
            label6 = new Label();
            label7 = new Label();
            cmbOfertas = new ComboBox();
            txtMotor = new TextBox();
            txtCarroceria = new TextBox();
            txtInterior = new TextBox();
            txtDocumentacion = new TextBox();
            txtObservaciones = new TextBox();
            btnGuardar = new Button();
            dtpFiltroFecha = new DateTimePicker();
            btnFiltrarFecha = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(147, 54);
            label1.TabIndex = 0;
            label1.Text = "Evaluar Estado";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 62);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 1;
            label2.Text = "Oferta:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 95);
            label3.Name = "label3";
            label3.Size = new Size(100, 15);
            label3.TabIndex = 2;
            label3.Text = "Estado del Motor:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(24, 128);
            label4.Name = "label4";
            label4.Size = new Size(102, 15);
            label4.TabIndex = 3;
            label4.Text = "Estado Carrocería:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(24, 164);
            label5.Name = "label5";
            label5.Size = new Size(83, 15);
            label5.TabIndex = 4;
            label5.Text = "Estado Interior";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(24, 199);
            label6.Name = "label6";
            label6.Size = new Size(133, 15);
            label6.TabIndex = 5;
            label6.Text = "Estado Documentación:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(24, 233);
            label7.Name = "label7";
            label7.Size = new Size(87, 15);
            label7.TabIndex = 6;
            label7.Text = "Observaciones:";
            // 
            // cmbOfertas
            // 
            cmbOfertas.FormattingEnabled = true;
            cmbOfertas.Location = new Point(184, 54);
            cmbOfertas.Name = "cmbOfertas";
            cmbOfertas.Size = new Size(193, 23);
            cmbOfertas.TabIndex = 7;
            // 
            // txtMotor
            // 
            txtMotor.Location = new Point(182, 87);
            txtMotor.Name = "txtMotor";
            txtMotor.Size = new Size(195, 23);
            txtMotor.TabIndex = 8;
            // 
            // txtCarroceria
            // 
            txtCarroceria.Location = new Point(182, 120);
            txtCarroceria.Name = "txtCarroceria";
            txtCarroceria.Size = new Size(195, 23);
            txtCarroceria.TabIndex = 9;
            // 
            // txtInterior
            // 
            txtInterior.Location = new Point(182, 156);
            txtInterior.Name = "txtInterior";
            txtInterior.Size = new Size(195, 23);
            txtInterior.TabIndex = 10;
            // 
            // txtDocumentacion
            // 
            txtDocumentacion.Location = new Point(182, 191);
            txtDocumentacion.Name = "txtDocumentacion";
            txtDocumentacion.Size = new Size(195, 23);
            txtDocumentacion.TabIndex = 11;
            // 
            // txtObservaciones
            // 
            txtObservaciones.Location = new Point(182, 225);
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.Size = new Size(426, 130);
            txtObservaciones.TabIndex = 12;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(288, 361);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(193, 47);
            btnGuardar.TabIndex = 13;
            btnGuardar.Text = "Guardar Evaluación";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // dtpFiltroFecha
            // 
            dtpFiltroFecha.Location = new Point(408, 56);
            dtpFiltroFecha.Name = "dtpFiltroFecha";
            dtpFiltroFecha.Size = new Size(200, 23);
            dtpFiltroFecha.TabIndex = 14;
            // 
            // btnFiltrarFecha
            // 
            btnFiltrarFecha.Location = new Point(408, 88);
            btnFiltrarFecha.Name = "btnFiltrarFecha";
            btnFiltrarFecha.Size = new Size(200, 23);
            btnFiltrarFecha.TabIndex = 15;
            btnFiltrarFecha.Text = "Filtrar";
            btnFiltrarFecha.UseVisualStyleBackColor = true;
            btnFiltrarFecha.Click += btnFiltrarFecha_Click;
            // 
            // EvaluarEstado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnFiltrarFecha);
            Controls.Add(dtpFiltroFecha);
            Controls.Add(btnGuardar);
            Controls.Add(txtObservaciones);
            Controls.Add(txtDocumentacion);
            Controls.Add(txtInterior);
            Controls.Add(txtCarroceria);
            Controls.Add(txtMotor);
            Controls.Add(cmbOfertas);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "EvaluarEstado";
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
        private Label label7;
        private ComboBox cmbOfertas;
        private TextBox txtMotor;
        private TextBox txtCarroceria;
        private TextBox txtInterior;
        private TextBox txtDocumentacion;
        private TextBox txtObservaciones;
        private Button btnGuardar;
        private DateTimePicker dtpFiltroFecha;
        private Button btnFiltrarFecha;
    }
}
