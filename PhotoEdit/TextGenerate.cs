// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PhotoEdit
{
    public class TextGenerate
    {
        private Image _source = null;
        private Color _colorText = Color.Red;
        private Color _colorStroke = Color.Transparent;
        private int _widthStroke = 0;
        private string _fontName = "Arial";
        private float _fontSize = 16;
        private FontStyle _fontStyle = FontStyle.Regular;
        private StringAlignment _stringAlignment = StringAlignment.Center;
        private StringAlignment _lineAlignment = StringAlignment.Center;
        private int _rectX = 50;
        private int _rectY = 50;
        private int _rectW = 150;
        private int _rectH = 150;
        private string _textString = "";
        private Color _colorShadow = Color.Transparent;
        private int _widthShadow = 2;
        private bool _debug = false;

        /// <summary>
        /// Исходное изображение
        /// </summary>
        public Image Source
        {
            get => _source;
            set => _source = value;
        }

        /// <summary>
        /// Цвет текста
        /// </summary>
        public Color ColorText
        {
            get => _colorText;
            set => _colorText = value;
        }

        /// <summary>
        /// Цвет обводки
        /// </summary>
        public Color ColorStroke
        {
            get => _colorStroke;
            set => _colorStroke = value;
        }

        /// <summary>
        /// Ширина обводки в пикселях (должна быть >= 0)
        /// </summary>
        public int WidthStroke
        {
            get => _widthStroke;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Ширина обводки не может быть отрицательной.");
                _widthStroke = value;
            }
        }

        /// <summary>
        /// Имя шрифта
        /// </summary>
        public string FontName
        {
            get => _fontName;
            set => _fontName = value ?? "Arial";
        }

        /// <summary>
        /// Размер шрифта в пикселях (должен быть > 0)
        /// </summary>
        public float FontSize
        {
            get => _fontSize;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Размер шрифта должен быть больше 0.");
                _fontSize = value;
            }
        }

        /// <summary>
        /// Стиль шрифта
        /// </summary>
        public FontStyle FontStyle
        {
            get => _fontStyle;
            set => _fontStyle = value;
        }

        /// <summary>
        /// Выравнивание текста по горизонтали
        /// </summary>
        public StringAlignment StringAlignment
        {
            get => _stringAlignment;
            set => _stringAlignment = value;
        }

        /// <summary>
        /// Выравнивание по высоте
        /// </summary>
        public StringAlignment LineAlignment
        {
            get => _lineAlignment;
            set => _lineAlignment = value;
        }

        /// <summary>
        /// Положение прямоугольника с текстом, точка X
        /// </summary>
        public int RectX
        {
            get => _rectX;
            set => _rectX = value;
        }

        /// <summary>
        /// Положение прямоугольника с текстом, точка Y
        /// </summary>
        public int RectY
        {
            get => _rectY;
            set => _rectY = value;
        }

        /// <summary>
        /// Ширина прямоугольника с текстом (должна быть > 0)
        /// </summary>
        public int RectW
        {
            get => _rectW;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Ширина прямоугольника должна быть больше 0.");
                _rectW = value;
            }
        }

        /// <summary>
        /// Высота прямоугольника с текстом (должна быть > 0)
        /// </summary>
        public int RectH
        {
            get => _rectH;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Высота прямоугольника должна быть больше 0.");
                _rectH = value;
            }
        }

        /// <summary>
        /// Текст для отрисовки
        /// </summary>
        public string TextString
        {
            get => _textString;
            set => _textString = value ?? "";
        }

        /// <summary>
        /// Цвет тени
        /// </summary>
        public Color ColorShadow
        {
            get => _colorShadow;
            set => _colorShadow = value;
        }

        /// <summary>
        /// Ширина тени в пикселях (должна быть >= 0)
        /// </summary>
        public int WidthShadow
        {
            get => _widthShadow;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Ширина тени не может быть отрицательной.");
                _widthShadow = value;
            }
        }

        /// <summary>
        /// Отображение рамки в которой рисуется текст (режим отладки)
        /// </summary>
        public bool Debug
        {
            get => _debug;
            set => _debug = value;
        }

        public Image DrawTextWithEffects()
        {
            if (Source == null)
                throw new ArgumentNullException(nameof(Source), "Исходное изображение не может быть null.");

            if (string.IsNullOrEmpty(TextString)) return Source;

            // Создаем клон, чтобы не модифицировать исходное изображение
            using (Bitmap canvas = new Bitmap(Source.Width, Source.Height))
            using (Graphics e = Graphics.FromImage(canvas))
            {
                // Качество изображения
                e.SmoothingMode = SmoothingMode.AntiAlias;
                // Качество изображения
                e.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // Рисуем исходное изображение на холсте
                e.DrawImage(Source, 0, 0, Source.Width, Source.Height);

                // Создайте экземпляр класса Font для текста и задайте ему нужный размер и стиль
                using (Font font = new Font(FontName, FontSize, FontStyle))
                // Отображаем текст с тенью и обводкой в прямоугольнике
                {
                    Rectangle rect = new Rectangle(RectX, RectY, RectW, RectH);

                    if (Debug)
                    {
                        using (Pen pen = new Pen(Color.Black, 2))
                        {
                            pen.Alignment = PenAlignment.Inset; //<-- this
                            e.DrawRectangle(pen, rect);
                        }
                    }

                    DrawTextWithBorderAndShadow(e, TextString, rect, font, ColorText, ColorStroke, ColorShadow, WidthStroke, WidthShadow);

                    // Возвращаем новый экземпляр изображения с наложенным текстом
                    return (Image)canvas.Clone();
                }
            }
        }

        private void DrawTextWithBorderAndShadow(Graphics graphics, string text, Rectangle rect, Font font, Color textColor, Color borderColor, Color shadowColor, int widthStroke, int widthShadow)
        {
            if (graphics == null) throw new ArgumentNullException(nameof(graphics));
            if (text == null) throw new ArgumentNullException(nameof(text));
            if (font == null) throw new ArgumentNullException(nameof(font));

            using (StringFormat sf = new StringFormat())
            {
                // Устанавливаем формат отображения текста в центре прямоугольника
                sf.Alignment = StringAlignment;
                sf.LineAlignment = LineAlignment;

                // Тень
                if (widthShadow > 0)
                {
                    // Устанавливаем тень за текстом
                    using (SolidBrush shadowBrush = new SolidBrush(shadowColor))
                    {
                        Rectangle shadowRect = new Rectangle(rect.X + widthShadow + widthStroke, rect.Y + widthShadow + widthStroke, rect.Width, rect.Height);
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
                    using (Pen pen = new Pen(borderColor, widthStroke))
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
