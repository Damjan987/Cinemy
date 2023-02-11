using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemy.Core.Models
{
    public class MovieActor : BaseEntity
    {
        public string movie { get; set; }
        public string actor { get; set; }
    }
}
