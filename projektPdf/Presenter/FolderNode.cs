using System;
namespace projektPdf
{
	public class FolderNode : Gtk.TreeNode
	{
		public long id;

		[Gtk.TreeNodeValue(Column = 0)]
		public String path;

		public FolderNode(Folder f)
		{
			this.path = f.Path;
			this.id = f.Id;
		}
	}
}
