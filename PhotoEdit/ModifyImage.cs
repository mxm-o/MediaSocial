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
        private Image _image = null;
        private float _brightness = 0f;
        private float _contrast = 0f;
        private float _hue = 0f;
        private float _saturation = 0f;
        private float _temperature = 0f;

        /// <summary>
        /// Исходное изображение
        /// </summary>
        public Image Image
        {
            get => _image;
            set => _image = value;
        }

        /// <summary>
        /// Значение яркости. 1 = без изменений, < 1 = темнее, > 1 = светлее. Должно быть > 0.
        /// </summary>
        public float Brightness
        {
            get => _brightness;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Яркость должна быть больше 0.");
                _brightness = value;
            }
        }

        /// <summary>
        /// Значение контраста. 1 = без изменений, > 1 = больше контраста, < 1 = меньше контраста. Должно быть > 0.
        /// </summary>
        public float Contrast
        {
            get => _contrast;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Контраст должен быть больше 0.");
                _contrast = value;
            }
        }

        /// <summary>
        /// Сдвиг оттенка в градусах (0-360). 0 = без изменений.
        /// </summary>
        public float Hue
        {
            get => _hue;
            set
            {
                if (value < 0 || value > 360)
                    throw new ArgumentOutOfRangeException(nameof(value), "Оттенок должен быть в диапазоне от 0 до 360.");
                _hue = value;
            }
        }

        /// <summary>
        /// Степень насыщенности. 1 = без изменений, < 1 = меньше насыщенности, > 1 = больше насыщенности. Должно быть > 0.
        /// </summary>
        public float Saturation
        {
            get => _saturation;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Насыщенность должна быть больше 0.");
                _saturation = value;
            }
        }

        /// <summary>
        /// Цветовая температура от -1 (холодный) до 1 (тёплый). 0 = без изменений.
        /// </summary>
        public float Temperature
        {
            get => _temperature;
            set
            {
                if (value < -1f || value > 1f)
                    throw new ArgumentOutOfRangeException(nameof(value), "Цветовая температура должна быть в диапазоне от -1 до 1.");
                _temperature = value;
            }
        }

        // Изменяем Яркость
        public Image BrightnessImage()
        {
            if (Image == null)
                throw new ArgumentNullException(nameof(Image), "Исходное изображение не может быть null.");

            if (Brightness == 1) return Image;

            // Создаем матрицу цветов
            float[][] colorMatrixElements = {
                new float[] { Brightness, 0, 0, 0, 0 },
                new float[] { 0, Brightness, 0, 0, 0 },
                new float[] { 0, 0, Brightness, 0, 0 },
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
            if (Image == null)
                throw new ArgumentNullException(nameof(Image), "Исходное изображение не может быть null.");

            if (Contrast == 1) return Image;

            // Создаем матрицу цветов
            float[][] colorMatrixElements = {
                new float[] { Contrast, 0, 0, 0, 0 },
                new float[] { 0, Contrast, 0, 0, 0 },
                new float[] { 0, 0, Contrast, 0, 0 },
                new float[] { 0, 0, 0, 1, 0 },
                new float[] { 0.5f * (1 - Contrast), 0.5f * (1 - Contrast), 0.5f * (1 - Contrast), 0, 1 }
            };
            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            // Возвращаем измененное изображение
            return GenerateImage(colorMatrix);
        }

        public Image ChangeSaturation()
        {
            if (Image == null)
                throw new ArgumentNullException(nameof(Image), "Исходное изображение не может быть null.");

            if (Saturation == 1) return Image;

            // Создаем матрицу цветов
            float saturationComplement = 1.0f - Saturation;
            float saturationRed = 0.3086f * saturationComplement;
            float saturationGreen = 0.6094f * saturationComplement;
            float saturationBlue = 0.0820f * saturationComplement;
            // Создаем новую матрицу цветов и устанавливаем значения насыщенности
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] { saturationRed + Saturation, saturationRed, saturationRed, 0, 0 },
                new float[] { saturationGreen, saturationGreen + Saturation, saturationGreen, 0, 0 },
                new float[] { saturationBlue, saturationBlue, saturationBlue + Saturation, 0, 0 },
                new float[] { 0, 0, 0, 1, 0 },
                new float[] { 0, 0, 0, 0, 1 }
            });

            // Возвращаем измененное изображение
            return GenerateImage(colorMatrix);
        }

        // Меняем температуру
        public Image ChangeTemperature()
        {
            if (Image == null)
                throw new ArgumentNullException(nameof(Image), "Исходное изображение не может быть null.");

            if (Temperature == 0) return Image;

            // Create a new ColorMatrix and set the temperature values
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {Temperature, 0, 0, 0, 1}
            });

            return GenerateImage(colorMatrix);
        }

        public Image ChangeHue()
        {
            if (Image == null)
                throw new ArgumentNullException(nameof(Image), "Исходное изображение не может быть null.");

            if (Hue == 0) return Image;

            float cos = (float)Math.Cos(Hue * Math.PI / 180.0);
            float sin = (float)Math.Sin(Hue * Math.PI / 180.0);
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
            using (ImageAttributes imageAttributes = new ImageAttributes())
            {
                imageAttributes.SetColorMatrix(colorMatrix);

                // Создаем новое изображение и отрисовываем на нем оригинальное изображение с примененными настройками яркости
                Bitmap resultBitmap = new Bitmap(Image.Width, Image.Height);
                using (Graphics graphics = Graphics.FromImage(resultBitmap))
                {
                    graphics.DrawImage(Image, new Rectangle(0, 0, Image.Width, Image.Height), 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, imageAttributes);
                }

                // Возвращаем измененное изображение
                return resultBitmap;
            }
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
