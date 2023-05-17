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
            if (textString == "") return source;

            // Исходное изображение
            Graphics e = Graphics.FromImage(source);
            // Качество изображения
            e.SmoothingMode = SmoothingMode.AntiAlias;
            // Качество изображения
            e.PixelOffsetMode = PixelOffsetMode.HighQuality;
            // Создайте экземпляр класса Font для текста и задайте ему нужный размер и стиль
            Font font = new Font(fontName, fontSize, fontStyle);
            // Отображаем текст с тенью и обводкой в прямоугольнике
            Rectangle rect = new Rectangle(rectX, rectY, rectW, rectH);

            if (debug)
            {
                Pen pen = new Pen(Color.Black, 2);
                pen.Alignment = PenAlignment.Inset; //<-- this
                e.DrawRectangle(pen, rect);
            }

            DrawTextWithBorderAndShadow(e, textString, rect, font, colorText, colorStroke, colorShadow, wightStroke, wightShadow);

            return source;
        }

        private void DrawTextWithBorderAndShadow(Graphics graphics, string text, Rectangle rect, Font font, Color textColor, Color borderColor, Color shadowColor, int wightStroke, int wightShadow)
        {
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
                        GraphicsPath gpShadow = new GraphicsPath();
                        gpShadow.AddString(text, font.FontFamily, (int)font.Style, font.Size, shadowRect, sf);
                        graphics.FillPath(shadowBrush, gpShadow);
                        //graphics.DrawString(text, font, shadowBrush, shadowRect, sf);
                    }
                }

                GraphicsPath gp = new GraphicsPath();
                // Создаем путь для букв
                gp.AddString(text, font.FontFamily, (int)font.Style, font.Size, rect, sf);
                // Цвет обводки
                Pen pen = new Pen(borderColor, wightStroke);
                // Рисуем путь
                graphics.DrawPath(pen, gp);
                // Заполняем буквы
                SolidBrush brushColor = new SolidBrush(textColor);
                graphics.FillPath(brushColor, gp);
            }
        }
    }
}
