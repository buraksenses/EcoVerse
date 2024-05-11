namespace EcoVerse.Shared.Messages;

public class StockCheckResponseEvent
{
    public string UserId { get; set; }
    
    public Guid ProductId { get; set; }

    public int StockQuantity { get; set; }

    public int CartQuantity { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool IsInStock { get; set; }
}