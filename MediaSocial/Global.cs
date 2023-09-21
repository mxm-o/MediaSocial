// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using MediaSocial.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;

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

        private static bool renderE;

        public static bool renderEditor
        {
            get { return renderE; }
            set
            {
                renderE = value;
                OnEditorChanged();
            }
        }

        public static event EventHandler EditorChanged;

        private static void OnEditorChanged()
        {
            if (EditorChanged != null)
                EditorChanged(null, EventArgs.Empty);
        }
    }
}
