using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Northwind.Core.Entities;
using Northwind.Core.Interfaces;
using Northwind.Web.Configuration;
using Northwind.Web.Controllers;
using Northwind.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Northwind.Web.Tests
{
	public class ProductsControllerTest
    {
		private ProductsController controller;
		private Mock<IApplicationConfiguration> appConfMock;
		private Mock<IProductsRepository> prodRepoMock;
		private Mock<ICategoriesRepository> catRepoMock;
		private Mock<IMapper> mapperMock;

		public ProductsControllerTest()
		{
			this.appConfMock = new Mock<IApplicationConfiguration>();
			this.prodRepoMock = new Mock<IProductsRepository>();
			this.catRepoMock = new Mock<ICategoriesRepository>();
            this.mapperMock = new Mock<IMapper>();
			this.controller = new ProductsController(
				prodRepoMock.Object, 
				catRepoMock.Object, 
				appConfMock.Object, 
				mapperMock.Object);
		}


        [Fact]
        public void Index()
        {
            var testProducts = GetTestProducts();
			appConfMock.Setup(conf => conf.MaxProductsOnPage).Returns(2);
			prodRepoMock.Setup(repo => repo.List(2)).Returns(GetTestProducts());

            var result = this.controller.Index();

            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<ProductsListViewModel>(viewResult.Model);
            Assert.Equal(2, viewModel.Products.Count());
        }

        [Fact]
        public void Create_Post()
        {
            ProductViewModel productViewModel = new ProductViewModel()
            {
                ProductId = 2
            };
            Product product = new Product()
            {
                ProductId = 2
            };
            this.mapperMock.Setup(m => m.Map<Product>(productViewModel)).Returns(product);
            this.prodRepoMock.Setup(m => m.Add(product));

            IActionResult result = this.controller.Create(productViewModel);

            RedirectToActionResult redirestResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirestResult.ActionName);
            this.prodRepoMock.Verify();
        }

        private IEnumerable<Product> GetTestProducts()
		{
			var products = new List<Product>
			{
				new Product
				{
					ProductId = 1,
					ProductName = "p1",
					UnitPrice = 1.2m
				},
				new Product
				{
					ProductId = 2,
					ProductName = "p2",
					UnitPrice = 3.2m
				}
			};

			return products;
		}
    }
}
