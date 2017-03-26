using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokemonWebApp.Controllers
{
    public class PokemonController : Controller
    {
        PokemonWCF.Service1 service = new PokemonWCF.Service1();
                
        // GET: Pokemon
        public ActionResult Index()
        {
            List<PokemonWCF.Pokemon> pokemons = service.getAllPokemons().ToList();
            ViewBag.Pokemons = pokemons;
            ViewBag.Length = pokemons.Count();
            return View();
        }

        // ADD POKEMON
        public ActionResult AddPokemon()
        {
            JObject pokemon = new JObject();
            pokemon["Nom"] = "Unknown";
            pokemon["Vie"] = "100";
            pokemon["Force"] = "100";
            pokemon["Defense"] = "100";
            pokemon["Id"] = "undefined";
            PokemonWCF.Pokemon pokemonCreated = new PokemonWCF.Pokemon(); 
            pokemonCreated.Nom = "Unknown";
            pokemonCreated.Vie = 100;
            pokemonCreated.Force = 100;
            pokemonCreated.Defense = 100;
            
            service.addPokemon(pokemonCreated); //ajout bdd
            return Content(pokemon.ToString(), "application/json"); 
        }

        // MODIFY POKEMON
        [HttpPost]
        public ActionResult ModifyPokemon(String Name, String NewName,String Vie,String Force, String Defense)
        {
            List<PokemonWCF.Pokemon> pokemons = service.getAllPokemons(); //faire les changement dans la base de données
            int i = 0;
            for (i = 0; i < pokemons.Count(); i++)
            {
                if (pokemons[i].Nom == Name)
                {
                    pokemons[i].Nom = NewName;
                    if (Vie != "") pokemons[i].Vie = Int32.Parse(Vie);
                    if (Defense != "") pokemons[i].Defense = Int32.Parse(Defense);
                    if (Force != "") pokemons[i].Force = Int32.Parse(Force);
                    break;
                }
            }
            if(i == pokemons.Count()) 
            {
                PokemonWCF.Pokemon newpoke = new PokemonWCF.Pokemon();
                newpoke.Nom = NewName;
                if (Vie != "") newpoke.Vie = Int32.Parse(Vie);
                if (Defense != "") newpoke.Defense = Int32.Parse(Defense);
                if (Force != "") newpoke.Force = Int32.Parse(Force);
                pokemons.Add(newpoke);
           }
                
           return Content(Name, "text/plain");
        }

        [HttpPost]
        public ActionResult DeletePokemon(String Name)
        {
            List<PokemonWCF.Pokemon> pokemons = service.getAllPokemons();
            int i = 0;
            for(i=0; i < pokemons.Count(); i++)
            {
                if(pokemons[i].Nom == Name)
                {
                    pokemons.Remove(pokemons[i]);
                    break;
                }
                
            }
            return Content("success", "text/plain");
        }

        // GET: Pokemon/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pokemon/Create
        public ActionResult Create(string name, int vie, int force)
        {
            return View();
        }

        // POST: Pokemon/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pokemon/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pokemon/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pokemon/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pokemon/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
