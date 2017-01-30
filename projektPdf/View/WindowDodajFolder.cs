using System;
using Gtk;

namespace projektPdf
{
	public partial class WindowDodajFolder : Gtk.Window
	{
		public WindowDodajFolder() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();

			buttonSpremi.Clicked += spremiFolder;
			buttonOdustani.Clicked += odustani;

			FileFilter filter = new FileFilter();
			filter.Name = "Folders";
			filter.AddPattern("*.x");
			filechooserwidget2.AddFilter(filter);
		}

		protected void spremiFolder(object sender, EventArgs a)
		{
			BPFolder.Spremi(filechooserwidget2.CurrentFolder);

			this.Destroy();
		}

		protected void odustani(object sender, EventArgs a)
		{
			this.Destroy();
		}
	}
}
