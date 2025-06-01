using System.Collections.Generic;

namespace PluginInterface
{
    public interface IPlugin
    {
        IPluginHost Host { get; set; }

        string Name { get; }
        string Description { get; }

        string Feed { get; }
        string Author { get; }
        string Version { get; }

        List<ListSizes> SizesList { get; }
        bool Editor { get; }

        System.Windows.Forms.UserControl MainInterface { get; }

        void Initialize();
        void Dispose();
    }

    public interface IPluginHost
    {
        void Feedback(string Feedback, IPlugin Plugin);

        System.Drawing.Image[] SendImages();

        void ReciveImage(System.Drawing.Image image);

        void RenderEditor();

        int IndexImage();

        bool IsExecuting(bool IsExecute);

        int Progress(int Progress);

        string Message(string Message);
    }

    public class ListSizes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
    }
}
