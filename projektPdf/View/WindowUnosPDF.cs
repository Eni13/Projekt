using System;
namespace projektPdf
{
	public partial class WindowUnosPDF : Gtk.Window
	{
		KategorijaListStore kategorijaPresenter = new KategorijaListStore();
		public WindowUnosPDF(string path) :base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			//postavi text labele na path
			putanjaLabela.LabelProp = path;
			// od path stringa makni nastavak .pdf
			string p = path.Substring(0, path.Length - 4);
			// postavi tex labele naziva na string od kojeg maknes sve osim onog što se nalazi nakon posljednjeg backslahsa 
			nazivLabela.LabelProp = p.Substring( p.LastIndexOf('\\')+1);
			//postavi dropdown combobox model na kategorija presenter da bi se prikazale kategorije
			combobox1.Model = kategorijaPresenter;
			//postavi početnu odabranu kategoriju
			combobox1.Active = 0;

			//kad korisnik klikne na dodaj novu kategoriju gum
			dodajNovuKatButton.Clicked += (sender, e) =>
			{	
				// kreiraj novi prozor i prenesi referencu na kategorijaPresenter
				var nova = new WindowNovaKategorija(kategorijaPresenter);	
			};
			// klikom na gumb odustani poziva se funkcija koja uništava prozor
			odustaniButton.Clicked+=(sender, e) => Destroy();
			// klikom na gumb spremi poziv se fukcija spremi
			spremiButton.Clicked+=(sender, e) => spremi();
		}
		private void spremi()
		{
			// kreiranje liste stringova u koju će se spremiti tagovi
			System.Collections.Generic.List<string> tagovi = new System.Collections.Generic.List<string>();

			//dijeljenje jednog stringa tagova u polje stringova
			var tags = textview1.Buffer.Text.Split(',');
			//za svaki tag u polju tagova
			foreach (var t in tags)
			{
				//dodaj tag u listu nakon što se maknu prazna mjesta sa početka i kraja taga
				tagovi.Add(t.Trim());
			}
			// kreiraj pdf i dodaj ga u bazu
			BPPDF.Spremi(new Pdf(nazivLabela.LabelProp, putanjaLabela.LabelProp, 0, Kategorija.dohvatiPoImenu(combobox1.ActiveText), tagovi));
			// uništi prozor
			this.Destroy();
		}
	}
}
