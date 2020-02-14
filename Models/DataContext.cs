using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirVandB.Models
{

    public class DataContext
    {
        private List<Apero> _aperos = new List<Apero>();
        private List<User> _user = new List<User>();

        //SINGLETON + methode DataContext() en private
        // >> Classe n'ayant qu'une instance. Accessible Par DataContext.Insance();
        public static DataContext Instance { get; private set; } = new DataContext();

        private DataContext()
        {
            var hasard = new Random();

            var dicoMots =      new Dictionnaire(dictionnaire);
            var dicoPrenoms =   new Dictionnaire(listFirstnames);
            var dicoNoms =      new Dictionnaire(listLastnames);
            var dicoLieux =     new Dictionnaire(listCities);

            for (int i = 0; i < 20; i++)
            {
                _user.Add(new User(
                    id : i + 1,
                    firstname : dicoPrenoms.TirerAuSort(),
                    lastname : dicoNoms.TirerAuSort(),
                    image : $"128_{hasard.Next(1, 6)}.png",
                    lieu : dicoLieux.TirerAuSort()
                ));
            }

            for (int i = 0; i < 100; i++)
            {
                var mot1 = dicoMots.TirerAuSort();
                var mot2 = dicoMots.TirerAuSort();
                var hasardMaxInvites = hasard.Next(2, 21);

                _aperos.Add(new Apero(
                    organisateur: _user[hasard.Next(_user.Count)],
                    titre: $"{mot1} {mot2}",
                    image: $"apero{hasard.Next(1, 11)}.jpg",
                    description: descTotaleApero.Substring(hasard.Next(descTotaleApero.Length - DescSize), DescSize),
                    altImage: mot1,
                    nbInvitesMax: hasardMaxInvites,
                    participation: 0.1m * (decimal)hasard.Next(20, 300),
                    nbInvites: hasard.Next(0, hasardMaxInvites + 1)
                ));
            }            
        }

        public IEnumerable<Apero> AllAperos => _aperos;
        public IEnumerable<User> AllUtilisateurs => _user;

        public int aperoCount => _aperos.Count;
        public int userCount => _user.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void Save(User user)
        {
            // TODO : Valider les données
            if (user.Id.HasValue)
            {
                #region boucleFor
                //for (int i = 0; i < _utilisateur.Count(); i++)
                //{
                //    if(_utilisateur[i].Id == utilisateur.Id.Value)
                //    {
                //        _utilisateur[indice] = utilisateur;
                //        break;
                //    }
                //}
                #endregion

                int indice = _user.FindIndex(myUser => myUser.Id == user.Id);
                if(indice < 0)
                {
                    throw new ArgumentException("User no longer exist");
                }
                _user[indice] = user;

            }
            else
            {
                // Utilisateur créé : Ajouter a la liste des utilisateurs
                user.InitWithUniqueId(AllUtilisateurs);
                _user.Add(user);
            }
        }    

        private const string dictionnaire = "apéritif,amuse-bouche,amuse-gueule,boisson apéritive, anchois, bande organisée,barbecue,basilic,biscuits,boire,Bon appétit, bretzel, cerises, chips, chorizo, convivial, copains, copieux, desserts, digestif, dînatoire, dîner, été, fêter, fromage, gâteaux, glaçons, grignoter, kir, muscat, olives, pastis, pinard, recette, Ricard, salés, sangria, sauciflard, saucisson, tapas, terrasse, tomates, trinquer, vendredi soir,verres,verrines,vin,whisky, Noël,anniversaire,pâque,célébration,festivité,Pentecôte,Toussaint,cérémonie,festin,réjouissance,kermesse,Saint-Valentin,solennité,soirée,bal,Halloween,bamboula,commémoration,foire,jubilé,Saint-Jean,gala,assomption,carnaval,Nativité,orgie,ducasse,Épiphanie,festival,fiesta,célébrant,célébrer,banquet,danse,fêtard,bringue,confetti,évènement,forain,party,noce,procession,Saint-Jean-Baptiste,bacchanale,teuf,avent,cadeau,divertissement,Pâques,rave,beuverie,centenaire,réveillon,surboum,calendrier,jour férié, festive, solstice, week-end,cocagne,guinguette,nouvel an, boum, liesse, Pourim, Aïd, bombe, fête nationale,honneur,inauguration,moisson,Java,occasion,raout,repas,spectacle,invité,solennelle,Thanksgiving,frairie,soir,Bacchus,célèbre,chouille,nouba,redoute,ribote,ribouldingue,surprise-partie,annuelle,chrétienne,grand-messe,pavoiser,animation,bombance,ivresse,fête des mères,hymne,Kippour,programme,somptueuse,toast,traditionnelle,joie,cavalcade,effervescence,féria,korité,mardi gras, mariage, populaire, ramadan, régal, rite, sauterie, convier, déguisement, friandise, grandiose, jubilaire, lupercales, Samain, saturnales, assemblée, dansant, danser, Fête-Dieu,folklorique,foule,joyeuse,mascarade,Pessa'h,plaisir,réunion,Souccot,veille,allégresse,battre son plein,équinoxe,minuit,nuit,organiser,païenne,panathénées,comité,convivialité,feux d'artifice,fiançailles,préparatifs,sabbat,veillée,guindaille,illumination,Oktoberfest,sacrifice,temps des fêtes,teufeur,vacance,vendange,agape,Aïd el-Kebir,amusement,apparat,Bacchante,bal musette, baloche, bamboche, Beltaine, boume, clubeur, congé, coutume, débauche, délice, détente, distraction, enchantement, épluchette, fête des pères, flonflon, free-party,garden-party,gueuleton,jubilation,pendaison de crémaillère,pessah,pince-fesses,rapta,rassembler,rave-party,réception,récréation,reinage,relâche,ripaille,surboume,surpatte,surprise-party,teufer,veglione,vogue";
        private const string listLastnames = "Martin,Bernard,Thomas,Petit,Robert,Richard,Durand,Dubois,Moreau,Laurent,Simon,Michel,Lefebvre,Leroy,Roux,David,Bertrand,Morel,Fournier,Girard,Bonnet,Dupont,Lambert,Fontaine,Rousseau,Vincent,Muller,Lefevre,Faure,Andre,Mercier,Blanc,Guerin,Boyer,Garnier,Chevalier,Francois,Legrand,Gauthier,Garcia,Perrin,Robin,Clement,Morin,Nicolas,Henry,Roussel,Mathieu,Gautier,Masson,Marchand,Duval,Denis,Dumont,Marie,Lemaire,Noel,Meyer,Dufour,Meunier,Brun,Blanchard,Giraud,Joly,Riviere,Lucas,Brunet,Gaillard,Barbier,Arnaud,Martinez,Gerard,Roche,Renard,Schmitt,Roy,Leroux,Colin,Vidal,Caron,Picard,Roger,Fabre,Aubert,Lemoine,Renaud,Dumas,Lacroix,Olivier,Philippe,Bourgeois,Pierre,Benoit,Rey,Leclerc,Payet,Rolland,Leclercq,Guillaume,Lecomte,Lopez,Jean,Dupuy,Guillot,Hubert,Berger,Carpentier,Sanchez,Dupuis,Moulin,Louis,Deschamps,Huet,Vasseur,Perez,Boucher,Fleury,Royer,Klein,Jacquet,Adam,Paris,Poirier,Marty,Aubry,Guyot,Carre,Charles,Renault,Charpentier,Menard,Maillard,Baron,Bertin,Bailly,Herve,Schneider,Fernandez,Le Gall,Collet,Leger,Bouvier,Julien,Prevost,Millet,Perrot,Daniel,Le Roux,Cousin,Germain,Breton,Besson,Langlois,Remy,Le Goff,Pelletier,Leveque,Perrier,Leblanc,Barre,Lebrun,Marchal,Weber,Mallet,Hamon,Boulanger,Jacob,Monnier,Michaud,Rodriguez,Guichard,Gillet,Etienne,Grondin,Poulain,Tessier,Chevallier,Collin,Chauvin,Da Silva,Bouchet,Gay,Lemaitre,Benard,Marechal,Humbert,Reynaud,Antoine,Hoarau,Perret,Barthelemy,Cordier,Pichon,Lejeune,Gilbert,Lamy,Delaunay,Pasquier,Carlier,Laporte";
        private const string listFirstnames = "Emma,Louise,Jade,Alice,Chloé,Lina,Léa,Manon,Mila,Lola,Camille,Anna,Rose,Inès,Léna,Ambre,Zoé,Julia,Juliette,Sarah,Lucie,Jeanne,Lou,Romane,Eva,Mia,Nina,Agathe,Louna,Charlotte,Inaya,Léonie,Clara,Margaux,Sofia,Lilou,Lana,Clémence,Olivia,Maëlys,Adèle,Luna,Anaïs,Victoria,Margot,Elena,Mathilde,Léana,Capucine,Aya,Giulia,Alicia,Elsa,Louane,Romy,Yasmine,Elise,Nour,Victoire,Lya,Mya,Lily,Lisa,Iris,Assia,Théa,Emy,Noémie,Marie,Lise,Apolline,Gabrielle,Charlie,Lyna,Alix,Valentine,Ines,Pauline,Soline,Faustine,Célia,Mélina,Maya,Roxane,Océane,Elisa,Sara,Héloïse,Laura,Emmy,Zélie,Thaïs,Salomé,Maria,Lila,Candice,Constance,Justine,Livia,Maëlle,Gabriel,Louis,Jules,Lucas,Raphaël,Adam,Léo,Hugo,Ethan,Arthur,Nathan,Liam,Paul,Maël,Sacha,Nolan,Tom,Noah,Gabin,Enzo,Mohamed,Timéo,Théo,Mathis,Aaron,Axel,Victor,Martin,Antoine,Noé,Clément,Baptiste,Valentin,Maxime,Robin,Eden,Rayan,Marius,Yanis,Maxence,Samuel,Evan,Thomas,Léon,Alexandre,Mathéo,Augustin,Tiago,Simon,Eliott,Gaspard,Lenny,Naël,Nino,Isaac,Amir,Lyam,Alexis,Malo,Ibrahim,Imran,Camille,Kaïs,Noa,Lorenzo,Marceau,Noam,Mathys,Oscar,Esteban,Kylian,Ilyes,Adrien,Youssef,Ayoub,Ayden,Soan,Benjamin,Milo,Amine,William,Kenzo,Antonin,Joseph,Diego,Côme,Sohan,Louka,Jean,Wassim,Ismaël,Naïm,Milan,Adem,David,Owen,Noham,Léandre,Ali,Rafael";
        private const string listCities = "Bordeaux,Bordeaux,Bordeaux,Bordeaux,Talence,Pessac,Blanquefort,Arcachon,Villenave,Lormont,Cenon,Le Bouscat,Merignac,Cestas";
        private const string descTotaleApero = "Un apéritif, ou familièrement un apéro, est une boisson, servie avant le repas dans certaines cultures afin d'ouvrir l'appétit. L'apéritif est souvent bu après avoir trinqué, selon la tradition. Sont choisies en général des boissons à base de plantes connues pour leurs vertus apéritives, comme l'anis. L\'apéritif désigne par extension la collation qui peut précéder le repas. Cela englobe alors les amuse-gueules, parmi lesquels les plus classiques sont les pistaches, les chips ou les cacahuètes. Par extension, l\'apéritif s\'applique à tous les aliments (petits gâteaux, fruits découpés, olives, charcuteries, tapas, fromages et autres assortiments variés) qui sont servis en accompagnement de cette boisson, ce qui correspond au concept de tapas et à celui des zakouski russes. Par extension, l\'apéritif désigne en français le moment de convivialité (le lieu de sociabilité) où des personnes se retrouvent pour consommer ces boissons et ces aliments en discutant, sans même qu\'il soit prévu de prendre un repas en commun ensuite.À ce titre, l\'apéritif est aussi un repas léger, où peuvent également être consommés des cocktails, on le qualifie souvent dans ce cas d\'apéritif dinatoire";
        private const int DescSize = 150;


    }
}
