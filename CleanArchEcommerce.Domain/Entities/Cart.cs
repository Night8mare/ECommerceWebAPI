namespace CleanArchEcommerce.Domain.Entities;

public partial class Cart
{
    public int Id { get; }

    public int UserId { get; private set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;
    public Cart() { }
    public Cart(int userId)
    {
        UserId = userId;
    }
}
