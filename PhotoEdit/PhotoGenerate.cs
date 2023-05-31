// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System.Drawing;
using System.Drawing.Drawing2D;

namespace PhotoEdit
{
    public class PhotoGenerate
    {
        /// <summary>
        /// Ширина итогового изображения в пикселях
        /// </summary>
        public int width = 800;
        /// <summary>
        /// Высота итогового изображения в пикселях
        /// </summary>
        public int height = 800;
        /// <summary>
        /// Цвет генерируемого изображения
        /// </summary>
        public Color colorMain = Color.Transparent;
        /// <summary>
        /// Цвет генерируемого изображения
        /// </summary>
        public Color colorSecond = Color.Transparent;
        /// <summary>
        /// Толщина обводки
        /// </summary>
        public float widthPen = 0;
        /// <summary>
        /// Цвет ренерируемого изображения
        /// </summary>
        public Color colorPen = Color.Transparent;
        /// <summary>
        /// Режим градиета
        /// </summary>
        public LinearGradientMode mode = LinearGradientMode.Horizontal;

        /// <summary>
        /// Создание прямоугольника с обводкой
        /// </summary>
        /// <returns>Image</returns>
        public Image Fill()
        {
            Image dest = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(dest))
            {
                // Заливка прямоугольника
                SolidBrush drawBrush = new SolidBrush(colorMain);
                graphics.FillRectangle(drawBrush, 0, 0, width, height);
                // Внутренняя обводка прямоугольника
                Pen pen = new Pen(colorPen, widthPen);
                graphics.DrawRectangle(pen, 0, 0, width, height);
            }
            return dest;
        }


        public Image Gradient()
        {
            Image dest = new Bitmap(width, height);

            using (Graphics graphics = Graphics.FromImage(dest))
            using (LinearGradientBrush brush = new LinearGradientBrush(
                new Rectangle(0, 0, width, height),
                colorMain,
                colorSecond,
                mode))
            {
                // Заливка прямоугольника
                brush.SetSigmaBellShape(0.5f);
                graphics.FillRectangle(brush, new Rectangle(0, 0, width, height));
                // Внутренняя обводка прямоугольника
                Pen pen = new Pen(colorPen, widthPen);
                graphics.DrawRectangle(pen, 0, 0, width, height);
            }

            return dest;
        }
    }
}
