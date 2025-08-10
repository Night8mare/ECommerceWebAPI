namespace CleanArchEcommerce.Domain.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public decimal PurchasePrice { get; private set; }

    public int StockQuantity { get; private set; }

    public string IsAvailable { get; private set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    public Product() { }
    public void UpdateProductQuantity(int quantity)
    {
        StockQuantity = StockQuantity - quantity;
    }
    public void UpdateProductAvaible(int quantity, string isAvailable)
    {
        StockQuantity = StockQuantity - quantity;
        IsAvailable = isAvailable;
    }
}
