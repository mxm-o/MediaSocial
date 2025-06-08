// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Drawing;
using System.Windows.Forms;
using PluginInterface;
using PhotoEdit;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;

namespace PluginKirsanovNews
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
            LoadingLayers();
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
            this.comboBoxLayers = new System.Windows.Forms.ComboBox();
            this.checkBoxLayers = new System.Windows.Forms.CheckBox();
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
            this.panelTitle.Controls.Add(this.comboBoxLayers);
            this.panelTitle.Controls.Add(this.checkBoxLayers);
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
            this.panelTitle.Size = new System.Drawing.Size(200, 299);
            this.panelTitle.TabIndex = 8;
            // 
            // comboBoxLayers
            // 
            this.comboBoxLayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLayers.Enabled = false;
            this.comboBoxLayers.FormattingEnabled = true;
            this.comboBoxLayers.Location = new System.Drawing.Point(3, 275);
            this.comboBoxLayers.Name = "comboBoxLayers";
            this.comboBoxLayers.Size = new System.Drawing.Size(193, 21);
            this.comboBoxLayers.TabIndex = 17;
            // 
            // checkBoxLayers
            // 
            this.checkBoxLayers.AutoSize = true;
            this.checkBoxLayers.Location = new System.Drawing.Point(7, 258);
            this.checkBoxLayers.Name = "checkBoxLayers";
            this.checkBoxLayers.Size = new System.Drawing.Size(51, 17);
            this.checkBoxLayers.TabIndex = 16;
            this.checkBoxLayers.Text = "Слои";
            this.checkBoxLayers.UseVisualStyleBackColor = true;
            this.checkBoxLayers.CheckedChanged += new System.EventHandler(this.checkBoxLayers_CheckedChanged);
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
            this.MinimumSize = new System.Drawing.Size(200, 474);
            this.Name = "ctlMain";
            this.Size = new System.Drawing.Size(200, 474);
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
        private ComboBox comboBoxLayers;
        private CheckBox checkBoxLayers;
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

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            // Собираем данные из UI в основном потоке
            var parameters = new ImageCreationParams
            {
                TitleEnabled = checkBoxTitle.Checked,
                TitleText = textBoxT1.Text,
                SourceText = textBoxT2.Text,
                SelectedImage = comboBoxImages.SelectedItem?.ToString(),
                FormatIndex = comboBoxFormat.SelectedIndex,
                FontSize = numericUpDown1.Value,
                LayersEnabled = checkBoxLayers.Checked,
                SelectedLayer = comboBoxLayers.SelectedItem?.ToString(),
                SourceImage = myHost.SendImages()[0],
                Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            };

            // Проверки в UI-потоке
            if (parameters.TitleEnabled && string.IsNullOrEmpty(parameters.TitleText))
            {
                MessageBox.Show("Отсутствует заголовок.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (parameters.SourceImage == null)
            {
                MessageBox.Show("Не выбрано изображение.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] requiredFiles = {
                Path.Combine(parameters.Path, "main.png"),
                Path.Combine(parameters.Path, "title.png"),
                Path.Combine(parameters.Path, "back.png"),
                Path.Combine(parameters.Path, "images", parameters.SelectedImage + ".png")
            };

            foreach (var file in requiredFiles)
            {
                if (!File.Exists(file))
                {
                    MessageBox.Show("Плагин поврежден. Обратитесь к разработчику.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (!CheckFont.FontExists("Lato"))
            {
                MessageBox.Show("В системе отсутствуют нужные шрифты. Проверьте наличие требуемых шрифтов к данному дизайну.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Enabled = false;

            myHost.IsExecuting(true);
            myHost.Progress(0);
            try
            {
                // Запуск создания изображения в фоновом потоке
                var result = await Task.Run(() => CreateImage(parameters));

                if (result.error != null)
                {
                    MessageBox.Show(result.error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (result.image != null)
                {
                    myHost.ReciveImage(result.image);
                }
            }
            finally
            {
                this.Enabled = true;
                myHost.IsExecuting(false);
                myHost.Progress(100);
            }
        }

        private (Image image, Exception error) CreateImage(ImageCreationParams parameters)
        {
            int totalProgress = 0;
            try
            {
                // Этап 1: Загрузка основных изображений (10%)
                myHost.Progress(10);
                Image imageMain = LoadImageSafe(Path.Combine(parameters.Path, "main.png"));
                Image imageBack = null;
                Image imageTitle = null;

                if (parameters.FormatIndex > 0)
                {
                    imageBack = LoadImageSafe(Path.Combine(parameters.Path, "back.png"));
                }
                if (parameters.TitleEnabled && !string.IsNullOrEmpty(parameters.TitleText))
                {
                    imageTitle = LoadImageSafe(Path.Combine(parameters.Path, "title.png"));
                }
                totalProgress = 15;
                myHost.Progress(totalProgress);

                // Этап 2: Подготовка базового изображения (5%)
                PhotoGenerate photo = new PhotoGenerate();
                photo.height = imageMain.Height;
                photo.width = imageMain.Width;
                photo.colorMain = Color.White;
                Image imageOut = photo.Fill();
                totalProgress += 5;
                myHost.Progress(totalProgress);

                // Этап 3: Обработка фона (10%)
                Image ImageBlur = null;
                if (parameters.FormatIndex > 0)
                {
                    PhotoSize resize = new PhotoSize();
                    resize.width = imageMain.Width;
                    resize.height = imageMain.Height;
                    resize.source = parameters.SourceImage;
                    ImageBlur = resize.ScaleImage();

                    BlurImage blurImage = new BlurImage();
                    BlurImage.ProgressBlurChanged += (percent) =>
                    {
                        totalProgress = 20 + percent/10;
                        myHost.Progress(totalProgress);
                    };
                    ImageBlur = blurImage.Blur(ImageBlur, 25);

                    Merge mergeImage = new Merge();
                    mergeImage.sourceBottom = imageOut;
                    mergeImage.sourceTop = ImageBlur;
                    imageOut = mergeImage.MergeImage();

                    mergeImage.sourceBottom = imageOut;
                    mergeImage.sourceTop = imageBack;
                    imageOut = mergeImage.MergeImage();
                }
                totalProgress = 30;
                myHost.Progress(totalProgress);

                // Этап 4: Позиционирование основного изображения (10%)
                Merge mergeMain = new Merge();
                switch (parameters.FormatIndex)
                {
                    case 0:
                        mergeMain.left = 0;
                        mergeMain.top = 0;
                        break;
                    case 1:
                        mergeMain.left = 0;
                        mergeMain.top = imageTitle != null ? 178 : 282;
                        break;
                    case 2:
                        mergeMain.left = 239;
                        mergeMain.top = 0;
                        break;
                }

                mergeMain.sourceBottom = imageOut;
                mergeMain.sourceTop = parameters.SourceImage;
                imageOut = mergeMain.MergeImage();
                mergeMain.left = 0;
                mergeMain.top = 0;

                mergeMain.sourceBottom = imageOut;
                mergeMain.sourceTop = imageMain;
                imageOut = mergeMain.MergeImage();
                totalProgress += 10;
                myHost.Progress(totalProgress);

                // Этап 5: Обработка заголовка новости (30%)
                if (imageTitle != null && !string.IsNullOrEmpty(parameters.TitleText))
                {
                    // Наложение фона заголовка (5%)
                    mergeMain.sourceBottom = imageOut;
                    mergeMain.sourceTop = imageTitle;
                    imageOut = mergeMain.MergeImage();
                    totalProgress += 5;
                    myHost.Progress(totalProgress);

                    // Добавление иконки (5%)
                    try
                    {
                        var iconPath = Path.Combine(parameters.Path, "images", parameters.SelectedImage + ".png");
                        mergeMain.sourceBottom = imageOut;
                        mergeMain.sourceTop = LoadImageSafe(iconPath);
                        mergeMain.top = 1385;
                        mergeMain.left = 85;
                        imageOut = mergeMain.MergeImage();
                    }
                    catch { /* Обработка ошибок уже есть выше */ }
                    totalProgress += 5;
                    myHost.Progress(totalProgress);

                    // Генерация текста (20%)
                    TextGenerate textE = new TextGenerate();
                    textE.textString = parameters.TitleText;
                    textE.rectH = 250;
                    textE.rectW = 1380;
                    textE.rectX = 375;
                    textE.rectY = 1395;
                    textE.source = imageOut;
                    textE.fontSize = (float)parameters.FontSize;
                    textE.colorText = Color.Black;
                    textE.fontName = "Lato";
                    textE.lineAlignment = StringAlignment.Center;
                    imageOut = textE.DrawTextWithEffects();
                    totalProgress += 20;
                    myHost.Progress(totalProgress);
                }
                else
                {
                    totalProgress += 30;
                    myHost.Progress(totalProgress);
                }

                // Этап 6: Добавление источника фото (15%)
                if (!string.IsNullOrEmpty(parameters.SourceText))
                {
                    TextGenerate textAutor = new TextGenerate();
                    textAutor.textString = "Фото: " + parameters.SourceText;
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
                    totalProgress += 15;
                    myHost.Progress(totalProgress);
                }
                else
                {
                    totalProgress += 15;
                    myHost.Progress(totalProgress);
                }

                // Этап 7: Добавление слоев (10%)
                if (parameters.LayersEnabled)
                {
                    var layerPath = Path.Combine(parameters.Path, "layers", parameters.SelectedLayer + ".png");
                    mergeMain.sourceBottom = imageOut;
                    mergeMain.sourceTop = LoadImageSafe(layerPath);
                    mergeMain.top = 0;
                    mergeMain.left = 0;
                    imageOut = mergeMain.MergeImage();
                }
                totalProgress += 10;
                myHost.Progress(totalProgress);

                // Финализация (10%)
                myHost.Progress(100);
                return (imageOut, null);
            }
            catch (Exception ex)
            {
                // При ошибке все равно сообщаем о 100% завершении
                myHost.Progress(100);
                return (null, ex);
            }
        }

        private Image LoadImageSafe(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found", path);

            return Image.FromStream(new MemoryStream(File.ReadAllBytes(path)));
        }

        // Класс для передачи параметров создания изображения
        private class ImageCreationParams
        {
            public bool TitleEnabled { get; set; }
            public string TitleText { get; set; }
            public string SourceText { get; set; }
            public string SelectedImage { get; set; }
            public int FormatIndex { get; set; }
            public decimal FontSize { get; set; }
            public bool LayersEnabled { get; set; }
            public string SelectedLayer { get; set; }
            public Image SourceImage { get; set; }
            public string Path { get; set; }
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

        // Загрузка списка дополнительных слоев для заголовка новости
        private void LoadingLayers()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            bool layers = false;
            if (Directory.Exists(Path.Combine(path, "layers")))
            {
                string[] files = Directory.GetFiles(Path.Combine(path, "layers"), "*.png");
                if (files.Length > 0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        comboBoxLayers.Items.Add(files[i].Replace(Path.Combine(path, "layers\\"), "").Replace(".png", ""));
                        layers = true;
                    }
                    comboBoxLayers.SelectedIndex = 0;
                }
            }
            if (layers == false)
            {
                comboBoxLayers.Visible = false;
                checkBoxLayers.Visible = false;
                checkBoxLayers.Enabled = false;
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

        // Переключение состояния возможности выбора слоев
        private void checkBoxLayers_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxLayers.Enabled = checkBoxLayers.Checked;
        }
    }
}
