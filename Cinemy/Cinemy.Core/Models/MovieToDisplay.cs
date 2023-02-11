using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemy.Core.Models
{
    public class MovieToDisplay : BaseEntity
    {
        public string Name { get; set; }
        public string GenreId { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public bool IsBought { get; set; }
        public float Rating { get; set; }
    }
}
