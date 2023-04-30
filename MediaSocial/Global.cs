using MediaSocial.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace MediaSocial
{
    /// <summary>
    /// Holds A static instance of global program shtuff
    /// </summary>
    internal class Global
    {
        //Constructor
        public Global() { }

        public static PluginServices Plugins = new PluginServices();

        public static List<Images> imagesSouser = new List<Images> { };
        public static List<Images> imagesRender = new List<Images> { };
        public static List<SettingImage> imagesSettings = new List<SettingImage> { };

        private static Image imageOut;
        public static Image ImageOut
        {
            get { return imageOut; }
            set
            {
                imageOut = value;
                OnImageOutChanged();
            }
        }

        public static event EventHandler ImageOutChanged;

        private static void OnImageOutChanged()
        {
            if (ImageOutChanged != null)
                ImageOutChanged(null, EventArgs.Empty);
        }

        public static int ImageIndexNow;

        /*
        вместо того, чтобы в frmMain.cs объявлять объект PluginService, то, что я сделал здесь, 
        создал его в глобальном классе. Я также сделал его статическим, поэтому нам не нужно беспокоиться об объекте. 
        Это всегда будет там для нас, и к одному и тому же объекту всегда будет обращаться все остальное в программе...

        Итак, теперь везде в этом проекте я могу ввести:

        Global.Plugins .... >

        и он вызовет объект Plugins, созданный выше...
        */
    }
}
