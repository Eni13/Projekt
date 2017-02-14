using System;
namespace projektPdf
{
	public  class KategorijaListStore :Gtk.ListStore
	{
		public KategorijaListStore() : base(typeof(string))
		{
			Kategorija.kategorije.ForEach((obj) => this.AppendValues(obj.Naziv));
		}
		public void dodajKategoriju(string naziv)
		{
			var nova = new Kategorija(0, naziv);
			nova = BPKategorija.dodajNovuKategoriju(nova);
			Kategorija.kategorije.Add(nova);
			this.AppendValues(naziv);
		}
	}
}
