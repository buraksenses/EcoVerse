namespace EcoVerse.Shared.Exceptions;

public class CartItemConcurrencyException : Exception
{
    public CartItemConcurrencyException(string message) : base(message)
    {
        
    }
}