// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Drawing;
using System.Windows.Forms;
using PluginInterface;
using PhotoEdit;
using System.Reflection;
using System.IO;

namespace PluginKirsanovDemography
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.labelT1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridViewDemography = new System.Windows.Forms.DataGridView();
            this.DemographyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DemographyNow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DemographyOld = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDemography)).BeginInit();
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
            this.panel1.Size = new System.Drawing.Size(244, 45);
            this.panel1.TabIndex = 8;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Checked = false;
            this.dateTimePicker.CustomFormat = "MMMM yyyy";
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker.Location = new System.Drawing.Point(7, 20);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(189, 20);
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
            this.panel2.Controls.Add(this.dataGridViewDemography);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(3, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(244, 244);
            this.panel2.TabIndex = 13;
            // 
            // dataGridViewDemography
            // 
            this.dataGridViewDemography.AllowUserToAddRows = false;
            this.dataGridViewDemography.AllowUserToDeleteRows = false;
            this.dataGridViewDemography.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDemography.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDemography.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DemographyName,
            this.DemographyNow,
            this.DemographyOld});
            this.dataGridViewDemography.Location = new System.Drawing.Point(7, 21);
            this.dataGridViewDemography.Name = "dataGridViewDemography";
            this.dataGridViewDemography.Size = new System.Drawing.Size(234, 220);
            this.dataGridViewDemography.TabIndex = 1;
            this.dataGridViewDemography.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDemography_CellEndEdit);
            // 
            // DemographyName
            // 
            this.DemographyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DemographyName.HeaderText = "Название";
            this.DemographyName.Name = "DemographyName";
            this.DemographyName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DemographyNow
            // 
            this.DemographyNow.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DemographyNow.HeaderText = "Параметр 1";
            this.DemographyNow.Name = "DemographyNow";
            this.DemographyNow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DemographyOld
            // 
            this.DemographyOld.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DemographyOld.HeaderText = "Параметр 2";
            this.DemographyOld.Name = "DemographyOld";
            this.DemographyOld.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // ctlMain
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(250, 330);
            this.Name = "ctlMain";
            this.Size = new System.Drawing.Size(250, 331);
            this.Load += new System.EventHandler(this.ctlMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDemography)).EndInit();
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
        private DataGridView dataGridViewDemography;
        private Label label1;
        private DataGridViewTextBoxColumn DemographyName;
        private DataGridViewTextBoxColumn DemographyNow;
        private DataGridViewTextBoxColumn DemographyOld;
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
            Clear();
            dataGridViewDemography.Columns["DemographyName"].ReadOnly = true;
        }

        private void generateStatisticTable()
        {
            var dt = dateTimePicker.Value;

            dataGridViewDemography.Rows.Clear();

            dataGridViewDemography.Rows.Add("Год", dt.Year, dt.Year - 1);
            //dataGridViewDemography.Rows.Add("Рождение девочки", 0, 0);
            //dataGridViewDemography.Rows.Add("Рождение мальчики", 0, 0);
            dataGridViewDemography.Rows.Add("Рождение", 0, 0);
            dataGridViewDemography.Rows.Add("Смерть", 0, 0);
            dataGridViewDemography.Rows.Add("Брак", 0, 0);
            dataGridViewDemography.Rows.Add("Развод", 0, 0);
            dataGridViewDemography.Rows.Add("Мужские", "Имена", "-");
            dataGridViewDemography.Rows.Add("Женские", "Имена", "-");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            dateTimePicker.Value = DateTime.Now.AddMonths(-1);
            generateStatisticTable();
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

            if (imageMain == null)
            {
                MessageBox.Show("Плагин поврежден. Обратитесь к разработчику.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            mergeImage.sourceTop = imageMain;
            mergeImage.top = 0;
            mergeImage.left = 0;
            imageOut = mergeImage.MergeImage(); // Помещаем оформление
            // Пишем дату
            TextGenerate textE = new TextGenerate();
            textE.textString = "за " + dateTimePicker.Value.ToString("MMMM").ToLower() + " в Кирсанове";
            textE.rectH = 100;
            textE.rectW = 1300;
            textE.rectX = 40;
            textE.rectY = 210;
            textE.source = imageOut;
            textE.fontSize = 100;
            textE.colorText = Color.White;
            textE.fontName = "Lato";
            textE.lineAlignment = StringAlignment.Center;
            textE.stringAlignment = StringAlignment.Near;
            //textE.debug = true;
            imageOut = textE.DrawTextWithEffects();
            // Пишем имена мужские
            TextGenerate textName = new TextGenerate();
            textName.textString = dataGridViewDemography[1, 5].Value.ToString().Replace(","," ").Replace("  ", " ").Replace("  ", " ").Replace(" ", Environment.NewLine);
            textName.rectH = 250;
            textName.rectW = 600;
            textName.rectX = 280;
            textName.rectY = 1350;
            textName.source = imageOut;
            textName.fontSize = 66;
            textName.colorText = Color.Black;
            textName.fontName = "Lato";
            textName.lineAlignment = StringAlignment.Center;
            textName.stringAlignment = StringAlignment.Near;
            //textName.debug = true;
            imageOut = textName.DrawTextWithEffects();
            // Пишем имена женские
            textName.textString = dataGridViewDemography[1, 6].Value.ToString().Replace(",", " ").Replace("  ", " ").Replace("  ", " ").Replace(" ", Environment.NewLine);
            textName.rectH = 250;
            textName.rectW = 580;
            textName.rectX = 1150;
            textName.rectY = 1350;
            textName.source = imageOut;
            textName.fontSize = 66;
            textName.colorText = Color.Black;
            textName.fontName = "Lato";
            textName.lineAlignment = StringAlignment.Center;
            textName.stringAlignment = StringAlignment.Near;
            //textName.debug = true;
            imageOut = textName.DrawTextWithEffects();
            // Рождаемость
            Graph graphBorn = new Graph();
            Image imageGraphBorn = graphBorn.RenderGraphYears(Convert.ToInt32(dataGridViewDemography[1, 1].Value), Convert.ToInt32(dataGridViewDemography[2, 1].Value), dateTimePicker.Value.Year);
            // Раскладываем по слоям
            Merge mergeImageGraph = new Merge();
            mergeImageGraph.sourceBottom = imageOut;
            mergeImageGraph.sourceTop = imageGraphBorn;
            mergeImageGraph.top = 500;
            mergeImageGraph.left = 280;
            imageOut = mergeImageGraph.MergeImage(); // Помещаем оформление
            // Смертность
            Graph graph = new Graph();
            Image imageGraph = graph.RenderGraphYears(Convert.ToInt16(dataGridViewDemography[1, 2].Value), Convert.ToInt16(dataGridViewDemography[2, 2].Value), dateTimePicker.Value.Year);
            // Раскладываем по слоям
            mergeImageGraph.sourceBottom = imageOut;
            mergeImageGraph.sourceTop = imageGraph;
            mergeImageGraph.top = 500;
            mergeImageGraph.left = 1150;
            imageOut = mergeImageGraph.MergeImage(); // Помещаем оформление
            // Браки
            Graph graphWedding = new Graph();
            Image imageGraphWedding = graphWedding.RenderGraphYears(Convert.ToInt16(dataGridViewDemography[1, 3].Value), Convert.ToInt16(dataGridViewDemography[2, 3].Value), dateTimePicker.Value.Year);
            // Раскладываем по слоям
            mergeImageGraph.sourceBottom = imageOut;
            mergeImageGraph.sourceTop = imageGraphWedding;
            mergeImageGraph.top = 900;
            mergeImageGraph.left = 280;
            imageOut = mergeImageGraph.MergeImage(); // Помещаем оформление
            // Разводы
            Graph graphDivorce = new Graph();
            Image imageGraphDivorce = graphDivorce.RenderGraphYears(Convert.ToInt16(dataGridViewDemography[1, 4].Value), Convert.ToInt16(dataGridViewDemography[2, 4].Value), dateTimePicker.Value.Year);
            // Раскладываем по слоям
            mergeImageGraph.sourceBottom = imageOut;
            mergeImageGraph.sourceTop = imageGraphDivorce;
            mergeImageGraph.top = 900;
            mergeImageGraph.left = 1150;
            imageOut = mergeImageGraph.MergeImage(); // Помещаем оформление

            myHost.ReciveImage(imageOut);
            this.Enabled = true;
        }

        private void dataGridViewDemography_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
            {
                if (e.RowIndex <= 4) {
                    int value;
                    try
                    {
                        value = Convert.ToInt32(dataGridViewDemography.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        if (value < 0)
                        {
                            value *= -1;
                        }
                    } catch
                    {
                        value = 0;
                    }

                    dataGridViewDemography.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = value;
                }
                if (e.RowIndex > 4 && e.ColumnIndex == 2)
                {
                    dataGridViewDemography.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "-";
                }
                if (e.RowIndex == 0 && e.ColumnIndex == 1)
                {
                    var dt = dateTimePicker.Value;
                    dataGridViewDemography.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dt.Year;
                }
                if (e.RowIndex == 0 && e.ColumnIndex == 2)
                {
                    var dt = dateTimePicker.Value;
                    dataGridViewDemography.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dt.Year - 1;
                }
            }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            var dt = dateTimePicker.Value;
            try
            {
                dataGridViewDemography.Rows[0].Cells[1].Value = dt.Year;
                dataGridViewDemography.Rows[0].Cells[2].Value = dt.Year - 1;
            }
            catch { }
        }
    }
}
