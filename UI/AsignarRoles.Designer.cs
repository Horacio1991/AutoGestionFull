namespace AutoGestion.Vista
{
    partial class AsignarRoles
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
            txtIdUsuario = new TextBox();
            txtNombreUsuario = new TextBox();
            label3 = new Label();
            txtContrasenaUsuario = new TextBox();
            label4 = new Label();
            chkEncriptar = new CheckBox();
            label5 = new Label();
            label7 = new Label();
            txtNombreRol = new TextBox();
            btnModificarRol = new Button();
            btnAltaRol = new Button();
            btnEliminarRol = new Button();
            label12 = new Label();
            txtNombrePermiso = new TextBox();
            label13 = new Label();
            label15 = new Label();
            label16 = new Label();
            cmbPermisoMenu = new ComboBox();
            cmbPermisoItem = new ComboBox();
            label17 = new Label();
            btnAltaPermiso = new Button();
            btnEliminarPermiso = new Button();
            btnAsociarPermisoARol = new Button();
            btnQuitarPermisoRol = new Button();
            label18 = new Label();
            label19 = new Label();
            btnAsociarRolAUsuario = new Button();
            btnQuitarRolUsuario = new Button();
            label21 = new Label();
            label22 = new Label();
            label23 = new Label();
            label24 = new Label();
            label25 = new Label();
            tvUsuarios = new TreeView();
            tvRoles = new TreeView();
            tvPermisos = new TreeView();
            tvPermisosPorRol = new TreeView();
            tvPermisosPorUsuario = new TreeView();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 0;
            label1.Text = "Usuario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 24);
            label2.Name = "label2";
            label2.Size = new Size(21, 15);
            label2.TabIndex = 1;
            label2.Text = "ID:";
            // 
            // txtIdUsuario
            // 
            txtIdUsuario.Location = new Point(85, 16);
            txtIdUsuario.Name = "txtIdUsuario";
            txtIdUsuario.ReadOnly = true;
            txtIdUsuario.Size = new Size(100, 23);
            txtIdUsuario.TabIndex = 2;
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.Location = new Point(85, 50);
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.ReadOnly = true;
            txtNombreUsuario.Size = new Size(100, 23);
            txtNombreUsuario.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 58);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 3;
            label3.Text = "Nombre:";
            // 
            // txtContrasenaUsuario
            // 
            txtContrasenaUsuario.Location = new Point(85, 84);
            txtContrasenaUsuario.Name = "txtContrasenaUsuario";
            txtContrasenaUsuario.ReadOnly = true;
            txtContrasenaUsuario.Size = new Size(100, 23);
            txtContrasenaUsuario.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 92);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 5;
            label4.Text = "Contraseña:";
            // 
            // chkEncriptar
            // 
            chkEncriptar.AutoSize = true;
            chkEncriptar.Location = new Point(191, 86);
            chkEncriptar.Name = "chkEncriptar";
            chkEncriptar.Size = new Size(133, 19);
            chkEncriptar.TabIndex = 7;
            chkEncriptar.Text = "Decifrar/Cifrar Clave";
            chkEncriptar.UseVisualStyleBackColor = true;
            chkEncriptar.CheckedChanged += chkEncriptar_CheckedChanged_1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(335, 0);
            label5.Name = "label5";
            label5.Size = new Size(24, 15);
            label5.TabIndex = 8;
            label5.Text = "Rol";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(335, 24);
            label7.Name = "label7";
            label7.Size = new Size(54, 15);
            label7.TabIndex = 10;
            label7.Text = "Nombre:";
            // 
            // txtNombreRol
            // 
            txtNombreRol.Location = new Point(395, 16);
            txtNombreRol.Name = "txtNombreRol";
            txtNombreRol.Size = new Size(185, 23);
            txtNombreRol.TabIndex = 11;
            // 
            // btnModificarRol
            // 
            btnModificarRol.Location = new Point(424, 45);
            btnModificarRol.Name = "btnModificarRol";
            btnModificarRol.Size = new Size(75, 36);
            btnModificarRol.TabIndex = 13;
            btnModificarRol.Text = "Modificar";
            btnModificarRol.UseVisualStyleBackColor = true;
            btnModificarRol.Click += btnModificarRol_Click_1;
            // 
            // btnAltaRol
            // 
            btnAltaRol.Location = new Point(339, 45);
            btnAltaRol.Name = "btnAltaRol";
            btnAltaRol.Size = new Size(75, 36);
            btnAltaRol.TabIndex = 14;
            btnAltaRol.Text = "Alta";
            btnAltaRol.UseVisualStyleBackColor = true;
            btnAltaRol.Click += btnAltaRol_Click_1;
            // 
            // btnEliminarRol
            // 
            btnEliminarRol.Location = new Point(505, 45);
            btnEliminarRol.Name = "btnEliminarRol";
            btnEliminarRol.Size = new Size(75, 36);
            btnEliminarRol.TabIndex = 15;
            btnEliminarRol.Text = "Eliminar";
            btnEliminarRol.UseVisualStyleBackColor = true;
            btnEliminarRol.Click += btnEliminarRol_Click_1;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(335, 86);
            label12.Name = "label12";
            label12.Size = new Size(50, 15);
            label12.TabIndex = 23;
            label12.Text = "Permiso";
            // 
            // txtNombrePermiso
            // 
            txtNombrePermiso.Location = new Point(403, 102);
            txtNombrePermiso.Name = "txtNombrePermiso";
            txtNombrePermiso.Size = new Size(177, 23);
            txtNombrePermiso.TabIndex = 26;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(346, 110);
            label13.Name = "label13";
            label13.Size = new Size(54, 15);
            label13.TabIndex = 25;
            label13.Text = "Nombre:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(344, 166);
            label15.Name = "label15";
            label15.Size = new Size(34, 15);
            label15.TabIndex = 29;
            label15.Text = "Item:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(344, 139);
            label16.Name = "label16";
            label16.Size = new Size(41, 15);
            label16.TabIndex = 28;
            label16.Text = "Menu:";
            // 
            // cmbPermisoMenu
            // 
            cmbPermisoMenu.FormattingEnabled = true;
            cmbPermisoMenu.Location = new Point(403, 129);
            cmbPermisoMenu.Name = "cmbPermisoMenu";
            cmbPermisoMenu.Size = new Size(121, 23);
            cmbPermisoMenu.TabIndex = 30;
            cmbPermisoMenu.SelectedIndexChanged += cmbPermisoMenu_SelectedIndexChanged_1;
            // 
            // cmbPermisoItem
            // 
            cmbPermisoItem.FormattingEnabled = true;
            cmbPermisoItem.Location = new Point(403, 158);
            cmbPermisoItem.Name = "cmbPermisoItem";
            cmbPermisoItem.Size = new Size(121, 23);
            cmbPermisoItem.TabIndex = 31;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(546, 166);
            label17.Name = "label17";
            label17.Size = new Size(141, 15);
            label17.TabIndex = 32;
            label17.Text = "Opciones Roles/Permisos";
            // 
            // btnAltaPermiso
            // 
            btnAltaPermiso.Location = new Point(346, 184);
            btnAltaPermiso.Name = "btnAltaPermiso";
            btnAltaPermiso.Size = new Size(75, 60);
            btnAltaPermiso.TabIndex = 34;
            btnAltaPermiso.Text = "Alta Permiso";
            btnAltaPermiso.UseVisualStyleBackColor = true;
            btnAltaPermiso.Click += btnAltaPermiso_Click_1;
            // 
            // btnEliminarPermiso
            // 
            btnEliminarPermiso.Location = new Point(427, 184);
            btnEliminarPermiso.Name = "btnEliminarPermiso";
            btnEliminarPermiso.Size = new Size(75, 60);
            btnEliminarPermiso.TabIndex = 33;
            btnEliminarPermiso.Text = "Eliminar Permiso";
            btnEliminarPermiso.UseVisualStyleBackColor = true;
            btnEliminarPermiso.Click += btnEliminarPermiso_Click_1;
            // 
            // btnAsociarPermisoARol
            // 
            btnAsociarPermisoARol.Location = new Point(546, 184);
            btnAsociarPermisoARol.Name = "btnAsociarPermisoARol";
            btnAsociarPermisoARol.Size = new Size(67, 60);
            btnAsociarPermisoARol.TabIndex = 36;
            btnAsociarPermisoARol.Text = "Asociar Permiso a Rol";
            btnAsociarPermisoARol.UseVisualStyleBackColor = true;
            btnAsociarPermisoARol.Click += btnAsociarPermisoARol_Click_1;
            // 
            // btnQuitarPermisoRol
            // 
            btnQuitarPermisoRol.Location = new Point(619, 184);
            btnQuitarPermisoRol.Name = "btnQuitarPermisoRol";
            btnQuitarPermisoRol.Size = new Size(75, 60);
            btnQuitarPermisoRol.TabIndex = 35;
            btnQuitarPermisoRol.Text = "Quitar Permiso a Rol";
            btnQuitarPermisoRol.UseVisualStyleBackColor = true;
            btnQuitarPermisoRol.Click += btnQuitarPermisoRol_Click_1;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(3, 148);
            label18.Name = "label18";
            label18.Size = new Size(134, 15);
            label18.TabIndex = 40;
            label18.Text = "Roles/Permisos Usuario:";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(9, 166);
            label19.Name = "label19";
            label19.Size = new Size(90, 15);
            label19.TabIndex = 41;
            label19.Text = "Roles a Usuario:";
            // 
            // btnAsociarRolAUsuario
            // 
            btnAsociarRolAUsuario.Location = new Point(10, 184);
            btnAsociarRolAUsuario.Name = "btnAsociarRolAUsuario";
            btnAsociarRolAUsuario.Size = new Size(67, 60);
            btnAsociarRolAUsuario.TabIndex = 44;
            btnAsociarRolAUsuario.Text = "Asociar Rol a Usuario";
            btnAsociarRolAUsuario.UseVisualStyleBackColor = true;
            btnAsociarRolAUsuario.Click += btnAsociarRolAUsuario_Click_1;
            // 
            // btnQuitarRolUsuario
            // 
            btnQuitarRolUsuario.Location = new Point(83, 184);
            btnQuitarRolUsuario.Name = "btnQuitarRolUsuario";
            btnQuitarRolUsuario.Size = new Size(75, 60);
            btnQuitarRolUsuario.TabIndex = 43;
            btnQuitarRolUsuario.Text = "Quitar Rol a Usuario";
            btnQuitarRolUsuario.UseVisualStyleBackColor = true;
            btnQuitarRolUsuario.Click += btnQuitarRolUsuario_Click_1;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(9, 262);
            label21.Name = "label21";
            label21.Size = new Size(55, 15);
            label21.TabIndex = 47;
            label21.Text = "Usuarios:";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(175, 262);
            label22.Name = "label22";
            label22.Size = new Size(38, 15);
            label22.TabIndex = 48;
            label22.Text = "Roles:";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(346, 262);
            label23.Name = "label23";
            label23.Size = new Size(58, 15);
            label23.TabIndex = 49;
            label23.Text = "Permisos:";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(514, 262);
            label24.Name = "label24";
            label24.Size = new Size(99, 15);
            label24.TabIndex = 50;
            label24.Text = "Permisos Por Rol:";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(683, 262);
            label25.Name = "label25";
            label25.Size = new Size(160, 15);
            label25.TabIndex = 51;
            label25.Text = "Roles y Permisos del Usuario:";
            // 
            // tvUsuarios
            // 
            tvUsuarios.Location = new Point(10, 280);
            tvUsuarios.Name = "tvUsuarios";
            tvUsuarios.Size = new Size(163, 417);
            tvUsuarios.TabIndex = 52;
            tvUsuarios.AfterSelect += tvUsuarios_AfterSelect_1;
            // 
            // tvRoles
            // 
            tvRoles.Location = new Point(179, 280);
            tvRoles.Name = "tvRoles";
            tvRoles.Size = new Size(161, 417);
            tvRoles.TabIndex = 53;
            tvRoles.AfterSelect += tvRoles_AfterSelect_1;
            // 
            // tvPermisos
            // 
            tvPermisos.Location = new Point(346, 280);
            tvPermisos.Name = "tvPermisos";
            tvPermisos.Size = new Size(162, 417);
            tvPermisos.TabIndex = 54;
            tvPermisos.AfterSelect += tvPermisos_AfterSelect_1;
            // 
            // tvPermisosPorRol
            // 
            tvPermisosPorRol.Location = new Point(514, 280);
            tvPermisosPorRol.Name = "tvPermisosPorRol";
            tvPermisosPorRol.Size = new Size(163, 417);
            tvPermisosPorRol.TabIndex = 55;
            // 
            // tvPermisosPorUsuario
            // 
            tvPermisosPorUsuario.Location = new Point(683, 280);
            tvPermisosPorUsuario.Name = "tvPermisosPorUsuario";
            tvPermisosPorUsuario.Size = new Size(210, 417);
            tvPermisosPorUsuario.TabIndex = 56;
            // 
            // AsignarRoles
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tvPermisosPorUsuario);
            Controls.Add(tvPermisosPorRol);
            Controls.Add(tvPermisos);
            Controls.Add(tvRoles);
            Controls.Add(tvUsuarios);
            Controls.Add(label25);
            Controls.Add(label24);
            Controls.Add(label23);
            Controls.Add(label22);
            Controls.Add(label21);
            Controls.Add(btnAsociarRolAUsuario);
            Controls.Add(btnQuitarRolUsuario);
            Controls.Add(label19);
            Controls.Add(label18);
            Controls.Add(btnAsociarPermisoARol);
            Controls.Add(btnQuitarPermisoRol);
            Controls.Add(btnAltaPermiso);
            Controls.Add(btnEliminarPermiso);
            Controls.Add(label17);
            Controls.Add(cmbPermisoItem);
            Controls.Add(cmbPermisoMenu);
            Controls.Add(label15);
            Controls.Add(label16);
            Controls.Add(txtNombrePermiso);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(btnEliminarRol);
            Controls.Add(btnAltaRol);
            Controls.Add(btnModificarRol);
            Controls.Add(txtNombreRol);
            Controls.Add(label7);
            Controls.Add(label5);
            Controls.Add(chkEncriptar);
            Controls.Add(txtContrasenaUsuario);
            Controls.Add(label4);
            Controls.Add(txtNombreUsuario);
            Controls.Add(label3);
            Controls.Add(txtIdUsuario);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "AsignarRoles";
            Size = new Size(1280, 720);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtIdUsuario;
        private TextBox txtNombreUsuario;
        private Label label3;
        private TextBox txtContrasenaUsuario;
        private Label label4;
        private CheckBox chkEncriptar;
        private Label label5;
        private Label label7;
        private TextBox txtNombreRol;
        private Button btnModificarRol;
        private Button btnAltaRol;
        private Button btnEliminarRol;
        private Label label12;
        private TextBox txtNombrePermiso;
        private Label label13;
        private Label label15;
        private Label label16;
        private ComboBox cmbPermisoMenu;
        private ComboBox cmbPermisoItem;
        private Label label17;
        private Button btnAltaPermiso;
        private Button btnEliminarPermiso;
        private Button btnAsociarPermisoARol;
        private Button btnQuitarPermisoRol;
        private Label label18;
        private Label label19;
        private Button btnAsociarRolAUsuario;
        private Button btnQuitarRolUsuario;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label24;
        private Label label25;
        private TreeView tvUsuarios;
        private TreeView tvRoles;
        private TreeView tvPermisos;
        private TreeView tvPermisosPorRol;
        private TreeView tvPermisosPorUsuario;
    }
}
