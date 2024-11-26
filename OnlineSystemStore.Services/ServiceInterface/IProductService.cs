using OnlineSystemStore.Domain.DTOs;
using OnlineSystemStore.Domain.HandelRequest;


namespace OnlineSystemStore.Services.ServiceInterface
{
    public interface IProductService
    {
        int Max();
        Task<ResponseDto<IEnumerable<ProductDto>>> GetAllProductAsync();
        Task<ResponseDto<ProductDto>> GetProductByIdAsync(int id);
        Task<ResponseDto<ProductDto>> AddProductAsync(ProductDto model);
        Task<ResponseDto<ProductDto>> UpdateProductAsync(ProductDto model, int id);
        Task<ResponseDto<ProductDto>> DeleteProductAsync(int id);
    }
}
