using System;
using System.IO;
using NReco.PdfRenderer;
using iTextSharp.text.pdf;
using Gtk;

namespace projektPdf
{
	public partial class WindowPregled : Gtk.Window
	{
		public WindowPregled(string pdfPath, long id, string naziv) :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();

			bool flag = false;

			DirectoryInfo d = new DirectoryInfo("C:\\temp");
			FileInfo[] Files = d.GetFiles("*.jpg");
			string str = "";
			foreach (FileInfo file in Files)
			{
				str = file.Name;

				if (str == String.Format("{0}{1}1.jpg", naziv, id))
				{
					flag = true;
					break;
				}
			}

			string ppath = pdfPath;
			PdfReader pdfReader = new PdfReader(ppath);
			int numberOfPages = pdfReader.NumberOfPages;

			if (!flag)
			{
				var pdfFile = pdfPath;
				var pdfToImg = new NReco.PdfRenderer.PdfToImageConverter();
				pdfToImg.ScaleTo = 800;

				for (int i = 1; i <= numberOfPages; i++)
				{
					pdfToImg.GenerateImage(pdfFile, i,
					                       ImageFormat.Jpeg, String.Format(@"C:\temp\{0}{1}{2}.jpg", naziv, id, i));
				}

			}


			for (int i = 1; i <= numberOfPages; i++)
			{
				Image temp = new Image();

				var buffer = System.IO.File.ReadAllBytes(String.Format(@"C:\temp\{0}{1}{2}.jpg", naziv, id, i));
				var pixbuf = new Gdk.Pixbuf(buffer);
				temp.Pixbuf = pixbuf;

				vbox1.Add(temp);
			}

			Build();
		}
	}
}
