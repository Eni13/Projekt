using System;
using System.Collections.Generic;
using Gtk;

namespace projektPdf
{
	public partial class WindowListaFoldera : Gtk.Window
	{
		public FolderNodeStore folderPresenter = new FolderNodeStore();

		public WindowListaFoldera() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();

			buttonIzbrisi.Clicked += izbrisiFolder;
			buttonOdustani.Clicked += odustani;

			// Dodjeljivanje presentera nodeview-u
			nodeview1.NodeStore = folderPresenter;

			//Dodavanje stupaca koji ce se prikazivati u nodeview
			nodeview1.AppendColumn("Putanja foldera", new CellRendererText(), "text", 0);

			osvjezi();
		}

		protected void izbrisiFolder(object sender, EventArgs a)
		{
			//Dohacanje odaberenog dogadaja
			FolderNode folderSelected = nodeview1.NodeSelection.SelectedNode as FolderNode;
			if (folderSelected == null)
				return;

			BPFolder.Izbrisi(folderSelected.id);

			osvjezi();
		}

		protected void odustani(object sender, EventArgs a)
		{
			this.Destroy();
		}

		protected void osvjezi()
		{
			folderPresenter.Clear();

			List<Folder> listaFoldera = BPFolder.DohavtiSve();

			foreach (var i in listaFoldera)
			{
				folderPresenter.dodajFolder(i);
			}
		}
	}
}
