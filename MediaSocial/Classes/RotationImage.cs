using System.Drawing;

namespace MediaSocial.Classes
{
    internal class RotationImage
    {
        public Image RotateImage(Image img)
        {
            foreach (var prop in img.PropertyItems)
            {
                if (prop.Id == 0x0112) //value of EXIF
                {
                    int orientationValue = img.GetPropertyItem(prop.Id).Value[0];
                    RotateFlipType rotateFlipType = GetOrientationToFlipType(orientationValue);
                    img.RotateFlip(rotateFlipType);
                    break;
                }
            }
            return img;
        }

        private RotateFlipType GetOrientationToFlipType(int orientationValue)
        {
            RotateFlipType rotateFlipType = RotateFlipType.RotateNoneFlipNone;

            switch (orientationValue)
            {
                case 1:
                    rotateFlipType = RotateFlipType.RotateNoneFlipNone;
                    break;
                case 2:
                    rotateFlipType = RotateFlipType.RotateNoneFlipX;
                    break;
                case 3:
                    rotateFlipType = RotateFlipType.Rotate180FlipNone;
                    break;
                case 4:
                    rotateFlipType = RotateFlipType.Rotate180FlipX;
                    break;
                case 5:
                    rotateFlipType = RotateFlipType.Rotate90FlipX;
                    break;
                case 6:
                    rotateFlipType = RotateFlipType.Rotate90FlipNone;
                    break;
                case 7:
                    rotateFlipType = RotateFlipType.Rotate270FlipX;
                    break;
                case 8:
                    rotateFlipType = RotateFlipType.Rotate270FlipNone;
                    break;
                default:
                    rotateFlipType = RotateFlipType.RotateNoneFlipNone;
                    break;
            }

            return rotateFlipType;
        }
    }
}
