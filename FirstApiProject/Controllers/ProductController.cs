using AutoMapper;
using FirstApiProject.DAL;
using FirstApiProject.Dtos.Product;
using FirstApiProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ProductController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var product = _appDbContext.Products
                .Include(p=>p.Category)
                .ThenInclude(p=>p.Products)
                .FirstOrDefault(p => p.Id == id &&!p.IsDeleted);
            if (product == null) return NotFound();
            var returnProduct = _mapper.Map<ProductReturnDto>(product);
            //var returnProduct = new ProductReturnDto
            //{
            //    Name = product.Name,
            //    SalePrice = product.SalePrice,
            //    CostPrice = product.CostPrice,
            //    IsDeleted = product.IsDeleted,
            //    Profit = product.SalePrice-product.CostPrice,
            //    Category =new CategoryInProductReturnDto
            //    {
            //         Name=product.Category.Name,
            //          ImageUrl=product.Category.ImageUrl,
            //           ProductCount=product.Category.Products.Count
            //    }
            //};
            return StatusCode(200, returnProduct);
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult GetAll()
        {
            var query = _appDbContext.Products.Where(p => !p.IsDeleted);
            //var itemList = new List<ProductListItemDto>();
            //foreach(var item in query.ToList())
            //{
            //    ProductListItemDto productListItemDto = new ()
            //    {
            //       Name=item.Name,
            //       SalePrice=item.SalePrice,
            //       CostPrice=item.CostPrice
            //    };
            //    itemList.Add(productListItemDto);
            //}
            var productListDto = new ProductListDto
            {
                TotalCount = query.Count(),
                Items=query.Select(p=>new ProductListItemDto
                {
                    Name=p.Name,
                    SalePrice=p.SalePrice,
                    CostPrice=p.CostPrice
                }).ToList()

            };
             return StatusCode(StatusCodes.Status200OK, productListDto);
        }

        [HttpPost]
        public IActionResult Create(ProductCreateDto product)
        {
            var newProduct = new Product();
            newProduct.Name = product.Name;
            newProduct.SalePrice = product.SalePrice;
            newProduct.CostPrice = product.CostPrice;
            newProduct.CategoryId = product.CategoryId;
            _appDbContext.Products.Add(newProduct);
            _appDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut]
        public IActionResult Update(Product product)
        {
            var existproduct = _appDbContext.Products.FirstOrDefault(p => p.Id == product.Id);
            if (existproduct == null) return NotFound();
            existproduct.Name = product.Name;
            existproduct.SalePrice = product.SalePrice;
            existproduct.CostPrice = product.CostPrice;
            existproduct.CategoryId = product.CategoryId;
            _appDbContext.SaveChanges();
            return NoContent();
        }
        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var existproduct = _appDbContext.Products.FirstOrDefault(p => p.Id ==id);
            if (existproduct == null) return NotFound();
            _appDbContext.Remove(existproduct);
            _appDbContext.SaveChanges();
            return NoContent();

        }

        [Route("{id}")]
        [HttpPatch]
        public IActionResult Update(int id,bool isDelete)
        {
            var existproduct = _appDbContext.Products.FirstOrDefault(p => p.Id == id);
            if (existproduct == null) return NotFound();
            existproduct.IsDeleted = isDelete;
            _appDbContext.SaveChanges();
            return NoContent();

        }
    }
}
