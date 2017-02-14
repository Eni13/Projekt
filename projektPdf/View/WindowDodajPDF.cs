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
			//klikom na gumb spremi pozovi funkciju spremi
			buttonSpremi.Clicked += spremi;
			// klikom na gumb odustani pozovi funkciju odustani
			buttonOdustani.Clicked += odustani;

			//kreiraj novi filter
			FileFilter filter = new FileFilter();
			//daj filteru ime koje će se prikazati korisniku
			filter.Name = "PDF";
			// definiraj po čemu če filter filtrirat
			filter.AddPattern("*.pdf");
			//dodaj taj filter widgetu za pretraživanje
			filechooserwidget2.AddFilter(filter);


		}

		protected void spremi(object sender, EventArgs a)
		{	// ako je odabrana datoteka pdf
			if (filechooserwidget2.Filename.Substring(filechooserwidget2.Filename.Length - 3) == "pdf")
			{
				//kreiraj window za unos pdf-a i predaj mu putanju zajedno sa imenom datoteke
				var unos = new WindowUnosPDF(filechooserwidget2.Filename);
				// kada se uništi prozor za unos pdf-a, uništi i ovaj prozor
				unos.Destroyed += (sender1, e1) => this.Destroy();
			}
		}

		protected void odustani(object sender, EventArgs a)
		{
			this.Destroy();
		}
	}
}
