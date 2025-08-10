namespace CleanArchEcommerce.Domain.Entities;

public partial class Item
{
    public int Id { get;}

    public int CartId { get; private set; }

    public int ProductId { get; private set; }

    public int Quantity { get; private set; }

    public decimal TotalAmount { get; private set; }

    public string ItemStatus { get; private set; } = null!;

    public int? OrderId { get; private set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Order? Order { get; set; }

    public virtual Product Product { get; set; } = null!;

    public Item() { }
    
    public Item(int cartId, int productId, int quantity, decimal totalAmount)
    {
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
        TotalAmount = totalAmount;
    }
    public Item(int cartId, int productId, int quantity, decimal totalAmount, string itemStatus)
    {
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
        TotalAmount = totalAmount;
        ItemStatus = itemStatus;
    }
    public Item(int cartId, int productId, int quantity, decimal totalAmount, string itemStatus, int orderId)
    {
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
        TotalAmount = totalAmount;
        ItemStatus = itemStatus;
        OrderId = orderId;
    }
    public void UpdateItemOrder(int orderId)
    {
        ItemStatus = "Ordered";
        OrderId = orderId;
    }
}
