using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSocial.Classes
{
    internal class CalculateFontSize
    {
        // Базовый размер шрифта в пикселях
        public int BaseFontSize { get; set; } = 14;
        // Коэффициент соотношения длины текста к размеру шрифта
        public double FontSizeRatio { get; set; } = 0.5;

        // Метод для расчета размера шрифта на основе длины текста
        public int GetFontSize(string text)
        {
            if (string.IsNullOrEmpty(text))
                return BaseFontSize;

            int length = text.Length;
            int fontSize = BaseFontSize + (int)(length * FontSizeRatio);

            return fontSize;
        }

        
    }
}
