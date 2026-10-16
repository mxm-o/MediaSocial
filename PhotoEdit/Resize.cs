// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Drawing;

namespace PhotoEdit
{
    public class Resize
    {
        Image sourceImage = null;
        int canvasWidth = 1800;
        int canvasHeight = 1800;
        int posX = 0;
        int posY = 0;
        float scaleFactor = 1.0f;

        public Image ResizeImage()
        {
            if (sourceImage == null)
                throw new ArgumentNullException(nameof(sourceImage), "Исходное изображение не может быть null.");

            Bitmap canvas = new Bitmap(canvasWidth, canvasHeight);
            using (Graphics graphics = Graphics.FromImage(canvas))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                int newWidth = (int)(sourceImage.Width * scaleFactor);
                int newHeight = (int)(sourceImage.Height * scaleFactor);

                graphics.DrawImage(sourceImage, posX, posY, newWidth, newHeight);
            }

            return canvas;
        }
    }
}
