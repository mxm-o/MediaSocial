// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System.Collections.Generic;
using PluginInterface;

namespace PluginKirsanovFotoTitle
{
    /// <summary>
    /// Plugin2
    /// </summary>
    public class Plugin : IPlugin  // <-- See how we inherited the IPlugin interface?
    {
        public Plugin()
        {
            //
            // TODO: Add constructor logic here
            //

            // Список изображений используемых плагином
            SizesList = new List<ListSizes>();
            SizesList.Add(new ListSizes() { Id = 1, Name = "Фото", SizeX = 1800, SizeY = 1800 });
        }

        //Declarations of all our internal plugin variables
        string myName = "Фото с заголовком (полный экран)";
        string myDescription = "Модуль новости с заголовком и фото на весь размер картинки поста.";
        string myAuthor = "Максим Отрохов";
        string myVersion = "1.0.0";
        bool myEditor = true;
        string feed = "";
        //int[] numImgs = { 1600, 1800 };
        public List<ListSizes> SizesList { get; }

        IPluginHost myHost = null;
        System.Windows.Forms.UserControl myMainInterface = new ctlMain();

        /// <summary>
        /// Description of the Plugin's purpose
        /// </summary>
        public string Description
        {
            get { return myDescription; }
        }

        /// <summary>
        /// Author of the plugin
        /// </summary>
        public string Author
        {
            get { return myAuthor; }

        }

        /// <summary>
        /// Host of the plugin.
        /// </summary>
        public IPluginHost Host
        {
            get
            {
                return myHost;
            }
            set
            {
                myHost = value;
                ctlMain mainCtl = (ctlMain)myMainInterface;
                mainCtl.PluginHost = this.Host;
                mainCtl.Plugin = this;
            }
        }

        public string Name
        {
            get { return myName; }
        }

        public System.Windows.Forms.UserControl MainInterface
        {
            get { return myMainInterface; }
        }

        public string Version
        {
            get { return myVersion; }
        }

        public string Feed
        {
            get { return feed; }
        }

        public bool Editor
        {
            get { return myEditor; }
        }

        public void Initialize()
        {
            //This is the first Function called by the host...
            //Put anything needed to start with here first
        }

        public void Dispose()
        {
            //Put any cleanup code in here for when the program is stopped
        }

    }
}