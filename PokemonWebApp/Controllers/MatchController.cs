using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokemonWebApp.Controllers
{
    public class MatchController : Controller
    {
        PokemonWCF.Service1 service = new PokemonWCF.Service1();
        // GET: Match
        public ActionResult Index()
        {
            List<PokemonWCF.Match> matchs = service.getAllMatchs().ToList();
            ViewBag.Matchs = matchs;
            ViewBag.Length = matchs.Count();
            return View();
        }

        //DELETE : Match
        [HttpPost]
        public ActionResult DeleteMatch(String Number)
        {
            List<PokemonWCF.Match> matchs = service.getAllMatchs();
            int i = 0;
            for (i = 0; i < matchs.Count(); i++)
            {
                if (matchs[i].Id == Int32.Parse(Number))
                {
                    matchs.Remove(matchs[i]);
                    break;
                }

            }
            return Content("success", "text/plain");
        }

        // GET: Match/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Match/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Match/Create
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

        // GET: Match/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Match/Edit/5
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

        // GET: Match/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Match/Delete/5
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
