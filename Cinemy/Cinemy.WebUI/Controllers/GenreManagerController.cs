using Cinemy.Core.Contracts;
using Cinemy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinemy.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GenreManagerController : Controller
    {
        IRepository<Genre> context;

        public GenreManagerController(IRepository<Genre> genreContext)
        {
            context = genreContext;
        }

        public ActionResult Index()
        {
            List<Genre> genres = context.Collection().ToList();
            return View(genres);
        }

        public ActionResult Create()
        {
            Genre genre = new Genre();
            return View(genre);
        }

        [HttpPost]
        public ActionResult Create(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }
            else
            {
                context.Insert(genre);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Genre genre = context.Find(Id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(genre);
            }
        }

        [HttpPost]
        public ActionResult Edit(Genre genre, string Id)
        {
            Genre genreToEdit = context.Find(Id);
            if (genreToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(genre);
                }
                genreToEdit.Name = genre.Name;
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Genre genreToDelete = context.Find(Id);
            if (genreToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(genreToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Genre genreToDelete = context.Find(Id);
            if (genreToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return View(genreToDelete);
            }
        }
    }
}