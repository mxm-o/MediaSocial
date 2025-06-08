using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

public static class ImageBlur
{
    public static Image ApplyBlur(Image image, int blurSize)
    {
        if (blurSize < 1) return image;

        var bitmap = new Bitmap(image);
        var format = PixelFormat.Format32bppArgb;
        var srcBitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), format);
        var destBitmap = new Bitmap(srcBitmap.Width, srcBitmap.Height, format);

        using (var tempBitmap = new Bitmap(srcBitmap.Width, srcBitmap.Height, format))
        {
            BlurHorizontal(srcBitmap, tempBitmap, blurSize);
            BlurVertical(tempBitmap, destBitmap, blurSize);
        }

        return destBitmap;
    }

    private static unsafe void BlurHorizontal(Bitmap src, Bitmap dest, int radius)
    {
        if (src.Width == 0 || src.Height == 0) return;

        var rect = new Rectangle(0, 0, src.Width, src.Height);
        var srcData = src.LockBits(rect, ImageLockMode.ReadOnly, src.PixelFormat);
        var destData = dest.LockBits(rect, ImageLockMode.WriteOnly, dest.PixelFormat);

        int bytesPerPixel = 4;
        int height = src.Height;
        int width = src.Width;

        Parallel.For(0, height, y =>
        {
            byte* srcRow = (byte*)srcData.Scan0 + y * srcData.Stride;
            byte* destRow = (byte*)destData.Scan0 + y * destData.Stride;

            int sumB = 0, sumG = 0, sumR = 0, sumA = 0;
            int count = 0;

            // Инициализация окна [0, min(radius, width-1)]
            int initEnd = Math.Min(radius, width - 1);
            for (int x = 0; x <= initEnd; x++)
            {
                byte* pixel = srcRow + x * bytesPerPixel;
                sumB += pixel[0];
                sumG += pixel[1];
                sumR += pixel[2];
                sumA += pixel[3];
                count++;
            }

            for (int x = 0; x < width; x++)
            {
                byte* destPixel = destRow + x * bytesPerPixel;
                destPixel[0] = (byte)(sumB / count);
                destPixel[1] = (byte)(sumG / count);
                destPixel[2] = (byte)(sumR / count);
                destPixel[3] = (byte)(sumA / count);

                // Обновление окна для следующего x
                if (x < width - 1)
                {
                    // Удаление уходящего пикселя (слева)
                    if (x >= radius)
                    {
                        byte* leftPixel = srcRow + (x - radius) * bytesPerPixel;
                        sumB -= leftPixel[0];
                        sumG -= leftPixel[1];
                        sumR -= leftPixel[2];
                        sumA -= leftPixel[3];
                        count--;
                    }

                    // Добавление нового пикселя (справа)
                    if (x + radius + 1 < width)
                    {
                        byte* rightPixel = srcRow + (x + radius + 1) * bytesPerPixel;
                        sumB += rightPixel[0];
                        sumG += rightPixel[1];
                        sumR += rightPixel[2];
                        sumA += rightPixel[3];
                        count++;
                    }
                }
            }
        });

        src.UnlockBits(srcData);
        dest.UnlockBits(destData);
    }

    private static unsafe void BlurVertical(Bitmap src, Bitmap dest, int radius)
    {
        if (src.Width == 0 || src.Height == 0) return;

        var rect = new Rectangle(0, 0, src.Width, src.Height);
        var srcData = src.LockBits(rect, ImageLockMode.ReadOnly, src.PixelFormat);
        var destData = dest.LockBits(rect, ImageLockMode.WriteOnly, dest.PixelFormat);

        int bytesPerPixel = 4;
        int height = src.Height;
        int width = src.Width;

        Parallel.For(0, width, x =>
        {
            int sumB = 0, sumG = 0, sumR = 0, sumA = 0;
            int count = 0;

            // Инициализация окна [0, min(radius, height-1)]
            int initEnd = Math.Min(radius, height - 1);
            for (int y = 0; y <= initEnd; y++)
            {
                byte* srcRow = (byte*)srcData.Scan0 + y * srcData.Stride;
                byte* pixel = srcRow + x * bytesPerPixel;
                sumB += pixel[0];
                sumG += pixel[1];
                sumR += pixel[2];
                sumA += pixel[3];
                count++;
            }

            for (int y = 0; y < height; y++)
            {
                byte* destRow = (byte*)destData.Scan0 + y * destData.Stride;
                byte* destPixel = destRow + x * bytesPerPixel;
                destPixel[0] = (byte)(sumB / count);
                destPixel[1] = (byte)(sumG / count);
                destPixel[2] = (byte)(sumR / count);
                destPixel[3] = (byte)(sumA / count);

                // Обновление окна для следующего y
                if (y < height - 1)
                {
                    if (y >= radius)
                    {
                        byte* removeRow = (byte*)srcData.Scan0 + (y - radius) * srcData.Stride;
                        byte* removePixel = removeRow + x * bytesPerPixel;
                        sumB -= removePixel[0];
                        sumG -= removePixel[1];
                        sumR -= removePixel[2];
                        sumA -= removePixel[3];
                        count--;
                    }

                    // Добавление новой строки (снизу)
                    if (y + radius + 1 < height)
                    {
                        byte* addRow = (byte*)srcData.Scan0 + (y + radius + 1) * srcData.Stride;
                        byte* addPixel = addRow + x * bytesPerPixel;
                        sumB += addPixel[0];
                        sumG += addPixel[1];
                        sumR += addPixel[2];
                        sumA += addPixel[3];
                        count++;
                    }
                }
            }
        });

        src.UnlockBits(srcData);
        dest.UnlockBits(destData);
    }
}