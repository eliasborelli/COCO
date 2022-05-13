using Coco.Core.Entities;
using Coco.Core.Interfaces;
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

        public async Task<Result<IEnumerable<Store>>> GetStoresByDateAsync(DateTime date)
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

        public async Task<Result> SetupAsync()
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
    }
}
