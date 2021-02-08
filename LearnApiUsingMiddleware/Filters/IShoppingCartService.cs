using LearnApiUsingMiddleware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnApiUsingMiddleware.Filters
{
    public interface IShoppingCartService
    {
        public IEnumerable<ShoppingItem> GetAllItems();
        public ShoppingItem Add(ShoppingItem shop);
        public ShoppingItem GetById(Guid id);
        public void Remove(Guid id);

    }
}
