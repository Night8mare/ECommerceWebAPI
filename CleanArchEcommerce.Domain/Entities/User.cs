using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CleanArchEcommerce.Domain.Entities;

public partial class User
{
    
    public int Id { get; set; }

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string PhoneNo { get; private set; } = null!;

    public string Country { get; private set; } = null!;

    public string State { get; private set; } = null!;

    public string City { get; private set; } = null!;

    public string Address { get; private set; } = null!;

    public string PostalCard { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Token { get; private set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    protected User() { }

    public User(string firstName, string lastName, string email, string phoneNo, string country, string state, string city, string address, string postalCard, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNo = phoneNo;
        Country = country;
        State = state;
        City = city;
        Address = address;
        PostalCard = postalCard;
        PasswordHash = HashedPassword(password);
    }

    public User(string firstName, string lastName, string email, string phoneNo, string country, string state, string city, string address, string postalCard, string password, string role)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNo = phoneNo;
        Country = country;
        State = state;
        City = city;
        Address = address;
        PostalCard = postalCard;
        PasswordHash = HashedPassword(password);
        Role = role;
    }

    public bool VerifyPassword(string password)
    {
        return new PasswordHasher<User>().VerifyHashedPassword(this, this.PasswordHash, password) == PasswordVerificationResult.Success;
    }
    public string HashedPassword(string password)
    {        
        return new PasswordHasher<User>().HashPassword(this, password);
    }
    public void SetToken(string token)
    {
        this.Token = token;
    }

}
