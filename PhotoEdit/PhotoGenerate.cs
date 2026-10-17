// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PhotoEdit
{
    public class PhotoGenerate
    {
        private int _width = 800;
        private int _height = 800;
        private Color _colorMain = Color.Transparent;
        private Color _colorSecond = Color.Transparent;
        private float _widthPen = 0;
        private Color _colorPen = Color.Transparent;
        private LinearGradientMode _mode = LinearGradientMode.Horizontal;

        /// <summary>
        /// Ширина итогового изображения в пикселях (должна быть > 0)
        /// </summary>
        public int Width
        {
            get => _width;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Ширина изображения должна быть больше 0.");
                _width = value;
            }
        }

        /// <summary>
        /// Высота итогового изображения в пикселях (должна быть > 0)
        /// </summary>
        public int Height
        {
            get => _height;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Высота изображения должна быть больше 0.");
                _height = value;
            }
        }

        /// <summary>
        /// Основной цвет генерируемого изображения
        /// </summary>
        public Color ColorMain
        {
            get => _colorMain;
            set => _colorMain = value;
        }

        /// <summary>
        /// Второй цвет для градиента
        /// </summary>
        public Color ColorSecond
        {
            get => _colorSecond;
            set => _colorSecond = value;
        }

        /// <summary>
        /// Толщина обводки в пикселях (должна быть >= 0)
        /// </summary>
        public float WidthPen
        {
            get => _widthPen;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Толщина обводки не может быть отрицательной.");
                _widthPen = value;
            }
        }

        /// <summary>
        /// Цвет обводки
        /// </summary>
        public Color ColorPen
        {
            get => _colorPen;
            set => _colorPen = value;
        }

        /// <summary>
        /// Режим градиента
        /// </summary>
        public LinearGradientMode Mode
        {
            get => _mode;
            set => _mode = value;
        }

        /// <summary>
        /// Создание прямоугольника с обводкой
        /// </summary>
        /// <returns>Image</returns>
        public Image Fill()
        {
            Image dest = new Bitmap(Width, Height);
            using (Graphics graphics = Graphics.FromImage(dest))
            {
                // Заливка прямоугольника
                using (SolidBrush drawBrush = new SolidBrush(ColorMain))
                {
                    graphics.FillRectangle(drawBrush, 0, 0, Width, Height);
                }
                // Внутренняя обводка прямоугольника
                using (Pen pen = new Pen(ColorPen, WidthPen))
                {
                    graphics.DrawRectangle(pen, 0, 0, Width, Height);
                }
            }
            return dest;
        }


        public Image Gradient()
        {
            Image dest = new Bitmap(Width, Height);

            using (Graphics graphics = Graphics.FromImage(dest))
            using (LinearGradientBrush brush = new LinearGradientBrush(
                new Rectangle(0, 0, Width, Height),
                ColorMain,
                ColorSecond,
                Mode))
            {
                // Заливка прямоугольника
                brush.SetSigmaBellShape(0.5f);
                graphics.FillRectangle(brush, new Rectangle(0, 0, Width, Height));
                // Внутренняя обводка прямоугольника
                using (Pen pen = new Pen(ColorPen, WidthPen))
                {
                    graphics.DrawRectangle(pen, 0, 0, Width, Height);
                }
            }

            return dest;
        }
    }
}
