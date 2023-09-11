using Recharge.Application.DTOs.Products;
using Recharge.Application.Services;

namespace Recharge.Application.Interfaces.Products;
public interface IDatasheetService {

    Task<ResultService<DatasheetDTO>> CreateDatasheet(DatasheetDTO datasheetDTO);
    Task<ResultService<DatasheetDTO>> GetDatasheetById(Guid id);
    Task<ResultService<ICollection<DatasheetDTO>>> GetAllDatasheets();
    Task<ResultService<DatasheetDTO>> UpdateDatasheet(Guid id, DatasheetDTO datasheetDTO);
    Task<ResultService<DatasheetDTO>> DeleteDatasheet(Guid id);
}