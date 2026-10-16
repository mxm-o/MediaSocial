// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PhotoEdit
{
    public class TextGenerate
    {
        /// <summary>
        /// Исходное изображение
        /// </summary>
        public Image source = null;
        /// <summary>
        /// Цвет текста
        /// </summary>
        public Color colorText = Color.Red;
        /// <summary>
        /// Цвет обводки
        /// </summary>
        public Color colorStroke = Color.Transparent;
        /// <summary>
        /// Ширина обводки
        /// </summary>
        public int wightStroke = 0;
        /// <summary>
        /// Имя шрифта
        /// </summary>
        public string fontName = "Arial";
        /// <summary>
        /// Размер шрифта
        /// </summary>
        public float fontSize = 16;
        /// <summary>
        /// Стиль шрифта
        /// </summary>
        public FontStyle fontStyle = FontStyle.Regular;
        /// <summary>
        /// Выравнивание текста по горизонтали
        /// </summary>
        public StringAlignment stringAlignment = StringAlignment.Center;
        /// <summary>
        /// Выравнивание по ширине
        /// </summary>
        public StringAlignment lineAlignment = StringAlignment.Center;
        /// <summary>
        /// Положение прямоугольника с текстом, точка X
        /// </summary>
        public int rectX = 50;
        /// <summary>
        /// Положение прямоугольника с текстом, точка Y
        /// </summary>
        public int rectY = 50;
        /// <summary>
        /// Ширина прямоугольника с текстом
        /// </summary>
        public int rectW = 150;
        /// <summary>
        /// Высота прямоугольника с текстом
        /// </summary>
        public int rectH = 150;
        /// <summary>
        /// Текст
        /// </summary>
        public string textString = "";
        /// <summary>
        /// Цвет тени
        /// </summary>
        public Color colorShadow = Color.Transparent;
        /// <summary>
        /// Ширина тень
        /// </summary>
        public int wightShadow = 2;
        /// <summary>
        /// Отображение рамки в которой рисуется текст
        /// </summary>
        public bool debug = false;


        public Image DrawTextWithEffects()
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "Исходное изображение не может быть null.");

            if (textString == "") return source;

            // Создаем клон, чтобы не модифицировать исходное изображение
            using (Bitmap canvas = new Bitmap(source.Width, source.Height))
            using (Graphics e = Graphics.FromImage(canvas))
            {
                // Качество изображения
                e.SmoothingMode = SmoothingMode.AntiAlias;
                // Качество изображения
                e.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // Рисуем исходное изображение на холсте
                e.DrawImage(source, 0, 0, source.Width, source.Height);

                // Создайте экземпляр класса Font для текста и задайте ему нужный размер и стиль
                using (Font font = new Font(fontName, fontSize, fontStyle))
                // Отображаем текст с тенью и обводкой в прямоугольнике
                {
                    Rectangle rect = new Rectangle(rectX, rectY, rectW, rectH);

                    if (debug)
                    {
                        using (Pen pen = new Pen(Color.Black, 2))
                        {
                            pen.Alignment = PenAlignment.Inset; //<-- this
                            e.DrawRectangle(pen, rect);
                        }
                    }

                    DrawTextWithBorderAndShadow(e, textString, rect, font, colorText, colorStroke, colorShadow, wightStroke, wightShadow);

                    // Возвращаем новый экземпляр изображения с наложенным текстом
                    return (Image)canvas.Clone();
                }
            }
        }

        private void DrawTextWithBorderAndShadow(Graphics graphics, string text, Rectangle rect, Font font, Color textColor, Color borderColor, Color shadowColor, int wightStroke, int wightShadow)
        {
            if (graphics == null) throw new ArgumentNullException(nameof(graphics));
            if (text == null) throw new ArgumentNullException(nameof(text));
            if (font == null) throw new ArgumentNullException(nameof(font));

            using (StringFormat sf = new StringFormat())
            {
                // Устанавливаем формат отображения текста в центре прямоугольника
                sf.Alignment = stringAlignment;
                sf.LineAlignment = lineAlignment;

                // Тень
                if (wightShadow > 0)
                {
                    // Устанавливаем тень за текстом
                    using (SolidBrush shadowBrush = new SolidBrush(shadowColor))
                    {
                        Rectangle shadowRect = new Rectangle(rect.X + wightShadow + wightStroke, rect.Y + wightShadow + wightStroke, rect.Width, rect.Height);
                        using (GraphicsPath gpShadow = new GraphicsPath())
                        {
                            gpShadow.AddString(text, font.FontFamily, (int)font.Style, font.Size, shadowRect, sf);
                            graphics.FillPath(shadowBrush, gpShadow);
                            //graphics.DrawString(text, font, shadowBrush, shadowRect, sf);
                        }
                    }
                }

                using (GraphicsPath gp = new GraphicsPath())
                {
                    // Создаем путь для букв
                    gp.AddString(text, font.FontFamily, (int)font.Style, font.Size, rect, sf);
                    // Цвет обводки
                    using (Pen pen = new Pen(borderColor, wightStroke))
                    {
                        // Рисуем путь
                        graphics.DrawPath(pen, gp);
                    }
                    // Заполняем буквы
                    using (SolidBrush brushColor = new SolidBrush(textColor))
                    {
                        graphics.FillPath(brushColor, gp);
                    }
                }
            }
        }
    }
}
