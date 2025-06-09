// Copyright © 2025 Maxim Otrokhov. All rights reserved.

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace PhotoEdit
{
    public class RotateImage
    {
        public Image image = null;
        public float angle = 45.0f;
        public RenderQuality Quality { get; set; } = RenderQuality.High;
        public enum RenderQuality
        {
            Low,
            Medium,
            High,
            Ultra
        }

        public Image Rotate()
        {
            // Переводим угол в радианы
            double radians = angle * Math.PI / 180.0;
            double cos = Math.Abs(Math.Cos(radians));
            double sin = Math.Abs(Math.Sin(radians));

            // Рассчитываем коэффициент масштабирования
            double ratio = (double)image.Height / image.Width;

            // Рассчитываем минимальную ширину внутреннего прямоугольника
            double minWidthByWidth = image.Width / (cos + sin * ratio);
            double minWidthByHeight = image.Height / (sin + cos * ratio);

            // Берём минимум, чтобы покрыть внутренний прямоугольник после поворота
            int innerWidth = (int)Math.Min(minWidthByWidth, minWidthByHeight);
            int innerHeight = (int)(ratio * innerWidth);

            // Рассчитываем размер временного холста (диагональ исходного изображения)
            int tempWidth = (int)Math.Ceiling(image.Width * cos + image.Height * sin);
            int tempHeight = (int)Math.Ceiling(image.Width * sin + image.Height * cos);

            // Создаем новое изображение (изображение 2)
            Bitmap rotatedImage = new Bitmap(tempWidth, tempHeight, PixelFormat.Format32bppArgb);

            // Применяем поворот к изображению 2
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                // Применяем настройки качества
                ApplyQualitySettings(g, Quality);

                g.Clear(Color.Transparent);

                g.TranslateTransform(tempWidth / 2f, tempHeight / 2f);
                g.RotateTransform(angle);
                g.DrawImage(
                    image,
                    new Rectangle(-image.Width / 2, -image.Height / 2, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height),
                    GraphicsUnit.Pixel
                );
            }

            // Создаем новое изображение (изображение 1) - результирующее
            Bitmap result = new Bitmap(innerWidth, innerHeight, PixelFormat.Format32bppArgb);

            // Настройки максимального качества для изображения 1
            using (Graphics g = Graphics.FromImage(result))
            {
                // Применяем настройки качества
                ApplyQualitySettings(g, Quality);

                // Очищаем прозрачным цветом
                g.Clear(Color.Transparent);

                // Центрируем графику в новом изображении
                g.TranslateTransform(innerWidth / 2f, innerHeight / 2f);

                // Рассчитываем смещение для рисования
                int offsetX = -rotatedImage.Width / 2;
                int offsetY = -rotatedImage.Height / 2;

                // Рисуем повернутое изображение 2 по центру изображения 1
                g.DrawImage(
                    rotatedImage,
                    new Rectangle(offsetX, offsetY, rotatedImage.Width, rotatedImage.Height),
                    new Rectangle(0, 0, rotatedImage.Width, rotatedImage.Height),
                    GraphicsUnit.Pixel
                );
            }

            rotatedImage.Dispose();
            return result;
        }

        private void ApplyQualitySettings(Graphics g, RenderQuality quality)
        {
            switch (quality)
            {
                case RenderQuality.Ultra:
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    break;

                case RenderQuality.High:
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    break;

                case RenderQuality.Medium:
                    g.CompositingQuality = CompositingQuality.HighSpeed;
                    g.InterpolationMode = InterpolationMode.Bilinear;
                    g.SmoothingMode = SmoothingMode.HighSpeed;
                    g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    break;

                case RenderQuality.Low:
                default:
                    g.CompositingQuality = CompositingQuality.HighSpeed;
                    g.InterpolationMode = InterpolationMode.Low;
                    g.SmoothingMode = SmoothingMode.None;
                    g.PixelOffsetMode = PixelOffsetMode.None;
                    break;
            }
        }
    }
}