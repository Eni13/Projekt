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

		buttonDodajFolder.Clicked += dodajFolder;
		buttonListaFoldera.Clicked += listaFolder;
		buttonOtvori.Clicked += otvoriPdf;
		buttonIzbrisi.Clicked += izbrisiPdf;

		combobox4.Changed += dohvatiSortirano;

		//pdfPresenter = new PdfNodeStore();

		dohvatiPdf(null, null);

		//Dodjeljivanje presentera nodeview-u
		nodeview1.NodeStore = pdfPresenter;

		//Dodavanje stupaca koji ce se prikazivati u nodeview
		nodeview1.AppendColumn("Naziv", new CellRendererText(), "text", 0);
	}

	protected void dohvatiPdf(object sender, EventArgs a)
	{
		List<Folder> listaFoldera = BPFolder.DohavtiSve();

		pdfPresenter.Clear();

		foreach (var i in listaFoldera)
		{
			DirectoryInfo d = new DirectoryInfo(i.Path);//Assuming Test is your Folder
			FileInfo[] Files = d.GetFiles("*.pdf"); //Getting Text files
			string str = "";
			foreach (FileInfo file in Files)
			{
				str = file.Name;

				Pdf temp = new Pdf(str.Substring(0, str.Length - 4), i.Path + "\\" + str, i.Id);

				pdfPresenter.dodajPdf(temp);
			}
		}
	}

	protected void otvoriPdf(object sender, EventArgs a)
	{
		//Dohacanje odaberenog dogadaja
		PdfNode pdfSelected = nodeview1.NodeSelection.SelectedNode as PdfNode;
		if (pdfSelected == null)
			return;

		var windowPregled = new WindowPregled(pdfSelected.path, pdfSelected.id, pdfSelected.naziv);
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

		dohvatiPdf(null, null);
	}

	protected void dohvatiSortirano(object sender, EventArgs a)
	{
		List<Folder> listaFoldera = BPFolder.DohavtiSve();
		List<Pdf> listaPdf = new List<Pdf>();

		foreach (var i in listaFoldera)
		{
			DirectoryInfo d = new DirectoryInfo(i.Path);//Assuming Test is your Folder
			FileInfo[] Files = d.GetFiles("*.pdf"); //Getting Text files
			string str = "";
			foreach (FileInfo file in Files)
			{
				str = file.Name;

				Pdf temp = new Pdf(str.Substring(0, str.Length - 4), i.Path + "\\" + str, i.Id);

				listaPdf.Add(temp);
			}
		}

		ComboBox comboBoxSort = sender as ComboBox;

		switch (comboBoxSort.ActiveText)
		{
			case "Abecedno (uzlazno)":
				listaPdf = listaPdf.OrderBy(s => s.Naziv).ToList();
				break;
			case "Abecedno (silazno)":
				listaPdf = listaPdf.OrderByDescending(s => s.Naziv).ToList();
				break;
		}

		pdfPresenter.Clear();

		foreach (var i in listaPdf)
		{
			pdfPresenter.dodajPdf(i);
		}
	}

	protected void dodajFolder(object sender, EventArgs a)
	{
		var dodajFolderWindow = new WindowDodajFolder();

		dodajFolderWindow.Destroyed += dohvatiPdf;
	}

	protected void listaFolder(object sender, EventArgs a)
	{
		var listaFolderWindow = new WindowListaFoldera();

		listaFolderWindow.Destroyed += dohvatiPdf;
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}
}
