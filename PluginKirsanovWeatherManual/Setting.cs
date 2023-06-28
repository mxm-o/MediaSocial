using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PluginKirsanovWeatherManual
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Configuration.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            dataGridViewWeather.DataSource = Global.weatherSetting;
            textBoxSite.Text = Global.sitePath;
            labelPogodaUpdate.Text = "Прогноз обновлен: " + Global.siteTime.ToString();
            dataGridSettings();
        }

        private void dataGridSettings()
        {
            foreach (DataGridViewColumn col in dataGridViewWeather.Columns)
            {
                // Отключаем сортировку
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            // Пишем названия
            dataGridViewWeather.Columns[0].HeaderText = "Параметр";
            dataGridViewWeather.Columns[1].HeaderText = "Значение";
            dataGridViewWeather.Columns[0].ReadOnly = true;
            dataGridViewWeather.Columns[2].HeaderText = "XPath";
            dataGridViewWeather.Columns[3].HeaderText = "Тип XPath";
            dataGridViewWeather.Columns[4].HeaderText = "Авто очистка";
            dataGridViewWeather.Columns[5].HeaderText = "Regexp очистка";
            // Прячем колонку
            dataGridViewWeather.Columns[6].Visible = false;
            // Перебираем ячейки
            foreach (DataGridViewRow row in dataGridViewWeather.Rows)
            {
                row.Cells[3] = WeatherType.XPathTypeCell();
            }
        }

        private void dataGridViewWeather_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string r = e.ThrowException.ToString();
        }

        private void dataGridViewWeather_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                dataGridViewWeather.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = WeatherType.DataGridCheck(dataGridViewWeather.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), dataGridViewWeather.Rows[e.RowIndex].Cells[6].Value.ToString());
            }
        }

        private void textBoxSite_Leave(object sender, EventArgs e)
        {
            string pattern = @"^(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
            bool isMatch = Regex.IsMatch(textBoxSite.Text, pattern);
            if (!isMatch)
            {
                MessageBox.Show("Некорректный адрес страницы. Проверьте данные", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                Global.sitePath = textBoxSite.Text;
            }
        }

        private void labelSiteRegex_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://regex101.com/");
        }

        private void buttonSavePogoda_Click(object sender, EventArgs e)
        {
            Configuration.Save();
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            labelPogodaUpdate.Text = "Обновление прогноза...";
            Weather weather = new Weather();
            bool error;
            weather.loadHtml(false, out error);
            labelPogodaUpdate.Text = "Прогноз обновлен: " + Global.siteTime.ToString();
            if (error)
            {
                MessageBox.Show("Нет доступа к указанному сайту!", "Ошибка обновления прогноза!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonLoadPogoda_Click(object sender, EventArgs e)
        {
            Weather weather = new Weather();
            bool error = true;
            if (Global.siteTime > DateTime.Now.AddHours(5)) {
                labelPogodaUpdate.Text = "Обновление прогноза...";
                weather.loadHtml(true, out error);
                labelPogodaUpdate.Text = "Прогноз обновлен: " + Global.siteTime.ToString();
            }
            if (error && Global.siteHtml != "")
            {
                weather.parsePogoda();
                dataGridViewWeather.Refresh();
            } else
            {
                MessageBox.Show("Нет доступа к указанному сайту!", "Ошибка обновления прогноза!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            Configuration.Load(true);
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            Configuration.Save(true);
        }
    }
}
