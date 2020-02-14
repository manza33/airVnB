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
    public class UsersController : Controller
    {
        //private const int maxPageSize = 50;

        //public static readonly Utilisateur[] ListeUtilisateurs =
        //{
        //    new Utilisateur(prenom:"Titi", nom:"Boubou", image:"128_6.png", lieu:"Bordeaux"),
        //    new Utilisateur(prenom:"Toto", nom:"Bibi", image:"128_15.png", lieu:"Pessac"),
        //    new Utilisateur(prenom:"Lola", nom:"Bidou", image:"128_9.png", lieu:"Talence"),
        //    new Utilisateur(prenom:"Betty", nom:"Boop", image:"128_14.png", lieu:"Blanquefort"),
        //};

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            try
            {
                return Ok(PagingHelper.ProcessGet(
                                Request.Query,
                                DataContext.Instance.AllUtilisateurs,
                                DataContext.Instance.AllUtilisateurs.Count(),
                                Response.Headers
                            ));
            }
            catch (ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id) //ActtionResult<> = Type générique
        {
            var resultat = DataContext.Instance.AllUtilisateurs.FirstOrDefault(utilisateur => utilisateur.Id == id);

            if (resultat == null)
            {
                return NotFound("Utilisateur disparu ou Inexistant!!");
            }
            else
            {
                return resultat;
            }

        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<User> Post([FromBody]User newUser)
        {
            if(newUser == null)
            {
                return BadRequest("No user provided");
            }

            if (newUser.Id.HasValue)
            {
                return BadRequest("Unexpected user id");
            }
            if (!newUser.IsValid)
            {
                return BadRequest("Invalid user data");
            }

            DataContext.Instance.Save(newUser);
            var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{newUser.Id}");
            return Created(location, newUser);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody]User user)
        {
            if (user == null)
            {
                return BadRequest("No user provided");
            }

            if (!user.Id.HasValue || id != user.Id.Value)
            {
                return BadRequest("Inconsistent user id");
            }

            if (!user.IsValid)
            {
                return BadRequest("Invalid user data");
            }

            //user.Id = id;

            DataContext.Instance.Save(user, id);
            var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{user.Id}");
            return (user);
           
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
