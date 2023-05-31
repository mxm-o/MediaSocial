// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System.Drawing;

namespace PhotoEdit
{
    public class CalculateFontSize
    {
        // Declare the font size variables
        public int baseFontSize = 14;
        public double fontSizeRatio = 0.5;

        // Declare a method to calculate the font size based on the length of text
        public int FontSize(string text)
        {
            int length = text.Length;
            int fontSize = baseFontSize - (int)(length * fontSizeRatio);
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
