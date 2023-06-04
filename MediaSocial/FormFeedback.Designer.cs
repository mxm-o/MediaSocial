namespace MediaSocial
{
    partial class FormFeedback
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblFeedback = new System.Windows.Forms.Label();
            this.lblPluginAuthor = new System.Windows.Forms.Label();
            this.lblPluginVersion = new System.Windows.Forms.Label();
            this.lblPluginName = new System.Windows.Forms.Label();
            this.lblPluginDesc = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblFeedback);
            this.groupBox2.Controls.Add(this.lblPluginAuthor);
            this.groupBox2.Controls.Add(this.lblPluginVersion);
            this.groupBox2.Controls.Add(this.lblPluginName);
            this.groupBox2.Controls.Add(this.lblPluginDesc);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 141);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Информация о модуле:";
            // 
            // lblFeedback
            // 
            this.lblFeedback.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFeedback.Location = new System.Drawing.Point(8, 105);
            this.lblFeedback.Name = "lblFeedback";
            this.lblFeedback.Size = new System.Drawing.Size(341, 28);
            this.lblFeedback.TabIndex = 0;
            // 
            // lblPluginAuthor
            // 
            this.lblPluginAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPluginAuthor.Location = new System.Drawing.Point(8, 56);
            this.lblPluginAuthor.Name = "lblPluginAuthor";
            this.lblPluginAuthor.Size = new System.Drawing.Size(341, 16);
            this.lblPluginAuthor.TabIndex = 5;
            this.lblPluginAuthor.Text = "By: <Author\'s Name>";
            // 
            // lblPluginVersion
            // 
            this.lblPluginVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPluginVersion.Location = new System.Drawing.Point(8, 32);
            this.lblPluginVersion.Name = "lblPluginVersion";
            this.lblPluginVersion.Size = new System.Drawing.Size(341, 16);
            this.lblPluginVersion.TabIndex = 4;
            this.lblPluginVersion.Text = "(<Version>)";
            // 
            // lblPluginName
            // 
            this.lblPluginName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPluginName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPluginName.Location = new System.Drawing.Point(8, 16);
            this.lblPluginName.Name = "lblPluginName";
            this.lblPluginName.Size = new System.Drawing.Size(341, 16);
            this.lblPluginName.TabIndex = 3;
            this.lblPluginName.Text = "<Plugin Name Here>";
            // 
            // lblPluginDesc
            // 
            this.lblPluginDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPluginDesc.Location = new System.Drawing.Point(16, 72);
            this.lblPluginDesc.Name = "lblPluginDesc";
            this.lblPluginDesc.Size = new System.Drawing.Size(333, 33);
            this.lblPluginDesc.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(111, 159);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(160, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FormFeedback
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 190);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.Name = "FormFeedback";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Информация";
            this.Load += new System.EventHandler(this.FormFeedback_Load);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblPluginAuthor;
        private System.Windows.Forms.Label lblPluginVersion;
        private System.Windows.Forms.Label lblPluginName;
        private System.Windows.Forms.Label lblPluginDesc;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblFeedback;
    }
}