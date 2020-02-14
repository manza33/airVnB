using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AirVandB.Models
{
    public class User
    {

        public const int LongueurMinChamp = 2, LongueurMaxChamp = 50;

        /// <summary>
        /// Initialize a people wih=th data
        /// </summary>
        /// <param name="firstname">Firstname of orgnaizer. Its length must be 2 to 50</param>
        /// <param name="lastname">Lastname of orgnaizer. Its length must be 2 to 50</param>
        /// <param name="image">Picture Filename (jpg, jpeg, png or gif)</param>
        /// <param name="lieu">Picture Filename (jpg, jpeg, png or gif)</param>
        public User(int? id, string firstname, string lastname, string image, string lieu)
        {

            PhotoUtil.VerifierChemin(image);
            StringUtil.VerifierLongueur(nameof(firstname), firstname, LongueurMinChamp, LongueurMaxChamp);
            StringUtil.VerifierLongueur(nameof(lastname), lastname, LongueurMinChamp, LongueurMaxChamp);
            StringUtil.VerifierLongueur(nameof(lieu), lieu, LongueurMinChamp, LongueurMaxChamp);

            Id = id;
            Firstname = firstname;
            LastName = lastname;
            Image = image;
            Lieu = lieu;
        }

        public void NouvelInscrit()
        {
            Id = null;
        } 
        
        public void InitWithUniqueId(IEnumerable<User> allUsers)
        {
            Id = allUsers.Max(u => u.Id) + 1;
        }

        public int? Id { get; private set; }
        public string Firstname { get; private set; }
        public string LastName { get; private set; }
        public string Image { get; private set; }
        public string Lieu { get; private set; }        

    }
}
