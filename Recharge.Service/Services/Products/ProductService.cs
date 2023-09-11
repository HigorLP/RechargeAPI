using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Recharge.Application.DTOs.Products;
using Recharge.Application.Interfaces.Products;
using Recharge.Application.Validator.Products;
using Recharge.Domain.Models.Products;
using Recharge.Domain.Repositories.Products;

namespace Recharge.Application.Services.Products;
public class ProductService : IProductService {
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductService(IProductRepository productRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
        _productRepository = productRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResultService<ProductDTO>> CreateProduct(ProductDTO productDTO) {
        try {
            var product = _mapper.Map<Product>(productDTO);

            var checkProduct = await _productRepository.GetProductByName(product.Name);
            if (checkProduct != null) {
                return ResultService.Fail<ProductDTO>("Produto já cadastrado.");
            }

            var validator = new ProductValidator();
            ValidationResult validationResult = await validator.ValidateAsync(product);

            if (!validationResult.IsValid) {
                return ResultService.RequestError<ProductDTO>("Erro ao criar produto.", validationResult);
            }

            //product.CreatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            product.CreatedOn = DateTime.Now;

            var createdProduct = await _productRepository.CreateProduct(product);
            var createdProductDTO = _mapper.Map<ProductDTO>(createdProduct);

            return ResultService.Ok(createdProductDTO);
        } catch (Exception ex) {
            return ResultService.Fail<ProductDTO>($"Erro ao criar produto: {ex.Message}");
        }
    }

    public async Task<ResultService<ProductDTO>> GetProductById(Guid id) {
        try {
            var product = await _productRepository.GetProductById(id);

            if (product == null) {
                return ResultService.Fail<ProductDTO>("Produto não encontrado.");
            }

            var productDTO = _mapper.Map<ProductDTO>(product);
            return ResultService.Ok(productDTO);
        } catch (Exception ex) {
            return ResultService.Fail<ProductDTO>($"Erro ao obter produto por ID: {ex.Message}");
        }
    }

    public async Task<ResultService<ProductDTO>> GetProductByName(string productName) {
        try {
            var product = await _productRepository.GetProductByName(productName);

            if (product == null) {
                return ResultService.Fail<ProductDTO>("Produto não encontrado.");
            }

            var productDTO = _mapper.Map<ProductDTO>(product);
            return ResultService.Ok(productDTO);
        } catch (Exception ex) {
            return ResultService.Fail<ProductDTO>($"Erro ao obter produto por nome: {ex.Message}");
        }
    }

    public async Task<ResultService<ProductDTO>> GetProductBySku(string sku) {
        try {
            var product = await _productRepository.GetProductBySku(sku);

            if (product == null) {
                return ResultService.Fail<ProductDTO>("Produto não encontrado.");
            }

            var productDTO = _mapper.Map<ProductDTO>(product);
            return ResultService.Ok(productDTO);
        } catch (Exception ex) {
            return ResultService.Fail<ProductDTO>($"Erro ao obter produto por SKU: {ex.Message}");
        }
    }

    public async Task<ResultService<ProductDTO>> GetProductByBarCode(string barCode) {
        try {
            var product = await _productRepository.GetProductByBarCoode(barCode);

            if (product == null) {
                return ResultService.Fail<ProductDTO>("Produto não encontrado.");
            }

            var productDTO = _mapper.Map<ProductDTO>(product);
            return ResultService.Ok(productDTO);
        } catch (Exception ex) {
            return ResultService.Fail<ProductDTO>($"Erro ao obter produto por código de barras: {ex.Message}");
        }
    }

    public async Task<ResultService<ICollection<ProductDTO>>> GetAllProducts() {
        try {
            var products = await _productRepository.GetAllProducts();
            var productDTOs = _mapper.Map<ICollection<ProductDTO>>(products);

            return ResultService.Ok(productDTOs);
        } catch (Exception ex) {
            return ResultService.Fail<ICollection<ProductDTO>>($"Erro ao obter todos os produtos: {ex.Message}");
        }
    }

    public async Task<ResultService<ICollection<ProductDTO>>> GetAllProductsInTheCategory(Guid categoryId) {
        try {
            var products = await _productRepository.GetAllProductsInTheCategory(categoryId);
            var productDTOs = _mapper.Map<ICollection<ProductDTO>>(products);

            return ResultService.Ok(productDTOs);
        } catch (Exception ex) {
            return ResultService.Fail<ICollection<ProductDTO>>($"Erro ao obter produtos na categoria: {ex.Message}");
        }
    }

    public async Task<ResultService<ICollection<ProductDTO>>> GetAllProductsInTheBrand(Guid brandId) {
        try {
            var products = await _productRepository.GetAllProductsInTheBrand(brandId);
            var productDTOs = _mapper.Map<ICollection<ProductDTO>>(products);

            return ResultService.Ok(productDTOs);
        } catch (Exception ex) {
            return ResultService.Fail<ICollection<ProductDTO>>($"Erro ao obter produtos na marca: {ex.Message}");
        }
    }

    public async Task<ResultService<ProductDTO>> UpdateProduct(Guid id, ProductDTO productDTO) {
        try {
            var existingProduct = await _productRepository.GetProductById(id);

            if (existingProduct == null) {
                return ResultService.Fail<ProductDTO>("Produto não encontrado.");
            }

            var checkProduct = await _productRepository.GetProductByName(productDTO.Name);
            if (checkProduct != null && checkProduct.Id != id) {
                return ResultService.Fail<ProductDTO>("Produto já cadastrado.");
            }

            productDTO.Id = existingProduct.Id;
            var updatedProduct = _mapper.Map(productDTO, existingProduct);

            var validator = new ProductValidator();
            ValidationResult validationResult = await validator.ValidateAsync(updatedProduct);

            if (!validationResult.IsValid) {
                return ResultService.RequestError<ProductDTO>("Erro ao atualizar produto.", validationResult);
            }

            //updatedProduct.EditedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            updatedProduct.EditedOn = DateTime.Now;

            var updatedProductResult = await _productRepository.UpdateProduct(updatedProduct);
            var updatedProductDTO = _mapper.Map<ProductDTO>(updatedProductResult);

            return ResultService.Ok(updatedProductDTO);
        } catch (Exception ex) {
            return ResultService.Fail<ProductDTO>($"Erro ao atualizar produto: {ex.Message}");
        }
    }

    public async Task<ResultService<ProductDTO>> RemoveProduct(Guid id) {
        try {
            var existingProduct = await _productRepository.GetProductById(id);

            if (existingProduct == null) {
                return ResultService.Fail<ProductDTO>("Produto não encontrado.");
            }

            var removedProduct = await _productRepository.RemoveProduct(existingProduct);
            var removedProductDTO = _mapper.Map<ProductDTO>(removedProduct);

            return ResultService.Ok(removedProductDTO);
        } catch (Exception ex) {
            return ResultService.Fail<ProductDTO>($"Erro ao remover produto: {ex.Message}");
        }
    }
}