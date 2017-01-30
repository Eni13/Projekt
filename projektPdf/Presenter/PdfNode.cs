using System;
namespace projektPdf
{
	public class PdfNode : Gtk.TreeNode
	{
		public string path;
		public long id;

		[Gtk.TreeNodeValue(Column = 0)]
		public String naziv;

		public PdfNode(Pdf p)
		{
			this.naziv = p.Naziv;
			this.path = p.Path;
			this.id = p.Id;
		}
	}
}
