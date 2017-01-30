using System;
using Mono.Data.Sqlite;
namespace projektPdf
{
	public static class BP
	{
		//String koji sadrzi putanju do baze podataka
		private static string connectionString = "URI=file:BazaPodataka.db";

		//Konekcija prema bazi
		internal static SqliteConnection konekcija = new SqliteConnection(connectionString);

		public static void otvoriKonekciju()
		{
			konekcija.Open();
		}

		public static void zatvoriKonekciju()
		{
			konekcija.Close();
		}
	}
}
