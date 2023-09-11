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

    public async Task<ResultService<CartItemDTO>> CreateCartItem(CartItemDTO cartItemDTO) {
        try {
            if (cartItemDTO.Amount <= 0) {
                return ResultService.Fail<CartItemDTO>("A quantidade deve ser maior que zero.");
            }

            var cartItem = _mapper.Map<CartItem>(cartItemDTO);

            var createdCartItem = await _cartItemRepository.CreateCartItem(cartItem);
            var createdCartItemDTO = _mapper.Map<CartItemDTO>(createdCartItem);

            return ResultService.Ok(createdCartItemDTO);
        } catch (Exception ex) {
            return ResultService.Fail<CartItemDTO>($"Erro ao criar item do carrinho: {ex.Message}");
        }
    }

    public async Task<ResultService<CartItemDTO>> GetCartItemById(Guid id) {
        try {
            var cartItem = await _cartItemRepository.GetCartItemById(id);

            if (cartItem == null) {
                return ResultService.Fail<CartItemDTO>("Item do carrinho não encontrado.");
            }

            var cartItemDTO = _mapper.Map<CartItemDTO>(cartItem);
            return ResultService.Ok(cartItemDTO);
        } catch (Exception ex) {
            return ResultService.Fail<CartItemDTO>($"Erro ao obter item do carrinho por ID: {ex.Message}");
        }
    }

    public async Task<ResultService<ICollection<CartItemDTO>>> GetCartItensByPurchase(Guid purchaseId) {
        try {
            var cartItems = await _cartItemRepository.GetCartItensByPurchase(purchaseId);
            var cartItemDTOs = _mapper.Map<ICollection<CartItemDTO>>(cartItems);

            return ResultService.Ok(cartItemDTOs);
        } catch (Exception ex) {
            return ResultService.Fail<ICollection<CartItemDTO>>($"Erro ao obter itens do carrinho por compra: {ex.Message}");
        }
    }

    public async Task<ResultService<CartItemDTO>> RemoveCartItem(Guid id, CartItemDTO cartItemDTO) {
        try {
            var existingCartItem = await _cartItemRepository.GetCartItemById(id);

            if (existingCartItem == null) {
                return ResultService.Fail<CartItemDTO>("Item do carrinho não encontrado.");
            }

            var removedCartItem = await _cartItemRepository.RemoveCartItem(existingCartItem);
            var removedCartItemDTO = _mapper.Map<CartItemDTO>(removedCartItem);

            return ResultService.Ok(removedCartItemDTO);
        } catch (Exception ex) {
            return ResultService.Fail<CartItemDTO>($"Erro ao remover item do carrinho: {ex.Message}");
        }
    }
}