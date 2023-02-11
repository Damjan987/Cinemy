using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemy.Core.Models
{
    public class Movie : BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }

        [DisplayName("Genre")]
        public string GenreId { get; set; }
        public string Description { get; set; }

        [Range(0, 1000)]
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Rating { get; set; }
    }
}
