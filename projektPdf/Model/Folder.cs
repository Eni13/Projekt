using System;
namespace projektPdf
{
	public class Folder
	{
		private long id;
		private string path;

		public Folder()
		{
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
	}
}
