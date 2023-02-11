using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemy.Core.Models
{
    public class MovieRating : BaseEntity
    {
        public string Movie { get; set; }
        public int Rating { get; set; }
        public string User { get; set; }
    }
}
