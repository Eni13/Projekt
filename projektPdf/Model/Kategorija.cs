using System;
using System.Collections.Generic;
namespace projektPdf
{
	public class Kategorija
	{
		private int id;
		private string naziv;
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
			{
				if (value.Length < 1)
				{
					throw new ArgumentException("Naziv kategorije nesmije biti prazan");
				}
				naziv = value;
			}
		}
		public static Kategorija dohvatiPoImenu(string naziv)
		{
			foreach (var k in kategorije)
			{
				if (k.naziv == naziv) return k;
			}
			return null;
		}

	}
}
