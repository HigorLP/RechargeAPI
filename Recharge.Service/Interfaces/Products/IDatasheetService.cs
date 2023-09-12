using Recharge.Application.DTOs.Products;

namespace Recharge.Application.Interfaces.Products;
public interface IDatasheetService {

    Task<object> CreateDatasheet(DatasheetDTO datasheetDTO);
    Task<object> GetDatasheetById(Guid id);
    Task<ICollection<object>> GetAllDatasheets();
    Task<object> UpdateDatasheet(Guid id, DatasheetDTO datasheetDTO);
    Task<object> DeleteDatasheet(Guid id);
}