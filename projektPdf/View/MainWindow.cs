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

		//klikom na gumb dodaj pdf pozovi funkciju dodajPdf
		buttonDodajPDF.Clicked += dodajPdf;

		//klikom na gumb otvori pdf pozovi funkciju otvoriPdf
		buttonOtvori.Clicked += otvoriPdf;

		//klikom na gumb izbrisi pdf pozovi funkciju izbrisiPdf
		buttonIzbrisi.Clicked += izbrisiPdf;

		//klikom na gumb svi pdfovi pozovi funkciju dohvatiPdf
		sviPdfoviButton.Clicked+=(sender, e) => dohvatiPdf(sender,e);


		//Dodjeljivanje presentera nodeview-u
		nodeview1.NodeStore = pdfPresenter;

		//Dodavanje stupaca koji ce se prikazivati u nodeview
		nodeview1.AppendColumn("Naziv", new CellRendererText(), "text", 0);

		// dodaje kategorije u obliku gumbova
		generirajKategorije();
		//klikom na gumb pretrazi pozovi funkciju pretrazi
		pretraziButton.Clicked+=(sender, e) => pretrazi();
	}

	protected void dohvatiPdf(object sender, EventArgs a)
	{
		//dohvati sve pdf-ove u listu
		List<Pdf> lista = BPPDF.DohavtiSve();
		// izbriši sve retke u nodeview-u
		pdfPresenter.Clear();
		//dodaj pdf-ove iz liste
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
		//Dohvacanje odaberenog retka
		PdfNode pdfSelected = nodeview1.NodeSelection.SelectedNode as PdfNode;
		//ako nije odabran nijedan redak vrati se natrag
		if (pdfSelected == null)
			return;
		//izbriši pdf iz računala
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
		//kreiraj novi vbox 
		var vbox = new VBox(false,5);
		// za svaku kategoriju iz liste svih kategorija
		foreach (var kat in Kategorija.kategorije)
		{
			//kreiraj novi gumb sa imenom kategorije
			var button = new Button(kat.Naziv);
			//pretplati se da se pritiskom na gumb poziva funkcija dohvatiPdfUKategoriji kojoj se predaje kategorija
			button.Clicked += (sender, e) => pdfPresenter.dohvatiPdfUKategoriji(kat);
			//dodaj gubm u vbox
			vbox.Add(button);

		}
		//dodaj vbox u srollabilni poglded(widget)
		scrolledwindow1.AddWithViewport(vbox);
		//prikazi sve što scrolledwindow1 sadrži(vbox i buttone sa svim kategorijama)
		scrolledwindow1.ShowAll();
	}



	protected void dodajPdf(object sender, EventArgs a)
	{
		// kreiraj prozor za odabur novog pdf-a
		var dodajNoviPdf = new WindowDodajPDF();
		//kada se prozor za odabir pdf-a uništi izvrši sljedeće
		dodajNoviPdf.Destroyed += (sender1, e1) =>
		{
			// pozovi funkciju dohvatiPdf
			dohvatiPdf(sender1, e1);
			//makni svu djecu u scrolledwindow1
			scrolledwindow1.Remove(scrolledwindow1.Child);
			//pozovi funkciju za popunjavanje kategorija
			generirajKategorije();

		};;
	}
	private void pretrazi()
	{
		//ako je pretraga prazna, prikazi sve pdf-ove
		if (entry2.Text.Length == 0) dohvatiPdf(null, null);
		//kreiraj novu list stringova
		System.Collections.Generic.List<string> tagovi = new System.Collections.Generic.List<string>();

		//podjeli string koji je korisnik unio u polje stringova, dijeli ih prema zarezima
		var tags = entry2.Text.Split(',');
		//za svaki tag u polju
		foreach (var t in tags)
		{
			//dodaj tag u polje nakon što mu makneš prazna mjesta s početka i kraja
			tagovi.Add(t.Trim());
		}
		//pozovi funkciju iz presentera koji briše sve retke i generira samo relevante(po tagovima) pdf-ove
		pdfPresenter.dohvatiPoTagovima(tagovi);
	}


	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		BP.zatvoriKonekciju();
		a.RetVal = true;
	}
}
