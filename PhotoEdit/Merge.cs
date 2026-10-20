// Copyright © 2026 Maxim Otrokhov. All rights reserved.

using System.Drawing;

namespace PhotoEdit
{
    // Объединение двух изображений
    public class Merge
    {
        /// <summary>Верхнее (накладываемое поверх) изображение.</summary>
        public Image SourceTop { get; set; } = null;

        /// <summary>Нижнее (фоновое) изображение — задаёт размер результата.</summary>
        public Image SourceBottom { get; set; } = null;

        /// <summary>Смещение верхнего изображения по вертикали, px.</summary>
        public int Top { get; set; } = 0;

        /// <summary>Смещение верхнего изображения по горизонтали, px.</summary>
        public int Left { get; set; } = 0;

        /// <summary>
        /// Накладывает <see cref="SourceTop"/> поверх <see cref="SourceBottom"/> со смещением
        /// <see cref="Left"/>/<see cref="Top"/>.
        /// </summary>
        /// <returns>Новое изображение-результат; null, если один из источников не задан.</returns>
        public Image MergeImage()
        {
            if (SourceTop == null || SourceBottom == null) return null;

            Image dest = new Bitmap(SourceBottom.Width, SourceBottom.Height);
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.DrawImage(SourceBottom, 0, 0, SourceBottom.Width, SourceBottom.Height);
                gr.DrawImage(SourceTop, Left, Top, SourceTop.Width, SourceTop.Height);
            }
            return dest;
        }
    }
}
