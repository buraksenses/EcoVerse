namespace EcoVerse.Shared.Messages;

public class AddItemToCartEvent
{
    public string UserId { get; set; }
    
    public Guid ProductId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}