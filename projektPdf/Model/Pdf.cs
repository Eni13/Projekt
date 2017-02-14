using System.Collections.Generic;
namespace projektPdf
{
	public class Pdf
	{
		private long id;
		private string naziv;
		private string path;
		public List<string> tagovi { get; }
		public Kategorija kategorija { get; }
		public Pdf(string naziv, string path, long id,Kategorija kategorija, List<string> tagovi)
		{
			this.naziv = naziv;
			this.path = path;
			this.id = id;
			this.tagovi = tagovi;
			this.kategorija = kategorija;
		}

		public string Naziv
		{
			get
			{
				return naziv;
			}

			set
			{
				naziv = value;
			}
		}

		public string Path
		{
			get
			{
				return path;
			}

			set
			{
				path = value;
			}
		}

		public long Id
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
	}
}
