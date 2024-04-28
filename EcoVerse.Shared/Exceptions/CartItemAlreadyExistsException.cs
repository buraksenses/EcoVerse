namespace EcoVerse.Shared.Exceptions;

public class CartItemAlreadyExistsException : Exception
{
    public CartItemAlreadyExistsException(string message) : base(message)
    {
        
    }
}