namespace AutoGestion.UI
{
    partial class ABMUsuarios
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
            dgvUsuarios = new DataGridView();
            label1 = new Label();
            lblID = new Label();
            txtID = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblClave = new Label();
            txtClave = new TextBox();
            chkVerClave = new CheckBox();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Location = new Point(456, 35);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.Size = new Size(673, 390);
            dgvUsuarios.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(91, 54);
            label1.TabIndex = 1;
            label1.Text = "Usuarios";
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Location = new Point(65, 43);
            lblID.Name = "lblID";
            lblID.Size = new Size(21, 15);
            lblID.TabIndex = 2;
            lblID.Text = "ID:";
            // 
            // txtID
            // 
            txtID.Location = new Point(122, 35);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new Size(67, 23);
            txtID.TabIndex = 3;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(65, 74);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(51, 15);
            lblNombre.TabIndex = 4;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(122, 66);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(197, 23);
            txtNombre.TabIndex = 5;
            // 
            // lblClave
            // 
            lblClave.AutoSize = true;
            lblClave.Location = new Point(65, 104);
            lblClave.Name = "lblClave";
            lblClave.Size = new Size(57, 15);
            lblClave.TabIndex = 6;
            lblClave.Text = "Password";
            // 
            // txtClave
            // 
            txtClave.Location = new Point(122, 95);
            txtClave.Name = "txtClave";
            txtClave.Size = new Size(197, 23);
            txtClave.TabIndex = 7;
            txtClave.UseSystemPasswordChar = true;
            // 
            // chkVerClave
            // 
            chkVerClave.AutoSize = true;
            chkVerClave.Location = new Point(325, 95);
            chkVerClave.Name = "chkVerClave";
            chkVerClave.Size = new Size(42, 19);
            chkVerClave.TabIndex = 8;
            chkVerClave.Text = "Ver";
            chkVerClave.UseVisualStyleBackColor = true;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(69, 151);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(100, 37);
            btnAgregar.TabIndex = 9;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(183, 151);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(100, 37);
            btnModificar.TabIndex = 10;
            btnModificar.Text = "Modificar ";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(294, 151);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(100, 37);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // ABMUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(chkVerClave);
            Controls.Add(txtClave);
            Controls.Add(lblClave);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Controls.Add(txtID);
            Controls.Add(lblID);
            Controls.Add(label1);
            Controls.Add(dgvUsuarios);
            Name = "ABMUsuarios";
            Size = new Size(1280, 720);
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvUsuarios;
        private Label label1;
        private Label lblID;
        private TextBox txtID;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblClave;
        private TextBox txtClave;
        private CheckBox chkVerClave;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
    }
}
