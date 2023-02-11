using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemy.Core.Models
{
    public class CartItem : BaseEntity
    {
        public string CartId { get; set; }
        public string MovieId { get; set; }
        public int Quantity { get; set; }
    }
}
