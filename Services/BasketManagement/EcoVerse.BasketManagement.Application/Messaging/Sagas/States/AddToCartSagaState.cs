using MassTransit;

namespace EcoVerse.BasketManagement.Application.Messaging.Sagas.States;

public class AddToCartSagaState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string UserId { get; set; }
    public decimal Price { get; set; }
}