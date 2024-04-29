namespace EcoVerse.Shared.Messages;

public class AddItemToCartCommand
{
    public string UserId { get; set; }
    
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}