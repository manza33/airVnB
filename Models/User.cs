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

            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Image = image;
            City = lieu;
        }

        public void InitWithUniqueId(IEnumerable<User> allUsers)
        {
            Id = allUsers.Max(u => u.Id) + 1;
        }

        public bool IsValid
        {
            get
            {
                return PhotoUtil.IsValidePath(Image)
                && StringUtil.IsValidLength(nameof(Firstname), Firstname, LongueurMinChamp, LongueurMaxChamp)
                && StringUtil.IsValidLength(nameof(Lastname), Lastname, LongueurMinChamp, LongueurMaxChamp)
                && StringUtil.IsValidLength(nameof(City), City, LongueurMinChamp, LongueurMaxChamp);
            }
        }

        public int? Id { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Image { get; private set; }
        public string City { get; private set; }        

    }
}
