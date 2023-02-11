using Cinemy.Core.Contracts;
using Cinemy.Core.Models;
using Cinemy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinemy.WebUI.Controllers
{
    public class MovieActorController : Controller
    {
        IRepository<MovieActor> context;
        IRepository<Movie> movieContext;
        IRepository<Actor> actorContext;

        public MovieActorController(IRepository<MovieActor> MovieActorContext, IRepository<Movie> MovieContext, IRepository<Actor> ActorContext)
        {
            context = MovieActorContext;
            movieContext = MovieContext;
            actorContext = ActorContext;
        }

        public ActionResult Index()
        {
            List<MovieActor> movieActors = context.Collection().ToList();
            return View(movieActors);
        }

        public ActionResult Create()
        {
            MovieActorViewModel viewModel = new MovieActorViewModel();
            viewModel.MovieActor = new MovieActor();
            viewModel.Movies = movieContext.Collection();
            IEnumerable<Actor> actors = actorContext.Collection();
            List<string> actorNames = new List<string>();
            foreach (var actor in actors)
            {
                actorNames.Add(actor.Firstname + " " + actor.Lastname);
            }
            IEnumerable<string> enActorNames = actorNames;
            viewModel.Actors = enActorNames;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(MovieActor movieActor)
        {
            if (!ModelState.IsValid)
            {
                return View(movieActor);
            }
            else
            {
                context.Insert(movieActor);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            MovieActor movieActor = context.Find(Id);
            if (movieActor == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(movieActor);
            }
        }

        [HttpPost]
        public ActionResult Edit(MovieActor movieActor, string Id)
        {
            MovieActor movieActorToEdit = context.Find(Id);
            if (movieActorToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(movieActor);
                }
                movieActorToEdit.movie = movieActor.movie;
                movieActorToEdit.actor = movieActor.actor;
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            MovieActor movieActorToDelete = context.Find(Id);
            if (movieActorToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(movieActorToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            MovieActor movieActorToDelete = context.Find(Id);
            if (movieActorToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return View(movieActorToDelete);
            }
        }
    }
}