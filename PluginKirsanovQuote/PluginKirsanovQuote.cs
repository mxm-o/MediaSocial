using System.Collections.Generic;
using PluginInterface;

namespace PluginKirsanovQuote
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
            SizesList.Add(new ListSizes() { Id = 1, Name = "Фото человека", SizeX = 450, SizeY = 450 });
        }

        //Declarations of all our internal plugin variables
        string myName = "Цитата";
        string myDescription = "Модуль поста цитаты.";
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

            //Эта часть в настоящее время не реализована
            /*
			Вот вкратце... Вы можете заставить хост-программу реализовать этот интерфейс...
			это, по сути, дает вам возможность разрешить плагинам доступ к некоторым функциям хост-программы.	
			
			Пример: mp3-плеер. Если бы у вас был интерфейс IPluginHost, например:
			
			public interface IPluginHost
			{
				void Play(string FileName);
				void Stop();			
			}
			
				то, что вы должны сделать, это когда плагин загружается на хост (это будет похоже 
				на файл PluginServices.cs в методе AddPlugin()), который вы должны установить:	

				newPlugin.Host = this;
				
				это дало бы плагину ссылку на хост... теперь, когда плагин
				знает, что хост содержит эти методы, он может легко получить к ним доступ, например:
			
				this.Host.Play("C:\MyCoolMp3.mp3");
				
			и тогда они могли бы пойти:
			
				this.Host.Stop();
				
			all this being from right inside the plugin!  I hope you get the point.  It 
			just means that you can indeed make your plugins able to interact with the 
			host program itself.  Let's face it.. It would be no fun if you couldn't do this,
			because otherwise all the plugin is, is just standalone functionality running
			inside the host program.. (of course there are cases when you can still accomplish
			many things without needing to allow the plugin to play with the host... for example
			you could have an spam filter, and have each plugin be a different filter... that would
			be pretty simple to make plugins for...
			
			so anyhow, that is what the host thingy is all aboot, eh!	
			
			*/
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