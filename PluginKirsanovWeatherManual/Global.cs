// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PluginKirsanovWeatherManual
{
    internal class Global
    {
        public Global() { }
        public static BindingList<WeatherSetting> weatherSetting = new BindingList<WeatherSetting> ();
        public static string sitePath;
        public static DateTime siteTime;
        public static string siteHtml;
    }

    public class WeatherSetting
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string XPath { get; set; }
        public string XPathType { get; set; }
        public bool AutoClear { get; set; }
        public string Clear { get; set; }
        public string Type { get; set; }
    }

    public class DoubleBufferedDataGridView : System.Windows.Forms.DataGridView
    {
        protected override bool DoubleBuffered { get => true; }
    }
}
