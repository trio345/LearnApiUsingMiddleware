using LearnApiUsingMiddleware.Filters;
using LearnApiUsingMiddleware.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LearnApiUsingMiddleware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _service;

        public ShoppingCartController(IShoppingCartService service)
        {
            _service = service;
        }

        // GET: api/ShoppingCart
        [HttpGet]
        public ActionResult<IEnumerable<ShoppingItem>> Get()
        {
            var items = _service.GetAllItems();
            return Ok(items);
        }

        // GET: api/ShoppingCart/5
        [HttpGet("{id}")]
        public ActionResult<ShoppingItem> GetById(Guid id)
        {
            var items = _service.GetById(id);

            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        // PUT: api/ShoppingCart/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*  [HttpPut("{id}")]
          public async Task<IActionResult> PutShoppingItem(Guid id, ShoppingItem shoppingItem)
          {
              if (id != shoppingItem.Id)
              {
                  return BadRequest();
              }

              _context.Entry(shoppingItem).State = EntityState.Modified;

              try
              {
                  await _context.SaveChangesAsync();
              }
              catch (DbUpdateConcurrencyException)
              {
                  if (!ShoppingItemExists(id))
                  {
                      return NotFound();
                  }
                  else
                  {
                      throw;
                  }
              }

              return NoContent();
          }*/

        // POST: api/ShoppingCart
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostShoppingItem(ShoppingItem shoppingItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _service.Add(shoppingItem);

            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        // DELETE: api/ShoppingCart/5
        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            var shopData = _service.GetById(id);

            if (shopData == null)
            {
                return NotFound();
            }

            _service.Remove(id);
            return Ok();
        }

        /*private bool ShoppingItemExists(Guid id)
        {
            return _context.ShoppingContext.Any(e => e.Id == id);
        }*/
    }
}
