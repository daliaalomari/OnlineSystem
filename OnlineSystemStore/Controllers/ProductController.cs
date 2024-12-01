using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineSystemStore.Domain.DTOs;
using OnlineSystemStore.Services.ServiceInterface;

namespace OnlineSystemStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductService;
        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }


        [HttpGet("Max")]
        public IActionResult GetProductsMaxNumber()
        {
            int max = _ProductService.Max() + 1;
            return Ok(max);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAllProducts()
        {
            var ProductsData = await _ProductService.GetAllProductAsync();
            return Ok(ProductsData);
        }


        [HttpPost("GetDataTable")]
        public async Task<IActionResult> GetProductsDataTalbe(string Search, string CoulmnName, bool isAsending, int skip, int take)
        {
            var ProductsData = await _ProductService.GetProductWithCategoryNameAsync();

            var data = ProductsData.AsQueryable();

            if (!string.IsNullOrEmpty(Search))
            {
                data = data.Where(x =>
                x.ProductName.Equals(Search, StringComparison.CurrentCultureIgnoreCase) ||
                x.Price.ToString().Equals(Search, StringComparison.CurrentCultureIgnoreCase) ||
                x.ProductId.ToString().Equals(Search, StringComparison.CurrentCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(CoulmnName))
            {
                switch (CoulmnName)
                {
                    case "ProductId": data = isAsending ? data.OrderBy(x => x.ProductId) : data.OrderByDescending(x => x.ProductId); break;
                    case "ProductName": data = isAsending ? data.OrderBy(x => x.ProductName) : data.OrderByDescending(x => x.ProductName); break;
                    default: data = data.OrderBy(x => x.ProductId); break;

                }
            }

            var Resulte = data.Take(take).Skip(skip).ToList();

            return Ok(Resulte);

        }

        
        [HttpGet("GetByIdAsync/{id}")]
        public async Task<IActionResult> GetProductsByIdAsync(int id)
        {
            var ProductsData = await _ProductService.GetProductByIdAsync(id);

            if (ProductsData == null)
            {
                return NotFound();
            }

            return Ok(ProductsData);
        }

        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddProductsAsync([FromBody] ProductDto Product)
        {
            await _ProductService.AddProductAsync(Product);
            return Ok();
        }


        [HttpPut("Update/{id}")]
        public IActionResult UpdateProduct([FromBody] ProductDto Product, int id)
        {
            _ProductService.UpdateProductAsync(Product, id);

            return Ok();
        }


        [HttpDelete("DeleteAsync/{id}")]
        public async Task<IActionResult> DeleteProductsAsync(int id)
        {
            await _ProductService.DeleteProductAsync(id);

            return Ok();
        }
    }
}
