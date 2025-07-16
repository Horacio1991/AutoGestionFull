namespace AutoGestion.UI
{
    partial class TasarVehiculo
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
            txtEvaluacion = new TextBox();
            txtRango = new TextBox();
            txtValorFinal = new TextBox();
            cmbOfertas = new ComboBox();
            cmbEstadoStock = new ComboBox();
            btnConfirmar = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(145, 54);
            label1.TabIndex = 0;
            label1.Text = "Tasar Vehículo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 79);
            label2.Name = "label2";
            label2.Size = new Size(103, 15);
            label2.TabIndex = 1;
            label2.Text = "Oferta de compra:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(26, 115);
            label3.Name = "label3";
            label3.Size = new Size(108, 15);
            label3.TabIndex = 2;
            label3.Text = "Evaluacion técnica:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(26, 220);
            label4.Name = "label4";
            label4.Size = new Size(85, 15);
            label4.TabIndex = 3;
            label4.Text = "Valor sugerido:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(26, 256);
            label5.Name = "label5";
            label5.Size = new Size(62, 15);
            label5.TabIndex = 4;
            label5.Text = "Valor final:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(26, 291);
            label6.Name = "label6";
            label6.Size = new Size(92, 15);
            label6.TabIndex = 5;
            label6.Text = "Estado en stock:";
            // 
            // txtEvaluacion
            // 
            txtEvaluacion.Location = new Point(177, 107);
            txtEvaluacion.Multiline = true;
            txtEvaluacion.Name = "txtEvaluacion";
            txtEvaluacion.Size = new Size(188, 99);
            txtEvaluacion.TabIndex = 6;
            // 
            // txtRango
            // 
            txtRango.Location = new Point(177, 212);
            txtRango.Name = "txtRango";
            txtRango.Size = new Size(188, 23);
            txtRango.TabIndex = 7;
            // 
            // txtValorFinal
            // 
            txtValorFinal.Location = new Point(177, 248);
            txtValorFinal.Name = "txtValorFinal";
            txtValorFinal.Size = new Size(188, 23);
            txtValorFinal.TabIndex = 8;
            // 
            // cmbOfertas
            // 
            cmbOfertas.FormattingEnabled = true;
            cmbOfertas.Location = new Point(177, 71);
            cmbOfertas.Name = "cmbOfertas";
            cmbOfertas.Size = new Size(188, 23);
            cmbOfertas.TabIndex = 9;
            // 
            // cmbEstadoStock
            // 
            cmbEstadoStock.FormattingEnabled = true;
            cmbEstadoStock.Location = new Point(177, 283);
            cmbEstadoStock.Name = "cmbEstadoStock";
            cmbEstadoStock.Size = new Size(188, 23);
            cmbEstadoStock.TabIndex = 10;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(179, 334);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(186, 45);
            btnConfirmar.TabIndex = 11;
            btnConfirmar.Text = "Confirmar Tasación";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // TasarVehiculo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnConfirmar);
            Controls.Add(cmbEstadoStock);
            Controls.Add(cmbOfertas);
            Controls.Add(txtValorFinal);
            Controls.Add(txtRango);
            Controls.Add(txtEvaluacion);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "TasarVehiculo";
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
        private TextBox txtEvaluacion;
        private TextBox txtRango;
        private TextBox txtValorFinal;
        private ComboBox cmbOfertas;
        private ComboBox cmbEstadoStock;
        private Button btnConfirmar;
    }
}
