// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Drawing;

namespace PhotoEdit
{
    /// <summary>
    /// Редактирование фото
    /// </summary>
    public class PhotoSize
    {
        private Image _source = null;
        private int _width = 800;
        private int _height = 800;
        private float _zoom = 0;
        private float _vertical = 0;
        private float _horizontal = 0;
        private alignVertical _alignV = alignVertical.Center;
        private alignHorizontal _alignH = alignHorizontal.Center;
        private bool _preventOverflow = true;

        /// <summary>
        /// Исходное изображение
        /// </summary>
        public Image Source
        {
            get => _source;
            set => _source = value;
        }

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
        /// Увеличение (+) или уменьшение (-/отрицательное значение) итогового изображения в процентах
        /// </summary>
        public float Zoom
        {
            get => _zoom;
            set => _zoom = value;
        }

        /// <summary>
        /// Сдвиг итогового изображения по вертикали в процентах
        /// </summary>
        public float Vertical
        {
            get => _vertical;
            set => _vertical = value;
        }

        /// <summary>
        /// Сдвиг итогового изображения по горизонтали в процентах
        /// </summary>
        public float Horizontal
        {
            get => _horizontal;
            set => _horizontal = value;
        }

        /// <summary>
        /// Положение изображения по вертикали
        /// </summary>
        public enum alignVertical
        {
            Center, Top, Bottom
        };

        /// <summary>
        /// Положение изображения по горизонтали
        /// </summary>
        public enum alignHorizontal
        {
            Center, Left, Right
        };

        /// <summary>
        /// Положение по вертикали
        /// </summary>
        public alignVertical AlignV
        {
            get => _alignV;
            set => _alignV = value;
        }

        /// <summary>
        /// Положение по горизонтали
        /// </summary>
        public alignHorizontal AlignH
        {
            get => _alignH;
            set => _alignH = value;
        }

        /// <summary>
        /// Предотвращение пустых областей на холсте
        /// </summary>
        public bool PreventOverflow
        {
            get => _preventOverflow;
            set => _preventOverflow = value;
        }

        /// <summary>
        /// Изменение размера изображения
        /// </summary>
        /// <returns></returns>
        public Image ScaleImage()
        {
            if (Source == null)
                throw new ArgumentNullException(nameof(Source), "Исходное изображение не может быть null.");

            Image dest = new Bitmap(Width, Height);
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.FillRectangle(Brushes.White, 0, 0, Width, Height);
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                float srcwidth = Source.Width;
                float srcheight = Source.Height;
                float dstwidth = Width;
                float dstheight = Height;
                float left = 0;
                float top = 0;

                // Применяем зум

                // Предотвращение пустых областей от отрицательного зума
                if (PreventOverflow && Zoom < 0) Zoom = 0;

                dstwidth += dstwidth * Zoom / 100;
                dstheight += dstheight * Zoom / 100;

                // Сохраняем исходные размеры после зума
                float zoomedWidth = dstwidth;
                float zoomedHeight = dstheight;

                // Рассчитываем соотношения сторон
                float ratio_x = srcwidth / dstwidth;
                float ratio_y = srcheight / dstheight;

                // Масштабирование с сохранением пропорций
                if (ratio_x < ratio_y)
                {
                    dstwidth = srcwidth / ratio_x;
                    dstheight = srcheight / ratio_x;
                }
                else
                {
                    dstwidth = srcwidth / ratio_y;
                    dstheight = srcheight / ratio_y;
                }

                // Определяем тип ориентации
                bool isLandscape = ratio_x > ratio_y;

                // Расчет позиции с учетом выравнивания
                if (isLandscape)
                {
                    if (AlignH == alignHorizontal.Center)
                    {
                        left = -(dstwidth - Width) / 2;
                    }
                    else if (AlignH == alignHorizontal.Right)
                    {
                        left = -(dstwidth - Width);
                    }

                    // Only adjust left offset if alignment is not Center
                    //if (AlignH != alignHorizontal.Center)
                    //{
                    //    left = left - Width * Horizontal * -1 / 100;
                    //}
                }
                else
                {
                    if (AlignV == alignVertical.Center)
                    {
                        top = -(dstheight - Height) / 2;
                    }
                    else if (AlignV == alignVertical.Bottom)
                    {
                        top = -(dstheight - Height);
                    }
                    // Only adjust top offset if alignment is not Center
                    //if (AlignV != alignVertical.Center)
                    //{
                    //    top = top - Height * Vertical * -1 / 100;
                    //}
                }

                // Применяем пользовательские сдвиги
                left += Width * Horizontal / 100;
                top += Height * Vertical / 100;

                // Корректировка для предотвращения пустых областей
                if (PreventOverflow)
                {
                    // Рассчитываем масштаб для полного покрытия холста
                    float scaleX = Width / dstwidth;
                    float scaleY = Height / dstheight;
                    float scale = Math.Max(scaleX, scaleY);

                    // Увеличиваем только если текущее изображение меньше холста
                    if (scale > 1f)
                    {
                        dstwidth *= scale;
                        dstheight *= scale;
                    }

                    // Корректируем позицию
                    float maxLeft = Width - dstwidth;
                    float maxTop = Height - dstheight;

                    left = Math.Max(left, maxLeft);
                    left = Math.Min(left, 0);
                    top = Math.Max(top, maxTop);
                    top = Math.Min(top, 0);
                }

                gr.DrawImage(Source, left, top, dstwidth, dstheight);
                return dest;
            }
        }
    }
}
