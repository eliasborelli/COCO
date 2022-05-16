using Coco.Core.Entities;
using Coco.Core.Interfaces;
using Coco.Core.Models.Request;
using Coco.Core.Models.Response;
using Coco.Infraestructure.Commons;
using Coco.Services.Interfaces;
using Coco.Services.Services.Voucher;

namespace Coco.Services.Services
{
    public class VirtualCartService : IVirtualCartService
    {
        private readonly IRepository<VirtualCart> _virtualCartRepository;
        private readonly IStockService _stockService;
        private readonly IVoucherService _voucherService;
        public VirtualCartService(IStockService stockService, IVoucherService voucherService, IRepository<VirtualCart> virtualCartRepository)
        {
            _stockService = stockService;
            _voucherService = voucherService;
            _virtualCartRepository = virtualCartRepository;
        }

        public async Task<Result<VirtualCartResponse>> ApplyVourcher(VoucherFilter voucherFilter)
        {
            var voucher = await _voucherService.GetVoucherByCode(voucherFilter.VoucherCode);

            if (voucher is null || voucher.Succeeded is false)
                return Result.Failed<VirtualCartResponse>("Voucher code not found");

            var virtualCart = await _virtualCartRepository.GetFirstAsync(x => x.Code == voucherFilter.VirtualCartCode);

            if (virtualCart is null)
                return Result.Failed<VirtualCartResponse>("The virtual Cart Code is not exists");

            var isVoucherStore = virtualCart.Store.Vouchers.FirstOrDefault(voucher => voucher.Code == voucherFilter.VoucherCode);

            if (isVoucherStore is null)
                return Result.Failed<VirtualCartResponse>($"The voucher belongs to another Store and not to {virtualCart.Store.Name}");

            var response = new VoucherStrategyContext().Execute(voucher.Value.VoucherConcrete, virtualCart);

            return Result.Success<VirtualCartResponse>(new VirtualCartResponse()
            {
                Code = response.Code,
                DiscountAmount = response.DiscountAmount,
                StoreName = response.Store.Name,
                VirtualProductCarts = response.VirtualProductCarts.Select(x => new VirtualProductCartResponse()
                {
                    DiscountAmount = x.DiscountAmount,
                    Quantity = x.Quantity,
                    Product = new ProductResponse()
                    {
                        Description = x.Product.Description,
                        Code = x.Product.Code,
                        Amount = x.Product.Amount,
                        ProductId = x.Product.Id,
                        Categories = x.Product.Categories.Select(x => new CategoryResponse()
                        {
                            Description = x.Description,
                            Code = x.Code,
                            CategoryId = x.Id
                        })
                    }
                }),
                TotalAmount = response.TotalAmount
            });

        }

