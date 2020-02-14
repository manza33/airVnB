using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirVandB.Models
{
    public class StringUtil
    {
        public static void CheckLength(string field, string text, int min, int max)
        {
            if (!IsValidLength(field, text, min, max))
            {
                throw new ArgumentException(
                    $"La longueur de '{field}' doit être compris entre {min} et {max} caractères"
                );
            }
        }

        public static bool IsValidLength(string field, string text, int min, int max)
        {
            return text.Length > min && text.Length < max;
        }
    }
}