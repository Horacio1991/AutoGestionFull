namespace AutoGestion.UI
{
    partial class FormLogin
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
            label1 = new Label();
            label2 = new Label();
            txtClave = new TextBox();
            txtUsuario = new TextBox();
            btnIngresar = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(466, 257);
            label1.Name = "label1";
            label1.Size = new Size(83, 51);
            label1.TabIndex = 0;
            label1.Text = "Usuario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Sans Serif Collection", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(466, 308);
            label2.Name = "label2";
            label2.Size = new Size(110, 51);
            label2.TabIndex = 1;
            label2.Text = "Contraseña";
            // 
            // txtClave
            // 
            txtClave.Location = new Point(582, 318);
            txtClave.Name = "txtClave";
            txtClave.PasswordChar = '*';
            txtClave.Size = new Size(191, 23);
            txtClave.TabIndex = 2;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(582, 267);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(191, 23);
            txtUsuario.TabIndex = 1;
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(582, 370);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(191, 44);
            btnIngresar.TabIndex = 3;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(btnIngresar);
            Controls.Add(txtUsuario);
            Controls.Add(txtClave);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormLogin";
            Text = "FormLogin";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtClave;
        private TextBox txtUsuario;
        private Button btnIngresar;
    }
}