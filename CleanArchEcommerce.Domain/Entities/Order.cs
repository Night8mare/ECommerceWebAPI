namespace CleanArchEcommerce.Domain.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int CartId { get; private set; }

    public DateTime OrderDate { get; private set; }

    public decimal TotalAmount { get; private set; }

    public string OrderStatus { get; private set; } = null!;

    public virtual Cart Cart { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    public Order() { }
    public Order(int cartId, decimal totalAmount)
    {
        CartId = cartId;
        TotalAmount = totalAmount;
        OrderStatus = "Ordered";
    }
}
