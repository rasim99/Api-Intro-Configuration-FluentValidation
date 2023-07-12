using FirstApiProject.DAL;
using FirstApiProject.Dtos.Category;
using FirstApiProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _appDbContext.Categories.ToList();
            return Ok(categories);
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var existCategory = _appDbContext.Categories.FirstOrDefault(p => p.Id == id);
            if (existCategory == null) return NotFound();
            _appDbContext.Remove(existCategory);
            _appDbContext.SaveChanges();
            return NoContent();
        }
        [HttpPost]
        public IActionResult Create([FromForm]CategoryCreateDto category)
        {
            if (_appDbContext.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower()))
            {
                return BadRequest();
            }
            _appDbContext.Categories.Add(new Category { Name=category.Name ,ImageUrl=category.File.FileName});
            _appDbContext.SaveChanges();
            return StatusCode(201);
        }
        [Route("{id}")]
        [HttpPut]
        public IActionResult Update(int id,CategoryCreateDto category)
        {
            var exisCategory = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (exisCategory == null) return NotFound();
            if (_appDbContext.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower()&& c.Id!=exisCategory.Id))
            {
                return BadRequest();
            }
            exisCategory.Name = category.Name;
            _appDbContext.SaveChanges();
            return NoContent();
        }
    }
}
