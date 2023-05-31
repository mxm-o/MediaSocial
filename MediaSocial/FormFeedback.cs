// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Windows.Forms;

namespace MediaSocial
{
    public partial class FormFeedback : Form
    {
        public FormFeedback()
        {
            InitializeComponent();
        }


        public string PluginAuthor
        {
            get { return this.lblPluginAuthor.Text; }
            set { this.lblPluginAuthor.Text = value; }
        }

        public string PluginDesc
        {
            get { return this.lblPluginDesc.Text; }
            set { this.lblPluginDesc.Text = value; }
        }

        public string PluginName
        {
            get { return this.lblPluginName.Text; }
            set { this.lblPluginName.Text = value; }
        }
        public string PluginVersion
        {
            get { return this.lblPluginVersion.Text; }
            set { this.lblPluginVersion.Text = value; }
        }
        public string Feedback
        {
            get { return this.lblFeedback.Text; }
            set { this.lblFeedback.Text = value; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
