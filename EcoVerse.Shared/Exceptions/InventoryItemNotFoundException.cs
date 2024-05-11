namespace EcoVerse.Shared.Exceptions;

public class InventoryItemNotFoundException : Exception
{
    public InventoryItemNotFoundException(string message) : base(message)
    {
        
    }
}