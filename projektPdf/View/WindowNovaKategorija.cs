using System;
using Gtk;
namespace projektPdf
{
	public partial class WindowNovaKategorija : Gtk.Window
	{
		public WindowNovaKategorija(KategorijaListStore presenter) :base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			// kada se klikne spremi gumb
			spremiButton.Clicked += (sender, e) =>
			{
				//pokušaj sljedeće
				try
				{
					//spremi novu kategoriju
					presenter.dodajKategoriju(entry1.Text);
					this.Destroy();
				}
				catch (Exception ex)
				{
					// ukoliko se javila greška prikaži je korisniku preko messagedialog-a
					MessageDialog d = new MessageDialog(this, DialogFlags.Modal, MessageType.Warning, ButtonsType.Ok, ex.Message);
					d.Run();
					d.Destroy();

				}
			};
		}
	}
}
