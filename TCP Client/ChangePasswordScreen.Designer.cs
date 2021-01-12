using System.ComponentModel;

namespace TCP_Client
{
    partial class ChangePasswordScreen
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNewPwd = new System.Windows.Forms.TextBox();
            this.textBoxOldPwd = new System.Windows.Forms.TextBox();
            this.checkBoxShowPass = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(241, 242);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(110, 29);
            this.buttonBack.TabIndex = 11;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(101, 242);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(134, 29);
            this.buttonSubmit.TabIndex = 10;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(36, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(339, 39);
            this.label1.TabIndex = 6;
            this.label1.Text = "Changing password";
            // 
            // textBoxNewPwd
            // 
            this.textBoxNewPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxNewPwd.ForeColor = System.Drawing.Color.Silver;
            this.textBoxNewPwd.Location = new System.Drawing.Point(101, 189);
            this.textBoxNewPwd.Name = "textBoxNewPwd";
            this.textBoxNewPwd.PasswordChar = '*';
            this.textBoxNewPwd.Size = new System.Drawing.Size(134, 29);
            this.textBoxNewPwd.TabIndex = 8;
            this.textBoxNewPwd.Text = "Password";
            this.textBoxNewPwd.Enter += new System.EventHandler(this.textBoxNewPwd_Enter);
            this.textBoxNewPwd.Leave += new System.EventHandler(this.textBoxNewPwd_Leave);
            // 
            // textBoxOldPwd
            // 
            this.textBoxOldPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxOldPwd.ForeColor = System.Drawing.Color.Silver;
            this.textBoxOldPwd.Location = new System.Drawing.Point(101, 134);
            this.textBoxOldPwd.Name = "textBoxOldPwd";
            this.textBoxOldPwd.PasswordChar = '*';
            this.textBoxOldPwd.Size = new System.Drawing.Size(134, 29);
            this.textBoxOldPwd.TabIndex = 7;
            this.textBoxOldPwd.Text = "Password";
            this.textBoxOldPwd.Enter += new System.EventHandler(this.textBoxOldPwd_Enter);
            this.textBoxOldPwd.Leave += new System.EventHandler(this.textBoxOldPwd_Leave);
            // 
            // checkBoxShowPass
            // 
            this.checkBoxShowPass.AutoSize = true;
            this.checkBoxShowPass.Location = new System.Drawing.Point(263, 199);
            this.checkBoxShowPass.Name = "checkBoxShowPass";
            this.checkBoxShowPass.Size = new System.Drawing.Size(101, 17);
            this.checkBoxShowPass.TabIndex = 9;
            this.checkBoxShowPass.Text = "Show password";
            this.checkBoxShowPass.UseVisualStyleBackColor = true;
            this.checkBoxShowPass.CheckedChanged += new System.EventHandler(this.checkBoxShowPass_CheckedChanged);
            // 
            // ChangePasswordScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxNewPwd);
            this.Controls.Add(this.textBoxOldPwd);
            this.Controls.Add(this.checkBoxShowPass);
            this.Name = "ChangePasswordScreen";
            this.Size = new System.Drawing.Size(400, 300);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button buttonSubmit;

        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.CheckBox checkBoxShowPass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxOldPwd;
        private System.Windows.Forms.TextBox textBoxNewPwd;

        #endregion
    }
}