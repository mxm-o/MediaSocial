// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System.Drawing;

namespace PhotoEdit
{
    // Объединение двух изображений
    public class Merge
    {
        public Image sourceTop = null;
        public Image sourceBottom = null;
        public int top = 0;
        public int left = 0;

        public Image MergeImage()
        {
            if (sourceTop == null || sourceBottom == null) return null;

            Image dest = new Bitmap(sourceBottom.Width, sourceBottom.Height);
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.DrawImage(sourceBottom, 0, 0, sourceBottom.Width, sourceBottom.Height);
                gr.DrawImage(sourceTop, left, top, sourceTop.Width, sourceTop.Height);
            }
            return dest;
        }
    }
}
