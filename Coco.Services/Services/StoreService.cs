using Coco.Core.Entities;
using Coco.Core.Interfaces;
using Coco.Services.Interfaces;

namespace Coco.Services.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Stock> _stockRepository;
        private readonly IRepository<WorkingDays> _workingDaysRepository;
        public StoreService(IStoreRepository storeRepository, IRepository<Category> categoryRepository, IRepository<Product> productRepository, IRepository<Stock> stockRepository, IRepository<WorkingDays> workingDaysRepository)
        {
            _storeRepository = storeRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _workingDaysRepository = workingDaysRepository;
        }
        public IEnumerable<Store> GetStoresByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Store>> GetStoresByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task SetupAsync()
        {
            //Remove all
            var categoriesRemove = await _categoryRepository.GetAsync();
            var productsRemove = await _productRepository.GetAsync();
            var storeRemove = await _storeRepository.GetAsync();
            var workingDaysRemove = await _workingDaysRepository.GetAsync();

            foreach (var store in storeRemove)
            {
                foreach (var stock in store.Stocks)
                {
                    await _stockRepository.DeleteAsync(stock.Id);
                }
                await _storeRepository.DeleteAsync(store.Id);
            }

            foreach (var day in workingDaysRemove)
            {
                await _workingDaysRepository.DeleteAsync(day.Id);
            }

            foreach (var product in productsRemove)
            {
                foreach (var category in product.Categories)
                {
                    await _categoryRepository.DeleteAsync(category.Id);
                }
                await _productRepository.DeleteAsync(product.Id);
            }

            //Categories and Products
            await _categoryRepository.AddRangeAsync(new List<Category>() {
               new Category() { Code = "CAT01", Description = "Sodas", Products = new List<Product>()
                                    {
                                    new Product() { Code = "COD01", Description = "Cold Ice Tea", Amount = 100 },
                                    new Product() { Code = "COD02", Description = "Coffee flavoured milk", Amount = 50},
                                    new Product() { Code = "COD03", Description = "Nuke-Cola", Amount = 70 },
                                    new Product() { Code = "COD04", Description = "Sprute", Amount = 40},
                                    new Product() { Code = "COD05", Description = "Slurm", Amount = 90},
                                    new Product() { Code = "COD06", Description = "Diet Slurm", Amount = 110},
                                    }
               },
               new Category() { Code = "CAT02", Description = "Cleaning", Products = new List<Product>()
                                   {
                                   new Product() { Code = "COD07", Description = "Atlantis detergent", Amount = 100 },
                                   new Product() { Code = "COD08", Description = "Virulanita", Amount = 50 },
                                   new Product() { Code = "COD09", Description = "Sponge, Bob", Amount = 70 },
                                   new Product() { Code = "COD10", Description = "Generic mop", Amount = 40 },
                                   }
               },
               new Category() { Code = "CAT03", Description = "Food", Products = new List<Product>()
                                   {
                                   new Product() { Code = "COD11", Description = "Salsa Cookies", Amount = 100 },
                                   new Product() { Code = "COD12", Description = "Windmill Cookies", Amount = 50 },
                                   new Product() { Code = "COD13", Description = "Garlic-o-bread 2000", Amount = 70 },
                                   new Product() { Code = "COD14", Description = "LACTEL bread", Amount = 40 },
                                   new Product() { Code = "COD15", Description = "Ravioloches x12", Amount = 90 },
                                   new Product() { Code = "COD16", Description = "Ravioloches x48", Amount = 110 },
                                   new Product() { Code = "COD17", Description = "Milanga ganga", Amount = 110 },
                                   new Product() { Code = "COD18", Description = "Milanga ganga napo", Amount = 110 },
                                   }
               },
               new Category() { Code = "CAT04", Description = "Bathroom", Products = new List<Product>()
                                   {
                                   new Product() { Code = "COD19", Description = "Pure steel toilet paper", Amount = 100 },
                                   new Product() { Code = "COD20", Description = "Generic soap", Amount = 50 },
                                   new Product() { Code = "COD21", Description = "PANTONE shampoo", Amount = 70 },
                                   new Product() { Code = "COD22", Description = "Cabbagegate toothpaste", Amount = 40 }
                                   }

               }
            });

            var products = await _productRepository.GetAsync();

            //Stores
            await _storeRepository.AddRangeAsync(new List<Store>() {
                new Store()
                {
                    Name = "COCO Downtown",
                    Address = "La Plata Calle 13",
                    Phone = "2214693454",
                    Stocks = new List<Stock>()
                    {
                        new Stock() { CurrentStock = 5, Product = products.First(x => x.Code == "COD01") },
                        new Stock() { CurrentStock = 7, Product = products.First(x => x.Code == "COD02") },
                        new Stock() { CurrentStock = 3, Product = products.First(x => x.Code == "COD03") },
                        new Stock() { CurrentStock = 2, Product = products.First(x => x.Code == "COD06") },
                        new Stock() { CurrentStock = 4, Product = products.First(x => x.Code == "COD11") },
                        new Stock() { CurrentStock = 7, Product = products.First(x => x.Code == "COD12") },
                        new Stock() { CurrentStock = 2, Product = products.First(x => x.Code == "COD13") },
                        new Stock() { CurrentStock = 15, Product = products.First(x => x.Code == "COD14") },
                        new Stock() { CurrentStock = 12, Product = products.First(x => x.Code == "COD20") },
                        new Stock() { CurrentStock = 11, Product = products.First(x => x.Code == "COD21") },
                        new Stock() { CurrentStock = 10, Product = products.First(x => x.Code == "COD22") },
                    },
                    WorkingDay = new WorkingDays() { Monday = true, Tuesday = true, Wednesday = true, Thursday = true, Friday = true, Saturday = false, Sunday = false, TimeFrom = TimeSpan.Parse("8:00:00"), TimeTo = TimeSpan.Parse("20:00:00") }
                },
                 new Store()
                 {
                     Name = "COCO Bay",
                     Address = "La Plata Calle 2",
                     Phone = "2214693454",
                     Stocks = new List<Stock>() {
                        new Stock() { CurrentStock= 5, Product = products.First(x => x.Code == "COD01") },
                        new Stock() { CurrentStock= 7, Product = products.First(x => x.Code == "COD02") },
                        new Stock() { CurrentStock= 3, Product = products.First(x => x.Code == "COD03") },
                        new Stock() { CurrentStock= 2, Product = products.First(x => x.Code == "COD04") },
                        new Stock() { CurrentStock= 2, Product = products.First(x => x.Code == "COD05") },
                        new Stock() { CurrentStock= 6, Product = products.First(x => x.Code == "COD07") },
                        new Stock() { CurrentStock= 1, Product = products.First(x => x.Code == "COD08") },
                        new Stock() { CurrentStock= 8, Product = products.First(x => x.Code == "COD09") },
                        new Stock() { CurrentStock= 9, Product = products.First(x => x.Code == "COD10") },
                        new Stock() { CurrentStock= 4, Product = products.First(x => x.Code == "COD11") },
                        new Stock() { CurrentStock= 7, Product = products.First(x => x.Code == "COD12") },
                        new Stock() { CurrentStock= 2, Product = products.First(x => x.Code == "COD13") },
                        new Stock() { CurrentStock= 15, Product =products.First(x => x.Code == "COD14") },
                        new Stock() { CurrentStock= 12, Product =products.First(x => x.Code == "COD15") },
                        new Stock() { CurrentStock= 12, Product =products.First(x => x.Code == "COD16") },
                        new Stock() { CurrentStock= 12, Product =products.First(x => x.Code == "COD17") },
                        new Stock() { CurrentStock= 12, Product =products.First(x => x.Code == "COD18") },
                    },
                     WorkingDay = new WorkingDays() { Monday = true, Tuesday = true, Wednesday = true, Thursday = true, Friday = true, Saturday = false, Sunday = true, TimeFrom = TimeSpan.Parse("5:00:00"), TimeTo = TimeSpan.Parse("15:00:00") }
                 },
                 new Store()
                 {
                     Name = "COCO Mail",
                     Address = "La Plata Calle 7",
                     Phone = "2214693454",
                     Stocks = new List<Stock>() {
                        new Stock() { CurrentStock= 5, Product = products.First(x => x.Code == "COD01") },
                        new Stock() { CurrentStock= 7, Product = products.First(x => x.Code == "COD02") },
                        new Stock() { CurrentStock= 3, Product = products.First(x => x.Code == "COD03") },
                        new Stock() { CurrentStock= 2, Product = products.First(x => x.Code == "COD04") },
                        new Stock() { CurrentStock= 2, Product = products.First(x => x.Code == "COD05") },
                        new Stock() { CurrentStock= 2, Product = products.First(x => x.Code == "COD06") },
                        new Stock() { CurrentStock= 12, Product = products.First(x => x.Code == "COD19") },
                        new Stock() { CurrentStock= 12, Product = products.First(x => x.Code == "COD20") },
                        new Stock() { CurrentStock = 11, Product = products.First(x => x.Code == "COD21") },
                        new Stock() { CurrentStock = 10, Product = products.First(x => x.Code == "COD22") },
                    },
                     WorkingDay = new WorkingDays() { Monday = true, Tuesday = true, Wednesday = true, Thursday = true, Friday = false, Saturday = false, Sunday = false, TimeFrom = TimeSpan.Parse("6:00:00"), TimeTo = TimeSpan.Parse("19:00:00") }
                 }
            });
        }
    }
}
