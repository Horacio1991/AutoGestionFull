namespace AutoGestion.UI
{
    partial class UC_AsignarRoles
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            trvPermisosRoles = new TreeView();
            lblUsuariosSistema = new Label();
            lblRoles = new Label();
            trvPermisos = new TreeView();
            lblPermisos = new Label();
            trvPermisosPorRol = new TreeView();
            lblPermisoPorRol = new Label();
            trvPermisosDelUsuario = new TreeView();
            label1 = new Label();
            treeViewUsuarios = new TreeView();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            textBoxUserId = new TextBox();
            textBoxUsename = new TextBox();
            textBoxUserPassword = new TextBox();
            checkBoxPassword = new CheckBox();
            label7 = new Label();
            label8 = new Label();
            textBoxRolNombre = new TextBox();
            btnAltaRol = new Button();
            btnModificarRol = new Button();
            btnEliminarRol = new Button();
            btnAsociarRolUsuario = new Button();
            btnDesasociarRolUsuario = new Button();
            label14 = new Label();
            label15 = new Label();
            textBoxRolId = new TextBox();
            textBoxPermisoId = new TextBox();
            textBoxPermisoNombre = new TextBox();
            label16 = new Label();
            label17 = new Label();
            comboBoxMenu = new ComboBox();
            comboBoxItem = new ComboBox();
            btnAltaPermiso = new Button();
            btnEliminarPermiso = new Button();
            btnAsociarPermisoARol = new Button();
            btnQuitarPermisoARol = new Button();
            btnAsociarPermisoAUsuario = new Button();
            btnQuitarPermisoUsuario = new Button();
            groupBoxUsuario = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox4 = new GroupBox();
            btnAsignarPermisosSeleccionados = new Button();
            groupBox5 = new GroupBox();
            groupBox6 = new GroupBox();
            groupBox7 = new GroupBox();
            groupBox8 = new GroupBox();
            groupBoxUsuario.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            SuspendLayout();
            // 
            // trvPermisosRoles
            // 
            trvPermisosRoles.Location = new Point(207, 330);
            trvPermisosRoles.Margin = new Padding(2);
            trvPermisosRoles.Name = "trvPermisosRoles";
            trvPermisosRoles.Size = new Size(193, 369);
            trvPermisosRoles.TabIndex = 0;
            // 
            // lblUsuariosSistema
            // 
            lblUsuariosSistema.AutoSize = true;
            lblUsuariosSistema.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsuariosSistema.Location = new Point(14, 308);
            lblUsuariosSistema.Margin = new Padding(2, 0, 2, 0);
            lblUsuariosSistema.Name = "lblUsuariosSistema";
            lblUsuariosSistema.Size = new Size(143, 20);
            lblUsuariosSistema.TabIndex = 3;
            lblUsuariosSistema.Text = "Usuarios Activos";
            // 
            // lblRoles
            // 
            lblRoles.AutoSize = true;
            lblRoles.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRoles.Location = new Point(207, 308);
            lblRoles.Margin = new Padding(2, 0, 2, 0);
            lblRoles.Name = "lblRoles";
            lblRoles.Size = new Size(55, 20);
            lblRoles.TabIndex = 4;
            lblRoles.Text = "Roles";
            // 
            // trvPermisos
            // 
            trvPermisos.CheckBoxes = true;
            trvPermisos.Location = new Point(430, 330);
            trvPermisos.Margin = new Padding(2);
            trvPermisos.Name = "trvPermisos";
            trvPermisos.Size = new Size(263, 369);
            trvPermisos.TabIndex = 5;
            trvPermisos.AfterCheck += trvPermisos_AfterCheck;
            // 
            // lblPermisos
            // 
            lblPermisos.AutoSize = true;
            lblPermisos.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPermisos.Location = new Point(430, 308);
            lblPermisos.Margin = new Padding(2, 0, 2, 0);
            lblPermisos.Name = "lblPermisos";
            lblPermisos.Size = new Size(82, 20);
            lblPermisos.TabIndex = 6;
            lblPermisos.Text = "Permisos";
            // 
            // trvPermisosPorRol
            // 
            trvPermisosPorRol.Location = new Point(717, 330);
            trvPermisosPorRol.Margin = new Padding(2);
            trvPermisosPorRol.Name = "trvPermisosPorRol";
            trvPermisosPorRol.Size = new Size(254, 369);
            trvPermisosPorRol.TabIndex = 7;
            // 
            // lblPermisoPorRol
            // 
            lblPermisoPorRol.AutoSize = true;
            lblPermisoPorRol.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPermisoPorRol.Location = new Point(717, 308);
            lblPermisoPorRol.Margin = new Padding(2, 0, 2, 0);
            lblPermisoPorRol.Name = "lblPermisoPorRol";
            lblPermisoPorRol.Size = new Size(146, 20);
            lblPermisoPorRol.TabIndex = 8;
            lblPermisoPorRol.Text = "Permisos Por Rol";
            // 
            // trvPermisosDelUsuario
            // 
            trvPermisosDelUsuario.Location = new Point(1001, 330);
            trvPermisosDelUsuario.Margin = new Padding(2);
            trvPermisosDelUsuario.Name = "trvPermisosDelUsuario";
            trvPermisosDelUsuario.Size = new Size(277, 369);
            trvPermisosDelUsuario.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(1002, 308);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(249, 20);
            label1.TabIndex = 10;
            label1.Text = "Roles Y Permisos Del Usuario";
            // 
            // treeViewUsuarios
            // 
            treeViewUsuarios.Location = new Point(14, 330);
            treeViewUsuarios.Margin = new Padding(2);
            treeViewUsuarios.Name = "treeViewUsuarios";
            treeViewUsuarios.Size = new Size(166, 369);
            treeViewUsuarios.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 35);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(18, 15);
            label3.TabIndex = 13;
            label3.Text = "ID";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 77);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 14;
            label4.Text = "Nombre";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 118);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 15;
            label5.Text = "Password";
            // 
            // textBoxUserId
            // 
            textBoxUserId.Location = new Point(64, 31);
            textBoxUserId.Margin = new Padding(4, 3, 4, 3);
            textBoxUserId.Name = "textBoxUserId";
            textBoxUserId.Size = new Size(70, 23);
            textBoxUserId.TabIndex = 16;
            // 
            // textBoxUsename
            // 
            textBoxUsename.Location = new Point(64, 69);
            textBoxUsename.Margin = new Padding(4, 3, 4, 3);
            textBoxUsename.Name = "textBoxUsename";
            textBoxUsename.Size = new Size(148, 23);
            textBoxUsename.TabIndex = 17;
            // 
            // textBoxUserPassword
            // 
            textBoxUserPassword.Location = new Point(76, 114);
            textBoxUserPassword.Margin = new Padding(4, 3, 4, 3);
            textBoxUserPassword.Name = "textBoxUserPassword";
            textBoxUserPassword.Size = new Size(148, 23);
            textBoxUserPassword.TabIndex = 18;
            // 
            // checkBoxPassword
            // 
            checkBoxPassword.AutoSize = true;
            checkBoxPassword.Location = new Point(240, 120);
            checkBoxPassword.Margin = new Padding(4, 3, 4, 3);
            checkBoxPassword.Name = "checkBoxPassword";
            checkBoxPassword.Size = new Size(138, 19);
            checkBoxPassword.TabIndex = 19;
            checkBoxPassword.Text = "Descifrar/Cifrar Clave";
            checkBoxPassword.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(7, 39);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(18, 15);
            label7.TabIndex = 21;
            label7.Text = "ID";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(117, 39);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(51, 15);
            label8.TabIndex = 22;
            label8.Text = "Nombre";
            // 
            // textBoxRolNombre
            // 
            textBoxRolNombre.Location = new Point(175, 36);
            textBoxRolNombre.Margin = new Padding(4, 3, 4, 3);
            textBoxRolNombre.Name = "textBoxRolNombre";
            textBoxRolNombre.Size = new Size(148, 23);
            textBoxRolNombre.TabIndex = 24;
            // 
            // btnAltaRol
            // 
            btnAltaRol.Location = new Point(7, 80);
            btnAltaRol.Margin = new Padding(4, 3, 4, 3);
            btnAltaRol.Name = "btnAltaRol";
            btnAltaRol.Size = new Size(88, 35);
            btnAltaRol.TabIndex = 25;
            btnAltaRol.Text = "Alta";
            btnAltaRol.UseVisualStyleBackColor = true;
            // 
            // btnModificarRol
            // 
            btnModificarRol.Location = new Point(120, 80);
            btnModificarRol.Margin = new Padding(4, 3, 4, 3);
            btnModificarRol.Name = "btnModificarRol";
            btnModificarRol.Size = new Size(88, 35);
            btnModificarRol.TabIndex = 26;
            btnModificarRol.Text = "Modificar";
            btnModificarRol.UseVisualStyleBackColor = true;
            // 
            // btnEliminarRol
            // 
            btnEliminarRol.Location = new Point(236, 80);
            btnEliminarRol.Margin = new Padding(4, 3, 4, 3);
            btnEliminarRol.Name = "btnEliminarRol";
            btnEliminarRol.Size = new Size(88, 35);
            btnEliminarRol.TabIndex = 27;
            btnEliminarRol.Text = "Eliminar";
            btnEliminarRol.UseVisualStyleBackColor = true;
            // 
            // btnAsociarRolUsuario
            // 
            btnAsociarRolUsuario.Location = new Point(74, 40);
            btnAsociarRolUsuario.Margin = new Padding(4, 3, 4, 3);
            btnAsociarRolUsuario.Name = "btnAsociarRolUsuario";
            btnAsociarRolUsuario.Size = new Size(163, 30);
            btnAsociarRolUsuario.TabIndex = 34;
            btnAsociarRolUsuario.Text = "Asociar Rol a Usuario";
            btnAsociarRolUsuario.UseVisualStyleBackColor = true;
            // 
            // btnDesasociarRolUsuario
            // 
            btnDesasociarRolUsuario.Location = new Point(74, 88);
            btnDesasociarRolUsuario.Margin = new Padding(4, 3, 4, 3);
            btnDesasociarRolUsuario.Name = "btnDesasociarRolUsuario";
            btnDesasociarRolUsuario.Size = new Size(163, 31);
            btnDesasociarRolUsuario.TabIndex = 35;
            btnDesasociarRolUsuario.Text = "Desasociar Rol a Usuario";
            btnDesasociarRolUsuario.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(7, 31);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(18, 15);
            label14.TabIndex = 37;
            label14.Text = "ID";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(114, 31);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(51, 15);
            label15.TabIndex = 38;
            label15.Text = "Nombre";
            // 
            // textBoxRolId
            // 
            textBoxRolId.Location = new Point(35, 36);
            textBoxRolId.Margin = new Padding(4, 3, 4, 3);
            textBoxRolId.Name = "textBoxRolId";
            textBoxRolId.Size = new Size(56, 23);
            textBoxRolId.TabIndex = 39;
            // 
            // textBoxPermisoId
            // 
            textBoxPermisoId.Location = new Point(35, 28);
            textBoxPermisoId.Margin = new Padding(4, 3, 4, 3);
            textBoxPermisoId.Name = "textBoxPermisoId";
            textBoxPermisoId.Size = new Size(56, 23);
            textBoxPermisoId.TabIndex = 40;
            // 
            // textBoxPermisoNombre
            // 
            textBoxPermisoNombre.Location = new Point(173, 28);
            textBoxPermisoNombre.Margin = new Padding(4, 3, 4, 3);
            textBoxPermisoNombre.Name = "textBoxPermisoNombre";
            textBoxPermisoNombre.Size = new Size(148, 23);
            textBoxPermisoNombre.TabIndex = 41;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(7, 69);
            label16.Margin = new Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new Size(38, 15);
            label16.TabIndex = 42;
            label16.Text = "Menu";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(169, 69);
            label17.Margin = new Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new Size(31, 15);
            label17.TabIndex = 43;
            label17.Text = "Item";
            // 
            // comboBoxMenu
            // 
            comboBoxMenu.FormattingEnabled = true;
            comboBoxMenu.Location = new Point(54, 66);
            comboBoxMenu.Margin = new Padding(4, 3, 4, 3);
            comboBoxMenu.Name = "comboBoxMenu";
            comboBoxMenu.Size = new Size(104, 23);
            comboBoxMenu.TabIndex = 44;
            // 
            // comboBoxItem
            // 
            comboBoxItem.FormattingEnabled = true;
            comboBoxItem.Location = new Point(208, 66);
            comboBoxItem.Margin = new Padding(4, 3, 4, 3);
            comboBoxItem.Name = "comboBoxItem";
            comboBoxItem.Size = new Size(126, 23);
            comboBoxItem.TabIndex = 45;
            // 
            // btnAltaPermiso
            // 
            btnAltaPermiso.Location = new Point(35, 114);
            btnAltaPermiso.Margin = new Padding(4, 3, 4, 3);
            btnAltaPermiso.Name = "btnAltaPermiso";
            btnAltaPermiso.Size = new Size(118, 37);
            btnAltaPermiso.TabIndex = 46;
            btnAltaPermiso.Text = "Alta Permiso";
            btnAltaPermiso.UseVisualStyleBackColor = true;
            // 
            // btnEliminarPermiso
            // 
            btnEliminarPermiso.Location = new Point(203, 114);
            btnEliminarPermiso.Margin = new Padding(4, 3, 4, 3);
            btnEliminarPermiso.Name = "btnEliminarPermiso";
            btnEliminarPermiso.Size = new Size(118, 37);
            btnEliminarPermiso.TabIndex = 47;
            btnEliminarPermiso.Text = "Eliminar Permiso";
            btnEliminarPermiso.UseVisualStyleBackColor = true;
            // 
            // btnAsociarPermisoARol
            // 
            btnAsociarPermisoARol.Location = new Point(7, 27);
            btnAsociarPermisoARol.Margin = new Padding(4, 3, 4, 3);
            btnAsociarPermisoARol.Name = "btnAsociarPermisoARol";
            btnAsociarPermisoARol.Size = new Size(146, 38);
            btnAsociarPermisoARol.TabIndex = 49;
            btnAsociarPermisoARol.Text = "Asociar Permiso a Rol";
            btnAsociarPermisoARol.UseVisualStyleBackColor = true;
            // 
            // btnQuitarPermisoARol
            // 
            btnQuitarPermisoARol.Location = new Point(161, 27);
            btnQuitarPermisoARol.Margin = new Padding(4, 3, 4, 3);
            btnQuitarPermisoARol.Name = "btnQuitarPermisoARol";
            btnQuitarPermisoARol.Size = new Size(128, 38);
            btnQuitarPermisoARol.TabIndex = 50;
            btnQuitarPermisoARol.Text = "Quitar Permiso a Rol";
            btnQuitarPermisoARol.UseVisualStyleBackColor = true;
            // 
            // btnAsociarPermisoAUsuario
            // 
            btnAsociarPermisoAUsuario.Location = new Point(1, 35);
            btnAsociarPermisoAUsuario.Margin = new Padding(4, 3, 4, 3);
            btnAsociarPermisoAUsuario.Name = "btnAsociarPermisoAUsuario";
            btnAsociarPermisoAUsuario.Size = new Size(161, 36);
            btnAsociarPermisoAUsuario.TabIndex = 58;
            btnAsociarPermisoAUsuario.Text = "Asociar Permiso a Usuario";
            btnAsociarPermisoAUsuario.UseVisualStyleBackColor = true;
            // 
            // btnQuitarPermisoUsuario
            // 
            btnQuitarPermisoUsuario.Location = new Point(186, 35);
            btnQuitarPermisoUsuario.Margin = new Padding(4, 3, 4, 3);
            btnQuitarPermisoUsuario.Name = "btnQuitarPermisoUsuario";
            btnQuitarPermisoUsuario.Size = new Size(164, 36);
            btnQuitarPermisoUsuario.TabIndex = 59;
            btnQuitarPermisoUsuario.Text = "Quitar Permiso a Usuario";
            btnQuitarPermisoUsuario.UseVisualStyleBackColor = true;
            // 
            // groupBoxUsuario
            // 
            groupBoxUsuario.Controls.Add(textBoxUserId);
            groupBoxUsuario.Controls.Add(label3);
            groupBoxUsuario.Controls.Add(label4);
            groupBoxUsuario.Controls.Add(textBoxUsename);
            groupBoxUsuario.Controls.Add(label5);
            groupBoxUsuario.Controls.Add(textBoxUserPassword);
            groupBoxUsuario.Controls.Add(checkBoxPassword);
            groupBoxUsuario.Location = new Point(14, 14);
            groupBoxUsuario.Margin = new Padding(4, 3, 4, 3);
            groupBoxUsuario.Name = "groupBoxUsuario";
            groupBoxUsuario.Padding = new Padding(4, 3, 4, 3);
            groupBoxUsuario.Size = new Size(396, 151);
            groupBoxUsuario.TabIndex = 60;
            groupBoxUsuario.TabStop = false;
            groupBoxUsuario.Text = "Usuario";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Location = new Point(14, 171);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(396, 134);
            groupBox2.TabIndex = 61;
            groupBox2.TabStop = false;
            groupBox2.Text = "Roles / Permisos Usuario";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(btnAsociarPermisoAUsuario);
            groupBox4.Controls.Add(btnQuitarPermisoUsuario);
            groupBox4.Location = new Point(7, 33);
            groupBox4.Margin = new Padding(4, 3, 4, 3);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(4, 3, 4, 3);
            groupBox4.Size = new Size(379, 95);
            groupBox4.TabIndex = 57;
            groupBox4.TabStop = false;
            groupBox4.Text = "Permisos a Usuario";
            // 
            // btnAsignarPermisosSeleccionados
            // 
            btnAsignarPermisosSeleccionados.Location = new Point(83, 71);
            btnAsignarPermisosSeleccionados.Margin = new Padding(4, 3, 4, 3);
            btnAsignarPermisosSeleccionados.Name = "btnAsignarPermisosSeleccionados";
            btnAsignarPermisosSeleccionados.Size = new Size(161, 42);
            btnAsignarPermisosSeleccionados.TabIndex = 60;
            btnAsignarPermisosSeleccionados.Text = "Asignar permisos seleccionados";
            btnAsignarPermisosSeleccionados.UseVisualStyleBackColor = true;
            btnAsignarPermisosSeleccionados.Click += btnAsignarPermisosSeleccionados_Click_1;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(groupBox6);
            groupBox5.Controls.Add(label7);
            groupBox5.Controls.Add(textBoxRolId);
            groupBox5.Controls.Add(label8);
            groupBox5.Controls.Add(textBoxRolNombre);
            groupBox5.Controls.Add(btnAltaRol);
            groupBox5.Controls.Add(btnModificarRol);
            groupBox5.Controls.Add(btnEliminarRol);
            groupBox5.Location = new Point(430, 14);
            groupBox5.Margin = new Padding(4, 3, 4, 3);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(4, 3, 4, 3);
            groupBox5.Size = new Size(363, 291);
            groupBox5.TabIndex = 62;
            groupBox5.TabStop = false;
            groupBox5.Text = "Rol";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(btnAsociarRolUsuario);
            groupBox6.Controls.Add(btnDesasociarRolUsuario);
            groupBox6.Location = new Point(10, 142);
            groupBox6.Margin = new Padding(4, 3, 4, 3);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(4, 3, 4, 3);
            groupBox6.Size = new Size(345, 136);
            groupBox6.TabIndex = 40;
            groupBox6.TabStop = false;
            groupBox6.Text = " Asociar / Desasociar Rol a Usuario";
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(label14);
            groupBox7.Controls.Add(textBoxPermisoId);
            groupBox7.Controls.Add(label15);
            groupBox7.Controls.Add(textBoxPermisoNombre);
            groupBox7.Controls.Add(label16);
            groupBox7.Controls.Add(comboBoxMenu);
            groupBox7.Controls.Add(label17);
            groupBox7.Controls.Add(comboBoxItem);
            groupBox7.Controls.Add(btnAltaPermiso);
            groupBox7.Controls.Add(btnEliminarPermiso);
            groupBox7.Location = new Point(813, 14);
            groupBox7.Margin = new Padding(4, 3, 4, 3);
            groupBox7.Name = "groupBox7";
            groupBox7.Padding = new Padding(4, 3, 4, 3);
            groupBox7.Size = new Size(377, 157);
            groupBox7.TabIndex = 63;
            groupBox7.TabStop = false;
            groupBox7.Text = "Permiso";
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(btnAsignarPermisosSeleccionados);
            groupBox8.Controls.Add(btnAsociarPermisoARol);
            groupBox8.Controls.Add(btnQuitarPermisoARol);
            groupBox8.Location = new Point(813, 177);
            groupBox8.Margin = new Padding(4, 3, 4, 3);
            groupBox8.Name = "groupBox8";
            groupBox8.Padding = new Padding(4, 3, 4, 3);
            groupBox8.Size = new Size(301, 128);
            groupBox8.TabIndex = 64;
            groupBox8.TabStop = false;
            groupBox8.Text = "Opciones Roles / Permisos";
            // 
            // UC_AsignarRoles
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox8);
            Controls.Add(groupBox7);
            Controls.Add(groupBox5);
            Controls.Add(groupBox2);
            Controls.Add(groupBoxUsuario);
            Controls.Add(treeViewUsuarios);
            Controls.Add(label1);
            Controls.Add(trvPermisosDelUsuario);
            Controls.Add(lblPermisoPorRol);
            Controls.Add(trvPermisosPorRol);
            Controls.Add(lblPermisos);
            Controls.Add(trvPermisos);
            Controls.Add(lblRoles);
            Controls.Add(lblUsuariosSistema);
            Controls.Add(trvPermisosRoles);
            Margin = new Padding(2);
            Name = "UC_AsignarRoles";
            Size = new Size(1280, 720);
            groupBoxUsuario.ResumeLayout(false);
            groupBoxUsuario.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox8.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvPermisosRoles;
        private System.Windows.Forms.Label lblUsuariosSistema;
        private System.Windows.Forms.Label lblRoles;
        private System.Windows.Forms.TreeView trvPermisos;
        private System.Windows.Forms.Label lblPermisos;
        private System.Windows.Forms.TreeView trvPermisosPorRol;
        private System.Windows.Forms.Label lblPermisoPorRol;
        private System.Windows.Forms.TreeView trvPermisosDelUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeViewUsuarios;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxUserId;
        private System.Windows.Forms.TextBox textBoxUsename;
        private System.Windows.Forms.TextBox textBoxUserPassword;
        private System.Windows.Forms.CheckBox checkBoxPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxRolNombre;
        private System.Windows.Forms.Button btnAltaRol;
        private System.Windows.Forms.Button btnModificarRol;
        private System.Windows.Forms.Button btnEliminarRol;
        private System.Windows.Forms.Button btnAsociarRolUsuario;
        private System.Windows.Forms.Button btnDesasociarRolUsuario;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxRolId;
        private System.Windows.Forms.TextBox textBoxPermisoId;
        private System.Windows.Forms.TextBox textBoxPermisoNombre;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboBoxMenu;
        private System.Windows.Forms.ComboBox comboBoxItem;
        private System.Windows.Forms.Button btnAltaPermiso;
        private System.Windows.Forms.Button btnEliminarPermiso;
        private System.Windows.Forms.Button btnAsociarPermisoARol;
        private System.Windows.Forms.Button btnQuitarPermisoARol;
        private System.Windows.Forms.Button btnAsociarPermisoAUsuario;
        private System.Windows.Forms.Button btnQuitarPermisoUsuario;
        private System.Windows.Forms.GroupBox groupBoxUsuario;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private Button btnAsignarPermisosSeleccionados;
    }
}