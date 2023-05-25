using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MediaSocial.Classes
{
    public class SaveImageFile
    {
        // This is an instance of the Image class that you want to save.
        public Image image = null;
        // This is the file path (including the file name and extension) where you want to save the image.
        public string savePath = "";
        // This is an instance of the ImageFormat enum that specifies the format of the image.
        public string saveFormat = "jpg";

        // This is a long value that specifies the quality level of the image. The value should be between 0 and 100, where 0 is the lowest quality and 100 is the highest quality.
        public long quality = 100;

        public void SaveImage()
        {
            // Create an encoder parameter for the image quality
            EncoderParameter qualityParameter = new EncoderParameter(Encoder.Quality, quality);

            // Get the codec for the specified image format
            ImageCodecInfo imageCodecInfo = GetEncoderInfo(ConvertFormat(saveFormat));

            // Create an encoder parameters object and add the image quality parameter
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = qualityParameter;
            try
            {
                // Save the image to the specified file path using the specified image format and quality
                image.Save(savePath, imageCodecInfo, encoderParameters);

            } catch (Exception ex)
            {
                throw new ArgumentException("Path : " + ex.ToString());
            }
        }

        // Helper method to get the codec for a specified image format
        private ImageCodecInfo GetEncoderInfo(ImageFormat formatE)
        {
            ImageCodecInfo[] codecInfos = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codecInfo in codecInfos)
            {
                if (codecInfo.FormatID == formatE.Guid)
                {
                    return codecInfo;
                }
            }

            throw new ArgumentException("Unsupported image format: " + formatE.ToString());
        }

        private ImageFormat ConvertFormat(string formatString) {

            ImageFormat formatR;
            switch (formatString.ToLower())
            {
                case "jpg":
                    formatR = ImageFormat.Jpeg;
                    break;
                case "jpeg":
                    formatR = ImageFormat.Jpeg;
                    break;
                case "bmp":
                    formatR = ImageFormat.Bmp;
                    break;
                case "gif":
                    formatR = ImageFormat.Gif;
                    break;
                case "png":
                    formatR = ImageFormat.Png;
                    break;
                case "tiff":
                    formatR = ImageFormat.Tiff;
                    break;
                default:
                    throw new ArgumentException("Invalid image format.");
            }

            return formatR;
        }
    }
}
