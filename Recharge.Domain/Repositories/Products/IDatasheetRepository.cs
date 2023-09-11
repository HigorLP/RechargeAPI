using Recharge.Domain.Models.Products;

namespace Recharge.Domain.Repositories.Products;
public interface IDatasheetRepository {

    Task<Datasheet> CreateDatasheet(Datasheet datasheet);
    Task<Datasheet> GetDatasheetById(Guid id);
    Task<ICollection<Datasheet>> GetAllDatasheets();
    Task<Datasheet> UpdateDatasheet(Datasheet datasheet);
    Task<Datasheet> DeleteDatasheet(Datasheet datasheet);
}