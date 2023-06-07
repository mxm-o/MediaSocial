using System;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.ComponentModel;
using System.Linq;

namespace PluginKirsanovWeatherManual
{

    public static class Configuration
    {
        private static string path = Path.GetDirectoryName(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath);
        public static void Generation()
        {
            if (Global.weatherSetting.Count <= 0)
            {
                Global.weatherSetting.Add(new WeatherSetting() { Name = "Температура сегодня", Value = "0", XPath = "", XPathType = "innerHtml", AutoClear = true, Clear = "", Type = "temp" });
                Global.weatherSetting.Add(new WeatherSetting() { Name = "Облачность", Value = "Ясно", XPath = "", XPathType = "innerHtml", AutoClear = true, Clear = "", Type = "cloudlong" });
                Global.weatherSetting.Add(new WeatherSetting() { Name = "Облачность кратко", Value = "Ясно", XPath = "", XPathType = "innerHtml", AutoClear = true, Clear = "", Type = "cloudshort" });
                Global.weatherSetting.Add(new WeatherSetting() { Name = "Ветер направление", Value = "Западный", XPath = "", XPathType = "innerHtml", AutoClear = true, Clear = "", Type = "winddirectionlong" });
                Global.weatherSetting.Add(new WeatherSetting() { Name = "Ветер сила", Value = "6", XPath = "", XPathType = "innerHtml", AutoClear = true, Clear = "", Type = "windpower" });
                Global.weatherSetting.Add(new WeatherSetting() { Name = "Атмосферное давление", Value = "755", XPath = "", XPathType = "innerHtml", AutoClear = true, Clear = "", Type = "pressure" });
                Global.weatherSetting.Add(new WeatherSetting() { Name = "Влажность", Value = "70", XPath = "", XPathType = "innerHtml", AutoClear = true, Clear = "", Type = "humidity" });
                Global.weatherSetting.Add(new WeatherSetting() { Name = "1. Облачность", Value = "Ясно", XPath = "", XPathType = "innerHtml", AutoClear = true, Clear = "", Type = "cloudshort" });
                Global.weatherSetting.Add(new WeatherSetting() { Name = "1. Температура день", Value = "0", XPath = "", XPathType = "innerHtml", AutoClear = true, Clear = "", Type = "temp" });
                Global.weatherSetting.Add(new WeatherSetting() { Name = "1. Температура ночь", Value = "0", XPath = "", XPathType = "innerHtml", AutoClear = true, Clear = "", Type = "temp" });
                Global.weatherSetting.Add(new WeatherSetting() { Name = "2. Облачность", Value = "Ясно", XPath = "", XPathType = "innerHtml", AutoClear = true, Clear = "", Type = "cloudshort" });
                Global.weatherSetting.Add(new WeatherSetting() { Name = "2. Температура день", Value = "0", XPath = "", XPathType = "innerHtml", AutoClear = true, Clear = "", Type = "temp" });
                Global.weatherSetting.Add(new WeatherSetting() { Name = "2. Температура ночь", Value = "0", XPath = "", XPathType = "innerHtml", AutoClear = true, Clear = "", Type = "temp" });
                Global.weatherSetting.Add(new WeatherSetting() { Name = "3. Облачность", Value = "Ясно", XPath = "", XPathType = "innerHtml", AutoClear = true, Clear = "", Type = "cloudshort" });
                Global.weatherSetting.Add(new WeatherSetting() { Name = "3. Температура день", Value = "0", XPath = "", XPathType = "innerHtml", AutoClear = true, Clear = "", Type = "temp" });
                Global.weatherSetting.Add(new WeatherSetting() { Name = "3. Температура ночь", Value = "0", XPath = "", XPathType = "innerHtml", AutoClear = true, Clear = "", Type = "temp" });

                Global.siteTime = DateTime.Now.AddYears(-10);
            }
        }
        public static void Save(bool clipboars = false)
        {
            // Save the weatherSetting data to an XML file
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("weatherSettings");
            doc.AppendChild(root);

            foreach (WeatherSetting setting in Global.weatherSetting)
            {
                XmlElement settingNode = doc.CreateElement("weatherSetting");
                settingNode.SetAttribute("name", setting.Name);
                settingNode.SetAttribute("value", setting.Value);
                settingNode.SetAttribute("xpath", setting.XPath);
                settingNode.SetAttribute("xpathType", setting.XPathType);
                settingNode.SetAttribute("autoClear", setting.AutoClear.ToString());
                settingNode.SetAttribute("clear", setting.Clear);
                root.AppendChild(settingNode);
            }

            XmlElement siteNode = doc.CreateElement("site");
            siteNode.SetAttribute("path", Global.sitePath);
            siteNode.SetAttribute("time", clipboars ? "" : Global.siteTime.ToString("dd.MM.yyyy HH:mm:ss"));
            siteNode.InnerText = clipboars ? "" : Global.siteHtml;
            doc.DocumentElement.AppendChild(siteNode);

            if (clipboars)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "xml files (*.xml)|*.xml";
                saveFileDialog.FileName = "copy";
                saveFileDialog.RestoreDirectory = true;
                var result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    doc.Save(saveFileDialog.FileName);
                }
            }
            else
            {
                doc.Save(Path.Combine(path, "weatherSettings.xml"));
            }
        }

        public static void Load(bool clipboars = false)
        {

            // Загружаем weatherSetting данные из XML файла
            // Делаем временную копию существующих данных
            BindingList<WeatherSetting> temp = new BindingList<WeatherSetting>(Global.weatherSetting.ToList());
            var tempPaht = Global.sitePath;
            bool error = false;

            XmlDocument doc = new XmlDocument();

            if (clipboars)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "xml files (*.xml)|*.xml";
                openFileDialog.FileName = "copy";
                openFileDialog.RestoreDirectory = true;
                var result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    doc.Load(Path.Combine(openFileDialog.FileName));
                }
                else
                {
                    error = true;
                }
            }
            else
            {
                try
                {
                    doc.Load(Path.Combine(path, "weatherSettings.xml"));
                }
                catch
                {
                    error = true;
                }
            }

            if (!error)
            {
                foreach (XmlNode settingNode in doc.SelectNodes("/weatherSettings/weatherSetting"))
                {
                    WeatherSetting setting = new WeatherSetting
                    {
                        Name = settingNode.Attributes["name"].Value,
                        Value = settingNode.Attributes["value"].Value,
                        XPath = settingNode.Attributes["xpath"].Value,
                        XPathType = settingNode.Attributes["xpathType"].Value,
                        AutoClear = bool.Parse(settingNode.Attributes["autoClear"].Value),
                        Clear = settingNode.Attributes["clear"].Value
                    };

                    for (int i = 0; i < Global.weatherSetting.Count; i++)
                    {
                        if (Global.weatherSetting[i].Name == setting.Name)
                        {
                            Global.weatherSetting[i].Value = setting.Value;
                            Global.weatherSetting[i].XPath = setting.XPath;
                            Global.weatherSetting[i].XPathType = setting.XPathType;
                            Global.weatherSetting[i].AutoClear = setting.AutoClear;
                            Global.weatherSetting[i].Clear = setting.Clear;
                        }
                    }
                }

                XmlNode siteNode = doc.SelectSingleNode("/weatherSettings/site");
                if (siteNode != null)
                {
                    if (siteNode.Attributes["path"] != null && !string.IsNullOrEmpty(siteNode.Attributes["path"].Value))
                    {
                        Global.sitePath = siteNode.Attributes["path"].Value;
                    }

                    if (siteNode.Attributes["time"] != null && DateTime.TryParseExact(siteNode.Attributes["time"].Value, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime siteTime))
                    {
                        Global.siteTime = siteTime;
                    }

                    if (!string.IsNullOrEmpty(siteNode.InnerText))
                    {
                        Global.siteHtml = siteNode.InnerText;
                    }
                }
                else
                {
                    error = true;
                }

            }
            if (error)
            {
                // Возращаем старые данные
                Global.weatherSetting = new BindingList<WeatherSetting>(temp.ToList());
                Global.sitePath = tempPaht;
                Global.siteTime = DateTime.Now.AddMonths(-12);
                Global.siteHtml = "";
                if (clipboars)
                {
                    MessageBox.Show("Ошибка в файле импорта. Операция отменена.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
