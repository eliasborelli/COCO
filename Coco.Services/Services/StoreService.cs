using Coco.Core.Entities;
using Coco.Core.Interfaces;
using Coco.Core.Models.Response;
using Coco.Infraestructure.Commons;
using Coco.Services.Interfaces;
using Newtonsoft.Json;

namespace Coco.Services.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Voucher> _voucherRepository;

        public StoreService(IStoreRepository storeRepository,
            IRepository<Category> categoryRepository,
            IRepository<Product> productRepository,
            IRepository<Voucher> voucherRepository)
        {
            _storeRepository = storeRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _voucherRepository = voucherRepository;
        }

        public async Task<Result<IEnumerable<Store>>> GetStoresByDate(DateTime date)
        {
            var stores = await _storeRepository.GetAsync();
            var response = new List<Store>();
            foreach (var store in stores)
            {
                var week = store.WorkingDay.GetCurrentWorkingWeek();

                if (week.Contains(date.DayOfWeek.ToString()) && (store.WorkingDay.TimeFrom <= date.TimeOfDay) && (store.WorkingDay.TimeTo >= date.TimeOfDay))
                {
                    response.Add(store);
                }
            }
            return Result.Success<IEnumerable<Store>>(response);
        }

        public async Task<Result> Setup()
        {
            //Remove all
            foreach (var store in await _storeRepository.GetAsync())
            {
                await _storeRepository.DeleteAsync(store.Id);
            }

            foreach (var category in await _categoryRepository.GetAsync())
            {
                await _categoryRepository.DeleteAsync(category.Id);
            }

            foreach (var product in await _productRepository.GetAsync())
            {
                await _productRepository.DeleteAsync(product.Id);
            }

            //Populate Tables
            await _categoryRepository.AddRangeAsync(JsonConvert.DeserializeObject<List<Category>>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"Data\CategoriesAndProducts.json")));
            await _storeRepository.AddRangeAsync(JsonConvert.DeserializeObject<List<Store>>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"Data\StoresAndStocks.json")));
            await _voucherRepository.AddRangeAsync(JsonConvert.DeserializeObject<List<Voucher>>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"Data\Vouchers.json")));

            return Result.Success();
        }

        public async Task<Result<IEnumerable<StoreResponse>>> GetSetupData()
        {
            var stores = await _storeRepository.GetAsync();

            return Result.Success<IEnumerable<StoreResponse>>(stores.Select(x => new StoreResponse()
            {
                Address = x.Address,
                Name = x.Name,
                Phone = x.Phone,
                StoreId = x.Id,
                WorkingDay = new WorkingDaysResponse()
                {
                    WorkingDaysId = x.WorkingDay.Id,
                    Monday = x.WorkingDay.Monday,
                    Tuesday = x.WorkingDay.Tuesday,
                    Wednesday = x.WorkingDay.Wednesday,
                    Thursday = x.WorkingDay.Thursday,
                    Friday = x.WorkingDay.Friday,
                    Saturday = x.WorkingDay.Saturday,
                    Sunday = x.WorkingDay.Sunday,
                    TimeFrom = x.WorkingDay.TimeFrom,
                    TimeTo = x.WorkingDay.TimeTo
                },
                Stocks = x.Stocks.Select(stock => new StockResponse()
                {
                    CurrentStock = stock.CurrentStock,
                    StockId = stock.Id,
                    Product = new ProductResponse()
                    {
                        ProductId = stock.Product.Id,
                        Description = stock.Product.Description,
                        Code = stock.Product.Code,
                        Amount = stock.Product.Amount,
                        Categories = stock.Product.Categories.Select(category => new CategoryResponse()
                        {
                            CategoryId = category.Id,
                            Code = category.Code,
                            Description = category.Description,
                        }).ToList()
                    }
                }).ToList(),
                Vouchers = x.Vouchers.Select(voucher => new VoucherResponse()
                {
                    VoucherId = voucher.Id,
                    Code = voucher.Code,
                    VoucherConcrete = new VoucherConcreteResponse()
                    {
                        VoucherConcreteId = voucher.VoucherConcrete.Id,
                        DiscountProductsOrCategories = voucher.VoucherConcrete.DiscountProductsOrCategories,
                        DiscountStrategy = voucher.VoucherConcrete.DiscountStrategy,
                        DateFrom = voucher.VoucherConcrete.DateFrom,
                        DateTo = voucher.VoucherConcrete.DateTo,
                        VoucherStrategy = new VoucherStrategyResponse()
                        {
                            VoucherStrategyId = voucher.VoucherConcrete.VoucherStrategy.Id,
                            DiscountProductsOrCategories = voucher.VoucherConcrete.VoucherStrategy.DiscountProductsOrCategories,
                            CodeStrategy = voucher.VoucherConcrete.VoucherStrategy.CodeStrategy,
                            DiscountStrategy = voucher.VoucherConcrete.VoucherStrategy.DiscountStrategy,
                        }
                    }
                }).ToList()
            }));
        }
    }
}
