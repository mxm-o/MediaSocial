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
        /// <summary>
        /// Исходное изображение
        /// </summary>
        public Image source = null;
        /// <summary>
        /// Ширина итогового изображения в пикселях
        /// </summary>
        public int width = 800;
        /// <summary>
        /// Высота итогового изображения в пикселях
        /// </summary>
        public int height = 800;
        /// <summary>
        /// Увеличение (+) или уменьшение (-/отрицательное значение) итогового изображения
        /// </summary>
        public float zoom = 0;
        /// <summary>
        /// Сдвиг итогового изображения по вертикали
        /// </summary>
        public float vertical = 0;
        /// <summary>
        /// Сдвиг итогового изображения по горизонтали
        /// </summary>
        public float horizontal = 0;
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
        public alignVertical alignV = alignVertical.Center;
        /// <summary>
        /// Положение по горизонтали
        /// </summary>
        public alignHorizontal alignH = alignHorizontal.Center;

        /// <summary>
        /// Предотвращение пустых областей на холсте
        /// </summary>
        public bool preventOverflow = true;

        /// <summary>
        /// Изменение размера изображения
        /// </summary>
        /// <returns></returns>
        public Image ScaleImage()
        {
            Image dest = new Bitmap(width, height);
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.FillRectangle(Brushes.White, 0, 0, width, height);
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                float srcwidth = source.Width;
                float srcheight = source.Height;
                float dstwidth = width;
                float dstheight = height;
                float left = 0;
                float top = 0;

                // Применяем зум

                // Предотвращение пустых областей от отрицательного зума
                if (preventOverflow && zoom < 0) zoom = 0;

                dstwidth += dstwidth * zoom / 100;
                dstheight += dstheight * zoom / 100;

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
                    if (alignH == alignHorizontal.Center)
                    {
                        left = -(dstwidth - width) / 2;
                    }
                    else if (alignH == alignHorizontal.Right)
                    {
                        left = -(dstwidth - width);
                    }

                    // Only adjust left offset if alignment is not Center
                    //if (alignH != alignHorizontal.Center)
                    //{
                    //    left = left - width * horizontal * -1 / 100;
                    //}
                }
                else
                {
                    if (alignV == alignVertical.Center)
                    {
                        top = -(dstheight - height) / 2;
                    }
                    else if (alignV == alignVertical.Bottom)
                    {
                        top = -(dstheight - height);
                    }
                    // Only adjust top offset if alignment is not Center
                    //if (alignV != alignVertical.Center)
                    //{
                    //    top = top - height * vertical * -1 / 100;
                    //}
                }

                // Применяем пользовательские сдвиги
                left += width * horizontal / 100;
                top += height * vertical / 100;

                // Корректировка для предотвращения пустых областей
                if (preventOverflow)
                {
                    // Рассчитываем масштаб для полного покрытия холста
                    float scaleX = width / dstwidth;
                    float scaleY = height / dstheight;
                    float scale = Math.Max(scaleX, scaleY);

                    // Увеличиваем только если текущее изображение меньше холста
                    if (scale > 1f)
                    {
                        dstwidth *= scale;
                        dstheight *= scale;
                    }

                    // Корректируем позицию
                    float maxLeft = width - dstwidth;
                    float maxTop = height - dstheight;

                    left = Math.Max(left, maxLeft);
                    left = Math.Min(left, 0);
                    top = Math.Max(top, maxTop);
                    top = Math.Min(top, 0);
                }

                gr.DrawImage(source, left, top, dstwidth, dstheight);
                return dest;
            }
        }
    }
}
