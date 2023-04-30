using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;


namespace MediaSocial.Classes
{
    public class ConverFormat
    {
        public Image readImage(string filePath)
        {
            Image image = null;
            // Проверяем наличие ffmpeg
            if (!File.Exists("ffmpeg.exe"))
            {
                return image;
            }

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
            startInfo.Arguments = "-i \"" + filePath + "\" -q:v 0 \"" + outputFile + "\"";
            // Параметр -q:v 0 дает максимальное качество jpeg файла
            process.StartInfo = startInfo;

            process.Start();
            process.WaitForExit();
            System.Threading.Thread.Sleep(500);

            // Обрабатываем полученное изображение
            try
            {
                Stream fileStream = new FileStream(outputFile, FileMode.Open);
                image = Image.FromStream(fileStream);
                fileStream.Close();
                File.Delete(outputFile);
            }
            catch
            {

            }

            return image;
        }
    }
}
