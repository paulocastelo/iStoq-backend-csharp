using AutoMapper;
using iStoq.Domain.Entities;
using iStoq.Application.DTOs;

namespace iStoq.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<StockMovement, StockMovementDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Se não quiser mapear de volta o hash
        }
    }
}
