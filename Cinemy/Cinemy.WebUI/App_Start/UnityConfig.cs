using Cinemy.Core.Contracts;
using Cinemy.Core.Models;
using Cinemy.DataAccess.InMemory;
using Cinemy.DataAccess.SQL;
using Cinemy.Services;
using System;

using Unity;

namespace Cinemy.WebUI
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IRepository<Movie>, DataAccess.SQL.SQLRepository<Movie>>();
            container.RegisterType<IRepository<Actor>, DataAccess.SQL.SQLRepository<Actor>>();
            container.RegisterType<IRepository<Genre>, DataAccess.SQL.SQLRepository<Genre>>();
            container.RegisterType<IRepository<Cart>, DataAccess.SQL.SQLRepository<Cart>>();
            container.RegisterType<IRepository<CartItem>, DataAccess.SQL.SQLRepository<CartItem>>();
            container.RegisterType<IRepository<Customer>, DataAccess.SQL.SQLRepository<Customer>>();
            container.RegisterType<IRepository<Order>, DataAccess.SQL.SQLRepository<Order>>();
            container.RegisterType<IRepository<MovieActor>, DataAccess.SQL.SQLRepository<MovieActor>>();
            container.RegisterType<IRepository<OrderItem>, DataAccess.SQL.SQLRepository<OrderItem>>();
            container.RegisterType<IRepository<MovieRating>, DataAccess.SQL.SQLRepository<MovieRating>>();
            container.RegisterType<ICartService, CartService>();
            container.RegisterType<IOrderService, OrderService>();
        }
    }
}