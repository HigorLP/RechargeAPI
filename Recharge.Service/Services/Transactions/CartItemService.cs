using AutoMapper;
using Microsoft.AspNetCore.Http;
using Recharge.Application.DTOs.Transactions;
using Recharge.Application.Interfaces.Transactions;
using Recharge.Domain.Models.Transactions;
using Recharge.Domain.Repositories.Transactions;

namespace Recharge.Application.Services.Transactions;
public class CartItemService : ICartItemService {
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartItemService(ICartItemRepository cartItemRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
        _cartItemRepository = cartItemRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<object> CreateCartItem(CartItemDTO cartItemDTO) {
        try {
            if (cartItemDTO.Amount <= 0) {
                return ResultService.Fail<CartItemDTO>("A quantidade deve ser maior que zero.");
            }

            var cartItem = _mapper.Map<CartItem>(cartItemDTO);

            var createdCartItem = await _cartItemRepository.CreateCartItem(cartItem);
            var createdCartItemDTO = _mapper.Map<CartItemDTO>(createdCartItem);

            return (createdCartItemDTO);
        } catch (Exception ex) {
            return ResultService.Fail<CartItemDTO>($"Erro ao criar item do carrinho: {ex.Message}");
        }
    }

    public async Task<ICollection<object>> GetAllCartItems() {
        try {
            var cartItems = await _cartItemRepository.GetAllCartItems();
            var cartItemsDTO = _mapper.Map<ICollection<CartItemDTO>>(cartItems);

            return new List<object>(cartItemsDTO);
        } catch (Exception ex) {
            return new List<object> { ($"Erro ao obter todas os carrinho: {ex.Message}") };
        }
    }

    public async Task<object> GetCartItemById(Guid id) {
        try {
            var cartItem = await _cartItemRepository.GetCartItemById(id);

            if (cartItem == null) {
                return ResultService.Fail<CartItemDTO>("Item do carrinho não encontrado.");
            }

            var cartItemDTO = _mapper.Map<CartItemDTO>(cartItem);
            return (cartItemDTO);
        } catch (Exception ex) {
            return ResultService.Fail<CartItemDTO>($"Erro ao obter item do carrinho por ID: {ex.Message}");
        }
    }

    public async Task<ICollection<object>> GetCartItensByPurchase(Guid purchaseId) {
        try {
            var cartItems = await _cartItemRepository.GetCartItensByPurchase(purchaseId);
            var cartItemDTOs = _mapper.Map<ICollection<CartItemDTO>>(cartItems);

            return new List<object>(cartItemDTOs);
        } catch (Exception ex) {
            return new List<object> { ($"Erro ao obter itens do carrinho por compra: {ex.Message}") };
        }
    }

    public async Task<object> RemoveCartItem(Guid id, CartItemDTO cartItemDTO) {
        try {
            var existingCartItem = await _cartItemRepository.GetCartItemById(id);

            if (existingCartItem == null) {
                return ResultService.Fail<CartItemDTO>("Item do carrinho não encontrado.");
            }

            var removedCartItem = await _cartItemRepository.RemoveCartItem(existingCartItem);
            var removedCartItemDTO = _mapper.Map<CartItemDTO>(removedCartItem);

            return (removedCartItemDTO);
        } catch (Exception ex) {
            return ResultService.Fail<CartItemDTO>($"Erro ao remover item do carrinho: {ex.Message}");
        }
    }
}