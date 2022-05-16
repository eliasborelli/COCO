using Coco.Core.Entities;
using Coco.Core.Interfaces;
using Coco.Core.Models.Request;
using Coco.Infraestructure.Persistence;
using Coco.Infraestructure.Repositories;
using Coco.Services.Interfaces;
using Coco.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Coco.UnitTests.Services
{
    [TestClass]
    public class ProductServiceTest
    {

        private IProductService _productService;

        private Mock<IProductRepository> _productRepository;
        private Mock<IRepository<Stock>> _stockRepository;
        private Mock<IRepository<Store>> _storeRepository;

        [TestInitialize]
        public void SetUp()
        {
            _productRepository = new Mock<IProductRepository>();
            _stockRepository = new Mock<IRepository<Stock>>();
            _storeRepository = new Mock<IRepository<Store>>();

            _productService = new ProductService(_productRepository.Object, _stockRepository.Object, _storeRepository.Object);
        }

        [TestMethod]
        public void Exists_ShouldFaildWhenCodeAndNameAreNull()
        {
            //init
            string code = string.Empty;
            string description = string.Empty;

            //act 
            var result = _productService.Exists(code, description).GetAwaiter().GetResult();

            //assert
            Assert.IsFalse(result.Succeeded);
        }

        [TestMethod]
        public void Exists_ShouldOkWhenCodeIsNotNull()
        {
            //init
            string code = "COD01";
            string description = string.Empty;

            _productRepository.Setup(x => x.GetFirstAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>())).Returns(Task.FromResult(new Product()));
            //act 
            var result = _productService.Exists(code, description).GetAwaiter().GetResult();

            //assert
            Assert.IsTrue(result.Succeeded);
        }

        [TestMethod]
        public void Exists_ShouldOkWhenDescriptionIsNotNull()
        {
            //init
            string code = "COD01";
            string description = string.Empty;

            _productRepository.Setup(x => x.GetFirstAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>())).Returns(Task.FromResult(new Product()));
            //act 
            var result = _productService.Exists(code, description).GetAwaiter().GetResult();

            //assert
            Assert.IsTrue(result.Succeeded);
        }

        [TestMethod]
        public void GetAllAvailableProducts_ShoulOk()
        {
            //init
            _productRepository.Setup(bs => bs.GetAsync(It.IsAny<Expression<Func<Product, bool>>>(), null, It.IsAny<string>(), null, null)).Verifiable();

            //act
            var result = _productService.GetAllAvailableProducts().GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.Succeeded);
        }

        [TestMethod]
        public void GetAllProductsByStore_ShoulFailWhenNameIsNull()
        {
            //init
            string name = string.Empty;

            //act
            var result = _productService.GetAllProductsByStore(name).GetAwaiter().GetResult();

            //Assert
            Assert.IsFalse(result.Succeeded);
        }

        [TestMethod]
        public void GetAllProductsByStore_ShoulFailWhenStoreIsNotFound()
        {
            //init
            string name = string.Empty;

            _storeRepository.Setup(x => x.GetFirstAsync(It.IsAny<Expression<Func<Store, bool>>>(), It.IsAny<string>()));

            //act
            var result = _productService.GetAllProductsByStore(name).GetAwaiter().GetResult();

            //Assert
            Assert.IsFalse(result.Succeeded);
        }

        [TestMethod]
        public void GetAllProductsByStore_ShoulOk()
        {
            //init
            string name = "hello";

            _storeRepository.Setup(x => x.GetFirstAsync(It.IsAny<Expression<Func<Store, bool>>>(), It.IsAny<string>())).Returns(Task.FromResult(new Store() { Stocks = new List<Stock>() }));

            //act
            var result = _productService.GetAllProductsByStore(name).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.Succeeded);
        }

        [TestMethod]
        public void GetProductByFilter_ShoulFailWhenCodeAndDescriptionIsNull()
        {
            //init
            var filter = new ProductFilter() { ProductDescription = string.Empty, StoreName = "coco", Code = string.Empty };

            //act
            var result = _productService.GetProductByFilter(filter).GetAwaiter().GetResult();

            //Assert
            Assert.IsFalse(result.Succeeded);
        }

        [TestMethod]
        public void GetProductByFilter_ShoulFailWhenProductIsNotExists()
        {
            //init
            var filter = new ProductFilter() { ProductDescription = "example", StoreName = "coco", Code = "COD01" };

            _productRepository.Setup(x => x.GetFirstAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()));

            //act
            var result = _productService.GetProductByFilter(filter).GetAwaiter().GetResult();

            //Assert
            Assert.IsFalse(result.Succeeded);
        }

        [TestMethod]
        public void GetProductByFilter_ShoulFailWhenProductAndStoreAreNotExists()
        {
            //init
            var filter = new ProductFilter() { ProductDescription = "example", StoreName = "coco", Code = "COD01" };

            _productRepository.Setup(x => x.GetFirstAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>())).Returns(Task.FromResult(new Product()));
            _storeRepository.Setup(x => x.GetFirstAsync(It.IsAny<Expression<Func<Store, bool>>>(), It.IsAny<string>()));
            //act
            var result = _productService.GetProductByFilter(filter).GetAwaiter().GetResult();

            //Assert
            Assert.IsFalse(result.Succeeded);
        }

        [TestMethod]
        public void GetProductByFilter_ShoulOk()
        {
            //init
            Guid ProductId = Guid.NewGuid();
            var filter = new ProductFilter() { ProductDescription = "example", StoreName = "coco", Code = "COD01" };

            _productRepository.Setup(x => x.GetFirstAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>())).Returns(Task.FromResult(new Product() { Id = ProductId }));
            _storeRepository.Setup(x => x.GetFirstAsync(It.IsAny<Expression<Func<Store, bool>>>(), It.IsAny<string>())).Returns(Task.FromResult(new Store() { Stocks = new List<Stock>() { new Stock() { ProductId = ProductId, CurrentStock = 3 } } }));
            //act
            var result = _productService.GetProductByFilter(filter).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.Succeeded);
        }

    }
}
