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

            if (!File.Exists("ffmpeg.exe")) { return image; }
            string outputFile = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".jpg";

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "ffmpeg.exe";
            startInfo.Arguments = "-i \"" + filePath + "\" -q:v 0 \"" + outputFile + "\"";
            // The -q:v 0 option sets maximum quality for the output JPG file
            process.StartInfo = startInfo;
            
            process.Start();
            process.WaitForExit();
            System.Threading.Thread.Sleep(500);

            try
            {
                Stream fileStream = new FileStream(outputFile, FileMode.Open);
                image = System.Drawing.Image.FromStream(fileStream);
                fileStream.Close();
                File.Delete(outputFile);
            } catch
            {

            }

            return image;
        }
    }
}
