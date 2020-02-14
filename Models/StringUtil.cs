using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirVandB.Models
{
    public class StringUtil
    {
        public static void VerifierLongueur(string champ, string texte, int min, int max)
        {
            if (texte.Length <= min || texte.Length > max)
            {
                throw new ArgumentException(
                    $"La longueur de '{champ}' doit être compris entre {min} et {max} caractères"
                );
            }
        }
    }
}