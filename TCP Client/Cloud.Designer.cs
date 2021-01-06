namespace TCP_Client
{
    partial class Cloud
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.textBoxFileText = new System.Windows.Forms.RichTextBox();
            this.textBoxNewFileName = new System.Windows.Forms.TextBox();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonAddNewFile = new System.Windows.Forms.Button();
            this.buttonShowFile = new System.Windows.Forms.Button();
            this.buttonSaveFile = new System.Windows.Forms.Button();
            this.buttonDeleteFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonChangePwd = new System.Windows.Forms.Button();
            this.buttonLogs = new System.Windows.Forms.Button();
            this.buttonUser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.Location = new System.Drawing.Point(3, 116);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(120, 290);
            this.listBoxFiles.TabIndex = 0;
            this.listBoxFiles.SelectedIndexChanged += new System.EventHandler(this.listBoxFiles_SelectedIndexChanged);
            // 
            // textBoxFileText
            // 
            this.textBoxFileText.Location = new System.Drawing.Point(126, 116);
            this.textBoxFileText.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxFileText.Name = "textBoxFileText";
            this.textBoxFileText.Size = new System.Drawing.Size(371, 348);
            this.textBoxFileText.TabIndex = 2;
            this.textBoxFileText.Text = "";
            // 
            // textBoxNewFileName
            // 
            this.textBoxNewFileName.Location = new System.Drawing.Point(397, 45);
            this.textBoxNewFileName.Name = "textBoxNewFileName";
            this.textBoxNewFileName.Size = new System.Drawing.Size(100, 20);
            this.textBoxNewFileName.TabIndex = 3;
            this.textBoxNewFileName.Enter += new System.EventHandler(this.textBoxNewFileName_Enter);
            this.textBoxNewFileName.Leave += new System.EventHandler(this.textBoxNewFileName_Leave);
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(3, 3);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(120, 107);
            this.buttonLogout.TabIndex = 4;
            this.buttonLogout.Text = "Log out";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // buttonAddNewFile
            // 
            this.buttonAddNewFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonAddNewFile.Location = new System.Drawing.Point(255, 3);
            this.buttonAddNewFile.Name = "buttonAddNewFile";
            this.buttonAddNewFile.Size = new System.Drawing.Size(120, 107);
            this.buttonAddNewFile.TabIndex = 6;
            this.buttonAddNewFile.Text = "Add new file";
            this.buttonAddNewFile.UseVisualStyleBackColor = true;
            this.buttonAddNewFile.Click += new System.EventHandler(this.buttonAddNewFile_Click);
            // 
            // buttonShowFile
            // 
            this.buttonShowFile.Location = new System.Drawing.Point(3, 412);
            this.buttonShowFile.Name = "buttonShowFile";
            this.buttonShowFile.Size = new System.Drawing.Size(119, 23);
            this.buttonShowFile.TabIndex = 7;
            this.buttonShowFile.Text = "Show file";
            this.buttonShowFile.UseVisualStyleBackColor = true;
            this.buttonShowFile.Click += new System.EventHandler(this.buttonShowFile_Click);
            // 
            // buttonSaveFile
            // 
            this.buttonSaveFile.Location = new System.Drawing.Point(4, 441);
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.Size = new System.Drawing.Size(119, 23);
            this.buttonSaveFile.TabIndex = 8;
            this.buttonSaveFile.Text = "Save file";
            this.buttonSaveFile.UseVisualStyleBackColor = true;
            this.buttonSaveFile.Click += new System.EventHandler(this.buttonSaveFile_Click);
            // 
            // buttonDeleteFile
            // 
            this.buttonDeleteFile.Location = new System.Drawing.Point(4, 471);
            this.buttonDeleteFile.Name = "buttonDeleteFile";
            this.buttonDeleteFile.Size = new System.Drawing.Size(119, 23);
            this.buttonDeleteFile.TabIndex = 9;
            this.buttonDeleteFile.Text = "Delete file";
            this.buttonDeleteFile.UseVisualStyleBackColor = true;
            this.buttonDeleteFile.Click += new System.EventHandler(this.buttonDeleteFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(399, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "File name";
            // 
            // buttonChangePwd
            // 
            this.buttonChangePwd.Location = new System.Drawing.Point(129, 3);
            this.buttonChangePwd.Name = "buttonChangePwd";
            this.buttonChangePwd.Size = new System.Drawing.Size(120, 107);
            this.buttonChangePwd.TabIndex = 11;
            this.buttonChangePwd.Text = "Change password";
            this.buttonChangePwd.UseVisualStyleBackColor = true;
            this.buttonChangePwd.Click += new System.EventHandler(this.buttonChangePwd_Click);
            // 
            // buttonLogs
            // 
            this.buttonLogs.Location = new System.Drawing.Point(422, 471);
            this.buttonLogs.Name = "buttonLogs";
            this.buttonLogs.Size = new System.Drawing.Size(75, 23);
            this.buttonLogs.TabIndex = 12;
            this.buttonLogs.Text = "Logs";
            this.buttonLogs.UseVisualStyleBackColor = true;
            this.buttonLogs.Click += new System.EventHandler(this.buttonLogs_Click);
            // 
            // buttonUser
            // 
            this.buttonUser.Location = new System.Drawing.Point(341, 471);
            this.buttonUser.Name = "buttonUser";
            this.buttonUser.Size = new System.Drawing.Size(75, 23);
            this.buttonUser.TabIndex = 13;
            this.buttonUser.Text = "User";
            this.buttonUser.UseVisualStyleBackColor = true;
            this.buttonUser.Click += new System.EventHandler(this.buttonUser_Click);
            // 
            // Cloud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonUser);
            this.Controls.Add(this.buttonLogs);
            this.Controls.Add(this.buttonChangePwd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDeleteFile);
            this.Controls.Add(this.buttonSaveFile);
            this.Controls.Add(this.buttonShowFile);
            this.Controls.Add(this.buttonAddNewFile);
            this.Controls.Add(this.buttonLogout);
            this.Controls.Add(this.textBoxNewFileName);
            this.Controls.Add(this.textBoxFileText);
            this.Controls.Add(this.listBoxFiles);
            this.Name = "Cloud";
            this.Size = new System.Drawing.Size(500, 500);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button buttonChangePwd;

        #endregion
        private System.Windows.Forms.RichTextBox textBoxFileText;
        private System.Windows.Forms.TextBox textBoxNewFileName;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Button buttonAddNewFile;
        private System.Windows.Forms.Button buttonShowFile;
        private System.Windows.Forms.Button buttonSaveFile;
        private System.Windows.Forms.Button buttonDeleteFile;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLogs;
        private System.Windows.Forms.Button buttonUser;
    }
}
