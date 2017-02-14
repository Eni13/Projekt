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
			lista.ForEach((obj) => dodajPdf(obj as Pdf));
		}
		public void dohvatiPdfUKategoriji(Kategorija kat)
		{
			this.Clear();
			this.dodajVise(BPPDF.DohvatiIzKategorije(kat));
		}
		public void dohvatiPoTagovima(List<string> tagovi)
		{
			this.Clear();
			
			BPPDF.DohavtiSve().ForEach((pdf) => 
			{
				foreach (var tag in pdf.tagovi)
				{
					if (tagovi.Contains(tag))
					{
						dodajPdf(pdf);
						break;
					}
				}
			
			});
		}
	}
}
