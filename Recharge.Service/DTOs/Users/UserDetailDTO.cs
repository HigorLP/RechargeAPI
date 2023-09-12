namespace Recharge.Application.DTOs.Users;
public class UserDetailDTO {
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public ICollection<AddressDTO> Addresses { get; set; }
}
