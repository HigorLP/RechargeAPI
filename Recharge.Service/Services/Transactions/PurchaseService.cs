using AutoMapper;
using Recharge.Application.DTOs.Transactions;
using Recharge.Application.Interfaces.Transactions;
using Recharge.Application.Validator.Transactions;
using Recharge.Domain.Models.Transactions;
using Recharge.Domain.Repositories.Transactions;

namespace Recharge.Application.Services.Transactions;
public class PurchaseService : IPurchaseService {
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IMapper _mapper;
    private readonly PurchaseValidator _purchaseValidator;

    public PurchaseService(IPurchaseRepository purchaseRepository, IMapper mapper, PurchaseValidator purchaseValidator) {
        _purchaseRepository = purchaseRepository;
        _mapper = mapper;
        _purchaseValidator = purchaseValidator;
    }

    public async Task<object> CreatePurchase(PurchaseDTO purchaseDTO) {
        try {
            var purchase = _mapper.Map<Purchase>(purchaseDTO);

            var validationResult = await _purchaseValidator.ValidateAsync(purchase);

            if (!validationResult.IsValid) {
                return ResultService.RequestError<PurchaseDTO>("Erro ao criar compra.", validationResult);
            }

            var createdPurchase = await _purchaseRepository.CreatePurchase(purchase);

            var createdPurchaseDTO = _mapper.Map<PurchaseDTO>(createdPurchase);

            return (createdPurchaseDTO);
        } catch (Exception ex) {
            return ResultService.Fail<PurchaseDTO>($"Erro ao criar compra: {ex.Message}");
        }
    }

    public async Task<object> GetPurchaseById(Guid id) {
        try {
            var purchase = await _purchaseRepository.GetPurchaseById(id);

            if (purchase == null) {
                return ResultService.Fail<PurchaseDTO>("Compra não encontrada.");
            }

            var purchaseDTO = _mapper.Map<PurchaseDTO>(purchase);
            return (purchaseDTO);
        } catch (Exception ex) {
            return ResultService.Fail<PurchaseDTO>($"Erro ao obter compra por ID: {ex.Message}");
        }
    }

    public async Task<ICollection<object>> GetPurchasesByUserId(Guid userId) {
        try {
            var purchases = await _purchaseRepository.GetPurchasesByUserId(userId);
            var purchaseDTOs = _mapper.Map<ICollection<PurchaseDTO>>(purchases);

            return new List<object>(purchaseDTOs);
        } catch (Exception ex) {
            return new List<object> { ($"Erro ao obter compras por usuário: {ex.Message}") };
        }
    }

    public async Task<ICollection<object>> GetAllPurchases() {
        try {
            var purchases = await _purchaseRepository.GetAllPurchases();
            var purchaseDTOs = _mapper.Map<ICollection<PurchaseDTO>>(purchases);

            return new List<object>(purchaseDTOs);
        } catch (Exception ex) {
            return new List<object> { ($"Erro ao obter todas as compras: {ex.Message}") };
        }
    }

    public async Task<object> UpdatePurchase(Guid id, PurchaseDTO purchaseDTO) {
        try {
            var existingPurchase = await _purchaseRepository.GetPurchaseById(id);

            if (existingPurchase == null) {
                return ResultService.Fail<PurchaseDTO>("Compra não encontrada.");
            }

            purchaseDTO.Id = existingPurchase.Id;
            var updatedPurchase = _mapper.Map(purchaseDTO, existingPurchase);

            var validationResult = await _purchaseValidator.ValidateAsync(updatedPurchase);

            if (!validationResult.IsValid) {
                return ResultService.RequestError<PurchaseDTO>("Erro ao atualizar compra.", validationResult);
            }

            var updatedPurchaseResult = await _purchaseRepository.UpdatePurchase(updatedPurchase);
            var updatedPurchaseDTO = _mapper.Map<PurchaseDTO>(updatedPurchaseResult);

            return (updatedPurchaseDTO);
        } catch (Exception ex) {
            return ResultService.Fail<PurchaseDTO>($"Erro ao atualizar compra: {ex.Message}");
        }
    }

    public async Task<object> RemovePurchase(Guid id, PurchaseDTO purchaseDTO) {
        try {
            var existingPurchase = await _purchaseRepository.GetPurchaseById(id);

            if (existingPurchase == null) {
                return ResultService.Fail<PurchaseDTO>("Compra não encontrada.");
            }

            var removedPurchase = await _purchaseRepository.RemovePurchase(existingPurchase);
            var removedPurchaseDTO = _mapper.Map<PurchaseDTO>(removedPurchase);

            return (removedPurchaseDTO);
        } catch (Exception ex) {
            return ResultService.Fail<PurchaseDTO>($"Erro ao remover compra: {ex.Message}");
        }
    }
}