using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AirVandB.Models
{
    public class Apero
    {

        public const int LongueurMinChamp = 2, LongueurMaxChamp = 50;

        /// <summary>
        /// Initialize an Apero with common data
        /// </summary>
        /// <param name="organisateur">Organisateur
        /// <param name="titre">Title for the Apero. Its length must be 2 to 50</param>
        /// <param name="image">Picture Filename (jpg, jpeg, png or gif)</param>
        /// <param name="description">Description for the Apero. Its length must be 2 to 50</param>
        /// <param name="altImage">alt for the image of Apero. Its length must be 2 to 50</param>
        /// <param name="nbInvites">Number of guests. Must be less than nbInvitesMax and more than 0
        /// <param name="nbInvitesMax">Number Max of guests. Must be more than 0
        /// <param name="participation">Price of apero
        public Apero(
            User organisateur,
            string titre,
            string image,
            string description,
            string altImage,
            int nbInvitesMax,
            decimal participation,
            int nbInvites = 0)
        {

            if (nbInvitesMax < 1 || nbInvitesMax > 100)
            {
                throw new ArgumentException("Le nombre d'invités max doit être compris entre 1 et 100");
            }

            if (nbInvites < 0 || nbInvites > nbInvitesMax)
            {
                throw new ArgumentException($"Le nombre d'invités doit être compris entre 0 et le nombre Max d'invités {nbInvitesMax}");
            }

            if (participation <= 0)
            {
                throw new ArgumentException("La participation doit être positive ou eqal à 0");
            }

            PhotoUtil.VerifierChemin(image);
            StringUtil.VerifierLongueur(nameof(titre), titre, LongueurMinChamp, LongueurMaxChamp);
            StringUtil.VerifierLongueur(nameof(altImage), altImage, LongueurMinChamp, LongueurMaxChamp);
            StringUtil.VerifierLongueur(nameof(description), description, LongueurMinChamp, 250);

            if (organisateur == null)
            {
                throw new ArgumentException("Un évènement sans organisateur ne peut existé");
            }

            Id = ++nextId; // Incrémentation
            Titre = titre;
            Image = image;
            Description = description;
            AltImage = altImage;
            NbInvites = nbInvites;
            NbInvitesMax = nbInvitesMax;
            Participation = participation;
            Organisateur = organisateur;
        }

        public int Id { get; private set; }
        public string Titre { get; private set; }
        public string Image { get; private set; }
        public string Description { get; private set; }
        public string AltImage { get; private set; }
        public int NbInvites { get; private set; }
        public int NbInvitesMax { get; private set; }
        public decimal  Participation{ get; private set; }

        public int OrganisateurId => Organisateur.Id.Value;
        public string OrganisateurNom => $"{Organisateur.Firstname} {Organisateur.LastName}";
        public string OrganisateurImage => Organisateur.Image;

        [JsonIgnore]
        public User Organisateur { get; private set; }

        private static int nextId = 0;

        public bool EstComplet // ou => NbInvites == NbInvitesMax;
        { 
            get {
                return NbInvites == NbInvitesMax;
            }
        }

        
    }
}
