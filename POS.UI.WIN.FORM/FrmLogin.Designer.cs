namespace POS.UI.WIN.FORM
{
    partial class FrmLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtUsuario = new TextBox();
            btnLogin = new Button();
            txtClave = new TextBox();
            label2 = new Label();
            txtNit = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 21);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 0;
            label1.Text = "Usuario";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(29, 39);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(131, 23);
            txtUsuario.TabIndex = 1;
            txtUsuario.Text = "Admin";
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(29, 207);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(131, 23);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Iniciar sesión";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtClave
            // 
            txtClave.Location = new Point(29, 95);
            txtClave.Name = "txtClave";
            txtClave.Size = new Size(131, 23);
            txtClave.TabIndex = 4;
            txtClave.Text = "12345678";
            txtClave.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 77);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 3;
            label2.Text = "Password";
            // 
            // txtNit
            // 
            txtNit.Location = new Point(29, 157);
            txtNit.Name = "txtNit";
            txtNit.Size = new Size(131, 23);
            txtNit.TabIndex = 6;
            txtNit.Text = "0987654321";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(29, 139);
            label3.Name = "label3";
            label3.Size = new Size(25, 15);
            label3.TabIndex = 5;
            label3.Text = "NIT";
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(193, 261);
            Controls.Add(txtNit);
            Controls.Add(label3);
            Controls.Add(txtClave);
            Controls.Add(label2);
            Controls.Add(btnLogin);
            Controls.Add(txtUsuario);
            Controls.Add(label1);
            Name = "FrmLogin";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtUsuario;
        private Button btnLogin;
        private TextBox txtClave;
        private Label label2;
        private TextBox txtNit;
        private Label label3;
    }
}
