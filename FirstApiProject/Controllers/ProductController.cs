using FirstApiProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private List<Product> products = new()
        {
          new Product{Id=1,Name="Product1"},
          new Product{Id=2,Name="Product2"},
          new Product{Id=3,Name="Product3"},
          new Product{Id=4,Name="Product4"}
        };
        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            return Ok(product);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return StatusCode(StatusCodes.Status200OK ,products);
        }
    }
}
