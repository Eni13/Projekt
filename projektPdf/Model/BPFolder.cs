using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace projektPdf
{
	public static class BPFolder
	{
		//Funkcija koja sprema događaj koji joj je dan kao argument
		public static void Spremi(string path)
		{
			BP.otvoriKonekciju();

			//Kreiranje komande
			SqliteCommand command = BP.konekcija.CreateCommand();

			//Dodjeljivanje SQL upita prethodno kreiranoj komandi
			//Upit umece redak u tablicu
			command.CommandText = String.Format(@"Insert into folder (path) Values ('{0}')", path);

			//Izvrsavanja komande
			command.ExecuteNonQuery();

			//"Ciscenje" komande
			command.Dispose();

			BP.zatvoriKonekciju();
		}

		public static void Izbrisi(long id)
		{
			BP.otvoriKonekciju();

			//Kreiranje komande
			SqliteCommand command = BP.konekcija.CreateCommand();

			//Dodjeljivanje SQL upita prethodno kreiranoj komandi
			//Upit brise redak koji ima isti id kao vrijednost argumenta
			command.CommandText = String.Format(@"Delete from folder Where id = '{0}'", id);

			//Izvrsavanja komande
			command.ExecuteNonQuery();

			//"Ciscenje" komande
			command.Dispose();

			BP.zatvoriKonekciju();
		}

		public static List<Folder> DohavtiSve()
		{
			BP.otvoriKonekciju();

			//Instanciranje liste u koju ce se spremiti svi dogadaji
			List<Folder> listaFoldera = new List<Folder>();

			//Kreiranje komande
			SqliteCommand command = BP.konekcija.CreateCommand();

			//Umetanje podataka u tablicu
			//Upit oznacava tj. vraca sve redke iz tablice dogadaj
			command.CommandText = "Select * from folder";

			//Kreiranje reader-a
			SqliteDataReader reader = command.ExecuteReader();

			//while petlja s kojom se prolazi kroz sve dohvacene redke
			while (reader.Read())
			{
				//Instanciranje novog dogadaja koji ce se spremati u listu
				Folder temp = new Folder();

				//Dodjeljivanje vrijednosti atributima
				temp.Id = (int)(Int64)reader["id"];
				temp.Path = (string)reader["path"];

				//Umetanje dogadaja u tablicu
				listaFoldera.Add(temp);
			}

			//Ciscenje reader-a i command-e
			reader.Dispose();
			command.Dispose();

			BP.zatvoriKonekciju();

			//Vracanje prethodno instaciranje liste dogadaja koja sad sadrzi sve dogadaje iz baze podataka
			return listaFoldera;
		}
	}
}