        public async Task<Result<VirtualCartResponse>> Create(VirtualCartFilter virtualCartFilter)
        {
            VirtualCart virtualCart = new VirtualCart();
            virtualCart.VirtualProductCarts = new List<VirtualProductCart>();

            var store = await _stockService.GetStockByStore(virtualCartFilter.Store);

            if (store is null || store.Succeeded is false)
                return Result.Failed<VirtualCartResponse>("Error");

            virtualCart.Code = Path.GetRandomFileName().Replace(".", "").Substring(0, 8);
            virtualCart.StoreId = store.Value.Id;

            foreach (var prod in virtualCartFilter.Products)
            {
                var exists = store.Value.Stocks.FirstOrDefault(x => x.Product.Code == prod.Code);

                if (exists is null)
                    return Result.Failed<VirtualCartResponse>($"The {prod.Code} is not exists in {virtualCartFilter.Store}");

                if ((exists.CurrentStock - prod.Quantity) < 0)
                    return Result.Failed<VirtualCartResponse>($"The {prod.Code} has not stock available '{exists.CurrentStock}' in {virtualCartFilter.Store}");

                var existsInvirtualCart = virtualCart.VirtualProductCarts.FirstOrDefault(x => x.Product.Code == prod.Code);

                if (existsInvirtualCart is not null)
                    return Result.Failed<VirtualCartResponse>($"The {prod.Code} is repeated in Virtual Cart");

                exists.CurrentStock -= prod.Quantity;

                virtualCart.VirtualProductCarts.Add(new VirtualProductCart()
                {
                    Product = exists.Product,
                    Quantity = prod.Quantity,
                });
                await _stockService.Update(exists);
            }

            virtualCart.TotalAmount = virtualCart.VirtualProductCarts.Sum(x => x.Product.Amount * x.Quantity);

            await _virtualCartRepository.AddAsync(virtualCart);

            return Result.Success<VirtualCartResponse>(new VirtualCartResponse()
            {
                Code = virtualCart.Code,
                StoreName = virtualCart.Store.Name,
                VirtualProductCarts = virtualCart.VirtualProductCarts.Select(x => new VirtualProductCartResponse()
                {
                    DiscountAmount = x.DiscountAmount,
                    Quantity = x.Quantity,
                    Product = new ProductResponse()
                    {
                        Description = x.Product.Description,
                        Code = x.Product.Code,
                        Amount = x.Product.Amount,
                        ProductId = x.Product.Id,
                        Categories = x.Product.Categories.Select(x => new CategoryResponse()
                        {
                            Description = x.Description,
                            Code = x.Code,
                            CategoryId = x.Id
                        })
                    }
                }),
                DiscountAmount = virtualCart.DiscountAmount,
                TotalAmount = virtualCart.TotalAmount,
            });
        }

        public async Task<Result<IEnumerable<VirtualCartResponse>>> GetAll()
        {
            var virtualCarts = await _virtualCartRepository.GetAsync();

            return Result.Success<IEnumerable<VirtualCartResponse>>(virtualCarts.Select(x => new VirtualCartResponse()
            {
                Code = x.Code,
                StoreName = x.Store.Name,
                TotalAmount = x.TotalAmount,
                DiscountAmount = x.DiscountAmount,
                VirtualProductCarts = x.VirtualProductCarts.Select(j => new VirtualProductCartResponse()
                {
                    DiscountAmount = j.DiscountAmount,
                    Quantity = j.Quantity,
                    Product = new ProductResponse()
                    {
                        Description = j.Product.Description,
                        Code = j.Product.Code,
                        Amount = j.Product.Amount,
                        ProductId = j.Product.Id,
                        Categories = j.Product.Categories.Select(x => new CategoryResponse()
                        {
                            Description = x.Description,
                            Code = x.Code,
                            CategoryId = x.Id
                        })
                    }
                }),
            }).ToList());
        }

        public async Task<Result> Remove(VirtualCartRemoveFilter virtualCartRemoveFilter)
        {
            var virtualCart = await _virtualCartRepository.GetFirstAsync(x => x.Code == virtualCartRemoveFilter.VirtualCartCode);

            if (virtualCart is null)
                return Result.Failed($"The Virtual Cart Code {virtualCartRemoveFilter.VirtualCartCode} not exists in db");

            _virtualCartRepository.Delete(virtualCart);
            return Result.Success();
        }

