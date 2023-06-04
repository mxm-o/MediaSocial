using System;
using System.Linq;
using System.Windows.Forms;

namespace PluginKirsanovWeatherManual
{
    public static class ComboBoxExtensions
    {
        public static string FindMatchingItem(this ComboBox comboBox, string searchStr)
        {
            var matchingItems = comboBox.Items.Cast<string>().Where(item => item.ToLower().Replace(" ", "").StartsWith(searchStr.ToLower().Replace(" ", ""))).ToList();

            if (matchingItems.Count == 0)
            {
                return comboBox.Items[0].ToString();
            }
            else
            {
                Random random = new Random();
                return matchingItems[random.Next(matchingItems.Count)];
            }
        }
    }
}
