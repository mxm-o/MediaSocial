// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System.Drawing;
using System.Drawing.Text;

namespace PhotoEdit
{
    /// <summary>
    /// Проверка наличия шрифта в системе
    /// </summary>
    public static class CheckFont
    {
        /// <summary>
        /// Проверка наличия шрифта в системе
        /// </summary>
        /// <param name="name">Имя шрифта</param>
        /// <returns>true/false</returns>
        public static bool FontExists (string name)
        {
            bool fontExists = false;

            using (InstalledFontCollection installedFonts = new InstalledFontCollection())
            {
                foreach (FontFamily font in installedFonts.Families)
                {
                    if (font.Name.Equals(name))
                    {
                        fontExists = true;
                        break;
                    }
                }
            }
            return fontExists;
        }
    }
}
