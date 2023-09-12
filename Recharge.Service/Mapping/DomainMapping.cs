using AutoMapper;
using Recharge.Application.DTOs.Products;
using Recharge.Application.DTOs.Transactions;
using Recharge.Application.DTOs.Users;
using Recharge.Domain.Models.Products;
using Recharge.Domain.Models.Transactions;
using Recharge.Domain.Models.Users;

namespace Recharge.Application.Mapping;
public class DomainMapping : Profile {

    public DomainMapping() {
        #region Users
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>().ReverseMap();

        CreateMap<User, UserResponseDTO>();
        CreateMap<UserResponseDTO, User>().ReverseMap();

        CreateMap<User, UserUpdateDTO>();
        CreateMap<UserUpdateDTO, User>().ReverseMap();

        CreateMap<User, RegisterUserDTO>();
        CreateMap<RegisterUserDTO, User>().ReverseMap();

        CreateMap<User, LoginDTO>();
        CreateMap<LoginDTO, User>().ReverseMap();

        CreateMap<Address, AddressDTO>();
        CreateMap<AddressDTO, Address>().ReverseMap();

        CreateMap<User, UserDetailDTO>().ReverseMap();
        #endregion

        #region Products
        CreateMap<Category, CategoryDTO>();
        CreateMap<CategoryDTO, Category>().ReverseMap();

        CreateMap<Brand, BrandDTO>();
        CreateMap<BrandDTO, Brand>().ReverseMap();

        CreateMap<Product, ProductDTO>();
        CreateMap<ProductDTO, Product>().ReverseMap();

        CreateMap<Datasheet, DatasheetDTO>();
        CreateMap<DatasheetDTO, Datasheet>().ReverseMap();
        #endregion

        #region Transactions
        CreateMap<Purchase, PurchaseDTO>();
        CreateMap<PurchaseDTO, Purchase>().ReverseMap();

        CreateMap<CartItem, CartItemDTO>();
        CreateMap<CartItemDTO, CartItem>().ReverseMap();
        #endregion
    }
}