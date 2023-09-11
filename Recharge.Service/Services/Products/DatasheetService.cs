using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Recharge.Application.DTOs.Products;
using Recharge.Application.Interfaces.Products;
using Recharge.Application.Validator.Products;
using Recharge.Domain.Models.Products;
using Recharge.Domain.Repositories.Products;

namespace Recharge.Application.Services.Products;
public class DatasheetService : IDatasheetService {
    private readonly IDatasheetRepository _datasheetRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DatasheetService(IDatasheetRepository datasheetRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
        _datasheetRepository = datasheetRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResultService<DatasheetDTO>> CreateDatasheet(DatasheetDTO datasheetDTO) {
        try {
            var datasheet = _mapper.Map<Datasheet>(datasheetDTO);

            var validator = new DatasheetValidator();
            ValidationResult validationResult = await validator.ValidateAsync(datasheet);

            if (!validationResult.IsValid) {
                return ResultService.RequestError<DatasheetDTO>("Erro ao criar datasheet.", validationResult);
            }

            //datasheet.CreatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            datasheet.CreatedOn = DateTime.Now;

            var createdDatasheet = await _datasheetRepository.CreateDatasheet(datasheet);
            var createdDatasheetDTO = _mapper.Map<DatasheetDTO>(createdDatasheet);

            return ResultService.Ok(createdDatasheetDTO);
        } catch (Exception ex) {
            return ResultService.Fail<DatasheetDTO>($"Erro ao criar datasheet: {ex.Message}");
        }
    }

    public async Task<ResultService<DatasheetDTO>> GetDatasheetById(Guid id) {
        try {
            var datasheet = await _datasheetRepository.GetDatasheetById(id);

            if (datasheet == null) {
                return ResultService.Fail<DatasheetDTO>("Datasheet não encontrado.");
            }

            var datasheetDTO = _mapper.Map<DatasheetDTO>(datasheet);
            return ResultService.Ok(datasheetDTO);
        } catch (Exception ex) {
            return ResultService.Fail<DatasheetDTO>($"Erro ao obter datasheet por ID: {ex.Message}");
        }
    }

    public async Task<ResultService<ICollection<DatasheetDTO>>> GetAllDatasheets() {
        try {
            var datasheets = await _datasheetRepository.GetAllDatasheets();
            var datasheetDTOs = _mapper.Map<ICollection<DatasheetDTO>>(datasheets);

            return ResultService.Ok(datasheetDTOs);
        } catch (Exception ex) {
            return ResultService.Fail<ICollection<DatasheetDTO>>($"Erro ao obter todos os datasheets: {ex.Message}");
        }
    }

    public async Task<ResultService<DatasheetDTO>> UpdateDatasheet(Guid id, DatasheetDTO datasheetDTO) {
        try {
            var existingDatasheet = await _datasheetRepository.GetDatasheetById(id);

            if (existingDatasheet == null) {
                return ResultService.Fail<DatasheetDTO>("Datasheet não encontrado.");
            }

            datasheetDTO.Id = existingDatasheet.Id;
            var updatedDatasheet = _mapper.Map(datasheetDTO, existingDatasheet);

            var validator = new DatasheetValidator();
            ValidationResult validationResult = await validator.ValidateAsync(updatedDatasheet);

            if (!validationResult.IsValid) {
                return ResultService.RequestError<DatasheetDTO>("Erro ao atualizar datasheet.", validationResult);
            }

            //updatedDatasheet.CreatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            updatedDatasheet.CreatedOn = DateTime.Now;

            var updatedDatasheetResult = await _datasheetRepository.UpdateDatasheet(updatedDatasheet);
            var updatedDatasheetDTO = _mapper.Map<DatasheetDTO>(updatedDatasheetResult);

            return ResultService.Ok(updatedDatasheetDTO);
        } catch (Exception ex) {
            return ResultService.Fail<DatasheetDTO>($"Erro ao atualizar datasheet: {ex.Message}");
        }
    }

    public async Task<ResultService<DatasheetDTO>> DeleteDatasheet(Guid id) {
        try {
            var existingDatasheet = await _datasheetRepository.GetDatasheetById(id);

            if (existingDatasheet == null) {
                return ResultService.Fail<DatasheetDTO>("Datasheet não encontrado.");
            }

            var deletedDatasheet = await _datasheetRepository.DeleteDatasheet(existingDatasheet);
            var deletedDatasheetDTO = _mapper.Map<DatasheetDTO>(deletedDatasheet);

            return ResultService.Ok(deletedDatasheetDTO);
        } catch (Exception ex) {
            return ResultService.Fail<DatasheetDTO>($"Erro ao remover datasheet: {ex.Message}");
        }
    }
}