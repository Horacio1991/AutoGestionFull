namespace AutoGestion.Vista
{
    partial class RegistrarCliente
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
            lblDNI = new Label();
            lblNombre = new Label();
            lblApellido = new Label();
            lblContacto = new Label();
            txtDni = new TextBox();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtContacto = new TextBox();
            btnRegistrar = new Button();
            label1 = new Label();
            btnBuscarDNI = new Button();
            SuspendLayout();
            // 
            // lblDNI
            // 
            lblDNI.AutoSize = true;
            lblDNI.Font = new Font("Sans Serif Collection", 11.25F);
            lblDNI.Location = new Point(245, 119);
            lblDNI.Name = "lblDNI";
            lblDNI.Size = new Size(49, 51);
            lblDNI.TabIndex = 0;
            lblDNI.Text = "DNI";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Sans Serif Collection", 11.25F);
            lblNombre.Location = new Point(245, 161);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(79, 51);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre";
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Font = new Font("Sans Serif Collection", 11.25F);
            lblApellido.Location = new Point(245, 198);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(79, 51);
            lblApellido.TabIndex = 2;
            lblApellido.Text = "Apellido";
            // 
            // lblContacto
            // 
            lblContacto.AutoSize = true;
            lblContacto.Font = new Font("Sans Serif Collection", 11.25F);
            lblContacto.Location = new Point(245, 234);
            lblContacto.Name = "lblContacto";
            lblContacto.Size = new Size(83, 51);
            lblContacto.TabIndex = 3;
            lblContacto.Text = "Contacto";
            // 
            // txtDni
            // 
            txtDni.Location = new Point(347, 129);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(202, 23);
            txtDni.TabIndex = 4;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(347, 171);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(202, 23);
            txtNombre.TabIndex = 5;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(347, 208);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(202, 23);
            txtApellido.TabIndex = 6;
            // 
            // txtContacto
            // 
            txtContacto.Location = new Point(347, 244);
            txtContacto.Name = "txtContacto";
            txtContacto.Size = new Size(202, 23);
            txtContacto.TabIndex = 7;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRegistrar.Location = new Point(297, 288);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(227, 43);
            btnRegistrar.TabIndex = 8;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(164, 54);
            label1.TabIndex = 9;
            label1.Text = "Registrar Cliente";
            // 
            // btnBuscarDNI
            // 
            btnBuscarDNI.Location = new Point(566, 129);
            btnBuscarDNI.Name = "btnBuscarDNI";
            btnBuscarDNI.Size = new Size(75, 23);
            btnBuscarDNI.TabIndex = 10;
            btnBuscarDNI.Text = "Buscar";
            btnBuscarDNI.UseVisualStyleBackColor = true;
            btnBuscarDNI.Click += btnBuscarDNI_Click;
            // 
            // RegistrarCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnBuscarDNI);
            Controls.Add(label1);
            Controls.Add(btnRegistrar);
            Controls.Add(txtContacto);
            Controls.Add(txtApellido);
            Controls.Add(txtNombre);
            Controls.Add(txtDni);
            Controls.Add(lblContacto);
            Controls.Add(lblApellido);
            Controls.Add(lblNombre);
            Controls.Add(lblDNI);
            Name = "RegistrarCliente";
            Size = new Size(800, 426);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDNI;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblContacto;
        private TextBox txtDni;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtContacto;
        private Button btnRegistrar;
        private Label label1;
        private Button btnBuscarDNI;
    }
}
