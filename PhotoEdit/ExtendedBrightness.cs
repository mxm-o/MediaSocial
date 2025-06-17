using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PhotoEdit
{
    public class ExtendedBrightness
    {
        // Adjusts the brightness of an image by applying separate factors to shadows, midtones, and highlights.
        // 
        // Parameters:
        //   inputImage: The original image to be adjusted.
        //   shadowsFactor: The factor to apply to pixel lightness values in the range 0-0.33 (shadows). Входное значение от 0 до 2, где 1 - обычная яркость
        //   midtonesFactor: The factor to apply to pixel lightness values in the range 0.33-0.67 (midtones). Входное значение от 0 до 2, где 1 - обычная яркость
        //   highlightsFactor: The factor to apply to pixel lightness values in the range 0.67-1 (highlights). Входное значение от 0 до 2, где 1 - обычная яркость
        // 
        // Returns:
        //   The adjusted image with modified brightness.
        public Image AdjustBrightness(Image inputImage, float shadowsFactor, float midtonesFactor, float highlightsFactor)
        {
            // Проверка необходимости обработки
            if (shadowsFactor == 1f && midtonesFactor == 1f && highlightsFactor == 1f)
            {
                return (Image)inputImage.Clone();
            }

            Bitmap srcBmp = new Bitmap(inputImage);
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height, PixelFormat.Format32bppArgb);

            // Настройки блокировки битов
            var rect = new Rectangle(0, 0, srcBmp.Width, srcBmp.Height);
            BitmapData srcData = srcBmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData destData = destBmp.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            try
            {
                int bytes = Math.Abs(srcData.Stride) * srcBmp.Height;
                byte[] pixelBuffer = new byte[bytes];
                byte[] resultBuffer = new byte[bytes];

                // Копируем данные изображения в буфер
                Marshal.Copy(srcData.Scan0, pixelBuffer, 0, bytes);

                // Параллельная обработка пикселей
                Parallel.For(0, pixelBuffer.Length / 4, i =>
                {
                    int offset = i * 4;

                    // Чтение компонент (BGRA)
                    byte b = pixelBuffer[offset];
                    byte g = pixelBuffer[offset + 1];
                    byte r = pixelBuffer[offset + 2];
                    byte a = pixelBuffer[offset + 3];

                    // Пропускаем прозрачные пиксели
                    if (a == 0)
                    {
                        Buffer.BlockCopy(pixelBuffer, offset, resultBuffer, offset, 4);
                        return;
                    }

                    // Преобразование RGB в HSL
                    var hsl = RgbToHsl(r, g, b);
                    double hue = hsl[0];
                    double saturation = hsl[1];
                    double lightness = hsl[2];

                    // Плавное применение яркости
                    double factor = CalculateSmoothFactor(lightness, shadowsFactor, midtonesFactor, highlightsFactor);
                    lightness = Clamp(lightness * factor, 0, 1);

                    // Обратное преобразование HSL в RGB
                    var rgb = HslToRgb(hue, saturation, lightness);

                    // Запись результата с сохранением альфа-канала
                    resultBuffer[offset] = rgb[2];     // B
                    resultBuffer[offset + 1] = rgb[1]; // G
                    resultBuffer[offset + 2] = rgb[0]; // R
                    resultBuffer[offset + 3] = a;      // A
                });

                // Копируем результат обратно в изображение
                Marshal.Copy(resultBuffer, 0, destData.Scan0, bytes);
            }
            finally
            {
                srcBmp.UnlockBits(srcData);
                destBmp.UnlockBits(destData);
            }

            return destBmp;
        }

        private static double CalculateSmoothFactor(double lightness, float shadows, float midtones, float highlights)
        {
            const double shadowEnd = 0.33;
            const double highlightStart = 0.67;

            double weightShadows = 1.0 - SmoothStep(lightness, 0.0, shadowEnd);
            double weightHighlights = SmoothStep(lightness, highlightStart, 1.0);
            double weightMidtones = 1.0 - (weightShadows + weightHighlights);

            weightMidtones = Clamp(weightMidtones, 0, 1);

            return shadows * weightShadows +
                   midtones * weightMidtones +
                   highlights * weightHighlights;
        }

        private static double SmoothStep(double value, double edge0, double edge1)
        {
            double t = Clamp((value - edge0) / (edge1 - edge0), 0.0, 1.0);
            return t * t * (3.0 - 2.0 * t);
        }

        private static double[] RgbToHsl(byte r, byte g, byte b)
        {
            double red = r / 255.0;
            double green = g / 255.0;
            double blue = b / 255.0;

            double max = Math.Max(red, Math.Max(green, blue));
            double min = Math.Min(red, Math.Min(green, blue));
            double delta = max - min;

            double lightness = (max + min) / 2.0;
            double saturation = 0;
            double hue = 0;

            if (delta > 0)
            {
                saturation = lightness < 0.5 ?
                    delta / (max + min) :
                    delta / (2.0 - max - min);

                if (max == red)
                {
                    hue = (green - blue) / delta + (green < blue ? 6 : 0);
                }
                else if (max == green)
                {
                    hue = (blue - red) / delta + 2;
                }
                else
                {
                    hue = (red - green) / delta + 4;
                }

                hue /= 6.0;
            }

            return new double[] { hue, saturation, lightness };
        }

        private static byte[] HslToRgb(double hue, double saturation, double lightness)
        {
            double r = lightness;
            double g = lightness;
            double b = lightness;

            if (saturation > 0)
            {
                double q = lightness < 0.5 ?
                    lightness * (1.0 + saturation) :
                    lightness + saturation - lightness * saturation;

                double p = 2.0 * lightness - q;

                r = HueToRgb(p, q, hue + 1.0 / 3.0);
                g = HueToRgb(p, q, hue);
                b = HueToRgb(p, q, hue - 1.0 / 3.0);
            }

            return new byte[]
            {
                (byte)Math.Round(r * 255),
                (byte)Math.Round(g * 255),
                (byte)Math.Round(b * 255)
            };
        }

        private static double HueToRgb(double p, double q, double t)
        {
            if (t < 0) t += 1;
            if (t > 1) t -= 1;

            if (t < 1.0 / 6.0) return p + (q - p) * 6.0 * t;
            if (t < 1.0 / 2.0) return q;
            if (t < 2.0 / 3.0) return p + (q - p) * (2.0 / 3.0 - t) * 6.0;

            return p;
        }

        private static double Clamp(double value, double min, double max)
        {
            return Math.Min(Math.Max(value, min), max);
        }
    }
}