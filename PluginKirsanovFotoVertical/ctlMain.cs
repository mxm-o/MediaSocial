// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Drawing;
using System.Windows.Forms;
using PluginInterface;
using PhotoEdit;
using System.Reflection;
using System.IO;

namespace PluginKirsanovFotoVertical
{
	/// <summary>
	/// Summary description for ctlMain.
	/// </summary>
	public class ctlMain : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctlMain()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
            // TODO: Add any initialization after the InitializeComponent call
        }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxT2 = new System.Windows.Forms.TextBox();
            this.labelT2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(166, 4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(33, 23);
            this.btnHelp.TabIndex = 12;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.butSend_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(85, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(4, 4);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 10;
            this.btnCreate.Text = "Создать";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.textBoxT2);
            this.panel2.Controls.Add(this.labelT2);
            this.panel2.Location = new System.Drawing.Point(2, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(195, 71);
            this.panel2.TabIndex = 9;
            // 
            // textBoxT2
            // 
            this.textBoxT2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxT2.Location = new System.Drawing.Point(7, 20);
            this.textBoxT2.MaxLength = 150;
            this.textBoxT2.Multiline = true;
            this.textBoxT2.Name = "textBoxT2";
            this.textBoxT2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxT2.Size = new System.Drawing.Size(185, 48);
            this.textBoxT2.TabIndex = 1;
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.Location = new System.Drawing.Point(4, 4);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(86, 13);
            this.labelT2.TabIndex = 0;
            this.labelT2.Text = "Источник фото:";
            // 
            // ctlMain
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.panel2);
            this.MinimumSize = new System.Drawing.Size(200, 110);
            this.Name = "ctlMain";
            this.Size = new System.Drawing.Size(200, 110);
            this.Load += new System.EventHandler(this.ctlMain_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		IPluginHost myHost = null;
        private Button btnHelp;
        private Button btnClear;
        private Button btnCreate;
        private Panel panel2;
        private TextBox textBoxT2;
        private Label labelT2;
        IPlugin myPlugin = null;

		public IPluginHost PluginHost
		{
			get {return myHost;}
			set {myHost = value;}
		}
		public IPlugin Plugin
		{
			get {return myPlugin;}
			set {myPlugin = value;}
		}

		private void butSend_Click(object sender, System.EventArgs e)
		{
			myHost.Feedback(myPlugin.Feed, myPlugin);
		}

		private void ctlMain_Load(object sender, System.EventArgs e)
		{
		
		}

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            textBoxT2.Text = "";
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            // Проверка исходных данных
            if (!File.Exists(Path.Combine(path, "main.png")))
            {
                MessageBox.Show("Плагин поврежден. Обратитесь к разработчику.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Считываем шаблонное изображение
            Image imageMain = null;
            try
            {
                imageMain = Image.FromStream(new MemoryStream(File.ReadAllBytes(Path.Combine(path, "main.png"))));
            } catch
            {

            }
            this.Enabled = false;

            // Генерируем новое изображение
            PhotoGenerate photo = new PhotoGenerate();
            photo.height = imageMain.Height;
            photo.width = imageMain.Width;
            photo.colorMain = Color.White;
            Image imageOut = photo.Fill();
            // Получаем исходник фотографии
            Image ImageSouser = myHost.SendImages()[0];
            // Создаем слой с размытием основного фото
            PhotoSize resize = new PhotoSize();
            resize.width = imageMain.Width;
            resize.height = imageMain.Height;
            resize.source = ImageSouser;
            Image ImageBlur = resize.ScaleImage();
            BlurImage blurImage = new BlurImage();
            ImageBlur = blurImage.Blur(ImageBlur, 25);

            // Раскладываем по слоям
            Merge mergeImage = new Merge();
            mergeImage.sourceBottom = imageOut;
            mergeImage.sourceTop = ImageBlur;
            imageOut = mergeImage.MergeImage(); // На фон помещаем размытую фотографию
            mergeImage.sourceBottom = imageOut;
            mergeImage.sourceTop = ImageSouser;
            mergeImage.left = 238;
            imageOut = mergeImage.MergeImage(); // Помещаем основное фото
            mergeImage.sourceBottom = imageOut;
            mergeImage.sourceTop = imageMain;
            mergeImage.left = 0;
            imageOut = mergeImage.MergeImage(); // Помещаем оформление
            
            // Пишем автора фото (если нужно)
            if (textBoxT2.Text != "")
            {
                TextGenerate textAutor = new TextGenerate();
                textAutor.textString = "Фото: " + textBoxT2.Text;
                textAutor.rectH = 100;
                textAutor.rectW = 920;
                textAutor.rectX = 820;
                textAutor.rectY = 1670;
                textAutor.fontStyle = FontStyle.Bold;
                textAutor.source = imageOut;
                textAutor.fontSize = 40;
                textAutor.colorText = Color.White;
                textAutor.stringAlignment = StringAlignment.Far;
                textAutor.fontName = "Lato";
                //textAutor.debug = true;
                imageOut = textAutor.DrawTextWithEffects();
            }
            myHost.ReciveImage(imageOut);
            this.Enabled = true;
        }
    }
}
