namespace EcoVerse.Shared.Exceptions;

public class AggregateNotFoundException : Exception
{
    public AggregateNotFoundException(string message) : base(message)
    {

    }
}