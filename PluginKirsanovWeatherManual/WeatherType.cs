// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PluginKirsanovWeatherManual
{
    public static class WeatherType
    {
        public static DataGridViewComboBoxCell CloudCell()
        {
            // Тип облачности
            DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();

            // Добавляем пункты
            comboBoxCell.Items.Add("Ясно");
            comboBoxCell.Items.Add("Малооблачно");
            comboBoxCell.Items.Add("Переменная облачность");
            comboBoxCell.Items.Add("Облачно");
            comboBoxCell.Items.Add("Дождь");
            comboBoxCell.Items.Add("Гроза");
            comboBoxCell.Items.Add("Снег");
            comboBoxCell.Items.Add("Снег с дождем");
            comboBoxCell.Items.Add("Туман");
            comboBoxCell.Items.Add("Град");

            return comboBoxCell;
        }

        public static DataGridViewComboBoxCell WindCell()
        {
            // Тип облачности
            DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();

            // Добавляем пункты
            comboBoxCell.Items.Add("Штиль");
            comboBoxCell.Items.Add("Северный");
            comboBoxCell.Items.Add("Северо-Восточный");
            comboBoxCell.Items.Add("Восточный");
            comboBoxCell.Items.Add("Юго-Восточный");
            comboBoxCell.Items.Add("Южный");
            comboBoxCell.Items.Add("Юго-Западный");
            comboBoxCell.Items.Add("Западный");
            comboBoxCell.Items.Add("Северо-западный");

            return comboBoxCell;
        }

        public static DataGridViewComboBoxCell XPathTypeCell()
        {
            // Тип облачности
            DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();

            // Добавляем пункты
            comboBoxCell.Items.Add("innerText");
            comboBoxCell.Items.Add("title");
            comboBoxCell.Items.Add("class");
            comboBoxCell.Items.Add("value");
            comboBoxCell.Items.Add("innerHtml");

            return comboBoxCell;
        }

        public static DataGridViewTextBoxCell TextCell()
        {
            DataGridViewTextBoxCell textBoxCell = new DataGridViewTextBoxCell();
            return textBoxCell;
        }

        public static string DataGridCheck(string text, string type)
        {
            var limits = new Dictionary<string, (double, double)>()
            {
                { "temp", (-100, 100) },
                { "humidity", (0, 100) },
                { "windpower", (0, 50) },
                { "pressure", (600, 900) },
            };

            if (!limits.ContainsKey(type))
            {
                return text;
            }

            if (double.TryParse(text, out double value) && limits.TryGetValue(type, out var limit) && value >= limit.Item1 && value <= limit.Item2)
            {
                if (type == "temp" && value > 0)
                {
                    return $"+{value}";
                }
                else
                {
                    return value.ToString();
                }
            }
            else
            {
                return "";
            }
        }

        public static string FilterWindDirection(string input, string format = "short")
        {

            string windDirection = FindWindDirection(input, format);
            if (windDirection == "")
            {
                input = input.Replace("-", " ");
                windDirection = FindWindDirection(input, format);
            }

            return windDirection;
        }

        private static string FindWindDirection(string input, string format = "short")
        {
            string[] words = input.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
            Array.Reverse(words);

            foreach (string word in words)
            {
                if (IsWindDirection(word))
                {
                    return format.Equals("short") ? word : GetFullDirection(word);
                }
            }
            return "";
        }

        private static bool IsWindDirection(string direction)
        {
            switch (direction.ToLower())
            {
                case "св":
                case "юв":
                case "юз":
                case "сз":
                case "ю":
                case "в":
                case "с":
                case "з":
                case "ш":
                    return true;
                default:
                    return false;
            }
        }

        private static string GetFullDirection(string direction)
        {
            switch (direction.ToLower())
            {
                case "с":
                    return "Северный";
                case "св":
                    return "Северо-Восточный";
                case "в":
                    return "Восточный";
                case "юв":
                    return "Юго-Восточный";
                case "ю":
                    return "Южный";
                case "юз":
                    return "Юго-Западный";
                case "з":
                    return "Западный";
                case "сз":
                    return "Северо-Западный";
                case "ш":
                    return "Штиль";
                default:
                    return "";
            }
        }

        public static string CheckWeatherClowd(string input)
        {
            string[] options = { "Ясно", "Малооблачно", "Переменная облачность", "Облачно", "Дождь", "Гроза", "Снег", "Снег с дождем", "Туман", "Град" };

            foreach (string option in options)
            {
                if (input.ToLower().Contains(option.ToLower())) // Преобразовываем строки к нижнему регистру для поиска
                {
                    return option;
                }
            }

            return "Ясно";
        }

        public static string ReplaceWeatherClowd(string input)
        {
            string[] find = { "Ясно", "Малооблачно", "Переменная облачность", "Облачно", "Дождь", "Гроза", "Снег", "Снег с дождем", "Туман", "Град" };
            string[] replace = { "", "", "", "", "", "", "", "", "", "" }; // Символы из шрифта linea-weather-10

            for (int i = 0; i < find.Length; i++)
            {
                input = input.ToLower().Replace(find[i].ToLower(), replace[i]);
            }

            return input;
        }

        public static string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return input.Substring(0, 1).ToUpper() + input.Substring(1);
        }
    }
}
