// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System.Drawing.Drawing2D;
using System.Drawing;

namespace PhotoEdit
{
    public class RotateImage
    {
        /// <summary>
        /// Исходное изображение
        /// </summary>
        public Image image = null;
        /// <summary>
        /// Угол поворота
        /// </summary>
        public float angle = 45.0f;

        public Image Rotate()
        {
            // Create a new blank bitmap to hold the rotated image
            Bitmap rotatedImage = new Bitmap(image.Width, image.Height);

            // Rotate the image using a Graphics object
            using (Graphics graphics = Graphics.FromImage(rotatedImage))
            {
                // Качество
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.Default;


                // Translate the graphics origin to the center of the image
                graphics.TranslateTransform(image.Width / 2f, image.Height / 2f);

                // Rotate the graphics object
                graphics.RotateTransform(angle);

                // Draw the image onto the new bitmap
                graphics.DrawImage(image, new Rectangle(-image.Width / 2, -image.Height / 2, image.Width, image.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

                // Clean up and return the new image
                graphics.Dispose();
            }
            // Return the rotated image
            return rotatedImage;
        }
    }
}