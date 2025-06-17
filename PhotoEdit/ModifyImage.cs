// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace PhotoEdit
{
    public class ModifyImage
    {
        /// <summary>
        /// Исходное изображение
        /// </summary>
        public Image image = null;
        /// <summary>
        /// значение выше 0, представляющее количество яркости, которое следует применить к изображению. Значение 1 означает отсутствие изменений яркости, тогда как значения меньше 1 уменьшают яркость, а значения больше 1 увеличивают яркость.
        /// </summary>
        public float brightness = 0f;
        /// <summary>
        /// значение больше 0, представляющее количество контраста, которое следует применить к изображению. Значение 1 означает отсутствие изменений контраста, тогда как значения больше 1 увеличивают контраст, а значения меньше 1 уменьшают контраст.
        /// </summary>
        public float contrast = 0;
        /// <summary>
        /// значение между 0 и 360, представляющее сдвиг оттенка, который следует применить к изображению. Значение 0 означает отсутствие сдвига оттенка, тогда как значения от 0 до 59 и от 301 до 360 сдвигают оттенок в сторону красного, значения от 60 до 149 сдвигают оттенок в сторону желтого, значения от 150 до 239 сдвигают оттенок в сторону зеленого, а значения от 240 до 300 сдвигают оттенок в сторону синего.
        /// </summary>
        public float hue = 0;
        /// <summary>
        /// Значение от 0 до 2, представляющее степень насыщенности цвета, которая будет применена к изображению. Значение 1 означает отсутствие изменения насыщенности, значения меньше 1 уменьшают насыщенность, а значения больше 1 увеличивают насыщенность.
        /// </summary>
        public float saturation = 0;
        /// <summary>
        /// Значение от -1 до 1, представляющее количество цветовой температуры, применяемой к изображению. Значение 0 означает отсутствие изменения цветовой температуры, в то время как значения меньше 0 уменьшают температуру (что приводит к более холодному изображению), а значения больше 0 увеличивают температуру (что приводит к более теплому изображению).
        /// </summary>
        public float temperature = 0f;

        // Изменяем Яркость
        public Image BrightnessImage()
        {
            if (brightness == 1) return image;

            // Создаем матрицу цветов
            float[][] colorMatrixElements = {
                new float[] { brightness, 0, 0, 0, 0 },
                new float[] { 0, brightness, 0, 0, 0 },
                new float[] { 0, 0, brightness, 0, 0 },
                new float[] { 0, 0, 0, 1, 0 },
                new float[] { 0, 0, 0, 0, 1 }
            };
            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            // Возвращаем измененное изображение
            return GenerateImage(colorMatrix);
        }

        // Изменяем контраст
        public Image ContrastImage()
        {
            if (contrast == 1) return image;

            // Создаем матрицу цветов
            float[][] colorMatrixElements = {
                new float[] { contrast, 0, 0, 0, 0 },
                new float[] { 0, contrast, 0, 0, 0 },
                new float[] { 0, 0, contrast, 0, 0 },
                new float[] { 0, 0, 0, 1, 0 },
                new float[] { 0.5f * (1 - contrast), 0.5f * (1 - contrast), 0.5f * (1 - contrast), 0, 1 }
            };
            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            // Возвращаем измененное изображение
            return GenerateImage(colorMatrix);
        }

        public Image ChangeSaturation()
        {
            if (saturation == 1) return image;

            // Создаем матрицу цветов
            float saturationComplement = 1.0f - saturation;
            float saturationRed = 0.3086f * saturationComplement;
            float saturationGreen = 0.6094f * saturationComplement;
            float saturationBlue = 0.0820f * saturationComplement;
            // Создаем новую матрицу цветов и устанавливаем значения насыщенности
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] { saturationRed + saturation, saturationRed, saturationRed, 0, 0 },
                new float[] { saturationGreen, saturationGreen + saturation, saturationGreen, 0, 0 },
                new float[] { saturationBlue, saturationBlue, saturationBlue + saturation, 0, 0 },
                new float[] { 0, 0, 0, 1, 0 },
                new float[] { 0, 0, 0, 0, 1 }
            });

            // Возвращаем измененное изображение
            return GenerateImage(colorMatrix);
        }

        // Меняем температуру
        public Image ChangeTemperature()
        {
            if (temperature == 0) return image;

            // Create a new ColorMatrix and set the temperature values
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {temperature, 0, 0, 0, 1}
            });

            return GenerateImage(colorMatrix);
        }

        public Image ChangeHue()
        {
            if (hue == 0) return image;

            float cos = (float)Math.Cos(hue * Math.PI / 180.0);
            float sin = (float)Math.Sin(hue * Math.PI / 180.0);
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                    new float[] {((1 - cos) / 2) + cos, ((1 - cos) / 2) - (0.143f * sin), ((1 - cos) / 2) + (0.140f * sin), 0, 0},
                    new float[] {((1 - cos) / 2) + (0.140f * sin), ((1 - cos) / 2) + cos, ((1 - cos) / 2) - (0.283f * sin), 0, 0},
                    new float[] {((1 - cos) / 2) - (0.143f * sin), ((1 - cos) / 2) + (0.283f * sin), ((1 - cos) / 2) + cos, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
            });

            return GenerateImage(colorMatrix);
        }

        private Image GenerateImage(ColorMatrix colorMatrix)
        {
            // Создаем ImageAttributes с матрицей цветов
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);

            // Создаем новое изображение и отрисовываем на нем оригинальное изображение с примененными настройками яркости
            Bitmap resultBitmap = new Bitmap(image.Width, image.Height);
            Graphics graphics = Graphics.FromImage(resultBitmap);
            graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);

            // Clean up and return the new image
            graphics.Dispose();
            imageAttributes.Dispose();

            // Возвращаем измененное изображение
            return resultBitmap;
        }

        public float CalcBrightness(Image image)
        {
            using (Bitmap bitmap = new Bitmap(image))
            {
                int width = bitmap.Width;
                int height = bitmap.Height;
                int totalPixels = width * height;

                if (totalPixels == 0)
                    return 1f;

                // Блокируем биты изображения для быстрого доступа
                BitmapData bd = bitmap.LockBits(
                    new Rectangle(0, 0, width, height),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

                try
                {
                    float totalBrightness = 0f;
                    int stride = bd.Stride;
                    unsafe
                    {
                        byte* scan0 = (byte*)bd.Scan0;

                        // Параллельный расчет по строкам
                        Parallel.For(0, height, y =>
                        {
                            float localBrightness = 0f;
                            byte* row = scan0 + (y * stride);

                            for (int x = 0; x < width; x++)
                            {
                                // Формат пикселя: BGRA
                                byte b = row[x * 4];
                                byte g = row[x * 4 + 1];
                                byte r = row[x * 4 + 2];

                                // Формула яркости (ITU-R BT.709)
                                float pixelBrightness = (0.2126f * r + 0.7152f * g + 0.0722f * b) / 255f;
                                localBrightness += pixelBrightness;
                            }

                            // Атомарное добавление к общей сумме
                            Interlocked.Exchange(ref totalBrightness, totalBrightness + localBrightness);
                        });
                    }

                    float average = totalBrightness / totalPixels;
                    if (average == 0) return 1f;

                    float factor = 0.5f / average;
                    return Math.Min(Math.Max(factor, 0f), 2f);
                }
                finally
                {
                    bitmap.UnlockBits(bd);
                }
            }
        }

        public double CalcContrast(Image image)
        {

            using (Bitmap bmp = new Bitmap(image))
            {
                int width = bmp.Width;
                int height = bmp.Height;
                int totalPixels = width * height;

                if (totalPixels == 0)
                    return 0;

                if (bmp.PixelFormat != PixelFormat.Format24bppRgb &&
                    bmp.PixelFormat != PixelFormat.Format32bppArgb)
                {
                    return CalculateForConverted(bmp);
                }

                int[] histogram = new int[256];
                int pixelSize = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;

                BitmapData bmpData = bmp.LockBits(
                    new Rectangle(0, 0, width, height),
                    ImageLockMode.ReadOnly,
                    bmp.PixelFormat
                );

                try
                {
                    unsafe
                    {
                        byte* ptr = (byte*)bmpData.Scan0;
                        int stride = bmpData.Stride;

                        Parallel.For(0, height, y =>
                        {
                            byte* row = ptr + (y * stride);
                            int[] localHist = new int[256];

                            for (int x = 0; x < width; x++)
                            {
                                int offset = x * pixelSize;
                                byte r = row[offset + 2];
                                byte g = row[offset + 1];
                                byte b = row[offset];

                                int brightness = (r * 299 + g * 587 + b * 114) / 1000;
                                if (brightness < 0) brightness = 0;
                                if (brightness > 255) brightness = 255;

                                localHist[brightness]++;
                            }

                            lock (histogram)
                            {
                                for (int i = 0; i < 256; i++)
                                    histogram[i] += localHist[i];
                            }
                        });
                    }
                }
                finally
                {
                    bmp.UnlockBits(bmpData);
                }

                // Рассчет пороговых значений с более широкими границами
                double lowerThreshold = totalPixels * 0.01;   // 1% вместо 0.5%
                double upperThreshold = totalPixels * 0.99;   // 99% вместо 99.5%

                int minBrightness = 0;
                int maxBrightness = 245;
                int accumulated = 0;

                // Поиск нижнего процентиля (1%)
                for (int i = 0; i < 256; i++)
                {
                    accumulated += histogram[i];
                    if (accumulated >= lowerThreshold)
                    {
                        minBrightness = i;
                        break;
                    }
                }

                accumulated = 0;
                // Поиск верхнего процентиля (99%)
                for (int i = 255; i >= 0; i--)
                {
                    accumulated += histogram[i];
                    if (accumulated >= totalPixels - upperThreshold)
                    {
                        maxBrightness = i;
                        break;
                    }
                }

                double currentRange = maxBrightness - minBrightness;
                if (currentRange <= 0)
                    return 0;

                // Корректный расчет контраста
                double normalizedRange = currentRange / 255.0;
                double contrastValue = 1.0;

                // Плавная коррекция контраста
                if (normalizedRange < 0.25)
                    contrastValue = 1.0 + (1.0 - normalizedRange * 2);
                else if (normalizedRange < 0.5)
                    contrastValue = 1.0 + (0.5 - normalizedRange);
                else if (normalizedRange > 0.9)
                    contrastValue = 0.9 / normalizedRange;

                // Ограничение диапазона
                return Math.Max(0.1, Math.Min(2.0, contrastValue));
            }
        }

        private double CalculateForConverted(Bitmap bmp)
        {
            using (Bitmap converted = new Bitmap(
                bmp.Width,
                bmp.Height,
                PixelFormat.Format24bppRgb))
            {
                using (Graphics g = Graphics.FromImage(converted))
                {
                    g.DrawImage(bmp, 0, 0);
                }
                return CalcContrast(converted);
            }
        }
    }
}
