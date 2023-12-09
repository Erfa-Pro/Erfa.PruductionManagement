using AutoMapper;
using Erfa.PruductionManagement.Application.RequestModels;
using Erfa.PruductionManagement.Domain.Entities;

namespace Erfa.PruductionManagement.Application.Profiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductRequestModel, Product>();
           
        }
    }
}
