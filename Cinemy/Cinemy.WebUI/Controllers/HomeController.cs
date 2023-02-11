using Cinemy.Core.Contracts;
using Cinemy.Core.Models;
using Cinemy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Web;
using Microsoft.AspNet.Identity;
using Cinemy.WebUI.Models;
using System.IO;

namespace Cinemy.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IRepository<Movie> context;
        IRepository<Genre> genreContext;
        IRepository<Actor> actorContext;
        IRepository<MovieActor> movieActorContext;
        IRepository<OrderItem> orderItemContext;
        IRepository<Order> orderContext;
        IRepository<MovieRating> movieRatingContext;

        public HomeController(IRepository<Movie> movieContext, IRepository<Genre> gContext, IRepository<Actor> aContext, 
            IRepository<MovieActor> maContext, IRepository<OrderItem> oiContext, IRepository<Order> oContext,
            IRepository<MovieRating> mrContext)
        {
            context              = movieContext;
            genreContext         = gContext;
            actorContext         = aContext;
            movieActorContext    = maContext;
            orderItemContext     = oiContext;
            orderContext         = oContext;
            movieRatingContext   = mrContext;
        }

        // ------------------------------------------------------------
        //                     PRIVATE FUNCTIONS
        // ------------------------------------------------------------

        private List<Movie> filterMoviesByGenre(string genre, List<Movie> movies)
        {
            List<Movie> filteredMovies = new List<Movie>();
            foreach (var movie in movies)
            {
                if (movie.GenreId == genre)
                {
                    filteredMovies.Add(movie);
                }
            }
            return filteredMovies;
        }
        private List<Movie> filterMoviesBySearchName(string searchName, List<Movie> movies)
        {
            List<Movie> filteredMovies = new List<Movie>();
            foreach (var movie in movies)
            {
                if (movie.Name.ToLower().Trim().Contains(searchName.ToLower().Trim()))
                {
                    filteredMovies.Add(movie);
                }
            }
            return filteredMovies;
        }
        private string returnMostBoughtMovie(List<OrderItem> orderItems)
        {
            int c = 0;
            int maxCount = 0;
            string maxMovie = "";
            for (int i = 0; i < orderItems.Count; i++)
            {
                for (int j = 0; j < orderItems.Count; j++)
                {
                    if (orderItems.ElementAt(j).MovieName == orderItems.ElementAt(i).MovieName)
                    {
                        c++;
                    }
                }
                if (maxCount < c)
                {
                    maxCount = c;
                    maxMovie = orderItems.ElementAt(i).MovieName;
                }
                c = 0;
            }
            return maxMovie;
        }
        private Movie returnMostBoughtMovieObject(List<OrderItem> orderItems)
        {
            List<Movie> movies = context.Collection().ToList();
            foreach (var movie in movies)
            {
                if (movie.Name == returnMostBoughtMovie(orderItems)) {
                    return movie;
                }
            }
            return null;
        }
        private List<Movie> returnFiveMostBoughtMovies()
        {
            List<Movie> movies = context.Collection().ToList();
            List<OrderItem> orderItems = orderItemContext.Collection().ToList();
            List<Movie> topFive = new List<Movie>();
            for (int i = 0; i < 5; i++)
            {
                Movie movie = returnMostBoughtMovieObject(orderItems);
                topFive.Add(movie);
                orderItems.RemoveAll(x => x.MovieName == movie.Name);
            }
            return topFive;
        }
        private List<Order> returnLoggedInUserOrders()
        {
            List<Order> orders = orderContext.Collection().ToList();
            List<Order> userOrders = new List<Order>();
            foreach (var order in orders)
            {
                if (order.Email == User.Identity.GetUserName())
                {
                    userOrders.Add(order);
                }
            }
            if (userOrders.Count == 0)
            {
                return orders;
            }
            return userOrders;
        }
        private List<OrderItem> returnLoggedInUserOrderItems()
        {
            List<Order> userOrders = returnLoggedInUserOrders();
            List<OrderItem> orderItems = orderItemContext.Collection().ToList();
            List<OrderItem> userOrderItems = new List<OrderItem>();
            foreach (var order in userOrders)
            {
                foreach (var orderItem in orderItems)
                {
                    if (order.Id == orderItem.OrderId)
                    {
                        userOrderItems.Add(orderItem);
                    }
                }
            }
            return userOrderItems;
        }
        private List<string> returnLoggedInUserMovies()
        {
            List<OrderItem> userOrderItems = returnLoggedInUserOrderItems();
            List<string> userMovies = new List<string>();
            foreach (var orderItem in userOrderItems)
            {
                userMovies.Add(orderItem.MovieName);
            }
            userMovies = userMovies.Distinct().ToList();
            return userMovies;
        }
        private List<MovieToDisplay> returnMoviesToDisplay(List<Movie> movies)
        {
            List<string> userMovies = returnLoggedInUserMovies();
            List<MovieToDisplay> moviesToDisplay = new List<MovieToDisplay>();
            foreach (var movie in movies)
            {
                MovieToDisplay movieToDisplay = new MovieToDisplay();
                foreach (var umovie in userMovies)
                {                    
                    movieToDisplay.Id = movie.Id;
                    movieToDisplay.Name = movie.Name;
                    movieToDisplay.GenreId = movie.GenreId;
                    movieToDisplay.Price = movie.Price;
                    movieToDisplay.Image = movie.Image;
                    movieToDisplay.Rating = movie.Rating;
                    if (movie.Name == umovie)
                    {
                        movieToDisplay.IsBought = true;
                        break;
                    }
                    else
                    {
                        movieToDisplay.IsBought = false;
                    }
                }
                moviesToDisplay.Add(movieToDisplay);
            }
            return moviesToDisplay;
        }
        private MovieRating returnMovieRatingObjectFromString(string rate)
        {
            string[] rateArray = rate.Split('/');
            MovieRating movieRating = new MovieRating();
            movieRating.Rating = int.Parse(rateArray[0]);
            movieRating.User = rateArray[1];
            movieRating.Movie = rateArray[2];
            return movieRating;
        }
        private Movie findMovieById(List<Movie> movies, string id)
        {
            foreach (var movie in movies)
            {
                if (movie.Id == id)
                {
                    return movie;
                }
            }
            return null;
        }

        // ------------------------------------------------------------

        public ActionResult Index(string genre, string searchName, string rate, int? i)
        {   
            List<Movie> movies = context.Collection().ToList();
            List<Genre> genres = genreContext.Collection().ToList();
            List<Movie> topFiveMovies = returnFiveMostBoughtMovies();
            if (genre != null)
            {
                movies = filterMoviesByGenre(genre, movies);
            }
            if (searchName != null)
            {
                movies = filterMoviesBySearchName(searchName, movies);
            }
            List<MovieToDisplay> moviesToDisplay = returnMoviesToDisplay(movies);
            if (rate != null)
            {
                MovieRating movieRating = returnMovieRatingObjectFromString(rate);
                int sum = 0;
                int counter = 0;
                List<MovieRating> movieRatings = movieRatingContext.Collection().ToList();
                bool isAlreadyThere = false;
                MovieRating movieRatingToFind = null;
                foreach (var mr in movieRatings)
                {
                    if (mr.Movie == movieRating.Movie && mr.User == movieRating.User)
                    {
                        isAlreadyThere = true;
                        movieRatingToFind = mr;
                        break;
                    }
                }
                if (!isAlreadyThere)
                {
                    movieRatingContext.Insert(movieRating);
                    movieRatingContext.Commit();
                }
                else
                {
                    movieRatingContext.Find(movieRatingToFind.Id).Rating = movieRating.Rating;
                    movieRatingContext.Commit();
                }
                foreach (var mr in movieRatings)
                {
                    if (mr.Movie == movieRating.Movie)
                    {
                        sum += mr.Rating;
                        counter++;
                    }
                }
                if (counter != 0)
                {
                    int totalRating = sum / counter;
                    context.Find(movieRating.Movie).Rating = totalRating;
                    context.Commit();
                }       
            }
            PagedList<Movie> moviePages = (PagedList<Movie>)movies.ToPagedList(i ?? 1, 9);
            PagedList<MovieToDisplay> moviesToDisplayPages = (PagedList<MovieToDisplay>)moviesToDisplay.ToPagedList(i ?? 1, 9);
            MovieListViewModel viewModel = new MovieListViewModel();
            viewModel.Movies = moviePages;
            viewModel.MoviesToDisplay = moviesToDisplayPages;
            viewModel.Genres = genres;
            viewModel.TopFive = topFiveMovies;
            return View(viewModel);
        }

        public ActionResult UserOrders()
        {
            return View();
        }

        public ActionResult ActiveOrders()
        {
            List<Order> orders = orderContext.Collection().ToList();
            List<Order> activeUserOrders = new List<Order>();
            foreach (var order in orders)
            {
                if (order.Email == User.Identity.GetUserName() && order.OrderStatus != "Order Complete")
                {
                    activeUserOrders.Add(order);
                }
            }
            return View(activeUserOrders);
        }

        public ActionResult PastOrders()
        {
            List<Order> orders = orderContext.Collection().ToList();
            List<Order> pastUserOrders = new List<Order>();
            foreach (var order in orders)
            {
                if (order.Email == User.Identity.GetUserName() && order.OrderStatus == "Order Complete")
                {
                    pastUserOrders.Add(order);
                }
            }
            return View(pastUserOrders);
        }

        public ActionResult Details(string Id)
        {
            MovieActorDetailViewModel viewModel = new MovieActorDetailViewModel();
            Movie movie = context.Find(Id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                viewModel.Movie = movie;
                IEnumerable<MovieActor> movieActors = movieActorContext.Collection();
                List<Actor> actorsInMovie = new List<Actor>();
                foreach (var ma in movieActors)
                {
                    if (ma.movie == movie.Name)
                    {
                        Actor actor = new Actor();
                        actor.Firstname = ma.actor;
                        actorsInMovie.Add(actor);
                    }
                }
                IEnumerable<Actor> enActorsInMovie = actorsInMovie;
                viewModel.Actors = enActorsInMovie;
                return View(viewModel);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}