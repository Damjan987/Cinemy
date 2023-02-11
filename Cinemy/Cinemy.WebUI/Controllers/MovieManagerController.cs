using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinemy.Core.Contracts;
using Cinemy.Core.Models;
using Cinemy.Core.ViewModels;
using Cinemy.DataAccess.InMemory;

namespace Cinemy.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MovieManagerController : Controller
    {
        IRepository<Movie> context;
        IRepository<Genre> genreContext;

        public MovieManagerController(IRepository<Movie> movieContext, IRepository<Genre> gContext)
        {
            context = movieContext;
            genreContext = gContext;
        }

        public ActionResult Index()
        {
            List<Movie> movies = context.Collection().ToList();
            return View(movies);
        }

        public ActionResult Create()
        {
            GenreMovieViewModel viewModel = new GenreMovieViewModel();
            viewModel.Movie = new Movie();
            viewModel.Genres = genreContext.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Movie movie, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            else
            {
                if (file != null)
                {
                    movie.Image = movie.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//MovieImages//") + movie.Image);
                }
                movie.Rating = 0;
                context.Insert(movie);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Movie movie = context.Find(Id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                GenreMovieViewModel viewModel = new GenreMovieViewModel();
                viewModel.Movie = movie;
                viewModel.Genres = genreContext.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Movie movie, string Id, HttpPostedFileBase file)
        {
            Movie movieToEdit = context.Find(Id);
            if (movieToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(movie);
                }
                if (file != null)
                {
                    movieToEdit.Image = movie.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//MovieImages//") + movieToEdit.Image);
                }
                movieToEdit.Name = movie.Name;
                movieToEdit.GenreId = movie.GenreId;
                movieToEdit.Description = movie.Description;
                movieToEdit.Price = movie.Price;   
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Movie movieToDelete = context.Find(Id);
            if (movieToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(movieToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Movie movieToDelete = context.Find(Id);
            if (movieToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return View(movieToDelete);
            }
        }
    }
}