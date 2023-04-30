using System;
using System.Collections.Generic;
using PluginInterface;

namespace Plugin1D
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

			// ������ ����������� ������������ ��������
            SizesList = new List<ListSizes>();
            SizesList.Add(new ListSizes() { Id = 1, Name = "������� �����������", SizeX = 1600, SizeY = 1800 });
            SizesList.Add(new ListSizes() { Id = 2, Name = "������ �����������", SizeX = 1000, SizeY = 1000 });
        }
		
		//Declarations of all our internal plugin variables
		string myName = "Default 1 Image";
		string myDescription = "�������� ������ � 1 ������������ � 2 ���������� ������.";
		string myAuthor = "������ �������";
		string myVersion = "1.0.0";
		bool myEditor = true;
		string feed = "�������� 2";
        //int[] numImgs = { 1600, 1800 };
        public List<ListSizes> SizesList { get; }

        IPluginHost myHost = null;
		System.Windows.Forms.UserControl myMainInterface = new ctlMain();
		
		/// <summary>
		/// Description of the Plugin's purpose
		/// </summary>
		public string Description
		{
			get {return myDescription;}
		}

		/// <summary>
		/// Author of the plugin
		/// </summary>
		public string Author
		{
			get	{return myAuthor;}
		
		}
		
		/// <summary>
		/// Host of the plugin.
		/// </summary>
		public IPluginHost Host
		{
			
            //��� ����� � ��������� ����� �� �����������
            /*
			��� �������... �� ������ ��������� ����-��������� ����������� ���� ���������...
			���, �� ����, ���� ��� ����������� ��������� �������� ������ � ��������� �������� ����-���������.	
			
			������: mp3-�����. ���� �� � ��� ��� ��������� IPluginHost, ��������:
			
			public interface IPluginHost
			{
				void Play(string FileName);
				void Stop();			
			}
			
				��, ��� �� ������ �������, ��� ����� ������ ����������� �� ���� (��� ����� ������ 
				�� ���� PluginServices.cs � ������ AddPlugin()), ������� �� ������ ����������:	

				newPlugin.Host = this;
				
				��� ���� �� ������� ������ �� ����... ������, ����� ������
				�����, ��� ���� �������� ��� ������, �� ����� ����� �������� � ��� ������, ��������:
			
				this.Host.Play("C:\MyCoolMp3.mp3");
				
			� ����� ��� ����� �� �����:
			
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
			get {return myName;}
		}

		public System.Windows.Forms.UserControl MainInterface
		{
			get {return myMainInterface;}
		}

		public string Version
		{
			get	{return myVersion;}
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