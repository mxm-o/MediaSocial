using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

//var originalImage = Image.FromFile("input.jpg");
//var blurredImage = ImageBlur.ApplyBlur(originalImage, 5);
//blurredImage.Save("output.jpg");

public static class ImageBlur
{
    public static Image ApplyBlur(Image image, int blurSize)
    {
        if (blurSize < 1) return image;

        // Конвертируем в Format32bppArgb для гарантированной работы с 4 каналами
        var bitmap = new Bitmap(image);
        var format = PixelFormat.Format32bppArgb;
        var srcBitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), format);
        var destBitmap = new Bitmap(srcBitmap.Width, srcBitmap.Height, format);

        // Раздельное размытие по горизонтали и вертикали
        using (var tempBitmap = new Bitmap(srcBitmap.Width, srcBitmap.Height, format))
        {
            BlurHorizontal(srcBitmap, tempBitmap, blurSize);
            BlurVertical(tempBitmap, destBitmap, blurSize);
        }

        return destBitmap;
    }

    private static unsafe void BlurHorizontal(Bitmap src, Bitmap dest, int radius)
    {
        var rect = new Rectangle(0, 0, src.Width, src.Height);
        var srcData = src.LockBits(rect, ImageLockMode.ReadOnly, src.PixelFormat);
        var destData = dest.LockBits(rect, ImageLockMode.WriteOnly, dest.PixelFormat);

        int bytesPerPixel = 4; // Format32bppArgb
        int height = src.Height;
        int width = src.Width;

        Parallel.For(0, height, y =>
        {
            byte* srcRow = (byte*)srcData.Scan0 + y * srcData.Stride;
            byte* destRow = (byte*)destData.Scan0 + y * destData.Stride;

            for (int x = 0; x < width; x++)
            {
                int sumB = 0, sumG = 0, sumR = 0, sumA = 0;
                int count = 0;

                int start = Math.Max(0, x - radius);
                int end = Math.Min(width - 1, x + radius);

                for (int dx = start; dx <= end; dx++)
                {
                    byte* pixel = srcRow + dx * bytesPerPixel;
                    sumB += pixel[0];
                    sumG += pixel[1];
                    sumR += pixel[2];
                    sumA += pixel[3];
                    count++;
                }

                byte* destPixel = destRow + x * bytesPerPixel;
                destPixel[0] = (byte)(sumB / count);
                destPixel[1] = (byte)(sumG / count);
                destPixel[2] = (byte)(sumR / count);
                destPixel[3] = (byte)(sumA / count);
            }
        });

        src.UnlockBits(srcData);
        dest.UnlockBits(destData);
    }

    private static unsafe void BlurVertical(Bitmap src, Bitmap dest, int radius)
    {
        var rect = new Rectangle(0, 0, src.Width, src.Height);
        var srcData = src.LockBits(rect, ImageLockMode.ReadOnly, src.PixelFormat);
        var destData = dest.LockBits(rect, ImageLockMode.WriteOnly, dest.PixelFormat);

        int bytesPerPixel = 4;
        int height = src.Height;
        int width = src.Width;

        Parallel.For(0, width, x =>
        {
            for (int y = 0; y < height; y++)
            {
                int sumB = 0, sumG = 0, sumR = 0, sumA = 0;
                int count = 0;

                int start = Math.Max(0, y - radius);
                int end = Math.Min(height - 1, y + radius);

                for (int dy = start; dy <= end; dy++)
                {
                    byte* srcRow = (byte*)srcData.Scan0 + dy * srcData.Stride;
                    byte* pixel = srcRow + x * bytesPerPixel;
                    sumB += pixel[0];
                    sumG += pixel[1];
                    sumR += pixel[2];
                    sumA += pixel[3];
                    count++;
                }

                byte* destRow = (byte*)destData.Scan0 + y * destData.Stride;
                byte* destPixel = destRow + x * bytesPerPixel;
                destPixel[0] = (byte)(sumB / count);
                destPixel[1] = (byte)(sumG / count);
                destPixel[2] = (byte)(sumR / count);
                destPixel[3] = (byte)(sumA / count);
            }
        });

        src.UnlockBits(srcData);
        dest.UnlockBits(destData);
    }
}