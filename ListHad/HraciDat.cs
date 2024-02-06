using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ListHad
{   //třída databáze hráčů
    internal class HraciDat
    {
        public static void TabulkaHracu(int score)
        {
            //připojovací string k databázi
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = @"(LocalDB)\MSSQLLocalDB";//sever
            csb.InitialCatalog = "Hraci";//databáze            
            csb.IntegratedSecurity = true;//true
            string connectionString = csb.ConnectionString;

            using (SqlConnection pripojeni = new SqlConnection(connectionString))
            {
                //vstupní data
                Console.Write("Zadej jméno: ");
                string jmeno = Console.ReadLine().Trim();
                if (jmeno.Length == 0) jmeno = "NoName";
                jmeno = jmeno.PadRight(30).Remove(30);
                int score2 = score;
                string dotaz = "INSERT INTO SeznamHracu (JmenoHrace, Score) VALUES (@jmeno, @score2)";
                string dotaz2 = "SELECT * FROM SeznamHracu ORDER BY Score DESC";//ASC=ascending;DESC=descending
                bool zapsat = false;
                //kontrola => porovnání 10 top hráčů
                using (SqlCommand sqlDotaz = new SqlCommand(dotaz2, pripojeni))
                {
                    pripojeni.Open();
                    SqlDataReader dataReader = sqlDotaz.ExecuteReader();
                    int i = 0;
                    while (dataReader.Read())
                    {
                        if (i < 10)
                        {
                            if (score2 > (int)dataReader["Score"])
                            {
                                zapsat = true;
                                break;
                            }
                        }
                        else
                            break;
                        i++;
                    }
                    pripojeni.Close();
                }
                //zapsání/přidání hráče do databáze
                if (zapsat)
                {
                    using (SqlCommand sqlDotaz = new SqlCommand(dotaz, pripojeni))
                    {
                        pripojeni.Open();
                        sqlDotaz.Parameters.AddWithValue("@jmeno", jmeno);
                        sqlDotaz.Parameters.AddWithValue("@score2", score2);
                        sqlDotaz.ExecuteNonQuery();
                        pripojeni.Close();
                    }
                }
                //odstranění záznamů nad 10 v tabulce
                using (SqlDataAdapter adapter = new SqlDataAdapter(dotaz2, pripojeni))
                using (DataSet vysledky = new DataSet())
                {
                    adapter.Fill(vysledky);
                    int i = 0;
                    foreach (DataRow radek in vysledky.Tables[0].Rows)
                    {
                        if (i >= 10)
                            radek.Delete();
                        i++;
                    }
                    //uložení změn databáze (DataSet a SqlDataAdapter v odpoj.režimu)
                    SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(adapter);
                    adapter.Update(vysledky.Tables[0]);
                }
                //výpis a seřazení dat z databáze (ORDER BY = seřazení)
                using (SqlCommand sqlDotaz = new SqlCommand(dotaz2, pripojeni))
                {
                    pripojeni.Open();
                    SqlDataReader dataReader = sqlDotaz.ExecuteReader();
                    string mezera = "";
                    Console.WriteLine($"\nHráč{mezera.PadRight(55)}Skóre\n");
                    while (dataReader.Read())
                        Console.WriteLine($"{dataReader["JmenoHrace"]}{mezera.PadRight(30)}{dataReader["Score"]}");
                    pripojeni.Close();
                }
                if (!zapsat)
                    Console.WriteLine("\nBohužel, neumístil jsi se mezi 10 TOP hráčů");
            }           
        }
    }
}
