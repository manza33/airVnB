using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirVandB.Models
{
    public class Dictionnaire
    {

        private string[] dico;
        private Random hasard = new Random();

        public Dictionnaire(string source, string separateur = ",")
        {
            dico = source.Split(separateur);
        }

        public string TirerAuSort() => dico[hasard.Next(dico.Length)];



    }
}
