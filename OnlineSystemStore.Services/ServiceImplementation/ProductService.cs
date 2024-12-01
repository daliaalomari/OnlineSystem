using AutoMapper;
using OnlineSystemStore.Domain.DTOs;
using OnlineSystemStore.Domain.HandelRequest;
using OnlineSystemStore.Domain.InterfaceRepository;
using OnlineSystemStore.Domain.Tables;
using OnlineSystemStore.Services.ServiceInterface;
using System.Diagnostics;


namespace OnlineSystemStore.Services.ServiceImplementation
{
    public class ProductService : IProductService
    {
        private readonly IMainRepository<Product> _product;
        private readonly IMainRepository<Category> _category;
        private readonly IMapper _mapp;

        public ProductService(IMainRepository<Product> product, IMapper mapp, IMainRepository<Category> category)
        {
            _product = product;
            _mapp = mapp;
            _category = category;
        }

        public int Max() => _product.Max(x => x.ProductId);

        public async Task<IEnumerable<ProductDto>> GetProductWithCategoryNameAsync()
        {
            var proudactData = await _product.GetAllAsync();
            var categorytData = await _category.GetAllAsync();

            var ProductWithCatName = (
                from p in proudactData
                join c in categorytData on p.CategoryRef equals c.CategoryId into cTbl
                from c in cTbl.DefaultIfEmpty()
                select new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    ProductDescription = p.ProductDescription,
                    CategoryRef = p.CategoryRef,
                    CategoryName = c?.CategoryName ?? ""

                }).ToList();

            return ProductWithCatName;
        }


        public async Task<ResponseDto<IEnumerable<ProductDto>>> GetAllProductAsync()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var ProductData = await _product.GetAllAsync();

            var ProductDto = _mapp.Map<IEnumerable<ProductDto>>(ProductData);

            stopwatch.Stop();

            return ResponseDto<IEnumerable<ProductDto>>.SuccessResponse(data: ProductDto, executionTimeInMilliseconds: stopwatch.ElapsedMilliseconds);
        }

        public async Task<ResponseDto<ProductDto>> GetProductByIdAsync(int id)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var ProductData = await _product.GetByIdAsync(id);

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

                await _product.AddAsync(newProduct);

                bool isSaveing = await _product.SaveDataAsync();

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

                var existProduct = await _product.GetByIdAsync(id);

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

                _product.Update(existProduct);

                bool isSaveing = await _product.SaveDataAsync();

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

                bool isDeleted = await _product.DeleteAsync(id);

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
