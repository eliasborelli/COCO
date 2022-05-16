using Coco.Core.Entities;
using Coco.Core.Interfaces;
using Coco.Core.Models.Response;
using Coco.Infraestructure.Commons;
using Coco.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Services.Services
{
    public class StockService : IStockService
    {
        private readonly IRepository<Stock> _stockRepository;
        private readonly IStoreService _storeService;
        public StockService(IRepository<Stock> stockRepository, IStoreService storeService)
        {
            _stockRepository = stockRepository;
            _storeService = storeService;
        }
        public async Task<Result<Store>> GetStockByStore(string name)
        {
            var store = await _storeService.Exists(name);

            if (store == null || store.Succeeded is false)
                return Result.Failed<Store>("The store is not exists");

            return Result.Success<Store>(new Store()
            {
                Id = store.Value.Id,
                Name = store.Value.Name,
                Address = store.Value.Address,
                Phone = store.Value.Phone,
                Stocks = store.Value.Stocks
            });
        }

        public async Task<Result> Update(Stock stock)
        {
            return Result.Success<Stock>(await _stockRepository.UpdateAsync(stock));
        }
    }
}
