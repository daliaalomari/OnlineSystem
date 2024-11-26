using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineSystemStore.Domain.DTOs;
using OnlineSystemStore.Domain.HandelRequest;
using OnlineSystemStore.Domain.InterfaceRepository;
using OnlineSystemStore.Domain.Tables;
using OnlineSystemStore.Services.ServiceInterface;
using System.Diagnostics;
using System.Security.Cryptography;


namespace OnlineSystemStore.Services.ServiceImplementation
{
    public class ProductService : IProductService
    {
        private readonly IMainRepository<Product> _repository;
        private readonly IMainRepository<Category> _repositorr;
        private readonly IMapper _mapp;

        public ProductService(IMainRepository<Product> repository, IMapper mapp , IMainRepository<Category> repositorr)
        {
            _repository = repository;
            _mapp = mapp;
            _repositorr = repositorr;
        }
        public async Task<ResponseDto<ProductDto>> GetProductWithCategoryName()
        {
            from product in 
        }
        public int Max() => _repository.Max(x => x.ProductId);

        public async Task<ResponseDto<IEnumerable<ProductDto>>> GetAllProductAsync()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var ProductData = await _repository.GetAllAsync();

            var ProductDto = _mapp.Map<IEnumerable<ProductDto>>(ProductData);

            stopwatch.Stop();

            return ResponseDto<IEnumerable<ProductDto>>.SuccessResponse(data: ProductDto, executionTimeInMilliseconds: stopwatch.ElapsedMilliseconds);
        }

        public async Task<ResponseDto<ProductDto>> GetProductByIdAsync(int id)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var ProductData = await _repository.GetByIdAsync(id);

            if (ProductData == null)
            {
                stopwatch.Stop();

                return ResponseDto<ProductDto>.ErrorResponse(
                    userMessageAr: "لا يوجد بيانات",
                    userMessageEng: "data Not Found",
                    statusCode: 404,
                    executionTime: stopwatch.ElapsedMilliseconds);
            }

            var ProductDto = _mapp.Map<ProductDto>(ProductData);

            stopwatch.Stop();

            return ResponseDto<ProductDto>.SuccessResponse(data: ProductDto, executionTimeInMilliseconds: stopwatch.ElapsedMilliseconds);
        }

        public async Task<ResponseDto<ProductDto>> AddProductAsync(ProductDto model)
        {
            try
            {
                if (model == null)
                {
                    ResponseDto<ProductDto>.ErrorResponse(userMessageAr: "لم يتم ارسال البيانات بشكل صحيح", userMessageEng: "The data is not sent correctly");
                }

                var stopwatch = new Stopwatch();

                stopwatch.Start();

                var newProduct = _mapp.Map<Product>(model);

                newProduct.ProductId = Max() + 1;

                await _repository.AddAsync(newProduct);

                bool isSaveing = await _repository.SaveDataAsync();

                stopwatch.Stop();

                return isSaveing
                    ? ResponseDto<ProductDto>.SuccessResponse(executionTimeInMilliseconds: stopwatch.ElapsedMilliseconds)
                    : ResponseDto<ProductDto>.ErrorResponse(executionTime: stopwatch.ElapsedMilliseconds);


            }
            catch (Exception ex)
            {
                return ResponseDto<ProductDto>.ErrorResponse(exception: ex);
            }

        }

        public async Task<ResponseDto<ProductDto>> UpdateProductAsync(ProductDto model, int id)
        {
            try
            {
                var stopwatch = new Stopwatch();

                stopwatch.Start();

                var existProduct = await _repository.GetByIdAsync(id);

                if (existProduct == null)
                {
                    stopwatch.Stop();

                    return ResponseDto<ProductDto>.ErrorResponse(
                        userMessageAr: "لا يوجد بيانات",
                        userMessageEng: "data Not Found",
                        statusCode: 404,
                        executionTime: stopwatch.ElapsedMilliseconds);
                }

                existProduct.ProductName = model.ProductName;

                _repository.Update(existProduct);

                bool isSaveing = await _repository.SaveDataAsync();

                stopwatch.Stop();

                return isSaveing
                    ? ResponseDto<ProductDto>.SuccessResponse(executionTimeInMilliseconds: stopwatch.ElapsedMilliseconds)
                    : ResponseDto<ProductDto>.ErrorResponse(executionTime: stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                return ResponseDto<ProductDto>.ErrorResponse(exception: ex);
            }
        }

        public async Task<ResponseDto<ProductDto>> DeleteProductAsync(int id)
        {
            try
            {
                var stopwatch = new Stopwatch();

                stopwatch.Start();

                bool isDeleted = await _repository.DeleteAsync(id);

                stopwatch.Stop();

                return isDeleted
                    ? ResponseDto<ProductDto>.SuccessResponse(executionTimeInMilliseconds: stopwatch.ElapsedMilliseconds)
                    : ResponseDto<ProductDto>.ErrorResponse(executionTime: stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                return ResponseDto<ProductDto>.ErrorResponse(exception: ex);
            }
        }

    }
}
