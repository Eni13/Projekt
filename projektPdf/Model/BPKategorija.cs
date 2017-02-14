using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;
namespace projektPdf
{
	public static class BPKategorija
	{
		static BPKategorija()
		{
			
			//Kreiranje komande
			SqliteCommand command = BP.konekcija.CreateCommand();

			//kreira tablicu kategorija ako ne postoji
			command.CommandText = @"CREATE TABLE IF NOT EXISTS Kategorija (
				id integer primary key autoincrement,
				ime nvarchar(60) NOT NULL)";

			//Izvrsavanja komande
			command.ExecuteNonQuery();
			//pobroji koliko redaka ima tablica
			command.CommandText = "select count(*) from kategorija";
			//ukoliko tablica ima 0 redaka
			if ((int)(long)command.ExecuteScalar() == 0)
			{
				//unesi tri retka, posao, fakultet i osobno 
				command.CommandText = "insert into kategorija (ime) values('Posao');" +
								   	  "insert into kategorija (ime) values('Fakultet');" +
									  "insert into kategorija (ime) values('Osobno')";
				//izvrši naredbu
				command.ExecuteNonQuery();
			}

			//"Ciscenje" komande
			command.Dispose();

		
		}
		public static List<Kategorija> getKategorije()
		{
			

			//Kreiranje komande
			SqliteCommand command = BP.konekcija.CreateCommand();

			//odabir svih kategorija iz baze
			command.CommandText = "select * from kategorija";

			//Kreiranje reader-a
			SqliteDataReader reader = command.ExecuteReader();

			//kreiranje liste kategorija
			List<Kategorija> lista = new List<Kategorija>();

			//while petlja s kojom se prolazi kroz sve dohvacene redke
			while (reader.Read())
			{
					//čitanje atributa
					int id = (int)(Int64)reader["id"];
					string ime = (string)reader["ime"];
					//kreiraj novi objekt kategorije
					var k = new Kategorija(id, ime);
					// dodaj kategoriju u listu kategorija
					lista.Add(k);


			}
			//"Ciscenje" komande
			command.Dispose();

			return lista;

		}
		public static Kategorija dodajNovuKategoriju(Kategorija kategorija)
		{
			//Kreiranje komande
			SqliteCommand command = BP.konekcija.CreateCommand();
			// insert novi redak u tablicu kategorija
			command.CommandText = String.Format(@"INSERT INTO Kategorija (ime)
											  VALUES ('{0}')", kategorija.Naziv);
			// izvrši naredbu
			command.ExecuteNonQuery();
			//"Ciscenje" komande
			command.Dispose();

			//kreiranje komande
			SqliteCommand getId = BP.konekcija.CreateCommand();
			//dohvati zadnje unešeni id retka
			getId.CommandText = "SELECT last_insert_rowid()";
			//postavi id kategorije na odabrani id
			kategorija.Id = (int)(long)getId.ExecuteScalar();
			//"Ciscenje" komande
			getId.Dispose();

			//vrati kategoriju
			return kategorija;

		}
	}
}
