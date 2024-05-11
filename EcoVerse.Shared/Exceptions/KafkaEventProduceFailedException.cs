namespace EcoVerse.Shared.Exceptions;

public class KafkaEventProduceFailedException : Exception
{
    public KafkaEventProduceFailedException(string message) : base(message)
    {
        
    }
}