using System;
namespace projektPdf
{
	public class FolderNodeStore : Gtk.NodeStore
	{
		public FolderNodeStore() : base(typeof(FolderNode))
		{
		}

		public void dodajFolder(Folder f)
		{
			FolderNode temp = new FolderNode(f);
			this.AddNode(temp);
		}
	}
}
