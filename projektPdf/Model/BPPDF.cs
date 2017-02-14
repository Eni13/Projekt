using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace projektPdf
{
	public static class BPPDF
	{

		static BPPDF()
		{
			

			//Kreiranje komande
			SqliteCommand command = BP.konekcija.CreateCommand();
			// ako tablica ne postoji, kreiraj novu tablicu 
			command.CommandText = @"CREATE TABLE IF NOT EXISTS PDF (
				id integer primary key autoincrement,
				id_kategorija integer NOT NULL,
				ime nvarchar(60) NOT NULL,
				path nvarchar(256) NOT NULL,
				tagovi TEXT NOT NULL,
				FOREIGN KEY(id_kategorija) REFERENCES kategorija(id))";

			//Izvrsavanja komande
			command.ExecuteNonQuery();

			//"Ciscenje" komande
			command.Dispose();


		}



		//Funkcija koja sprema događaj koji joj je dan kao argument
		public static void Spremi(Pdf pdf)
		{
			

			//Kreiranje komande
			SqliteCommand command = BP.konekcija.CreateCommand();

			//pretvaranje tagova u string 
			string tagovi = "";
			pdf.tagovi.ForEach((obj) => tagovi+=obj+",");

			//Dodjeljivanje SQL upita prethodno kreiranoj komandi
			//Upit umece redak u tablicu
			command.CommandText = String.Format(@"Insert into pdf (id_kategorija, ime ,path, tagovi) 
											Values ('{0}','{1}','{2}','{3}')", pdf.kategorija.Id,pdf.Naziv,pdf.Path,tagovi);

			//Izvrsavanja komande
			command.ExecuteNonQuery();

			//"Ciscenje" komande
			command.Dispose();

		}

		public static void Izbrisi(Pdf p)
		{
			
			//Kreiranje komande
			SqliteCommand command = BP.konekcija.CreateCommand();

			//Dodjeljivanje SQL upita prethodno kreiranoj komandi
			//Upit brise redak koji ima isti id kao vrijednost argumenta
			command.CommandText = String.Format(@"Delete from pdf Where id = '{0}'", p.Id);

			//Izvrsavanja komande
			command.ExecuteNonQuery();

			//"Ciscenje" komande
			command.Dispose();


		}

		public static List<Pdf> DohavtiSve()
		{
			

			//Instanciranje liste u koju ce se spremiti svi pdf-ovi
			List<Pdf> lista = new List<Pdf>();

			//Kreiranje komande
			SqliteCommand command = BP.konekcija.CreateCommand();

			//Umetanje podataka u tablicu
			//Upit oznacava tj. vraca sve redke iz tablice dogadaj
			command.CommandText = "Select * from pdf";

			//Kreiranje reader-a
			SqliteDataReader reader = command.ExecuteReader();

			//while petlja s kojom se prolazi kroz sve dohvacene redke
			while (reader.Read())
			{



				//Dodjeljivanje vrijednosti atributima
				int Id = (int)(Int64)reader["id"];
				int idKategorije = (int)(Int64)reader["id_kategorija"];
				string ime = (string)reader["ime"];
				string Path = (string)reader["path"];
				string tag = (string)reader["tagovi"];

				//kreiranje liste u kojoj će se sadžavati tagovi
				List<string> tagovi = new List<string>();

				// podjeli pročitane tagove u polje stringova, tagovi su odjeljeni zarezima
				var tags = tag.Split(',');
				// za svaki tag u tags
				foreach (var t in tags)
				{
					// dodaj tag u prije kreiranu listu tagova
					tagovi.Add(t);
				}
				//kreiraj novi pdf objekt
				Pdf temp = new Pdf(ime, Path, Id, Kategorija.dohvatiPoId(idKategorije),tagovi);


				//Umetni objekt pdf u listu pdf-ova
				lista.Add(temp);
			}

			//Ciscenje reader-a i command-e
			reader.Dispose();
			command.Dispose();

		
			//Vracanje prethodno instaciranje liste dogadaja koja sad sadrzi sve dogadaje iz baze podataka
			return lista;
		}

		public static List<Pdf> DohvatiIzKategorije(Kategorija kategorija)
		{
			

			//Instanciranje liste u koju ce se spremiti svi dogadaji
			List<Pdf> lista = new List<Pdf>();

			//Kreiranje komande
			SqliteCommand command = BP.konekcija.CreateCommand();

			//Umetanje podataka u tablicu
			//Upit oznacava tj. vraca sve redke iz tablice dogadaj
			command.CommandText =String.Format( "Select * from pdf where id_kategorija = '{0}'",kategorija.Id);

			//Kreiranje reader-a
			SqliteDataReader reader = command.ExecuteReader();

			//while petlja s kojom se prolazi kroz sve dohvacene redke
			while (reader.Read())
			{



				//Dodjeljivanje vrijednosti atributima
				int Id = (int)(Int64)reader["id"];
				int idKategorije = (int)(Int64)reader["id_kategorija"];
				string ime = (string)reader["ime"];
				string Path = (string)reader["path"];
				string tag = (string)reader["tagovi"];

				//kreiranje liste u kojoj će se sadžavati tagovi
				List<string> tagovi = new List<string>();

				// podjeli pročitane tagove u polje stringova, tagovi su odjeljeni zarezima
				var tags = tag.Split(',');
				// za svaki tag iz polja tagova(tags)
				foreach (var t in tags)
				{
					// dodaj tag u prije kreiranu listu tagova
					tagovi.Add(t);
				}
				//kreiraj novi objekt pdf-a
				Pdf temp = new Pdf(ime, Path, Id, Kategorija.dohvatiPoId(idKategorije), tagovi);


				//Umetanje dogadaja u tablicu
				lista.Add(temp);
			}

			//Ciscenje reader-a i command-e
			reader.Dispose();
			command.Dispose();

		

			//Vracanje prethodno instaciranje liste dogadaja koja sad sadrzi sve dogadaje iz baze podataka
			return lista;
		}
	}
}
