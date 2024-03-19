using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paninoteca
{
    internal class Panino
    {
        public string Nome {  get; set; }
        public string Descrizione {  get; set; }
        public double Prezzo { get; set; }
        public bool Vegan {  get; set; }

        public Panino() { }

        public Panino(string Nome, string Descrizione, double Prezzo,bool Vegan)
        {
            this.Nome = Nome;
            this.Descrizione= Descrizione;
            this.Prezzo = Prezzo;
            this.Vegan = Vegan;
        }
    }
}
