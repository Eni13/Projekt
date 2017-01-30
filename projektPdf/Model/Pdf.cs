using System;
namespace projektPdf
{
	public class Pdf
	{
		private long id;
		private string naziv;
		private string path;

		public Pdf(string naziv, string path, long id)
		{
			this.naziv = naziv;
			this.path = path;
			this.id = id;
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
