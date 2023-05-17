using System;
using System.Drawing;
using System.Windows.Forms;
using PluginInterface;
using PhotoEdit;
using System.Reflection;
using System.IO;

namespace Plugin1D
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
            LoadingImages();

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
            this.labelTextLeght = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.textBoxT1 = new System.Windows.Forms.TextBox();
            this.labelT1 = new System.Windows.Forms.Label();
            this.comboBoxImages = new System.Windows.Forms.ComboBox();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
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
            this.panel2.Location = new System.Drawing.Point(2, 135);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(195, 71);
            this.panel2.TabIndex = 9;
            // 
            // textBoxT2
            // 
            this.textBoxT2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxT2.Location = new System.Drawing.Point(7, 20);
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
            this.panel1.Controls.Add(this.labelTextLeght);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.textBoxT1);
            this.panel1.Controls.Add(this.labelT1);
            this.panel1.Location = new System.Drawing.Point(3, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 96);
            this.panel1.TabIndex = 8;
            // 
            // labelTextLeght
            // 
            this.labelTextLeght.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTextLeght.Location = new System.Drawing.Point(130, 4);
            this.labelTextLeght.Name = "labelTextLeght";
            this.labelTextLeght.Size = new System.Drawing.Size(61, 13);
            this.labelTextLeght.TabIndex = 4;
            this.labelTextLeght.Text = "0";
            this.labelTextLeght.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Размер шрифта";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(122, 73);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(69, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // textBoxT1
            // 
            this.textBoxT1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxT1.Location = new System.Drawing.Point(7, 20);
            this.textBoxT1.Multiline = true;
            this.textBoxT1.Name = "textBoxT1";
            this.textBoxT1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxT1.Size = new System.Drawing.Size(184, 48);
            this.textBoxT1.TabIndex = 1;
            this.textBoxT1.TextChanged += new System.EventHandler(this.textBoxT1_TextChanged);
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.Location = new System.Drawing.Point(4, 4);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(61, 13);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "Заголовок";
            // 
            // comboBoxImages
            // 
            this.comboBoxImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxImages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxImages.FormattingEnabled = true;
            this.comboBoxImages.Location = new System.Drawing.Point(10, 232);
            this.comboBoxImages.Name = "comboBoxImages";
            this.comboBoxImages.Size = new System.Drawing.Size(187, 21);
            this.comboBoxImages.TabIndex = 13;
            this.comboBoxImages.SelectedIndexChanged += new System.EventHandler(this.comboBoxImages_SelectedIndexChanged);
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.BackColor = System.Drawing.Color.SlateBlue;
            this.pictureBoxImage.Location = new System.Drawing.Point(9, 260);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(188, 107);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxImage.TabIndex = 14;
            this.pictureBoxImage.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Иконка новости";
            // 
            // ctlMain
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBoxImage);
            this.Controls.Add(this.comboBoxImages);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(200, 370);
            this.Name = "ctlMain";
            this.Size = new System.Drawing.Size(200, 370);
            this.Load += new System.EventHandler(this.ctlMain_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private TextBox textBoxT1;
        private Label labelT1;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private ComboBox comboBoxImages;
        private PictureBox pictureBoxImage;
        private Label label2;
        private Label labelTextLeght;
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
            textBoxT1.Text = "";
            textBoxT2.Text = "";
            if (comboBoxImages.Items.Count > 0) comboBoxImages.SelectedIndex = 0;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            // Проверка исходных данных
            if (textBoxT1.Text == "")
            {
                MessageBox.Show("Отсутствует заголовок.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!(File.Exists(Path.Combine(path, "main.png")) || File.Exists(Path.Combine(path, comboBoxImages.SelectedItem.ToString() + ".png"))))
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
            // Генерируем новое изображение
            PhotoGenerate photo = new PhotoGenerate();
            photo.height = imageMain.Height;
            photo.width = imageMain.Width;
            photo.colorMain = Color.White;
            Image imageOut = photo.Fill();
            // Получаем исходник фотографии
            Image ImageSouser = myHost.SendImages()[0];
            // Раскладываем по слоям
            Merge mergeImage = new Merge();
            mergeImage.sourceBottom = imageOut;
            mergeImage.sourceTop = ImageSouser;
            imageOut = mergeImage.MergeImage(); // На фон помещаем фотографию
            mergeImage.sourceBottom = imageOut;
            mergeImage.sourceTop = imageMain;
            imageOut = mergeImage.MergeImage(); // Помещаем оформление
            // Рисуем иконку новости
            try
            {
                mergeImage.sourceBottom = imageOut;
                mergeImage.sourceTop = Image.FromStream(new MemoryStream(File.ReadAllBytes(Path.Combine(path, "images", comboBoxImages.SelectedItem.ToString() + ".png"))));
                mergeImage.top = 1385;
                mergeImage.left = 85;
                imageOut = mergeImage.MergeImage();
            } catch
            {

            }
            // Пишем текст названия
            TextGenerate textE = new TextGenerate();
            textE.textString = textBoxT1.Text;
            textE.rectH = 250;
            textE.rectW = 1380;
            textE.rectX = 375;
            textE.rectY = 1395;
            textE.source = imageOut;
            textE.fontSize = (float)numericUpDown1.Value;
            textE.colorText = Color.Black;
            textE.fontName = "Lato";
            //textE.debug = true;
            imageOut = textE.DrawTextWithEffects();
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
                imageOut = textAutor.DrawTextWithEffects();
            }
            myHost.ReciveImage(imageOut);

        }

        // Загрузка списка иконок для заголовка новости
        private void LoadingImages()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string[] files = Directory.GetFiles(Path.Combine(path, "images"), "*.png");
            if (files.Length > 0)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    comboBoxImages.Items.Add(files[i].Replace(Path.Combine(path, "images\\"), "").Replace(".png", ""));
                }
                    comboBoxImages.SelectedIndex = 0;
            }
        }

        // Выбор изображения иконки новости
        private void comboBoxImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "images");
            try
            {
                Image image = Image.FromStream(new MemoryStream(File.ReadAllBytes(Path.Combine(path, comboBoxImages.SelectedItem.ToString() + ".png"))));
                pictureBoxImage.Image = image;
            } catch
            {

            }
        }
        // Расчет размера шрифта
        private void textBoxT1_TextChanged(object sender, EventArgs e)
        {
            // Расчет размера шрифта
            CalculateFontSize fontSize = new CalculateFontSize();
            fontSize.baseFontSize = 105;
            fontSize.fontSizeRatio = 0.5;
            numericUpDown1.Value = fontSize.FontSize(textBoxT1.Text);
            // Отображение длины текста
            labelTextLeght.Text = textBoxT1.Text.Length.ToString();
            labelTextLeght.ForeColor = textBoxT1.Text.Length > 60 ? Color.Red : System.Drawing.SystemColors.ControlText;
        }
    }
}
