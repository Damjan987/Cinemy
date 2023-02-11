using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemy.Core.Models
{
    public class MovieUser : BaseEntity
    {
        public string MovieId { get; set; }
        public string UserId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string GenreId { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
        public bool IsBought { get; set; }
    }
}
