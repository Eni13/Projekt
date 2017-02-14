using System;
using System.Collections.Generic;
namespace projektPdf
{
	public class Kategorija
	{
		private int id;
		private string naziv;
		// lista svih kategorija 
		public static List<Kategorija> kategorije = BPKategorija.getKategorije();
		public Kategorija(int id, string naziv)
		{
			Id = id;
			Naziv = naziv;
		}

		public int Id
		{
			get
			{
				return id;
			}

			set
			{
				id = value;
			}
		}

		public string Naziv
		{
			get
			{
				return naziv;
			}

			set
			{	//ako se naziv kategorije želi postaviti na ime sa manje od jednog znaka, baci grešku
				if (value.Length < 1)
				{
					throw new ArgumentException("Naziv kategorije nesmije biti prazan");
				}


				naziv = value;
			}
		}
		public static Kategorija dohvatiPoImenu(string naziv)
		{
			// za svaku kategoriju u statičkoj listi svih kategorija
			foreach (var k in kategorije)
			{
				// provjeri dali se naziv kategorije podudara sa nazivom predanim preko argumenta, ukoliko da vrati tu kategoriju
				if (k.naziv == naziv) return k;
			}
			return null;
		}
		public static Kategorija dohvatiPoId(int id)
		{
			// za svaku kategoriju u statičkoj listi svih kategorija
			foreach (var k in kategorije)
			{
				// provjeri dali se id kategorije podudara sa id predanim preko argumenta, ukoliko da vrati tu kategoriju
				if (k.id == id) return k;
			}
			return null;
			
		}

	}
}
