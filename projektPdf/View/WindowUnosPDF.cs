using System;
namespace projektPdf
{
	public partial class WindowUnosPDF : Gtk.Window
	{
		KategorijaListStore kategorijaPresenter = new KategorijaListStore();
		public WindowUnosPDF(string path) :base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			putanjaLabela.LabelProp = path;
			string p = path.Substring(0, path.Length - 4);
			nazivLabela.LabelProp =p.Substring( p.LastIndexOf('\\')+1);
			combobox1.Model = kategorijaPresenter;
			combobox1.Active = 0;

			dodajNovuKatButton.Clicked += (sender, e) =>
			{
				var nova = new WindowNovaKategorija(kategorijaPresenter);	
			};
			odustaniButton.Clicked+=(sender, e) => Destroy();
			spremiButton.Clicked+=(sender, e) => spremi();
		}
		private void spremi()
		{
			System.Collections.Generic.List<string> tagovi = new System.Collections.Generic.List<string>();

			var tags = textview1.Buffer.Text.Split(',');
			foreach (var t in tags)
			{
				tagovi.Add(t.Trim());

			}

			BPPDF.Spremi(new Pdf(nazivLabela.LabelProp, putanjaLabela.LabelProp, 0, Kategorija.dohvatiPoImenu(combobox1.ActiveText), tagovi));
			this.Destroy();
		}
	}
}
