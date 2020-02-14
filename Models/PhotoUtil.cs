using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AirVandB.Models
{
    public class PhotoUtil
    {
        public static void CheckPath(string image)
        {

            if (image.Length <= 2 || image.Length > 50)
            {
                throw new ArgumentException("L'url de l photo doit faire au moins 2 caractères et maximum 50 caractères");
            }

            if (!IsValidePath(image))
            {
                throw new ArgumentException("Format de fichier non toléré");
            }
        }

        public static bool IsValidePath(string image)
        {
            var extension = Path.GetExtension(image).ToLower();

            return ".jpg;.jpeg;.png;.gif".Split(";").Contains(extension);
        }

        
    }
}
