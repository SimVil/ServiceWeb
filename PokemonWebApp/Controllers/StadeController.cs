using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokemonWebApp.Controllers
{
    public class StadeController : Controller
    {
        PokemonWCF.Service1 service = new PokemonWCF.Service1();

        // GET: Stade
        public ActionResult Index()
        {
            List<PokemonWCF.Stade> stades = service.getAllStades();
            ViewBag.Stades = stades;
            ViewBag.Length = stades.Count();
            return View();
            
        }

        // Add Stade
        public ActionResult AddStade()
        {
            JObject stade = new JObject();
            stade["NbPlaces"] = "10000";
            
            PokemonWCF.Stade stadeCreated = new PokemonWCF.Stade();
            stadeCreated.NbPlaces = 10000;
            
            service.addStade(stadeCreated); //ajout bdd
            return Content(stade.ToString(), "application/json"); 
        }

        [HttpPost]
        public ActionResult DeleteStade(String Name)
        {
            List<PokemonWCF.Stade> stades = service.getAllStades();
            int i = 0;
            for (i = 0; i < stades.Count(); i++)
            {
                if (stades[i].Nom == Name)
                {
                    stades.Remove(stades[i]);
                    break;
                }

            }
            return Content("success", "text/plain");
        }

        [HttpPost]
        public ActionResult ModifyStade(String Name, String NewName, String NbPlaces)
        {
            List<PokemonWCF.Stade> stades = service.getAllStades(); //faire les changement dans la base de données
            int i = 0;
            for (i = 0; i < stades.Count(); i++)
            {
                if (stades[i].Nom == Name)
                {
                    stades[i].Nom = NewName;
                    if (NbPlaces != "") stades[i].NbPlaces = Int32.Parse(NbPlaces);
                    
                }
            }
            if (i == stades.Count())
            {
                PokemonWCF.Stade newstade = new PokemonWCF.Stade();
                newstade.Nom = NewName;
                if (NbPlaces!= "") newstade.NbPlaces = Int32.Parse(NbPlaces);
                
                stades.Add(newstade);
            }

            return Content(Name, "text/plain");
        }

        // GET: Stade/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Stade/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stade/Create
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

        // GET: Stade/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Stade/Edit/5
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

        // GET: Stade/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Stade/Delete/5
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
