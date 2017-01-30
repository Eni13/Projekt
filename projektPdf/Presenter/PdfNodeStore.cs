using System;
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
	}
}