        public async Task<Result<VirtualCartResponse>> AddItems(VirtualCartEditFilter virtualCartEditFilter)
        {
            var virtualCart = await _virtualCartRepository.GetFirstAsync(x => x.Code == virtualCartEditFilter.VirtualCartCode);

            if (virtualCart is null)
                return Result.Failed<VirtualCartResponse>("The virtual Cart Code is not exists");

            Store store = virtualCart.Store;

            foreach (var prod in virtualCartEditFilter.Products)
            {
                var productDb = virtualCart.VirtualProductCarts.FirstOrDefault(virtualProduct => virtualProduct.Product.Code == prod.Code);

                var exists = store.Stocks.FirstOrDefault(x => x.Product.Code == prod.Code);

                if (exists is null)
                    return Result.Failed<VirtualCartResponse>($"The {prod.Code} is not exists in {virtualCart.Store.Name}");

                if (productDb is not null)
                {
                    exists.CurrentStock += productDb.Quantity;
                    productDb.Quantity = prod.Quantity;
                }

                if ((exists.CurrentStock - prod.Quantity) < 0)
                    return Result.Failed<VirtualCartResponse>($"The {prod.Code} has not stock available '{exists.CurrentStock}' in {virtualCart.Store.Name}");

                exists.CurrentStock -= prod.Quantity;
                if (productDb is null)
                {
                    virtualCart.VirtualProductCarts.Add(new VirtualProductCart()
                    {
                        Product = exists.Product,
                        Quantity = prod.Quantity,
                    });
                }

                virtualCart.TotalAmount = virtualCart.VirtualProductCarts.Sum(x => x.Quantity * x.Product.Amount);

                await _stockService.Update(exists);
                await _virtualCartRepository.UpdateAsync(virtualCart);
            }

            return Result.Success<VirtualCartResponse>(new VirtualCartResponse()
            {
                Code = virtualCart.Code,
                StoreName = virtualCart.Store.Name,
                VirtualProductCarts = virtualCart.VirtualProductCarts.Select(x => new VirtualProductCartResponse()
                {
                    DiscountAmount = x.DiscountAmount,
                    Quantity = x.Quantity,
                    Product = new ProductResponse()
                    {
                        Description = x.Product.Description,
                        Code = x.Product.Code,
                        Amount = x.Product.Amount,
                        ProductId = x.Product.Id,
                        Categories = x.Product.Categories.Select(x => new CategoryResponse()
                        {
                            Description = x.Description,
                            Code = x.Code,
                            CategoryId = x.Id
                        })
                    }
                }),
                DiscountAmount = virtualCart.DiscountAmount,
                TotalAmount = virtualCart.TotalAmount,
            });
        }

        public async Task<Result<VirtualCartResponse>> RemoveItems(VirtualCartProductsRemoveFilter virtualCartProductsRemoveFilter)
        {
            var virtualCart = await _virtualCartRepository.GetFirstAsync(x => x.Code == virtualCartProductsRemoveFilter.VirtualCartCode);

            if (virtualCart is null)
                return Result.Failed<VirtualCartResponse>("The virtual Cart Code is not exists");

            Store store = virtualCart.Store;

            foreach (var prod in virtualCartProductsRemoveFilter.Products)
            {
                var productDb = virtualCart.VirtualProductCarts.FirstOrDefault(virtualProduct => virtualProduct.Product.Code == prod);

                var exists = store.Stocks.FirstOrDefault(x => x.Product.Code == prod);

                if (exists is null)
                    return Result.Failed<VirtualCartResponse>($"The {prod} is not exists in {virtualCart.Store.Name}");

                if (productDb is not null)
                {
                    exists.CurrentStock += productDb.Quantity;
                    virtualCart.VirtualProductCarts.Remove(productDb);
                    await _stockService.Update(exists);
                }
                virtualCart.TotalAmount = virtualCart.VirtualProductCarts.Sum(x => x.Quantity * x.Product.Amount);
                await _virtualCartRepository.UpdateAsync(virtualCart);
            }

            return Result.Success<VirtualCartResponse>(new VirtualCartResponse()
            {
                Code = virtualCart.Code,
                StoreName = virtualCart.Store.Name,
                VirtualProductCarts = virtualCart.VirtualProductCarts.Select(x => new VirtualProductCartResponse()
                {
                    DiscountAmount = x.DiscountAmount,
                    Quantity = x.Quantity,
                    Product = new ProductResponse()
                    {
                        Description = x.Product.Description,
                        Code = x.Product.Code,
                        Amount = x.Product.Amount,
                        ProductId = x.Product.Id,
                        Categories = x.Product.Categories.Select(x => new CategoryResponse()
                        {
                            Description = x.Description,
                            Code = x.Code,
                            CategoryId = x.Id
                        })
                    }
                }),
                DiscountAmount = virtualCart.DiscountAmount,
                TotalAmount = virtualCart.TotalAmount,
            });
        }
    }
}
