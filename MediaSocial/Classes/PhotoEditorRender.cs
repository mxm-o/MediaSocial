using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoEdit;

namespace MediaSocial.Classes
{
    public class PhotoEditorRender
    {
        public Image inputImage = null;
        public Image outputImage = null;
        public SettingImage setting = null;
        
        public Image RenderImage()
        {
            return outputImage;
        }
    }
}
