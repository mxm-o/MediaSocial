// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace PhotoEdit
{

    public class BlurImage
    {
        public Image Blur(Image image, int blurAmount)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), "Изображение для размытия не может быть null.");

            if (checkFfmpeg()) {
                image = BlurFfmpeg(image, blurAmount); 
            } 
            else
            {
                image = ImageBlur.ApplyBlur(image, blurAmount);
            }

            return image;
        }

        // Проверяем наличие ffmpeg
        private bool checkFfmpeg()
        {
            if (File.Exists("ffmpeg.exe"))
            {
                return true;
            }
            return false;
        }

        private Image BlurFfmpeg(Image image, int blurAmount)
        {
            // Проверяем наличие ffmpeg
            if (!checkFfmpeg())
            {
                return image;
            }

            // Создаем имя временного файла
            string inputFile;
            do
            {
                inputFile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".jpg";
            }
            while (File.Exists(inputFile));

            // Записываем временный файл
            SaveImageFile saveImageFile = new SaveImageFile();
            saveImageFile.image = image;
            saveImageFile.savePath = inputFile;
            saveImageFile.SaveImage();

            // Создаем имя временного файла
            string outputFile;
            do
            {
                outputFile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".jpg";
            }
            while (File.Exists(outputFile));

            // Запускаем FFMPEG с параметрами
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "ffmpeg.exe";
            startInfo.Arguments = "-i \"" + inputFile + "\" -q:v 0 -filter_complex \"[0:v]boxblur=" + blurAmount + "[blurred]\" -map \"[blurred]\" \"" + outputFile + "\"";
            // Параметр -q:v 0 дает максимальное качество jpeg файла
            process.StartInfo = startInfo;

            process.Start();
            process.WaitForExit();
            System.Threading.Thread.Sleep(500);

            // Обрабатываем полученное изображение
            try
            {
                using (Stream fileStream = new FileStream(outputFile, FileMode.Open))
                {
                    image = Image.FromStream(fileStream);
                }
                File.Delete(outputFile);
                File.Delete(inputFile);
            }
            catch (Exception ex)
            {
                // Удаляем временные файлы даже при ошибке
                try
                {
                    if (File.Exists(outputFile)) File.Delete(outputFile);
                }
                catch { }

                try
                {
                    if (File.Exists(inputFile)) File.Delete(inputFile);
                }
                catch { }

                throw new InvalidOperationException("Ошибка обработки изображения через FFmpeg", ex);
            }

            return image;
        }
    }
}
