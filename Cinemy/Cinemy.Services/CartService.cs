using Cinemy.Core.Contracts;
using Cinemy.Core.Models;
using Cinemy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cinemy.Services
{
    public class CartService : ICartService
    {
        IRepository<Movie> movieContext;
        IRepository<Cart> cartContext;

        public const string CartSessionName = "CinemyCart";

        public CartService(IRepository<Movie> MovieContext, IRepository<Cart> CartContext)
        {
            this.movieContext = MovieContext;
            this.cartContext = CartContext;
        }

        private Cart GetCart(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(CartSessionName);
            Cart cart = new Cart();
            if (cookie != null)
            {
                string cartId = cookie.Value;
                if (!string.IsNullOrEmpty(cartId))
                {
                    cart = cartContext.Find(cartId);
                }
                else
                {
                    if (createIfNull)
                    {
                        cart = CreateNewCart(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    cart = CreateNewCart(httpContext);
                }
            }
            return cart;
        }

        private Cart CreateNewCart(HttpContextBase httpContext)
        {
            Cart cart = new Cart();
            cartContext.Insert(cart);
            cartContext.Commit();

            HttpCookie cookie = new HttpCookie(CartSessionName);
            cookie.Value = cart.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return cart;
        }

        public void AddToCart(HttpContextBase httpContext, string movieId)
        {
            Cart cart = GetCart(httpContext, true);
            CartItem item = cart.CartItems.FirstOrDefault(i => i.MovieId == movieId);
            if (item == null)
            {
                item = new CartItem()
                {
                    CartId = cart.Id,
                    MovieId = movieId,
                    Quantity = 1
                };

                cart.CartItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity + 1;
            }

            cartContext.Commit();
        }

        public void RemoveFromCart(HttpContextBase httpContext, string itemId)
        {
            Cart cart = GetCart(httpContext, true);
            CartItem item = cart.CartItems.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                cart.CartItems.Remove(item);
                cartContext.Commit();
            }
        }
    
        public List<CartItemViewModel> GetCartItems(HttpContextBase httpContext)
        {
            Cart cart = GetCart(httpContext, false);

            if (cart != null)
            {
                var results = (from c in cart.CartItems
                               join m in movieContext.Collection()
                               on c.MovieId equals m.Id
                               select new CartItemViewModel()
                               {
                                   Id = c.Id,
                                   Quantity = c.Quantity,
                                   MovieName = m.Name,
                                   Image = m.Image,
                                   Price = m.Price
                               }).ToList();
                return results;
            }
            else
            {
                return new List<CartItemViewModel>();
            }
        }

        public CartSummaryViewModel GetCartSummary(HttpContextBase httpContext)
        {
            Cart cart = GetCart(httpContext, false);
            CartSummaryViewModel model = new CartSummaryViewModel(0, 0);

            if (cart != null)
            {
                int? cartCount = (from item in cart.CartItems
                                  select item.Quantity).Sum();

                decimal? cartTotal = (from item in cart.CartItems
                                      join m in movieContext.Collection()
                                      on item.MovieId equals m.Id
                                      select item.Quantity * m.Price).Sum();

                model.CartCount = cartCount ?? 0;
                model.CartTotal = cartTotal ?? decimal.Zero;

                return model;
            }
            else
            {
                return null;
            }
        }

        public void ClearCart(HttpContextBase httpContext)
        {
            Cart cart = GetCart(httpContext, false);
            cart.CartItems.Clear();
            cartContext.Commit();
        }
    }
}
