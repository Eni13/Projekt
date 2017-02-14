using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Gtk;
using projektPdf;

public partial class MainWindow : Gtk.Window
{
	public PdfNodeStore pdfPresenter = new PdfNodeStore();

	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		Build();

		buttonDodajFolder.Clicked += dodajPdf;

		buttonOtvori.Clicked += otvoriPdf;
		buttonIzbrisi.Clicked += izbrisiPdf;


		sviPdfoviButton.Clicked+=(sender, e) => dohvatiPdf(sender,e);


		//Dodjeljivanje presentera nodeview-u
		nodeview1.NodeStore = pdfPresenter;

		//Dodavanje stupaca koji ce se prikazivati u nodeview
		nodeview1.AppendColumn("Naziv", new CellRendererText(), "text", 0);

		// dodaje kategorije u obliku gumbova
		generirajKategorije();

		pretraziButton.Clicked+=(sender, e) => pretrazi();
	}

	protected void dohvatiPdf(object sender, EventArgs a)
	{
		List<Pdf> lista = BPPDF.DohavtiSve();

		pdfPresenter.Clear();
		pdfPresenter.dodajVise(lista);

	}

	protected void otvoriPdf(object sender, EventArgs a)
	{
		//Dohacanje odaberenog dogadaja
		PdfNode pdfSelected = nodeview1.NodeSelection.SelectedNode as PdfNode;
		if (pdfSelected == null)
			return;
		//otvaranje pdf-a u defaultno podešenom programu
		System.Diagnostics.Process.Start(@pdfSelected.path);
	}
	protected async void izbrisiPdf(object sender, EventArgs a)
	{
		//Dohacanje odaberenog dogadaja
		PdfNode pdfSelected = nodeview1.NodeSelection.SelectedNode as PdfNode;
		if (pdfSelected == null)
			return;
		
		System.Diagnostics.Process process = new System.Diagnostics.Process();
		System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
		startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
		startInfo.FileName = "cmd.exe";
		startInfo.Arguments = "/C del \""  + pdfSelected.path + "\"";
		process.StartInfo = startInfo;
		process.Start();

		await Task.Delay(TimeSpan.FromSeconds(1));

		//izbrisi prikaz izbrisanog pdf-a
		pdfPresenter.RemoveNode(pdfSelected);
	}
	private void generirajKategorije()
	{
		var vbox = new VBox(false,5);
		foreach (var kat in Kategorija.kategorije)
		{
			var button = new Button(kat.Naziv);
			button.Clicked += (sender, e) => pdfPresenter.dohvatiPdfUKategoriji(kat);
			vbox.Add(button);

		}
		scrolledwindow1.AddWithViewport(vbox);
		scrolledwindow1.ShowAll();
	}



	protected void dodajPdf(object sender, EventArgs a)
	{
		var dodajFolderWindow = new WindowDodajPDF();

		dodajFolderWindow.Destroyed += dohvatiPdf;
	}
	private void pretrazi()
	{
		if (entry2.Text.Length == 0) dohvatiPdf(null, null);
		System.Collections.Generic.List<string> tagovi = new System.Collections.Generic.List<string>();

		var tags = entry2.Text.Split(',');
		foreach (var t in tags)
		{
			tagovi.Add(t.Trim());
		}
		pdfPresenter.dohvatiPoTagovima(tagovi);
	}


	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}
}
