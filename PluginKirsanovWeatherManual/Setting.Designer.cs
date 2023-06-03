namespace PluginKirsanovWeatherManual
{
    partial class Setting
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
            this.buttonLoadPogoda = new System.Windows.Forms.Button();
            this.labelPogodaUpdate = new System.Windows.Forms.Label();
            this.labelSiteRegex = new System.Windows.Forms.Label();
            this.buttonSavePogoda = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxSite = new System.Windows.Forms.TextBox();
            this.buttonReload = new System.Windows.Forms.Button();
            this.dataGridViewWeather = new PluginKirsanovWeatherManual.DoubleBufferedDataGridView();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWeather)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLoadPogoda
            // 
            this.buttonLoadPogoda.Location = new System.Drawing.Point(129, 344);
            this.buttonLoadPogoda.Name = "buttonLoadPogoda";
            this.buttonLoadPogoda.Size = new System.Drawing.Size(130, 23);
            this.buttonLoadPogoda.TabIndex = 30;
            this.buttonLoadPogoda.Text = "Обработать прогноз";
            this.buttonLoadPogoda.UseVisualStyleBackColor = true;
            this.buttonLoadPogoda.Click += new System.EventHandler(this.buttonLoadPogoda_Click);
            // 
            // labelPogodaUpdate
            // 
            this.labelPogodaUpdate.AutoSize = true;
            this.labelPogodaUpdate.Location = new System.Drawing.Point(265, 349);
            this.labelPogodaUpdate.Name = "labelPogodaUpdate";
            this.labelPogodaUpdate.Size = new System.Drawing.Size(69, 13);
            this.labelPogodaUpdate.TabIndex = 29;
            this.labelPogodaUpdate.Text = "Обновлено: ";
            // 
            // labelSiteRegex
            // 
            this.labelSiteRegex.AutoSize = true;
            this.labelSiteRegex.Location = new System.Drawing.Point(12, 370);
            this.labelSiteRegex.Name = "labelSiteRegex";
            this.labelSiteRegex.Size = new System.Drawing.Size(154, 13);
            this.labelSiteRegex.TabIndex = 28;
            this.labelSiteRegex.Text = "Сайт составления регулярок";
            this.labelSiteRegex.Click += new System.EventHandler(this.labelSiteRegex_Click);
            // 
            // buttonSavePogoda
            // 
            this.buttonSavePogoda.Location = new System.Drawing.Point(343, 4);
            this.buttonSavePogoda.Name = "buttonSavePogoda";
            this.buttonSavePogoda.Size = new System.Drawing.Size(131, 23);
            this.buttonSavePogoda.TabIndex = 27;
            this.buttonSavePogoda.Text = "Сохранить настройки";
            this.buttonSavePogoda.UseVisualStyleBackColor = true;
            this.buttonSavePogoda.Click += new System.EventHandler(this.buttonSavePogoda_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 13);
            this.label15.TabIndex = 26;
            this.label15.Text = "Страница";
            // 
            // textBoxSite
            // 
            this.textBoxSite.Location = new System.Drawing.Point(73, 6);
            this.textBoxSite.Name = "textBoxSite";
            this.textBoxSite.Size = new System.Drawing.Size(264, 20);
            this.textBoxSite.TabIndex = 25;
            this.textBoxSite.Text = "http://";
            this.textBoxSite.Leave += new System.EventHandler(this.textBoxSite_Leave);
            // 
            // buttonReload
            // 
            this.buttonReload.Location = new System.Drawing.Point(12, 344);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(111, 23);
            this.buttonReload.TabIndex = 24;
            this.buttonReload.Text = "Обновить прогноз";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
            // 
            // dataGridViewWeather
            // 
            this.dataGridViewWeather.AllowUserToAddRows = false;
            this.dataGridViewWeather.AllowUserToDeleteRows = false;
            this.dataGridViewWeather.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWeather.Location = new System.Drawing.Point(12, 42);
            this.dataGridViewWeather.Name = "dataGridViewWeather";
            this.dataGridViewWeather.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewWeather.Size = new System.Drawing.Size(668, 296);
            this.dataGridViewWeather.TabIndex = 23;
            this.dataGridViewWeather.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewWeather_CellEndEdit);
            this.dataGridViewWeather.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewWeather_DataError);
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(480, 4);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(97, 23);
            this.buttonImport.TabIndex = 31;
            this.buttonImport.Text = "Импорт";
            this.buttonImport.UseVisualStyleBackColor = true;
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(583, 4);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(97, 23);
            this.buttonExport.TabIndex = 32;
            this.buttonExport.Text = "Экспорт";
            this.buttonExport.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(550, 356);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(130, 23);
            this.buttonOk.TabIndex = 33;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 391);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.buttonLoadPogoda);
            this.Controls.Add(this.labelPogodaUpdate);
            this.Controls.Add(this.labelSiteRegex);
            this.Controls.Add(this.buttonSavePogoda);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textBoxSite);
            this.Controls.Add(this.buttonReload);
            this.Controls.Add(this.dataGridViewWeather);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.Setting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWeather)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLoadPogoda;
        private System.Windows.Forms.Label labelPogodaUpdate;
        private System.Windows.Forms.Label labelSiteRegex;
        private System.Windows.Forms.Button buttonSavePogoda;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxSite;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonOk;
        private DoubleBufferedDataGridView dataGridViewWeather;
    }
}