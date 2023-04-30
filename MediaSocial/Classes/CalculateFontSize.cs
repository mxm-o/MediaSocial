using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSocial.Classes
{
    internal class CalculateFontSize
    {
        // Declare the font size variables
        public int baseFontSize = 14;
        public double fontSizeRatio = 0.5;

        // Declare a method to calculate the font size based on the length of text
        public int FontSize(string text)
        {
            int length = text.Length;
            int fontSize = baseFontSize + (int)(length * fontSizeRatio);

            return fontSize;
        }

        
    }
}
