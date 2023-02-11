using Cinemy.Core.Contracts;
using Cinemy.Core.Models;
using Cinemy.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinemy.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ActorManagerController : Controller
    {
        IRepository<Actor> context;

        public ActorManagerController(IRepository<Actor> actorContext)
        {
            context = actorContext;
        }

        public ActionResult Index()
        {
            List<Actor> actors = context.Collection().ToList();
            return View(actors);
        }

        public ActionResult Create()
        {
            Actor actor = new Actor();
            return View(actor);
        }

        [HttpPost]
        public ActionResult Create(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            else
            {
                context.Insert(actor);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Actor actor = context.Find(Id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(actor);
            }
        }

        [HttpPost]
        public ActionResult Edit(Actor actor, string Id)
        {
            Actor actorToEdit = context.Find(Id);
            if (actorToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(actor);
                }
                actorToEdit.Firstname = actor.Firstname;
                actorToEdit.Lastname = actor.Lastname;
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Actor actorToDelete = context.Find(Id);
            if (actorToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(actorToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Actor actorToDelete = context.Find(Id);
            if (actorToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return View(actorToDelete);
            }
        }
    }
}