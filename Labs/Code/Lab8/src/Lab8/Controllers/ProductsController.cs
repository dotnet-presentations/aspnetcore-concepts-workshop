using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab8.Models;

namespace Lab8.Controllers
{
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private static List<Product> _products = new List<Product>(new[] {
            new Product() { Id = 1, Name = "Computer" },
            new Product() { Id = 2, Name = "Radio" },
            new Product() { Id = 3, Name = "Apple" },
        });

        [HttpGet]
        public IEnumerable<Product> Get() => _products;

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _products.SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }
    }
}
