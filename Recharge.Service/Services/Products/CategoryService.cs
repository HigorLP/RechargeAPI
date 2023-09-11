using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Recharge.Application.DTOs.Products;
using Recharge.Application.Interfaces.Products;
using Recharge.Application.Validators.Products;
using Recharge.Domain.Models.Products;
using Recharge.Domain.Repositories.Products;

namespace Recharge.Application.Services.Products {
    public class CategoryService : ICategoryService {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResultService<CategoryDTO>> CreateCategory(CategoryDTO categoryDTO) {
            try {
                var category = _mapper.Map<Category>(categoryDTO);

                var checkCategory = await _categoryRepository.GetCategoryByName(category.Name);
                if (checkCategory != null) {
                    return ResultService.Fail<CategoryDTO>("Categoria já cadastrada.");
                }

                var validator = new CategoryValidator();
                ValidationResult validationResult = await validator.ValidateAsync(category);

                if (!validationResult.IsValid) {
                    return ResultService.RequestError<CategoryDTO>("Erro ao criar categoria.", validationResult);
                }

                //category.CreatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                category.CreatedOn = DateTime.Now;

                var createdCategory = await _categoryRepository.CreateCategory(category);
                var createdCategoryDTO = _mapper.Map<CategoryDTO>(createdCategory);

                return ResultService.Ok(createdCategoryDTO);
            } catch (Exception ex) {
                return ResultService.Fail<CategoryDTO>($"Erro ao criar categoria: {ex.Message}");
            }
        }

        public async Task<ResultService<CategoryDTO>> GetCategoryById(Guid id) {
            try {
                var category = await _categoryRepository.GetCategoryById(id);

                if (category == null) {
                    return ResultService.Fail<CategoryDTO>("Categoria não encontrada.");
                }

                var categoryDTO = _mapper.Map<CategoryDTO>(category);
                return ResultService.Ok(categoryDTO);
            } catch (Exception ex) {
                return ResultService.Fail<CategoryDTO>($"Erro ao obter categoria por ID: {ex.Message}");
            }
        }

        public async Task<ResultService<CategoryDTO>> GetCategoryByName(string name) {
            try {
                var category = await _categoryRepository.GetCategoryByName(name);

                if (category == null) {
                    return ResultService.Fail<CategoryDTO>("Categoria não encontrada.");
                }

                var categoryDTO = _mapper.Map<CategoryDTO>(category);
                return ResultService.Ok(categoryDTO);
            } catch (Exception ex) {
                return ResultService.Fail<CategoryDTO>($"Erro ao obter categoria por nome: {ex.Message}");
            }
        }

        public async Task<ResultService<ICollection<CategoryDTO>>> GetAllCategories() {
            try {
                var categories = await _categoryRepository.GetAllCategories();
                var categoryDTOs = _mapper.Map<ICollection<CategoryDTO>>(categories);

                return ResultService.Ok(categoryDTOs);
            } catch (Exception ex) {
                return ResultService.Fail<ICollection<CategoryDTO>>($"Erro ao obter todas as categorias: {ex.Message}");
            }
        }

        public async Task<ResultService<CategoryDTO>> UpdateCategory(Guid id, CategoryDTO categoryDTO) {
            try {
                var existingCategory = await _categoryRepository.GetCategoryById(id);

                if (existingCategory == null) {
                    return ResultService.Fail<CategoryDTO>("Categoria não encontrada.");
                }

                var checkCategory = await _categoryRepository.GetCategoryByName(categoryDTO.Name);
                if (checkCategory != null) {
                    return ResultService.Fail<CategoryDTO>("Categoria já cadastrada.");
                }

                categoryDTO.Id = existingCategory.Id;
                var updatedCategory = _mapper.Map(categoryDTO, existingCategory);

                var validator = new CategoryValidator();
                ValidationResult validationResult = await validator.ValidateAsync(updatedCategory);

                if (!validationResult.IsValid) {
                    return ResultService.RequestError<CategoryDTO>("Erro ao atualizar categoria.", validationResult);
                }

                //updatedCategory.EditedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                updatedCategory.EditedOn = DateTime.Now;

                var updatedCategoryResult = await _categoryRepository.UpdateCategory(updatedCategory);
                var updatedCategoryDTO = _mapper.Map<CategoryDTO>(updatedCategoryResult);

                return ResultService.Ok(updatedCategoryDTO);
            } catch (Exception ex) {
                return ResultService.Fail<CategoryDTO>($"Erro ao atualizar categoria: {ex.Message}");
            }
        }


        public async Task<ResultService<CategoryDTO>> DeleteCategory(Guid id) {
            try {
                var existingCategory = await _categoryRepository.GetCategoryById(id);

                if (existingCategory == null) {
                    return ResultService.Fail<CategoryDTO>("Categoria não encontrada.");
                }

                var deletedCategory = await _categoryRepository.DeleteCategory(existingCategory);
                var deletedCategoryDTO = _mapper.Map<CategoryDTO>(deletedCategory);

                return ResultService.Ok(deletedCategoryDTO);
            } catch (Exception ex) {
                return ResultService.Fail<CategoryDTO>($"Erro ao remover categoria: {ex.Message}");
            }
        }
    }
}