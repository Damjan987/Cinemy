using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemy.Core.ViewModels
{
    public class CartItemViewModel
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string MovieName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
