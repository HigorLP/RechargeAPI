namespace Recharge.Application.DTOs.Users;
public class AddressDTO {
    public Guid? Id { get; set; }
    public string? Cep { get; set; }
    public string? Logradouro { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Localidade { get; set; }
    public string? Uf { get; set; }
    public Guid? UserId { get; set; }
}