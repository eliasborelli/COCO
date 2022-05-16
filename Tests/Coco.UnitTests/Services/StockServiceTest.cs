using Coco.Core.Entities;
using Coco.Core.Interfaces;
using Coco.Infraestructure.Commons;
using Coco.Services.Interfaces;
using Coco.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Coco.UnitTests.Services
{
    [TestClass]
    public class StockServiceTest
    {

        private IStockService _stockService;
        private Mock<IRepository<Stock>> _stockRepository;
        private Mock<IStoreService> _storeService;

        [TestInitialize]
        public void SetUp()
        {
            _stockRepository = new Mock<IRepository<Stock>>();
            _storeService = new Mock<IStoreService>();

            _stockService = new StockService(_stockRepository.Object, _storeService.Object);
        }

        [TestMethod]
        public void GetStockByStore_ShoudFailWhenNameIsNull()
        {
            //init
            string name = string.Empty;

            //act 
            var result = _stockService.GetStockByStore(name).GetAwaiter().GetResult();

            //assert
            Assert.IsFalse(result.Succeeded);
        }

        [TestMethod]
        public void GetStockByStore_ShoudFailWhenStoreIsNotFound()
        {
            //init
            string name = "Coco";
            _storeService.Setup(x => x.Exists(It.IsAny<string>()));
            _stockService = new StockService(_stockRepository.Object, _storeService.Object);

            //act 
            var result = _stockService.GetStockByStore(name).GetAwaiter().GetResult();

            //assert
            Assert.IsFalse(result.Succeeded);
        }

        [TestMethod]
        public void GetStockByStore_ShoudOk()
        {
            //init
            string name = "Coco";
            _storeService.Setup(x => x.Exists(It.IsAny<string>())).Returns(Task.FromResult(Result.Success<Store>(new Store())));
            _stockService = new StockService(_stockRepository.Object, _storeService.Object);

            //act 
            var result = _stockService.GetStockByStore(name).GetAwaiter().GetResult();

            //assert
            Assert.IsTrue(result.Succeeded);
        }


    }
}
