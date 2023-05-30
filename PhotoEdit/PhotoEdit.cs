using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public enum alignVertical { 
            Center, Top, Bottom
        };
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
        /// Изменение размера изображения
        /// </summary>
        /// <returns></returns>
        public Image ScaleImage()
        {
            Image dest = new Bitmap(width, height);
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.FillRectangle(Brushes.White, 0, 0, width, height);  // Очищаем экран
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                float srcwidth = source.Width;  // Исходная ширина.
                float srcheight = source.Height; // Исходная высота.
                float dstwidth = width; // Результирующая ширина.
                float dstheight = height; // Результирующая высота.
                float left = 0; // x-координата результирующего изображения.
                float top = 0; // y-координата результирующего изображения.

                dstwidth = dstwidth + dstwidth * zoom / 100;
                dstheight = dstheight + dstheight * zoom / 100;

                float ratio_x = srcwidth / dstwidth;
                float ratio_y = srcheight / dstheight;

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

                if (ratio_x > ratio_y)
                {
                    // Горизонтальная фотография
                    if (alignH == alignHorizontal.Center)
                    {
                        left = -(dstwidth - width) / 2;
                    }
                    else if (alignH == alignHorizontal.Right)
                    {
                        left = -(dstwidth - width);
                    }
                    // Only adjust left offset if alignment is not Center
                    if (alignH != alignHorizontal.Center)
                    {
                        left = left - width * horizontal * -1 / 100;
                    }
                }
                else
                {
                    //Вертикальная
                    if (alignV == alignVertical.Center)
                    {
                        top = -(dstheight - height) / 2;
                    }
                    if (alignV == alignVertical.Bottom)
                    {
                        top = -(dstheight - height);
                    }

                    // Only adjust top offset if alignment is not Center
                    if (alignV != alignVertical.Center)
                    {
                        top = top - height * vertical * -1 / 100;
                    }
                }

                left = left - width * horizontal * -1 / 100;
                top = top - height * vertical * -1 / 100;


                gr.DrawImage(source, left, top, dstwidth, dstheight);

                return dest;
            }
        }
    }
}
