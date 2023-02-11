using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemy.Core.Models
{
    public class OrderItem : BaseEntity
    {
        public string OrderId { get; set; }
        public string MovieId { get; set; }
        public string MovieName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }
}
