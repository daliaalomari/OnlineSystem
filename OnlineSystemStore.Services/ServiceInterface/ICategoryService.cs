using OnlineSystemStore.Domain.DTOs;
using OnlineSystemStore.Domain.HandelRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSystemStore.Services.ServiceInterface
{
    public interface ICategoryService
    {
        int Max();
        Task<ResponseDto<IEnumerable<CategoryDto>>> GetAllCategoryAsync();
        Task<ResponseDto<CategoryDto>> GetCategoryByIdAsync(int id);
        Task<ResponseDto<CategoryDto>> AddCategoryAsync(CategoryDto model);
        Task<ResponseDto<CategoryDto>> UpdateCategoryAsync(CategoryDto model, int id);
        Task<ResponseDto<CategoryDto>> DeleteCategoryAsync(int id);
    }
}
