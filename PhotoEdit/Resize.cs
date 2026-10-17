// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Drawing;

namespace PhotoEdit
{
    public class Resize
    {
        private Image _sourceImage = null;
        private int _canvasWidth = 1800;
        private int _canvasHeight = 1800;
        private int _posX = 0;
        private int _posY = 0;
        private float _scaleFactor = 1.0f;

        /// <summary>
        /// Исходное изображение
        /// </summary>
        public Image SourceImage
        {
            get => _sourceImage;
            set => _sourceImage = value;
        }

        /// <summary>
        /// Ширина холста в пикселях (должна быть > 0)
        /// </summary>
        public int CanvasWidth
        {
            get => _canvasWidth;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Ширина холста должна быть больше 0.");
                _canvasWidth = value;
            }
        }

        /// <summary>
        /// Высота холста в пикселях (должна быть > 0)
        /// </summary>
        public int CanvasHeight
        {
            get => _canvasHeight;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Высота холста должна быть больше 0.");
                _canvasHeight = value;
            }
        }

        /// <summary>
        /// Позиция X для вставки изображения на холсте
        /// </summary>
        public int PosX
        {
            get => _posX;
            set => _posX = value;
        }

        /// <summary>
        /// Позиция Y для вставки изображения на холсте
        /// </summary>
        public int PosY
        {
            get => _posY;
            set => _posY = value;
        }

        /// <summary>
        /// Коэффициент масштабирования (должен быть > 0)
        /// </summary>
        public float ScaleFactor
        {
            get => _scaleFactor;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Коэффициент масштабирования должен быть больше 0.");
                _scaleFactor = value;
            }
        }

        public Image ResizeImage()
        {
            if (SourceImage == null)
                throw new ArgumentNullException(nameof(SourceImage), "Исходное изображение не может быть null.");

            Bitmap canvas = new Bitmap(CanvasWidth, CanvasHeight);
            using (Graphics graphics = Graphics.FromImage(canvas))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                int newWidth = (int)(SourceImage.Width * ScaleFactor);
                int newHeight = (int)(SourceImage.Height * ScaleFactor);

                graphics.DrawImage(SourceImage, PosX, PosY, newWidth, newHeight);
            }

            return canvas;
        }
    }
}
