using System;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Windows.Forms;

namespace PluginKirsanovWeatherManual
{
    public static class Configuration
    {
        private static string path = Path.GetDirectoryName(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath);
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
                settingNode.SetAttribute("type", setting.Type);
                root.AppendChild(settingNode);
            }

            XmlElement siteNode = doc.CreateElement("site");
            siteNode.SetAttribute("path", Global.sitePath);
            siteNode.SetAttribute("time", clipboars ? "" : Global.siteTime.ToString("dd.MM.yyyy HH:mm:ss"));
            siteNode.InnerText = clipboars ? "" :Global.siteHtml;
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

        public static void Load(bool clipboars = false) {

            // Загружаем weatherSetting данные из XML файла
            var temp = Global.weatherSetting; // Делаем временную копию существующих данных
            var tempPaht = Global.sitePath;
            Global.weatherSetting.Clear(); // Очищаем существующие данные
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Path.Combine(path, "weatherSettings.xml"));

                foreach (XmlNode settingNode in doc.SelectNodes("/weatherSettings/weatherSetting"))
                {
                    WeatherSetting setting = new WeatherSetting
                    {
                        Name = settingNode.Attributes["name"].Value,
                        Value = settingNode.Attributes["value"].Value,
                        XPath = settingNode.Attributes["xpath"].Value,
                        XPathType = settingNode.Attributes["xpathType"].Value,
                        AutoClear = bool.Parse(settingNode.Attributes["autoClear"].Value),
                        Clear = settingNode.Attributes["clear"].Value,
                        Type = settingNode.Attributes["type"].Value
                    };
                    Global.weatherSetting.Add(setting);
                }

                Global.sitePath = doc.SelectSingleNode("/weatherSettings/site").Attributes["path"].Value;

                var formats = new[] { "dd.MM.yyyy HH:mm:ss" };
                Global.siteTime = DateTime.ParseExact(
                    doc.SelectSingleNode("/weatherSettings/site").Attributes["time"].Value, formats, new CultureInfo("sl-SI"), DateTimeStyles.None);
                Global.siteHtml = doc.SelectSingleNode("/weatherSettings/site").InnerText;
            }
            catch {
                // Возращаем старые данные
                Global.weatherSetting = temp;
                Global.sitePath = tempPaht;
                Global.siteTime = DateTime.Now.AddMonths(-12);
                Global.siteHtml = "";
            }
        }
    }
}
