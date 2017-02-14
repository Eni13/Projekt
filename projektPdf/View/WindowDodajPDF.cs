using System;
using Gtk;
using System.IO;

namespace projektPdf
{
	public partial class WindowDodajPDF : Gtk.Window
	{
		public WindowDodajPDF() :base(Gtk.WindowType.Toplevel)
		{
			this.Build();

			buttonSpremi.Clicked += spremi;
			buttonOdustani.Clicked += odustani;

			FileFilter filter = new FileFilter();
			filter.Name = "PDF";
			filter.AddPattern("*.pdf");
			filechooserwidget2.AddFilter(filter);


		}

		protected void spremi(object sender, EventArgs a)
		{
			var unos = new WindowUnosPDF(filechooserwidget2.Filename);
			unos.Destroyed+=(sender1, e1) => this.Destroy();
		}

		protected void odustani(object sender, EventArgs a)
		{
			this.Destroy();
		}
	}
}
