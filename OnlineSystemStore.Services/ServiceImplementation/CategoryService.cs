using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineSystemStore.Domain.DTOs;
using OnlineSystemStore.Domain.HandelRequest;
using OnlineSystemStore.Domain.InterfaceRepository;
using OnlineSystemStore.Domain.Tables;
using OnlineSystemStore.Services.ServiceInterface;
using System.Diagnostics;


namespace OnlineSystemStore.Services.ServiceImplementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IMainRepository<Category> _repository;
        private readonly IMapper _mapp;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IMainRepository<Category> repository, IMapper mapp, ILogger<CategoryService> logger)
        {
            _repository = repository;
            _mapp = mapp;
            _logger = logger;
        }

        public int Max() => _repository.Max(x => x.CategoryId);

        public async Task<ResponseDto<IEnumerable<CategoryDto>>> GetAllCategoryAsync()
        {
            try
            {
                var stopwatch = new Stopwatch();

                stopwatch.Start();

                var CategoryData = await _repository.GetAllAsync();

                var CategoryDto = _mapp.Map<IEnumerable<CategoryDto>>(CategoryData);

                stopwatch.Stop();

                return ResponseDto<IEnumerable<CategoryDto>>.SuccessResponse(data: CategoryDto, executionTimeInMilliseconds: stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ResponseDto<CategoryDto>> GetCategoryByIdAsync(int id)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var CategoryData = await _repository.GetByIdAsync(id);

            if (CategoryData == null)
            {
                stopwatch.Stop();

                return ResponseDto<CategoryDto>.ErrorResponse(
                    userMessageAr: "لا يوجد بيانات",
                    userMessageEng: "data Not Found",
                    statusCode: 404,
                    executionTime: stopwatch.ElapsedMilliseconds);
            }

            var CategoryDto = _mapp.Map<CategoryDto>(CategoryData);

            stopwatch.Stop();

            return ResponseDto<CategoryDto>.SuccessResponse(data: CategoryDto, executionTimeInMilliseconds: stopwatch.ElapsedMilliseconds);
        }

        public async Task<ResponseDto<CategoryDto>> AddCategoryAsync(CategoryDto model)
        {
            try
            {
                if (model == null)
                {
                    ResponseDto<CategoryDto>.ErrorResponse(userMessageAr: "لم يتم ارسال البيانات بشكل صحيح", userMessageEng: "The data is not sent correctly");
                }

                var stopwatch = new Stopwatch();

                stopwatch.Start();

                var newCategory = _mapp.Map<Category>(model);

                newCategory.CategoryId = Max() + 1;

                await _repository.AddAsync(newCategory);

                bool isSaveing = await _repository.SaveDataAsync();

                stopwatch.Stop();

                return isSaveing
                    ? ResponseDto<CategoryDto>.SuccessResponse(executionTimeInMilliseconds: stopwatch.ElapsedMilliseconds)
                    : ResponseDto<CategoryDto>.ErrorResponse(executionTime: stopwatch.ElapsedMilliseconds);


            }
            catch (Exception ex)
            {
                return ResponseDto<CategoryDto>.ErrorResponse(exception: ex);
            }

        }

        public async Task<ResponseDto<CategoryDto>> UpdateCategoryAsync(CategoryDto model, int id)
        {
            try
            {
                var stopwatch = new Stopwatch();

                stopwatch.Start();

                var existCategory = await _repository.GetByIdAsync(id);

                if (existCategory == null)
                {
                    stopwatch.Stop();

                    return ResponseDto<CategoryDto>.ErrorResponse(
                        userMessageAr: "لا يوجد بيانات",
                        userMessageEng: "data Not Found",
                        statusCode: 404,
                        executionTime: stopwatch.ElapsedMilliseconds);
                }

                existCategory.CategoryName = model.CategoryName;

                _repository.Update(existCategory);

                bool isSaveing = await _repository.SaveDataAsync();

                stopwatch.Stop();

                return isSaveing
                    ? ResponseDto<CategoryDto>.SuccessResponse(executionTimeInMilliseconds: stopwatch.ElapsedMilliseconds)
                    : ResponseDto<CategoryDto>.ErrorResponse(executionTime: stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                return ResponseDto<CategoryDto>.ErrorResponse(exception: ex);
            }
        }

        public async Task<ResponseDto<CategoryDto>> DeleteCategoryAsync(int id)
        {
            try
            {
                var stopwatch = new Stopwatch();

                stopwatch.Start();

                bool isDeleted = await _repository.DeleteAsync(id);

                stopwatch.Stop();

                return isDeleted
                    ? ResponseDto<CategoryDto>.SuccessResponse(executionTimeInMilliseconds: stopwatch.ElapsedMilliseconds)
                    : ResponseDto<CategoryDto>.ErrorResponse(executionTime: stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                return ResponseDto<CategoryDto>.ErrorResponse(exception: ex);
            }
        }
    }
}
