using Cinemy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemy.Core.ViewModels
{
    public class MovieActorViewModel
    {
        public MovieActor MovieActor { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<string> Actors { get; set; }
    }
}
