using System;
namespace projektPdf
{
	public  class KategorijaListStore :Gtk.ListStore
	{
		public KategorijaListStore() : base(typeof(string))
		{
			//za svaku kategoriju dodaj vrijednost naziva u sadrćaj liststora
			Kategorija.kategorije.ForEach((obj) => this.AppendValues(obj.Naziv));
		}
		public void dodajKategoriju(string naziv)
		{
			// instanciraj novu kategoriju
			var nova = new Kategorija(0, naziv);
			//dodaj novu kategoriju u bazu
			nova = BPKategorija.dodajNovuKategoriju(nova);
			// dodaj u listu svih kategorija
			Kategorija.kategorije.Add(nova);
			// dodaj naziv kategorije u liststore
			this.AppendValues(naziv);
		}
	}
}
