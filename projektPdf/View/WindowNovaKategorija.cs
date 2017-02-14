using System;
namespace projektPdf
{
	public partial class WindowNovaKategorija : Gtk.Window
	{
		public WindowNovaKategorija(KategorijaListStore presenter) :base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			spremiButton.Clicked += (sender, e) =>
			{
				presenter.dodajKategoriju(entry1.Text);
				this.Destroy();
			};
		}
	}
}
