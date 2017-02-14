using System;
using Gtk;

namespace projektPdf
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Application.Init();
			BP.otvoriKonekciju();
			MainWindow win = new MainWindow();
			win.Show();
			Application.Run();
		}
	}
}
