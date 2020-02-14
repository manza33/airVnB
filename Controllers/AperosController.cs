using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirVandB.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirVandB.Controllers
{
    [Route("api/[controller]")]
    public class AperosController : Controller
    {
        //private const int maxPageSize = 50;

        //private static Apero[] listeDesAperos =
        //{
        //    new Apero(UtilisateursController.ListeUtilisateurs[0], "Bièraubeurre chez Potter", "autumn-3679751_1920.jpg", "Miam mam, ça sent bon la magie!", "Bièraubeurre", 10, 20.50M),
        //    new Apero(UtilisateursController.ListeUtilisateurs[3], "Du vin rose chez Lulu", "pink-wine-1964457_1920.jpg", "Ca sent l'amouuuuuuuuuuuuuur!", "Vin rose", 5, 15, 5),
        //    new Apero(UtilisateursController.ListeUtilisateurs[1], "Une biere chez Ragnar", "beer-199650_1920.jpg", "Ca sent le fauve oO!", "Bière", 10, 24.99M),
        //    new Apero(UtilisateursController.ListeUtilisateurs[2], "Un Cidre chez Gwen", "apple-cider-570106_1920.jpg", "Ca sent bon la Bretagne!!", "Cidre", 100, 50),
        //    new Apero(UtilisateursController.ListeUtilisateurs[0], "Du vin chez Michel", "wine-791133_1920.jpg", "Ca sent bon le Médoc!!", "Vin", 10, 2, 10)
        //};

        // GET: api/<controller>

        [HttpGet]
        public ActionResult<IEnumerable<Apero>> Get()
        {
            
            var resultat = DataContext.Instance.AllAperos;
            var totalApero = DataContext.Instance.aperoCount;

            if (Request.Query.ContainsKey("user"))
            {
                var userInput = Request.Query["user"];

                if (userInput.Count != 1 || !int.TryParse(userInput, out int userId))
                {
                    throw new ArgumentOutOfRangeException("user", "Bad user parameter number");
                }
                else
                {
                    resultat = resultat.Where(apero => apero.OrganizerId == userId);
                    totalApero = resultat.Count();
                }               

            }
            
            try
            {
                return Ok(  PagingHelper.ProcessGet(
                            Request.Query,
                            resultat,
                            totalApero,
                            Response.Headers
                ));
            }
            catch ( ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Apero> Get(int id) //ActtionResult<> = Type générique
        {
            var resultat = DataContext.Instance.AllAperos.FirstOrDefault(apero => apero.Id == id);

            if (resultat == null)
            {
                return NotFound("Apéro disparu ou Inexistant!!");
            }
            else
            {
                return resultat;
            }

        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
