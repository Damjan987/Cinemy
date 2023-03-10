using Cinemy.Core.Models;
using Cinemy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemy.Core.Contracts
{
    public interface IOrderService
    {
        void CreateOrder(Order baseOrder, List<CartItemViewModel> cartItems);
        List<Order> GetOrderList();
        Order GetOrder(string Id);
        void UpdateOrder(Order updatedOrder);
    }
}
