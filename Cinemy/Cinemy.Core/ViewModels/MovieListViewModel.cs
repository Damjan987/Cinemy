using Cinemy.Core.Models;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemy.Core.ViewModels
{
    public class MovieListViewModel
    {
        public PagedList<Movie> Movies { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Movie> TopFive { get; set; }
        public PagedList<MovieToDisplay> MoviesToDisplay { get; set; }
    }
}
