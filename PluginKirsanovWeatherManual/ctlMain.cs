// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Drawing;
using System.Windows.Forms;
using PluginInterface;
using PhotoEdit;
using System.Reflection;
using System.IO;

namespace PluginKirsanovWeatherManual
{
    /// <summary>
    /// Summary description for ctlMain.
    /// </summary>
    public class ctlMain : UserControl
    {
        private System.ComponentModel.IContainer components;

        public ctlMain()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            // TODO: Add any initialization after the InitializeComponent call
            LoadingImages();
            // Проверка наличия HtmlAgilityPack.dll
            try
            {
                // Пробуем загрузить сборку
                Assembly.Load("HtmlAgilityPack");
            }
            catch
            {
                panelAuto.Enabled = false;
                panelAuto.Visible = false;
            }
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
            this.components = new System.ComponentModel.Container();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.labelT1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSetting = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.labelUpdate = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.comboBoxImages = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewWeather = new PluginKirsanovWeatherManual.DoubleBufferedDataGridView();
            this.globalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelAuto = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWeather)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.globalBindingSource)).BeginInit();
            this.panelAuto.SuspendLayout();
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dateTimePicker);
            this.panel1.Controls.Add(this.labelT1);
            this.panel1.Location = new System.Drawing.Point(3, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 45);
            this.panel1.TabIndex = 8;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Checked = false;
            this.dateTimePicker.CustomFormat = "";
            this.dateTimePicker.Location = new System.Drawing.Point(7, 20);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(150, 20);
            this.dateTimePicker.TabIndex = 1;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
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
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(3, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(250, 244);
            this.panel2.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Данные";
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(168, 3);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(75, 23);
            this.btnSetting.TabIndex = 14;
            this.btnSetting.Text = "Настройки";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(94, 3);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(68, 23);
            this.buttonUpdate.TabIndex = 15;
            this.buttonUpdate.Text = "Обновить";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // labelUpdate
            // 
            this.labelUpdate.Location = new System.Drawing.Point(4, 29);
            this.labelUpdate.Name = "labelUpdate";
            this.labelUpdate.Size = new System.Drawing.Size(239, 13);
            this.labelUpdate.TabIndex = 16;
            this.labelUpdate.Text = "Прогноз обновлён: ";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.comboBoxImages);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(3, 334);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(250, 45);
            this.panel3.TabIndex = 17;
            // 
            // comboBoxImages
            // 
            this.comboBoxImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxImages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxImages.FormattingEnabled = true;
            this.comboBoxImages.Location = new System.Drawing.Point(7, 21);
            this.comboBoxImages.Name = "comboBoxImages";
            this.comboBoxImages.Size = new System.Drawing.Size(236, 21);
            this.comboBoxImages.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Фон";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Обработать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridViewWeather
            // 
            this.dataGridViewWeather.AllowUserToAddRows = false;
            this.dataGridViewWeather.AllowUserToDeleteRows = false;
            this.dataGridViewWeather.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewWeather.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWeather.Location = new System.Drawing.Point(6, 104);
            this.dataGridViewWeather.Name = "dataGridViewWeather";
            this.dataGridViewWeather.Size = new System.Drawing.Size(240, 221);
            this.dataGridViewWeather.TabIndex = 1;
            this.dataGridViewWeather.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewWeather_CellEndEdit);
            this.dataGridViewWeather.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewWeather_CellValueChanged);
            this.dataGridViewWeather.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridViewWeather_DataBindingComplete);
            this.dataGridViewWeather.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewWeather_DataError);
            // 
            // globalBindingSource
            // 
            this.globalBindingSource.DataSource = typeof(PluginKirsanovWeatherManual.Global);
            // 
            // panelAuto
            // 
            this.panelAuto.Controls.Add(this.btnSetting);
            this.panelAuto.Controls.Add(this.button1);
            this.panelAuto.Controls.Add(this.buttonUpdate);
            this.panelAuto.Controls.Add(this.labelUpdate);
            this.panelAuto.Location = new System.Drawing.Point(3, 382);
            this.panelAuto.Name = "panelAuto";
            this.panelAuto.Size = new System.Drawing.Size(250, 48);
            this.panelAuto.TabIndex = 19;
            // 
            // ctlMain
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelAuto);
            this.Controls.Add(this.dataGridViewWeather);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(250, 430);
            this.Name = "ctlMain";
            this.Size = new System.Drawing.Size(256, 433);
            this.Load += new System.EventHandler(this.ctlMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWeather)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.globalBindingSource)).EndInit();
            this.panelAuto.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        IPluginHost myHost = null;
        private Button btnHelp;
        private Button btnClear;
        private Button btnCreate;
        private Panel panel1;
        private Label labelT1;
        private DateTimePicker dateTimePicker;
        private Panel panel2;
        private DoubleBufferedDataGridView dataGridViewWeather;
        private Label label1;
        private Button btnSetting;
        private Button buttonUpdate;
        private Label labelUpdate;
        private Panel panel3;
        private ComboBox comboBoxImages;
        private Label label2;
        private BindingSource globalBindingSource;
        IPlugin myPlugin = null;
        private Panel panelAuto;
        private Button button1;

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
            Clear();
            generateWeathersTable();
            labelUpdate.Text = "Прогноз обновлен: " + Global.siteTime.ToString();
        }

        private void generateWeathersTable()
        {
            dataGridViewWeather.DataSource = Global.weatherSetting;
        }

        private void dataGridSettings()
        {
            foreach (DataGridViewColumn col in dataGridViewWeather.Columns)
            {
                // Отключаем сортировку
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                col.HeaderText = " 1 ";
            }
            // прячем колонки
            dataGridViewWeather.Columns[2].Visible = false;
            dataGridViewWeather.Columns[3].Visible = false;
            dataGridViewWeather.Columns[4].Visible = false;
            dataGridViewWeather.Columns[5].Visible = false;
            dataGridViewWeather.Columns[6].Visible = false;
            // Пишем названия
            dataGridViewWeather.Columns[0].HeaderText = "Параметр";
            dataGridViewWeather.Columns[1].HeaderText = "Значение";
            dataGridViewWeather.Columns[0].ReadOnly = true;

            foreach (DataGridViewRow row in dataGridViewWeather.Rows)
            {
                switch (row.Cells[6].Value.ToString())
                {
                    case "cloudshort":
                        row.Cells[1] = WeatherType.CloudCell();
                        break;
                    case "winddirectionlong":
                        row.Cells[1] = WeatherType.WindCell();
                        break;
                    default:
                        break;
                }
            }
        }

        private void dataGridViewWeather_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridSettings();
            try
            {
                comboBoxImages.SelectedItem = comboBoxImages.FindMatchingItem(Global.weatherSetting[2].Value);
            }
            catch
            {

            }
        }

        // Загрузка списка иконок для заголовка новости
        private void LoadingImages()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            comboBoxImages.Items.Clear();
            string[] files = Directory.GetFiles(Path.Combine(path, "background"), "*.jpg");

            if (files.Length > 0)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    comboBoxImages.Items.Add(files[i].Replace(Path.Combine(path, "background\\"), "").Replace(".jpg", ""));
                }
                comboBoxImages.SelectedIndex = 0;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            dateTimePicker.Value = DateTime.Now;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            bool error = false;

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            // Считываем шаблонное изображение
            Image imageMain = null;
            Image imageFon = null;

            try
            {
                imageMain = Image.FromStream(new MemoryStream(File.ReadAllBytes(Path.Combine(path, "main.png"))));
            }
            catch
            {
                error = true;
                MessageBox.Show("Ошибка целостности плагина. Обратитесь к разработчику.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                imageFon = Image.FromStream(new MemoryStream(File.ReadAllBytes(Path.Combine(path, "background", comboBoxImages.Text + ".jpg"))));
            }
            catch
            {
                MessageBox.Show("Ошибка обработки файла фона. Укажите другой файл.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadingImages();
                error = true;
            }

            if (error)
            {
                return;
            }

            Enabled = false;
            // Обрезаем фон
            PhotoSize photoSizeFon = new PhotoSize();
            photoSizeFon.source = imageFon;
            photoSizeFon.height = imageMain.Height;
            photoSizeFon.width = imageMain.Width;
            Image imageOut = photoSizeFon.ScaleImage();
            // Раскладываем по слоям
            Merge mergeImage = new Merge();
            mergeImage.sourceBottom = imageOut;
            mergeImage.sourceTop = imageMain;
            mergeImage.top = 0;
            mergeImage.left = 0;
            imageOut = mergeImage.MergeImage(); // Помещаем оформление
            // Пишем дату
            TextGenerate textE = new TextGenerate();
            textE.textString = dateTimePicker.Value.ToString("d MMMM yyyy").ToLower();
            textE.rectH = 100;
            textE.rectW = 1000;
            textE.rectX = 45;
            textE.rectY = 190;
            textE.source = imageOut;
            textE.fontSize = 100;
            textE.colorText = Color.White;
            textE.fontName = "Lato";
            textE.lineAlignment = StringAlignment.Center;
            textE.stringAlignment = StringAlignment.Near;
            //textE.debug = true;
            imageOut = textE.DrawTextWithEffects();
            // Пишем градусы сегодня
            TextGenerate textName = new TextGenerate();
            textName.textString = Global.weatherSetting[0].Value + "°C";
            textName.rectH = 250;
            textName.rectW = 800;
            textName.rectX = 90;
            textName.rectY = 550;
            textName.source = imageOut;
            textName.fontSize = 210;
            textName.colorText = Color.FromArgb(50, 141, 212);
            textName.fontName = "Lato";
            textName.lineAlignment = StringAlignment.Center;
            textName.stringAlignment = StringAlignment.Center;
            //textName.debug = true;
            imageOut = textName.DrawTextWithEffects();
            // Пишем облачность
            textName.textString = WeatherType.Capitalize(Global.weatherSetting[1].Value);
            textName.rectH = 180;
            textName.rectW = 800;
            textName.rectX = 90;
            textName.rectY = 760;
            textName.source = imageOut;
            textName.fontSize = 60;
            textName.colorText = Color.FromArgb(50, 141, 212);
            textName.fontName = "Lato";
            textName.lineAlignment = StringAlignment.Center;
            textName.stringAlignment = StringAlignment.Center;
            //textName.debug = true;
            imageOut = textName.DrawTextWithEffects();
            // Пишем другие на сегодня
            string wind;
            if (Global.weatherSetting[4].Value == "0")
            {
                wind = "Ветер: " + Global.weatherSetting[3].Value.ToLower() + "";
            }
            else
            {
                wind = "Ветер: " + Global.weatherSetting[3].Value.ToLower() + ", " + Global.weatherSetting[4].Value + " м/с";
            }

            textName.textString = wind +
                Environment.NewLine + "Атмосферное давление: " + Global.weatherSetting[5].Value + " мм" +
                Environment.NewLine + "Влажность: " + Global.weatherSetting[6].Value + "%";
            textName.rectH = 200;
            textName.rectW = 800;
            textName.rectX = 100;
            textName.rectY = 950;
            textName.source = imageOut;
            textName.fontSize = 50;
            textName.colorText = Color.Black;
            textName.fontName = "Lato";
            textName.lineAlignment = StringAlignment.Center;
            textName.stringAlignment = StringAlignment.Near;
            //textName.debug = true;
            imageOut = textName.DrawTextWithEffects();
            // Символ облачности
            textName.textString = WeatherType.ReplaceWeatherClowd(Global.weatherSetting[2].Value);
            textName.rectH = 750;
            textName.rectW = 750;
            textName.rectX = 980;
            textName.rectY = 400;
            textName.source = imageOut;
            textName.fontSize = 700;
            textName.colorText = Color.FromArgb(50, 141, 212);
            textName.fontName = "linea-weather-10";
            textName.lineAlignment = StringAlignment.Center;
            textName.stringAlignment = StringAlignment.Center;
            //textName.debug = true;
            imageOut = textName.DrawTextWithEffects();
            // Пишем день недели первого дня
            textName.textString = WeatherType.Capitalize(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dateTimePicker.Value.AddDays(1).DayOfWeek).ToString().ToLower());
            textName.rectH = 90;
            textName.rectW = 510;
            textName.rectX = 50;
            textName.rectY = 1275;
            textName.source = imageOut;
            textName.fontSize = 60;
            textName.colorText = Color.Black;
            textName.fontName = "Lato";
            textName.fontStyle = FontStyle.Regular;
            textName.lineAlignment = StringAlignment.Center;
            textName.stringAlignment = StringAlignment.Center;
            //textName.debug = true;
            imageOut = textName.DrawTextWithEffects();
            // Символ облачности первого дня
            textName.textString = WeatherType.ReplaceWeatherClowd(Global.weatherSetting[7].Value);
            textName.rectH = 300;
            textName.rectW = 510;
            textName.rectX = 50;
            textName.rectY = 1400;
            textName.source = imageOut;
            textName.fontSize = 250;
            textName.colorText = Color.FromArgb(50, 141, 212);
            textName.fontName = "linea-weather-10";
            textName.fontStyle = FontStyle.Bold;
            textName.lineAlignment = StringAlignment.Center;
            textName.stringAlignment = StringAlignment.Center;
            //textName.debug = true;
            imageOut = textName.DrawTextWithEffects();
            // Пишем температуру первого дня
            textName.textString = Global.weatherSetting[8].Value + "° / " + Global.weatherSetting[9].Value + "°";
            textName.rectH = 90;
            textName.rectW = 510;
            textName.rectX = 50;
            textName.rectY = 1650;
            textName.source = imageOut;
            textName.fontSize = 60;
            textName.colorText = Color.Black;
            textName.fontName = "Lato";
            textName.fontStyle = FontStyle.Regular;
            textName.lineAlignment = StringAlignment.Center;
            textName.stringAlignment = StringAlignment.Center;
            //textName.debug = true;
            imageOut = textName.DrawTextWithEffects();
            // Пишем день недели второго дня
            textName.textString = WeatherType.Capitalize(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dateTimePicker.Value.AddDays(2).DayOfWeek).ToString().ToLower());
            textName.rectH = 90;
            textName.rectW = 510;
            textName.rectX = 640;
            textName.rectY = 1275;
            textName.source = imageOut;
            textName.fontSize = 60;
            textName.colorText = Color.Black;
            textName.fontName = "Lato";
            textName.fontStyle = FontStyle.Regular;
            textName.lineAlignment = StringAlignment.Center;
            textName.stringAlignment = StringAlignment.Center;
            //textName.debug = true;
            imageOut = textName.DrawTextWithEffects();
            // Символ облачности второго дня
            textName.textString = WeatherType.ReplaceWeatherClowd(Global.weatherSetting[10].Value);
            textName.rectH = 300;
            textName.rectW = 510;
            textName.rectX = 640;
            textName.rectY = 1400;
            textName.source = imageOut;
            textName.fontSize = 250;
            textName.colorText = Color.FromArgb(50, 141, 212);
            textName.fontName = "linea-weather-10";
            textName.fontStyle = FontStyle.Bold;
            textName.lineAlignment = StringAlignment.Center;
            textName.stringAlignment = StringAlignment.Center;
            //textName.debug = true;
            imageOut = textName.DrawTextWithEffects();
            // Пишем температуру второго дня
            textName.textString = Global.weatherSetting[11].Value + "° / " + Global.weatherSetting[12].Value + "°";
            textName.rectH = 90;
            textName.rectW = 510;
            textName.rectX = 640;
            textName.rectY = 1650;
            textName.source = imageOut;
            textName.fontSize = 60;
            textName.colorText = Color.Black;
            textName.fontName = "Lato";
            textName.fontStyle = FontStyle.Regular;
            textName.lineAlignment = StringAlignment.Center;
            textName.stringAlignment = StringAlignment.Center;
            //textName.debug = true;
            imageOut = textName.DrawTextWithEffects();
            // Пишем день недели третьего дня
            textName.textString = WeatherType.Capitalize(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dateTimePicker.Value.AddDays(3).DayOfWeek).ToString().ToLower());
            textName.rectH = 90;
            textName.rectW = 510;
            textName.rectX = 1230;
            textName.rectY = 1275;
            textName.source = imageOut;
            textName.fontSize = 60;
            textName.colorText = Color.Black;
            textName.fontName = "Lato";
            textName.fontStyle = FontStyle.Regular;
            textName.lineAlignment = StringAlignment.Center;
            textName.stringAlignment = StringAlignment.Center;
            //textName.debug = true;
            imageOut = textName.DrawTextWithEffects();
            // Символ облачности третьего дня
            textName.textString = WeatherType.ReplaceWeatherClowd(Global.weatherSetting[13].Value);
            textName.rectH = 300;
            textName.rectW = 510;
            textName.rectX = 1230;
            textName.rectY = 1400;
            textName.source = imageOut;
            textName.fontSize = 250;
            textName.colorText = Color.FromArgb(50, 141, 212);
            textName.fontName = "linea-weather-10";
            textName.fontStyle = FontStyle.Bold;
            textName.lineAlignment = StringAlignment.Center;
            textName.stringAlignment = StringAlignment.Center;
            //textName.debug = true;
            imageOut = textName.DrawTextWithEffects();
            // Пишем температуру третьего дня
            textName.textString = Global.weatherSetting[14].Value + "° / " + Global.weatherSetting[15].Value + "°";
            textName.rectH = 90;
            textName.rectW = 510;
            textName.rectX = 1230;
            textName.rectY = 1650;
            textName.source = imageOut;
            textName.fontSize = 60;
            textName.colorText = Color.Black;
            textName.fontName = "Lato";
            textName.fontStyle = FontStyle.Regular;
            textName.lineAlignment = StringAlignment.Center;
            textName.stringAlignment = StringAlignment.Center;
            //textName.debug = true;
            imageOut = textName.DrawTextWithEffects();

            myHost.ReciveImage(imageOut);
            this.Enabled = true;
        }

        private void dataGridViewWeather_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                dataGridViewWeather.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = WeatherType.DataGridCheck(dataGridViewWeather.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), dataGridViewWeather.Rows[e.RowIndex].Cells[6].Value.ToString());
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting();
            setting.ShowDialog();

            // Обновить данные в таблице
            dataGridViewWeather.Refresh();

            labelUpdate.Text = "Прогноз обновлен: " + Global.siteTime.ToString();
        }

        private void dataGridViewWeather_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            labelUpdate.Text = "Обновление прогноза...";
            Weather weather = new Weather();
            bool error;
            weather.loadHtml(false, out error);
            labelUpdate.Text = "Прогноз обновлен: " + Global.siteTime.ToString();
            if (error)
            {
                MessageBox.Show("Нет доступа к указанному сайту!", "Ошибка обновления прогноза!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Weather weather = new Weather();
            bool error = false;
            if (Global.siteTime.AddHours(5) < DateTime.Now)
            {
                labelUpdate.Text = "Обновление прогноза...";
                weather.loadHtml(true, out error);
                labelUpdate.Text = "Прогноз обновлен: " + Global.siteTime.ToString();
            }
            if (!error && Global.siteHtml != "")
            {
                weather.parsePogoda();
                dataGridViewWeather.Refresh();
                comboBoxImages.SelectedItem = comboBoxImages.FindMatchingItem(Global.weatherSetting[2].Value);
            }
            else
            {
                MessageBox.Show("Нет доступа к указанному сайту!", "Ошибка обновления прогноза!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker.Value.ToString("dd.MM.yyyy") != DateTime.Now.ToString("dd.MM.yyyy"))
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void dataGridViewWeather_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 2) comboBoxImages.SelectedItem = comboBoxImages.FindMatchingItem(Global.weatherSetting[2].Value);
        }
    }
}
