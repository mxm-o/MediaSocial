using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSocial.Classes
{
    // Список изображений
    public class Images
    {
        public int Id { get; set; }
        public Image Pictures { get; set; } 

        public bool Exist { get; set; }
    }

    public class SettingImage
    {
        public int Id { get; set; }
        public int Step { get; set; }
        public int canvasWidth { get; set; }
        public int canvasHeight { get; set; }
        public float Vertical { get; set; }
        public float Horizontal { get; set; }
        public float Zoom { get; set; }
        public float Brightness { get; set; }
        public float Contrast { get; set; }
        public float Saturation { get; set; }
        public float Temperature { get; set; }
        public float Tone { get; set; }
        public int Rotate { get; set; }
    }
}
