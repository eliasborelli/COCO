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
        private readonly IRepository<Voucher> _voucherRepository;
        private readonly IRepository<VoucherConcrete> _voucherConcreteRepository;
        private readonly IRepository<VoucherStrategy> _voucherStrategyRepository;


        public StoreService(IStoreRepository storeRepository, 
            IRepository<Category> categoryRepository, 
            IRepository<Product> productRepository, 
            IRepository<Stock> stockRepository, 
            IRepository<WorkingDays> workingDaysRepository, 
            IRepository<Voucher> voucherRepository,
            IRepository<VoucherConcrete> voucherConcreteRepository,
            IRepository<VoucherStrategy> voucherStrategyRepository)
        {
            _storeRepository = storeRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _workingDaysRepository = workingDaysRepository;
            _voucherRepository = voucherRepository;
            _voucherConcreteRepository = voucherConcreteRepository;
            _voucherStrategyRepository = voucherStrategyRepository;
        }

        public async Task<IEnumerable<Store>> GetStoresByDateAsync(DateTime date)
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

            return response;
        }

        public async Task SetupAsync()
        {
            //Remove all
            var categoriesRemove = await _categoryRepository.GetAsync();
            var productsRemove = await _productRepository.GetAsync();
            var storeRemove = await _storeRepository.GetAsync();
            var workingDaysRemove = await _workingDaysRepository.GetAsync();
            var vouchersRemove = await _voucherRepository.GetAsync();
            var vouchersStrategyRemove = await _voucherStrategyRepository.GetAsync();
            var vouchersConcreteRemove = await _voucherConcreteRepository.GetAsync();

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

            foreach (var voucher in vouchersRemove)
            {
                var strategy = voucher.VoucherConcrete.VoucherStrategy;

                if (strategy is not null)
                    await _voucherStrategyRepository.DeleteAsync(strategy.Id);
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
                     Name = "COCO Mall",
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

            //Vaucher...
            var stores = await _storeRepository.GetAsync();

            await _voucherRepository.AddRangeAsync(new List<Voucher>()
            {
                new Voucher()
                {
                    Code = "COCO1V1F8XOG1MZZ",
                    Store = stores.First(x => x.Name == "COCO Bay"),
                    VoucherConcrete = new VoucherConcrete()
                    {
                        VoucherStrategy = new VoucherStrategy()
                        {
                            CodeStrategy = "STRATEGY01",
                            DiscountStrategy = $"[percentage]% off on [day1] and [day2]",
                            DiscountProductsOrCategories = "on [category] products",
                        },
                        DiscountStrategy = "[20]% off on [Wednesdays] and [Thursdays]",
                        DateFrom = new DateTime(2022, 1, 27),
                        DateTo = new DateTime(2022, 2, 13),
                        DiscountProductsOrCategories = "on [Cleaning] products"
                    },
                },
                new Voucher()
                {
                    Code = "COCOKCUD0Z9LUKBN",
                    Store = stores.First(x => x.Name == "COCO Bay"),
                    VoucherConcrete = new VoucherConcrete()
                    {
                        VoucherStrategy = new VoucherStrategy()
                        {
                            CodeStrategy = "STRATEGY02",
                            DiscountStrategy = $"Pay [payNumberItem] take [takeNumberItem] ",
                            DiscountProductsOrCategories = "on [product] on up to [units] units",
                        },
                        DiscountStrategy = "Pay [2] take [3] ",
                        DiscountProductsOrCategories = "on ['Windmill Cookies'] on up to [6] units",
                        DateFrom = new DateTime(2022, 1, 24),
                        DateTo = new DateTime(2022, 2, 6),
                    },
                },
                new Voucher()
                {
                    Code = "COCOG730CNSG8ZVX",
                    Store = stores.First(x => x.Name == "COCO Mall"),
                    VoucherConcrete = new VoucherConcrete()
                    {
                        VoucherStrategy = new VoucherStrategy()
                        {
                            CodeStrategy = "STRATEGY03",
                            DiscountStrategy = $"[percentage]% off ",
                            DiscountProductsOrCategories = "on [category1] and [category2]",
                        },
                        DiscountStrategy = "[10]% off ",
                        DiscountProductsOrCategories = "on [Bathroom] and [Sodas]",
                        DateFrom = new DateTime(2022, 1, 31),
                        DateTo = new DateTime(2022, 2, 9),
                    },
                },
                new Voucher()
                {
                    Code = "COCO2O1USLC6QR22",
                    Store = stores.First(x => x.Name == "COCO Downtown"),
                    VoucherConcrete = new VoucherConcrete()
                    {
                        VoucherStrategy = new VoucherStrategy()
                        {
                            CodeStrategy = "STRATEGY04",
                            DiscountStrategy = $"[percentage]% off on the second unit (of the same product),",
                            DiscountProductsOrCategories = "on [product1], [product2] and [product3]",
                        },
                        DiscountStrategy = "[30]% off on the second unit (of the same product),",
                        DiscountProductsOrCategories = "on ['Nuka-Cola'], ['Slurm'] and ['Diet Slurm']",
                        DateFrom = new DateTime(2022, 2, 1),
                        DateTo = new DateTime(2022, 2, 28),
                    },
                },
                new Voucher()
                {
                    Code = "COCO0FLEQ287CC05",
                    Store = stores.First(x => x.Name == "COCO Downtown"),
                    VoucherConcrete = new VoucherConcrete()
                    {
                        VoucherStrategy = new VoucherStrategy()
                        {
                            CodeStrategy = "STRATEGY05",
                            DiscountStrategy = $"[percentage]% off on the second unit (of the same product),",
                            DiscountProductsOrCategories = "on [product1], only on [day]",
                        },
                        DiscountStrategy = "[30]% off on the second unit (of the same product),",
                        DiscountProductsOrCategories = "on ['Nuka-Cola'], only on [Mondays]",
                        DateFrom = new DateTime(2022, 2, 1),
                        DateTo = new DateTime(2022, 2, 15),
                    },
                },
            });
        }
    }
}
