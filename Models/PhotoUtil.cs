using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AirVandB.Models
{
    public class PhotoUtil
    {
        public static void VerifierChemin(string image)
        {
            var extension = Path.GetExtension(image).ToLower();


            if (image.Length <= 2 || image.Length > 50)
            {
                throw new ArgumentException("L'url de l photo doit faire au moins 2 caractères et maximum 50 caractères");
            }

            if (!".jpg;.jpeg;.png;.gif".Split(";").Contains(extension))
            {
                throw new ArgumentException("Format de fichier non toléré");
            }
        }
        
    }
}
