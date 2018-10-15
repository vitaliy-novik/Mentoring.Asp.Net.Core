using AutoMapper;
using Northwind.Core.Entities;
using Northwind.Infrastructure.Models;

namespace Northwind.Infrastructure
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Products, Product>();
			CreateMap<Product, Products>();
		}
	}
}
