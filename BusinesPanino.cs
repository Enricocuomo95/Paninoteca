using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Paninoteca
{
    internal class BusinesPanino
    {
        private string credenziali;
        private List<Panino> lista;

        public BusinesPanino()
        {
            credenziali = "Server=ACADEMY2024-16\\SQLEXPRESS;Database=panino;User Id=academy;Password=academy!;MultipleActiveResultSets=true;Encrypt=false;TrustServerCertificate=false";
            lista = new List<Panino>();
        }


        private void inizializzaLista()
        {
            Panino p;

            using (SqlConnection con = new SqlConnection(credenziali))
            {
                string query = "SELECT Nome, Descrizione, Prezzo, Vegan FROM Panino";
                //SqlCommand cmd = con.CreateCommand(query, );
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    p = new Panino();
                    p.Nome = reader.GetString(0);
                    p.Descrizione = reader.GetString(1);
                    p.Prezzo = Convert.ToDouble(reader["prezzo"]);
                    p.Vegan = reader.GetBoolean(3);
                    lista.Add(p);
                }

                con.Close();
            }
        }

        public List<Panino> getLista()
        {
            if (lista.Count == 0)
                inizializzaLista();
            return lista;
        }

        public List<Panino> getListaVegano()
        {
            List<Panino> risultato = new List<Panino>();
            if (lista.Count == 0)
                inizializzaLista();

            foreach (Panino p in lista)
                if (p.Vegan)
                    risultato.Add(p);

            return risultato;
        }

        public List<Panino> getListaVeganoBetter()
        {
            List<Panino> risultato = new List<Panino> ();
            Panino p;

            if (lista.Count == 0)
                return (null);

            var valore = from Panino in lista
                         where Panino.Vegan == true
                         select Panino;
            foreach (var item in risultato)
            {
                p = new Panino();
                p.Nome = item.Nome;
                p.Descrizione = item.Descrizione;
                p.Vegan = item.Vegan;
                p.Prezzo = item.Prezzo;
            }
                return(risultato);
        }


        public int getNElementi()
        {
            int numeroValori = 0;

            try
            {
                numeroValori = (from Panino in lista
                                select Panino.Nome).Count();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return (numeroValori);
        }


        public int getMedia()
        {
            int mediaProdotto = 0;

            try
            {
                var risultato = (from Panino in lista
                                 select Panino.Prezzo).Average();

                mediaProdotto = Convert.ToInt32(risultato);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return (mediaProdotto);
        }


        public bool inserisciPanino(Panino p)
        {
            using (SqlConnection con = new SqlConnection(credenziali))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO Panino (Nome, Descrizione, Prezzo, Vegan) values (@Nome, @Descrizione, @Prezzo, @Vegan);";
                cmd.Parameters.AddWithValue("@Nome", p.Nome);
                cmd.Parameters.AddWithValue("@Descrizione", p.Descrizione);
                cmd.Parameters.AddWithValue("@Prezzo", p.Prezzo);
                cmd.Parameters.AddWithValue("@Vegan", p.Vegan);

                try
                {
                    con.Open();
                    int affRows = cmd.ExecuteNonQuery();
                    if (affRows > 0)
                        Console.WriteLine("ok");
                    else
                        Console.WriteLine("errore");
                    con.Close();
                }
                catch (SqlException sqlex)
                {
                    //Console.WriteLine(sqlex.Message);
                    return (false);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    return (false);
                }
                finally
                {
                    con.Close();
                    Console.WriteLine("connessione chiusa");
                }
            }

            return (true);
        }
    }
}
