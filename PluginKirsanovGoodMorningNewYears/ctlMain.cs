// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Drawing;
using System.Windows.Forms;
using PluginInterface;
using PhotoEdit;
using System.Reflection;
using System.IO;

namespace PluginKirsanovGoodMorningNewYears
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelT1 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.panel2.Location = new System.Drawing.Point(3, 84);
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dateTimePicker);
            this.panel1.Controls.Add(this.labelT1);
            this.panel1.Location = new System.Drawing.Point(3, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 45);
            this.panel1.TabIndex = 8;
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.Location = new System.Drawing.Point(4, 4);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(33, 13);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "Дата";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Checked = false;
            this.dateTimePicker.Location = new System.Drawing.Point(7, 20);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(184, 20);
            this.dateTimePicker.TabIndex = 1;
            // 
            // ctlMain
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(200, 160);
            this.Name = "ctlMain";
            this.Size = new System.Drawing.Size(200, 160);
            this.Load += new System.EventHandler(this.ctlMain_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private Panel panel1;
        private Label labelT1;
        private DateTimePicker dateTimePicker;
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
            dateTimePicker.ResetText();
            textBoxT2.Text = "";
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            // Считываем шаблонное изображение
            Image imageMain = null;
            try
            {
                imageMain = Image.FromStream(new MemoryStream(File.ReadAllBytes(Path.Combine(path, "main.png"))));
            } catch
            {

            }

            // Получаем исходник фотографии
            Image ImageSouser = myHost.SendImages()[0];
            if (ImageSouser == null)
            {
                MessageBox.Show("Не выбрано изображение.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!CheckFont.FontExists("Lato"))
            {
                MessageBox.Show("В системе отсутствуют нужные шрифты. Проверьте наличие требуемых шрифтов к данному дизайну.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Enabled = false;

            // Генерируем новое изображение
            PhotoGenerate photo = new PhotoGenerate();
            photo.height = imageMain.Height;
            photo.width = imageMain.Width;
            photo.colorMain = Color.White;
            Image imageOut = photo.Fill();
            // Раскладываем по слоям
            Merge mergeImage = new Merge();
            mergeImage.sourceBottom = imageOut;
            mergeImage.sourceTop = ImageSouser;
            mergeImage.top = 140;
            mergeImage.left = 22;
            imageOut = mergeImage.MergeImage(); // На фон помещаем фотографию
            mergeImage.sourceBottom = imageOut;
            mergeImage.sourceTop = imageMain;
            mergeImage.top = 0;
            mergeImage.left = 0;
            imageOut = mergeImage.MergeImage(); // Помещаем оформление
            // Пишем дату
            TextGenerate textE = new TextGenerate();
            textE.textString = dateTimePicker.Value.ToString("d MMMM yyyy");
            textE.rectH = 100;
            textE.rectW = 1000;
            textE.rectX = 430;
            textE.rectY = 30;
            textE.source = imageOut;
            textE.fontSize = 90;
            textE.colorText = Color.White;
            textE.fontName = "Lato";
            textE.lineAlignment = StringAlignment.Center;
            textE.stringAlignment = StringAlignment.Far;
            //textE.debug = true;
            imageOut = textE.DrawTextWithEffects();
            // Пишем восход
            TextGenerate textSet = new TextGenerate();
            SunSetRise sunSet = new SunSetRise();
            sunSet.dt = dateTimePicker.Value;
            textSet.textString = sunSet.SunRise();
            textSet.rectH = 80;
            textSet.rectW = 250;
            textSet.rectX = 170;
            textSet.rectY = 1290;
            textSet.source = imageOut;
            textSet.fontSize = 80;
            textSet.colorText = Color.White;
            textSet.fontName = "Lato";
            textSet.lineAlignment = StringAlignment.Center;
            textSet.stringAlignment = StringAlignment.Center;
            //textSet.debug = true;
            imageOut = textSet.DrawTextWithEffects();
            // Пишем закат
            textSet.textString = sunSet.SunSet();
            textSet.rectY = 1400;
            textSet.source = imageOut;
            imageOut = textSet.DrawTextWithEffects();
            // Пишем Долготу
            textSet.textString = sunSet.DayDuration();
            textSet.rectY = 1510;
            textSet.source = imageOut;
            imageOut = textSet.DrawTextWithEffects();

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
            Enabled = true;
        }
    }
}
