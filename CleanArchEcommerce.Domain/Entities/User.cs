using CleanArchEcommerce.Domain.Entities.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace CleanArchEcommerce.Domain.Entities;

public partial class User : BaseEntity<int>
{
    
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

    public User(
        string firstName, string lastName, string email, string phoneNo, 
        string country, string state, string city, string address, string postalCard, string password)
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

    public User(
        string firstName, string lastName, string email, string phoneNo, string country, 
        string state, string city, string address, string postalCard, string password, string role)
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

    public List<string> UpdateAdminFields(
    string firstName, string lastName, string email, string phoneNo, string country,
    string state, string city, string address, string postalCard, string password, string role)
    {
        List<string> fields = new List<string>();

        if (!string.IsNullOrEmpty(firstName) && FirstName != firstName)
        {
            FirstName = firstName;
            fields.Add("First name field updated.");
        }
        if (!string.IsNullOrEmpty(lastName) && LastName != lastName)
        {
            LastName = lastName;
            fields.Add("Last name field updated.");
        }
        if (!string.IsNullOrEmpty(email) && Email != email)
        {
            Email = email;
            fields.Add("Email field updated.");
        }
        if (!string.IsNullOrEmpty(phoneNo) && PhoneNo != phoneNo)
        {
            PhoneNo = phoneNo;
            fields.Add("Phone number field updated.");
        }
        if (!string.IsNullOrEmpty(country) && Country != country)
        {
            Country = country;
            fields.Add("Country field updated.");
        }
        if (!string.IsNullOrEmpty(state) && State != state)
        {
            State = state;
            fields.Add("State field updated.");
        }
        if (!string.IsNullOrEmpty(city) && City != city)
        {
            City = city;
            fields.Add("City field updated.");
        }
        if (!string.IsNullOrEmpty(address) && Address != address)
        {
            Address = address;
            fields.Add("Address field updated.");
        }
        if (!string.IsNullOrEmpty(postalCard) && PostalCard != postalCard)
        {
            PostalCard = postalCard;
            fields.Add("Postal Card field updated.");
        }
        if (!string.IsNullOrEmpty(password) && !VerifyPassword(password))
        {
            PasswordHash = HashedPassword(password);
            fields.Add("Password field updated.");
        }

        if (!string.IsNullOrEmpty(role) && Role != role)
        {
            Role = role;
            fields.Add("Role field updated.");
        }
        return fields;
    }
    public List<string> UpdateUserFields(
    string firstName, string lastName, string email, string phoneNo, string country,
    string state, string city, string address, string postalCard, string password)
    {
        List<string> fields = new List<string>();

        if (!string.IsNullOrEmpty(firstName) && FirstName != firstName)
        {
            FirstName = firstName;
            fields.Add("First name field updated.");
        }
        if (!string.IsNullOrEmpty(lastName) && LastName != lastName)
        {
            LastName = lastName;
            fields.Add("Last name field updated.");
        }
        if (!string.IsNullOrEmpty(email) && Email != email)
        {
            Email = email;
            fields.Add("Email field updated.");
        }
        if (!string.IsNullOrEmpty(phoneNo) && PhoneNo != phoneNo)
        {
            PhoneNo = phoneNo;
            fields.Add("Phone number field updated.");
        }
        if (!string.IsNullOrEmpty(country) && Country != country)
        {
            Country = country;
            fields.Add("Country field updated.");
        }
        if (!string.IsNullOrEmpty(state) && State != state)
        {
            State = state;
            fields.Add("State field updated.");
        }
        if (!string.IsNullOrEmpty(city) && City != city)
        {
            City = city;
            fields.Add("City field updated.");
        }
        if (!string.IsNullOrEmpty(address) && Address != address)
        {
            Address = address;
            fields.Add("Address field updated.");
        }
        if (!string.IsNullOrEmpty(postalCard) && PostalCard != postalCard)
        {
            PostalCard = postalCard;
            fields.Add("Postal Card field updated.");
        }
        if (!string.IsNullOrEmpty(password) && !VerifyPassword(password))
        {
            PasswordHash = HashedPassword(password);
            fields.Add("Password field updated.");
        }

        return fields;
    }
}
