using AutoMapper;
using Moq;
using Northwind.Core.Entities;
using Northwind.Core.Interfaces;
using Northwind.Web.Configuration;
using Northwind.Web.Controllers;
using System.Collections.Generic;
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


		public void Initialize()
		{
			this.appConfMock = new Mock<IApplicationConfiguration>();
			this.prodRepoMock = new Mock<IProductsRepository>();
			this.catRepoMock = new Mock<ICategoriesRepository>();
			this.controller = new ProductsController(
				prodRepoMock.Object, 
				catRepoMock.Object, 
				appConfMock.Object, 
				mapperMock.Object);
		}


        [Fact]
        public void Index()
        {
			appConfMock.Setup(conf => conf.MaxProductsOnPage).Returns(2);
			prodRepoMock.Setup(repo => repo.List(2)).Returns(GetTestProducts());
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
