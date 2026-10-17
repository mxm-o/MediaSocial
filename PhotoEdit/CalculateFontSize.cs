// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Drawing;

namespace PhotoEdit
{
    public class CalculateFontSize
    {
        private int _baseFontSize = 14;
        private double _fontSizeRatio = 0.5;

        /// <summary>
        /// Базовый размер шрифта в пикселях (должен быть > 0)
        /// </summary>
        public int BaseFontSize
        {
            get => _baseFontSize;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Базовый размер шрифта должен быть больше 0.");
                _baseFontSize = value;
            }
        }

        /// <summary>
        /// Коэффициент соотношения длины текста к размеру шрифта (должен быть > 0)
        /// </summary>
        public double FontSizeRatio
        {
            get => _fontSizeRatio;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Коэффициент должен быть больше 0.");
                _fontSizeRatio = value;
            }
        }

        /// <summary>
        /// Метод для расчета размера шрифта на основе длины текста
        /// </summary>
        public int GetFontSize(string text)
        {
            if (string.IsNullOrEmpty(text))
                return BaseFontSize;

            int length = text.Length;
            int fontSize = BaseFontSize - (int)(length * FontSizeRatio);
            if (fontSize < 1) fontSize = 1;

            return fontSize;
        }

        private bool CheckFontSize(string text, string fontName, float fontSize, FontStyle fontStyle, int width, int height, StringFormat stringFormat)
        {
            // Create a new Bitmap with the specified width and height
            Bitmap bitmap = new Bitmap(width, height);

            // Create a Graphics object from the Bitmap
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                Font font = new Font(fontName, fontSize, fontStyle);
                // Measuring the text to check if it fits in the rectangle
                SizeF textSize = graphics.MeasureString(text, font, new SizeF(width, height), stringFormat);
                if (textSize.Width > width || textSize.Height > height)
                {
                    // Text won't fit, do nothing
                    bitmap.Dispose();
                    return false;
                }
            }
            bitmap.Dispose();
            return true;
        }

        public float CalcMaxFontSize(string text, string fontName, float fontSize, FontStyle fontStyle, int width, int height, StringFormat stringFormat)
        {
            while (!CheckFontSize(text, fontName, fontSize, fontStyle, width, height, stringFormat))
            {
                fontSize = (float)fontSize - 0.5f;
                if (fontSize < 20)
                {
                    fontSize = 20;
                    break;
                }
            }
            return fontSize;
        }
    }
}
