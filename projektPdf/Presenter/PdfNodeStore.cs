using System;
using System.Collections.Generic;

namespace projektPdf
{
	public class PdfNodeStore : Gtk.NodeStore
	{
		public PdfNodeStore() : base(typeof(PdfNode))
		{
		}

		public void dodajPdf(Pdf p)
		{
			PdfNode temp = new PdfNode(p);
			this.AddNode(temp);
		}
		public void dodajVise(List<Pdf> lista)
		{
			//za svaki pdf iz liste pozovi funkciju dodajPdf
			lista.ForEach((obj) => dodajPdf(obj as Pdf));
		}
		public void dohvatiPdfUKategoriji(Kategorija kat)
		{
			// izbriši sve nodove
			this.Clear();
			// dodaj nove nodove iz kategorije kat
			this.dodajVise(BPPDF.DohvatiIzKategorije(kat));
		}
		public void dohvatiPoTagovima(List<string> tagovi)
		{	//izbriši sve nodove
			this.Clear();

			//dohvati sve pdf-ove te za svakog radi sljedeće
			BPPDF.DohavtiSve().ForEach((pdf) => 
			{
				// za svaki tag provjeri sljedeće
				foreach (var tag in pdf.tagovi)
				{
					//ako traženi tag postoji u tagovima pdf-a 
					if (tagovi.Contains(tag))
					{	// dodaj pdf u nodeview i prekini
						dodajPdf(pdf);
						break;
					}
				}
			
			});
		}
	}
}
