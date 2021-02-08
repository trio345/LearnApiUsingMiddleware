using LearnApiUsingMiddleware.Filters;
using LearnApiUsingMiddleware.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnApiUsingMiddleware.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly List<ShoppingItem> _shoppingCart;

        public ShoppingCartService()
        {
            _shoppingCart = new List<ShoppingItem>()
            {
                 new ShoppingItem() { Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Name = "Orange Juice", Manufacturer="Orange Tree", Price = 5.00M },
                new ShoppingItem() { Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                    Name = "Diary Milk", Manufacturer="Cow", Price = 4.00M },
                new ShoppingItem() { Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                    Name = "Frozen Pizza", Manufacturer="Uncle Mickey", Price = 12.00M }
            };
        }

        public ShoppingItem Add(ShoppingItem shop)
        {
            shop.Id = Guid.NewGuid();
            _shoppingCart.Add(shop);
            return shop;
        }

        public IEnumerable<ShoppingItem> GetAllItems()
        {
            return _shoppingCart;
        }

        public ShoppingItem GetById(Guid id)
        {
            return _shoppingCart.Where(x => x.Id == id)
                                .FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            var existingCart = _shoppingCart.First(x => x.Id == id);
            _shoppingCart.Remove(existingCart);
        }
    }
}
