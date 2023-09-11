using Recharge.Domain.Models.Transactions;

namespace Recharge.Domain.Models.Users;
public sealed class User : Entity {

    public string Name { get; set; }
    public string? Cpf { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string HashPassword { get; set; }

    public string Role = "Admin";

    public bool IsLogged { get; set; } = false;

    public ICollection<Purchase>? Purchases { get; set; }
    public ICollection<Address>? Addresses { get; set; }

    public User(string name, string email, string password) {
        Name = name;
        Email = email;
        HashPassword = password;
        Purchases = new List<Purchase>();
        Addresses = new List<Address>();
    }

    public User(string name, string? cpf, string email, string? phone, string password) {
        Name = name;
        Cpf = cpf;
        Email = email;
        Phone = phone;
        HashPassword = password;
    }

    public User(bool isLogged) {
        IsLogged = isLogged;
    }

    public User() { }
}