using EF_Practise.Data;
using EF_Practise.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;



namespace EF_Practise.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedResult<Product>>> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string search="")
        {
            try
            {
                var query = from p in _db.Products
                            join c in _db.Categories on p.CategoryId equals c.CategoryId into pc
                            from category in pc.DefaultIfEmpty()
                            orderby p.Name ascending
                            select new Product
                            {
                                ProductId = p.ProductId,
                                Name = p.Name,
                                Price = p.Price,
                                CategoryId = p.CategoryId,
                                CategoryName = category != null ? category.CategoryName : null
                            };
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Name.Contains(search) ||
                                             p.CategoryName.Contains(search) ||
                                             p.Price.ToString().Contains(search)
                                             );
                }
                var totalRecords = await query.CountAsync();
                var products = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

                var paginatedResult = new PaginatedResult<Product>
                {
                    Page = page,
                    PageSize = pageSize,
                    Data = products,
                    Total = totalRecords,
                };

                return Ok(paginatedResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   





    }
}
