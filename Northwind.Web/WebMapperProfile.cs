using AutoMapper;
using Northwind.Core.Entities;
using Northwind.Web.ViewModels;

namespace Northwind.Web
{
	public class WebMapperProfile : Profile
	{
		public WebMapperProfile()
		{
			CreateMap<ProductViewModel, Product>();
			CreateMap<Product, ProductViewModel>();
		}
	}
}
