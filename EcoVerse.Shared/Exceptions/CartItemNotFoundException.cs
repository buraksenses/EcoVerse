namespace EcoVerse.Shared.Exceptions;

public class CartItemNotFoundException : Exception
{
    public CartItemNotFoundException(string message) : base(message)
    {
        
    }
}