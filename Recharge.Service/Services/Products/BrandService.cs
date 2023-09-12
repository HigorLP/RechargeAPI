using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Recharge.Application.DTOs.Products;
using Recharge.Application.Interfaces.Products;
using Recharge.Application.Validator.Products;
using Recharge.Domain.Models.Products;
using Recharge.Domain.Repositories.Products;

namespace Recharge.Application.Services.Products {
    public class BrandService : IBrandService {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrandService(IBrandRepository brandRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<object> CreateBrand(BrandDTO brandDTO) {
            try {
                var brand = _mapper.Map<Brand>(brandDTO);

                var checkBrand = await _brandRepository.GetBrandByName(brand.Name);
                if (checkBrand != null) {
                    return ResultService.Fail<BrandDTO>("Marca já cadastrada.");
                }

                var validator = new BrandValidator();
                ValidationResult validationResult = await validator.ValidateAsync(brand);

                if (!validationResult.IsValid) {
                    return ResultService.RequestError<BrandDTO>("Erro ao criar marca.", validationResult);
                }

                //brand.CreatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                brand.CreatedOn = DateTime.Now;

                var createdBrand = await _brandRepository.CreateBrand(brand);
                var createdBrandDTO = _mapper.Map<BrandDTO>(createdBrand);

                return createdBrandDTO;
            } catch (Exception ex) {
                return ResultService.Fail<BrandDTO>($"Erro ao criar marca: {ex.Message}");
            }
        }

        public async Task<object> GetBrandById(Guid id) {
            try {
                var brand = await _brandRepository.GetBrandById(id);

                if (brand == null) {
                    return ResultService.Fail<BrandDTO>("Marca não encontrada.");
                }

                var brandDTO = _mapper.Map<BrandDTO>(brand);
                return brandDTO;
            } catch (Exception ex) {
                return ResultService.Fail<BrandDTO>($"Erro ao obter marca por ID: {ex.Message}");
            }
        }

        public async Task<object> GetBrandByName(string name) {
            try {
                var brand = await _brandRepository.GetBrandByName(name);

                if (brand == null) {
                    return ResultService.Fail<BrandDTO>("Marca não encontrada.");
                }

                var brandDTO = _mapper.Map<BrandDTO>(brand);
                return brandDTO;
            } catch (Exception ex) {
                return ResultService.Fail<BrandDTO>($"Erro ao obter marca por nome: {ex.Message}");
            }
        }

        public async Task<ICollection<object>> GetAllBrands() {
            try {
                var brands = await _brandRepository.GetAllBrands();
                var brandDTOs = _mapper.Map<ICollection<BrandDTO>>(brands);

                return new List<object>(brandDTOs);
            } catch (Exception ex) {
                return new List<object> { $"Erro ao obter todas as marcas: {ex.Message}" };
            }
        }

        public async Task<object> UpdateBrand(Guid id, BrandDTO brandDTO) {
            try {
                var existingBrand = await _brandRepository.GetBrandById(id);

                if (existingBrand == null) {
                    return ResultService.Fail<BrandDTO>("Marca não encontrada.");
                }

                brandDTO.Id = existingBrand.Id;
                var updatedBrand = _mapper.Map(brandDTO, existingBrand);

                var validator = new BrandValidator();
                ValidationResult validationResult = await validator.ValidateAsync(updatedBrand);

                if (!validationResult.IsValid) {
                    return ResultService.RequestError<BrandDTO>("Erro ao atualizar marca.", validationResult);
                }

                //updatedBrand.EditedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                updatedBrand.EditedOn = DateTime.Now;

                var updatedBrandResult = await _brandRepository.UpdateBrand(updatedBrand);
                var updatedBrandDTO = _mapper.Map<BrandDTO>(updatedBrandResult);

                return updatedBrandDTO;
            } catch (Exception ex) {
                return ResultService.Fail<BrandDTO>($"Erro ao atualizar marca: {ex.Message}");
            }
        }

        public async Task<object> DeleteBrand(Guid id) {
            try {
                var existingBrand = await _brandRepository.GetBrandById(id);

                if (existingBrand == null) {
                    return ResultService.Fail<BrandDTO>("Marca não encontrada.");
                }

                var deletedBrand = await _brandRepository.DeleteBrand(existingBrand);
                var deletedBrandDTO = _mapper.Map<BrandDTO>(deletedBrand);

                return deletedBrandDTO;
            } catch (Exception ex) {
                return ResultService.Fail<BrandDTO>($"Erro ao remover marca: {ex.Message}");
            }
        }
    }
}