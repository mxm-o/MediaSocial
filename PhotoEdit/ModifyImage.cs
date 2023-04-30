using System;
using System.Drawing.Imaging;
using System.Drawing;

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
            if (saturation == 0) return image;

            // Создаем матрицу цветов
            float saturationComplement = 1.0f - saturation;
            float saturationRed = 0.3086f * saturationComplement;
            float saturationGreen = 0.6094f * saturationComplement;
            float saturationBlue = 0.0820f * saturationComplement;
            // Create a new ColorMatrix and set the temperature values
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
            Bitmap bitmap = new Bitmap(image); // Convert Image to Bitmap
            float totalBrightness = 0;
            int numPixels = bitmap.Width * bitmap.Height;
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    totalBrightness += color.GetBrightness();
                }
            }
            float averageBrightness = totalBrightness / numPixels;
            float brightnessFactor = 1 / averageBrightness;
            return brightnessFactor;
        }
    }
}
