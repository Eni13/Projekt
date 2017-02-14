using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;
namespace projektPdf
{
	public static class BPKategorija
	{
		static BPKategorija()
		{
			BP.otvoriKonekciju();

			//Kreiranje komande
			SqliteCommand command = BP.konekcija.CreateCommand();

			//kreira tablicu kategorija ako ne postoji
			command.CommandText = @"CREATE TABLE IF NOT EXISTS Kategorija (
				id integer primary key autoincrement,
				ime nvarchar(60) NOT NULL)";

			//Izvrsavanja komande
			command.ExecuteNonQuery();

			//"Ciscenje" komande
			command.Dispose();

			BP.zatvoriKonekciju();
		}
		public static List<Kategorija> getKategorije()
		{
			BP.otvoriKonekciju();

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
				int id = (int)(Int64)reader["id"];
				string ime = (string)reader["ime"];

				var k = new Kategorija(id, ime);

				lista.Add(k);
			}
			BP.zatvoriKonekciju();
			return lista;

		}
		public static Kategorija dodajNovuKategoriju(Kategorija kategorija)
		{
			BP.otvoriKonekciju();

			SqliteCommand command = BP.konekcija.CreateCommand();

			command.CommandText = String.Format(@"INSERT INTO Kategorija (ime)
											  VALUES ('{0}')", kategorija.Naziv);
			command.ExecuteNonQuery();
			command.Dispose();

			SqliteCommand getId = BP.konekcija.CreateCommand();
			getId.CommandText = "SELECT last_insert_rowid()";
			kategorija.Id = (int)(long)getId.ExecuteScalar();
			getId.Dispose();

			BP.zatvoriKonekciju();
			return kategorija;

		}
	}
}
