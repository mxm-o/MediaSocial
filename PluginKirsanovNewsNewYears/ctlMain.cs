// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Drawing;
using System.Windows.Forms;
using PluginInterface;
using PhotoEdit;
using System.Reflection;
using System.IO;

namespace PluginKirsanovNewsNewYears
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.panelCopyright = new System.Windows.Forms.Panel();
            this.textBoxT2 = new System.Windows.Forms.TextBox();
            this.labelT2 = new System.Windows.Forms.Label();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxImages = new System.Windows.Forms.ComboBox();
            this.labelTextLeght = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.textBoxT1 = new System.Windows.Forms.TextBox();
            this.labelT1 = new System.Windows.Forms.Label();
            this.checkBoxTitle = new System.Windows.Forms.CheckBox();
            this.panelHead = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.panelCopyright.SuspendLayout();
            this.panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panelHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(165, 3);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(33, 23);
            this.btnHelp.TabIndex = 12;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.butSend_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(84, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(3, 3);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 10;
            this.btnCreate.Text = "Создать";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // panelCopyright
            // 
            this.panelCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCopyright.Controls.Add(this.textBoxT2);
            this.panelCopyright.Controls.Add(this.labelT2);
            this.panelCopyright.Location = new System.Drawing.Point(0, 77);
            this.panelCopyright.Name = "panelCopyright";
            this.panelCopyright.Size = new System.Drawing.Size(200, 71);
            this.panelCopyright.TabIndex = 9;
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
            this.textBoxT2.Size = new System.Drawing.Size(190, 48);
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
            // panelTitle
            // 
            this.panelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTitle.Controls.Add(this.pictureBoxImage);
            this.panelTitle.Controls.Add(this.label2);
            this.panelTitle.Controls.Add(this.comboBoxImages);
            this.panelTitle.Controls.Add(this.labelTextLeght);
            this.panelTitle.Controls.Add(this.label1);
            this.panelTitle.Controls.Add(this.numericUpDown1);
            this.panelTitle.Controls.Add(this.textBoxT1);
            this.panelTitle.Controls.Add(this.labelT1);
            this.panelTitle.Location = new System.Drawing.Point(0, 173);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(200, 259);
            this.panelTitle.TabIndex = 8;
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.BackColor = System.Drawing.Color.DodgerBlue;
            this.pictureBoxImage.Location = new System.Drawing.Point(3, 145);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(195, 107);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxImage.TabIndex = 14;
            this.pictureBoxImage.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Иконка новости";
            // 
            // comboBoxImages
            // 
            this.comboBoxImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxImages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxImages.FormattingEnabled = true;
            this.comboBoxImages.Location = new System.Drawing.Point(3, 118);
            this.comboBoxImages.Name = "comboBoxImages";
            this.comboBoxImages.Size = new System.Drawing.Size(193, 21);
            this.comboBoxImages.TabIndex = 13;
            this.comboBoxImages.SelectedIndexChanged += new System.EventHandler(this.comboBoxImages_SelectedIndexChanged);
            // 
            // labelTextLeght
            // 
            this.labelTextLeght.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTextLeght.Location = new System.Drawing.Point(136, 4);
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
            this.numericUpDown1.Location = new System.Drawing.Point(129, 74);
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
            this.textBoxT1.MaxLength = 300;
            this.textBoxT1.Multiline = true;
            this.textBoxT1.Name = "textBoxT1";
            this.textBoxT1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxT1.Size = new System.Drawing.Size(190, 48);
            this.textBoxT1.TabIndex = 1;
            this.textBoxT1.TextChanged += new System.EventHandler(this.textBoxT1_TextChanged);
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.Location = new System.Drawing.Point(4, 4);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(57, 13);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "Название";
            // 
            // checkBoxTitle
            // 
            this.checkBoxTitle.AutoSize = true;
            this.checkBoxTitle.Location = new System.Drawing.Point(5, 153);
            this.checkBoxTitle.Name = "checkBoxTitle";
            this.checkBoxTitle.Size = new System.Drawing.Size(124, 17);
            this.checkBoxTitle.TabIndex = 13;
            this.checkBoxTitle.Text = "Заголовок новости";
            this.checkBoxTitle.UseVisualStyleBackColor = true;
            this.checkBoxTitle.CheckedChanged += new System.EventHandler(this.checkBoxTitle_CheckedChanged);
            // 
            // panelHead
            // 
            this.panelHead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHead.Controls.Add(this.label3);
            this.panelHead.Controls.Add(this.comboBoxFormat);
            this.panelHead.Controls.Add(this.btnCreate);
            this.panelHead.Controls.Add(this.btnClear);
            this.panelHead.Controls.Add(this.btnHelp);
            this.panelHead.Location = new System.Drawing.Point(0, 0);
            this.panelHead.Name = "panelHead";
            this.panelHead.Size = new System.Drawing.Size(200, 71);
            this.panelHead.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Расположение фото:";
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Items.AddRange(new object[] {
            "Полный экран",
            "Горизонтальная",
            "Вертикальная"});
            this.comboBoxFormat.Location = new System.Drawing.Point(2, 44);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new System.Drawing.Size(187, 21);
            this.comboBoxFormat.TabIndex = 16;
            this.comboBoxFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxFormat_SelectedIndexChanged);
            // 
            // ctlMain
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelHead);
            this.Controls.Add(this.panelCopyright);
            this.Controls.Add(this.checkBoxTitle);
            this.Controls.Add(this.panelTitle);
            this.MinimumSize = new System.Drawing.Size(200, 370);
            this.Name = "ctlMain";
            this.Size = new System.Drawing.Size(200, 435);
            this.Load += new System.EventHandler(this.ctlMain_Load);
            this.panelCopyright.ResumeLayout(false);
            this.panelCopyright.PerformLayout();
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panelHead.ResumeLayout(false);
            this.panelHead.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        IPluginHost myHost = null;
        private Button btnHelp;
        private Button btnClear;
        private Button btnCreate;
        private Panel panelCopyright;
        private TextBox textBoxT2;
        private Label labelT2;
        private Panel panelTitle;
        private TextBox textBoxT1;
        private Label labelT1;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private ComboBox comboBoxImages;
        private PictureBox pictureBoxImage;
        private Label label2;
        private Label labelTextLeght;
        private CheckBox checkBoxTitle;
        private Panel panelHead;
        private Label label3;
        private ComboBox comboBoxFormat;
        IPlugin myPlugin = null;

        public IPluginHost PluginHost
        {
            get { return myHost; }
            set { myHost = value; }
        }
        public IPlugin Plugin
        {
            get { return myPlugin; }
            set { myPlugin = value; }
        }

        private void butSend_Click(object sender, System.EventArgs e)
        {
            myHost.Feedback(myPlugin.Feed, myPlugin);
        }

        private void ctlMain_Load(object sender, System.EventArgs e)
        {
            comboBoxFormat.SelectedIndex = 0;
            panelTitle.Enabled = checkBoxTitle.Checked;
            panelTitle.Visible = checkBoxTitle.Checked;
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
            if (checkBoxTitle.Checked && textBoxT1.Text == "")
            {
                MessageBox.Show("Отсутствует заголовок.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Получаем исходник фотографии
            Image ImageSouser = myHost.SendImages()[0];
            if (ImageSouser == null)
            {
                MessageBox.Show("Не выбрано изображение.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!(File.Exists(Path.Combine(path, "main.png"))) || !(File.Exists(Path.Combine(path, "title.png"))) || !(File.Exists(Path.Combine(path, "back.png"))) || File.Exists(Path.Combine(path, comboBoxImages.SelectedItem.ToString() + ".png")))
            {
                MessageBox.Show("Плагин поврежден. Обратитесь к разработчику.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!CheckFont.FontExists("Lato"))
            {
                MessageBox.Show("В системе отсутствуют нужные шрифты. Проверьте наличие требуемых шрифтов к данному дизайну.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Задаем основные изображения
            Image imageMain = null;  // Дизайн
            Image imageBack = null; // Фон
            Image imageTitle = null; // Название
            Image ImageBlur = null; // Размытое фото

            // Считываем изображениt основного дизайна
            try
            {
                imageMain = Image.FromStream(new MemoryStream(File.ReadAllBytes(Path.Combine(path, "main.png"))));
            }
            catch
            {

            }

            if (comboBoxFormat.SelectedIndex > 0)
            {
                // Считываем фоновое изображение
                try
                {
                    imageBack = Image.FromStream(new MemoryStream(File.ReadAllBytes(Path.Combine(path, "back.png"))));
                }
                catch
                {

                }
            }

            if (checkBoxTitle.Checked && textBoxT1.Text != "")
            {

                // Считываем изображение для названия
                try
                {
                    imageTitle = Image.FromStream(new MemoryStream(File.ReadAllBytes(Path.Combine(path, "title.png"))));
                }
                catch
                {

                }
            }
            this.Enabled = false;

            // Генерируем новое изображение
            PhotoGenerate photo = new PhotoGenerate();
            photo.height = imageMain.Height;
            photo.width = imageMain.Width;
            photo.colorMain = Color.White;
            Image imageOut = photo.Fill();

            if (comboBoxFormat.SelectedIndex > 0)
            {
                // Создаем слой с размытием основного фото
                PhotoSize resize = new PhotoSize();
                resize.width = imageMain.Width;
                resize.height = imageMain.Height;
                resize.source = ImageSouser;
                ImageBlur = resize.ScaleImage();
                BlurImage blurImage = new BlurImage();
                ImageBlur = blurImage.Blur(ImageBlur, 25);
            }

            // Раскладываем по слоям
            Merge mergeImage = new Merge();

            // На фон помещаем размытую фотографию если формат не квадрат
            if (comboBoxFormat.SelectedIndex > 0)
            {
                mergeImage.sourceBottom = imageOut;
                mergeImage.sourceTop = ImageBlur;
                imageOut = mergeImage.MergeImage();
                // Помещаем прозрачный синий фон
                mergeImage.sourceBottom = imageOut;
                mergeImage.sourceTop = imageBack;
                imageOut = mergeImage.MergeImage();
            }

            // Помещаем основное фото
            // Задаем координаты
            switch (comboBoxFormat.SelectedIndex)
            {
                case 0:
                    mergeImage.left = 0;
                    mergeImage.top = 0;
                    break;
                case 1:
                    mergeImage.left = 0;
                    if (imageTitle != null && textBoxT1.Text != "")
                    {
                        mergeImage.top = 178;
                    } else
                    {
                        mergeImage.top = 282;
                    }
                    break;
                case 2:
                    mergeImage.left = 239;
                    mergeImage.top = 0;
                    break;
                default:
                    mergeImage.left = 0;
                    mergeImage.top = 0;
                    break;
            }

            mergeImage.sourceBottom = imageOut;
            mergeImage.sourceTop = ImageSouser;
            imageOut = mergeImage.MergeImage();
            mergeImage.left = 0;
            mergeImage.top = 0;

            // Помещаем оформление
            mergeImage.sourceBottom = imageOut;
            mergeImage.sourceTop = imageMain;
            imageOut = mergeImage.MergeImage();


            // Помещаем фон названия новости
            if (imageTitle != null && textBoxT1.Text != "") {

                // Помещаем  оформление фона
                mergeImage.sourceBottom = imageOut;
                mergeImage.sourceTop = imageTitle;
                imageOut = mergeImage.MergeImage();

                // Рисуем иконку новости
                try
                {
                    mergeImage.sourceBottom = imageOut;
                    mergeImage.sourceTop = Image.FromStream(new MemoryStream(File.ReadAllBytes(Path.Combine(path, "images", comboBoxImages.SelectedItem.ToString() + ".png"))));
                    mergeImage.top = 1385;
                    mergeImage.left = 85;
                    imageOut = mergeImage.MergeImage();
                }
                catch
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
                textE.lineAlignment = StringAlignment.Center;
                //textE.debug = true;
                imageOut = textE.DrawTextWithEffects();

            }
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
            }
            catch
            {

            }
        }
        // Расчет размера шрифта
        private void textBoxT1_TextChanged(object sender, EventArgs e)
        {
            // Расчет размера шрифта
            CalculateFontSize fontSize = new CalculateFontSize();
            fontSize.baseFontSize = 100;
            fontSize.fontSizeRatio = 0.5;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            decimal fontsize = (decimal)fontSize.CalcMaxFontSize(textBoxT1.Text, "Lato", fontSize.FontSize(textBoxT1.Text), FontStyle.Regular, 1380, 250, stringFormat);
            if (fontsize < numericUpDown1.Minimum) fontsize = numericUpDown1.Minimum;
            if (fontsize > numericUpDown1.Maximum) fontsize = numericUpDown1.Maximum;
            numericUpDown1.Value = fontsize;
            // Отображение длины текста
            labelTextLeght.Text = textBoxT1.Text.Length.ToString();
            labelTextLeght.ForeColor = textBoxT1.Text.Length > 60 ? Color.Red : SystemColors.ControlText;
        }

        private void checkBoxTitle_CheckedChanged(object sender, EventArgs e)
        {
            panelTitle.Enabled = checkBoxTitle.Checked;
            panelTitle.Visible = checkBoxTitle.Checked;
        }

        private void comboBoxFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxFormat.SelectedIndex)
            {
                case 0:
                    Plugin.SizesList[0].SizeY = 1800;
                    Plugin.SizesList[0].SizeX = 1800;
                    break;
                case 1:
                    Plugin.SizesList[0].SizeY = 1800;
                    Plugin.SizesList[0].SizeX = 1218;
                    break;
                case 2:
                    Plugin.SizesList[0].SizeY = 1363;
                    Plugin.SizesList[0].SizeX = 1800;
                    break;
                default:
                    Plugin.SizesList[0].SizeY = 1800;
                    Plugin.SizesList[0].SizeX = 1800;
                    break;
            }
            myHost.RenderEditor();
        }
    }
}
