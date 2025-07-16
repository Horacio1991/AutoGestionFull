namespace AutoGestion.UI
{
    partial class RegistrarOferta
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
            txtDni = new TextBox();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtContacto = new TextBox();
            btnBuscarOferente = new Button();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            btnGuardarOferta = new Button();
            txtMarca = new TextBox();
            dtpFechaInspeccion = new DateTimePicker();
            txtModelo = new TextBox();
            txtColor = new TextBox();
            txtDominio = new TextBox();
            numAnio = new NumericUpDown();
            numKm = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numAnio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numKm).BeginInit();
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
            label1.Text = "Registrar Oferta";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Sans Serif Collection", 9.749998F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(15, 40);
            label2.Name = "label2";
            label2.Size = new Size(72, 44);
            label2.TabIndex = 1;
            label2.Text = "Oferente";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Sans Serif Collection", 8.999999F);
            label3.Location = new Point(27, 69);
            label3.Name = "label3";
            label3.Size = new Size(40, 41);
            label3.TabIndex = 2;
            label3.Text = "DNI";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Sans Serif Collection", 8.999999F);
            label4.Location = new Point(27, 104);
            label4.Name = "label4";
            label4.Size = new Size(64, 41);
            label4.TabIndex = 3;
            label4.Text = "Nombre";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Sans Serif Collection", 8.999999F);
            label5.Location = new Point(27, 137);
            label5.Name = "label5";
            label5.Size = new Size(63, 41);
            label5.TabIndex = 4;
            label5.Text = "Apellido";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Sans Serif Collection", 8.999999F);
            label6.Location = new Point(27, 172);
            label6.Name = "label6";
            label6.Size = new Size(68, 41);
            label6.TabIndex = 5;
            label6.Text = "Contacto";
            // 
            // txtDni
            // 
            txtDni.Location = new Point(108, 74);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(141, 23);
            txtDni.TabIndex = 6;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(108, 109);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(222, 23);
            txtNombre.TabIndex = 7;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(108, 142);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(222, 23);
            txtApellido.TabIndex = 8;
            // 
            // txtContacto
            // 
            txtContacto.Location = new Point(108, 177);
            txtContacto.Name = "txtContacto";
            txtContacto.Size = new Size(222, 23);
            txtContacto.TabIndex = 9;
            // 
            // btnBuscarOferente
            // 
            btnBuscarOferente.Location = new Point(255, 74);
            btnBuscarOferente.Name = "btnBuscarOferente";
            btnBuscarOferente.Size = new Size(75, 23);
            btnBuscarOferente.TabIndex = 10;
            btnBuscarOferente.Text = "Buscar";
            btnBuscarOferente.UseVisualStyleBackColor = true;
            btnBuscarOferente.Click += btnBuscarOferente_Click_1;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Sans Serif Collection", 9.749998F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(336, 40);
            label7.Name = "label7";
            label7.Size = new Size(70, 44);
            label7.TabIndex = 11;
            label7.Text = "Vehículo";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Sans Serif Collection", 8.999999F);
            label8.Location = new Point(348, 84);
            label8.Name = "label8";
            label8.Size = new Size(54, 41);
            label8.TabIndex = 12;
            label8.Text = "Marca";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Sans Serif Collection", 8.999999F);
            label9.Location = new Point(348, 118);
            label9.Name = "label9";
            label9.Size = new Size(40, 41);
            label9.TabIndex = 13;
            label9.Text = "Año";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Sans Serif Collection", 8.999999F);
            label10.Location = new Point(348, 153);
            label10.Name = "label10";
            label10.Size = new Size(78, 41);
            label10.TabIndex = 14;
            label10.Text = "Kilómetros";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Sans Serif Collection", 8.999999F);
            label11.Location = new Point(348, 187);
            label11.Name = "label11";
            label11.Size = new Size(113, 41);
            label11.TabIndex = 15;
            label11.Text = "Fecha Inspección";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Sans Serif Collection", 8.999999F);
            label12.Location = new Point(585, 84);
            label12.Name = "label12";
            label12.Size = new Size(60, 41);
            label12.TabIndex = 16;
            label12.Text = "Modelo";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Sans Serif Collection", 8.999999F);
            label13.Location = new Point(585, 118);
            label13.Name = "label13";
            label13.Size = new Size(48, 41);
            label13.TabIndex = 17;
            label13.Text = "Color";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Sans Serif Collection", 8.999999F);
            label14.Location = new Point(585, 153);
            label14.Name = "label14";
            label14.Size = new Size(65, 41);
            label14.TabIndex = 18;
            label14.Text = "Dominio";
            // 
            // btnGuardarOferta
            // 
            btnGuardarOferta.Location = new Point(286, 267);
            btnGuardarOferta.Name = "btnGuardarOferta";
            btnGuardarOferta.Size = new Size(212, 56);
            btnGuardarOferta.TabIndex = 19;
            btnGuardarOferta.Text = "Registrar Oferta";
            btnGuardarOferta.UseVisualStyleBackColor = true;
            btnGuardarOferta.Click += btnGuardarOferta_Click;
            // 
            // txtMarca
            // 
            txtMarca.Location = new Point(429, 89);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(138, 23);
            txtMarca.TabIndex = 20;
            // 
            // dtpFechaInspeccion
            // 
            dtpFechaInspeccion.Location = new Point(467, 189);
            dtpFechaInspeccion.Name = "dtpFechaInspeccion";
            dtpFechaInspeccion.Size = new Size(200, 23);
            dtpFechaInspeccion.TabIndex = 23;
            // 
            // txtModelo
            // 
            txtModelo.Location = new Point(651, 89);
            txtModelo.Name = "txtModelo";
            txtModelo.Size = new Size(138, 23);
            txtModelo.TabIndex = 24;
            // 
            // txtColor
            // 
            txtColor.Location = new Point(651, 123);
            txtColor.Name = "txtColor";
            txtColor.Size = new Size(138, 23);
            txtColor.TabIndex = 25;
            // 
            // txtDominio
            // 
            txtDominio.Location = new Point(651, 158);
            txtDominio.Name = "txtDominio";
            txtDominio.Size = new Size(138, 23);
            txtDominio.TabIndex = 26;
            // 
            // numAnio
            // 
            numAnio.Location = new Point(429, 123);
            numAnio.Maximum = new decimal(new int[] { 3000, 0, 0, 0 });
            numAnio.Name = "numAnio";
            numAnio.Size = new Size(138, 23);
            numAnio.TabIndex = 27;
            // 
            // numKm
            // 
            numKm.Location = new Point(429, 153);
            numKm.Maximum = new decimal(new int[] { 300000, 0, 0, 0 });
            numKm.Name = "numKm";
            numKm.Size = new Size(138, 23);
            numKm.TabIndex = 28;
            // 
            // RegistrarOferta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(numKm);
            Controls.Add(numAnio);
            Controls.Add(txtDominio);
            Controls.Add(txtColor);
            Controls.Add(txtModelo);
            Controls.Add(dtpFechaInspeccion);
            Controls.Add(txtMarca);
            Controls.Add(btnGuardarOferta);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(btnBuscarOferente);
            Controls.Add(txtContacto);
            Controls.Add(txtApellido);
            Controls.Add(txtNombre);
            Controls.Add(txtDni);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "RegistrarOferta";
            Size = new Size(800, 426);
            ((System.ComponentModel.ISupportInitialize)numAnio).EndInit();
            ((System.ComponentModel.ISupportInitialize)numKm).EndInit();
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
        private TextBox txtDni;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtContacto;
        private Button btnBuscarOferente;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Button btnGuardarOferta;
        private TextBox txtMarca;
        private DateTimePicker dtpFechaInspeccion;
        private TextBox txtModelo;
        private TextBox txtColor;
        private TextBox txtDominio;
        private NumericUpDown numAnio;
        private NumericUpDown numKm;
    }
}
