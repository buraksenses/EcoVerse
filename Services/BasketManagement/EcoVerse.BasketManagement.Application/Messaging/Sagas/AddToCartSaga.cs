using EcoVerse.BasketManagement.Application.Messaging.Sagas.States;
using EcoVerse.Shared.Messages;
using MassTransit;

namespace EcoVerse.BasketManagement.Application.Messaging.Sagas;

public class AddToCartSaga : MassTransitStateMachine<AddToCartSagaState>
{
    public State AwaitingProductVerification { get; private set; }
    public State AwaitingInventoryCheck { get; private set; }

    public Event<ProductCheckResponseEvent> ProductCheckResponseEvent { get; private set; }
    public Event<StockCheckResponseEvent> StockCheckResponseEvent { get; private set; }
    public Event<AddItemToCartEvent> AddItemToCartEvent { get; private set; }

    // public AddToCartSaga()
    // {
    //     Event(() => AddItemToCartEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
    //     Event(() => ProductCheckResponseEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
    //     Event(() => StockCheckResponseEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
    //
    //     InstanceState(x => x.CurrentState);
    //
    //     Initially(
    //         When(AddItemToCartEvent)
    //             .Then(context =>
    //             {
    //                 context.Saga.ProductId = context.Message.ProductId;
    //                 context.Saga.Quantity = context.Message.Quantity;
    //                 context.Saga.UserId = context.Message.UserId;
    //             })
    //             .Publish(context => new VerifyStockCommand
    //             {
    //                 ProductId = context.Saga.ProductId,
    //                 Quantity = context.Saga.Quantity
    //             })
    //             .TransitionTo(AwaitingInventoryCheck)
    //     );
    //     
    //     During(AwaitingInventoryCheck,
    //         When(StockCheckResponseEvent)
    //             .Then(context =>
    //             {
    //                 if (!context.Message.IsInStock)
    //                     throw new InvalidOperationException("Insufficient stock.");
    //             })
    //             .Publish(context => new VerifyProductCommand
    //             {
    //                 ProductId = context.Saga.ProductId,
    //                 Quantity = context.Saga.Quantity,
    //                 Price = context.Saga.Price,
    //                 UserId = context.Saga.UserId
    //             })
    //             .TransitionTo(AwaitingProductVerification)
    //         
    //     );
    //     During(AwaitingProductVerification,
    //         When(ProductCheckResponseEvent)
    //             .Then(context =>
    //             {
    //                 if (!context.Message.Exists)
    //                     throw new InvalidOperationException("Product does not exist.");
    //             })
    //             .Publish(context => new ProductVerified
    //             {
    //                 ProductId = context.Saga.ProductId,
    //                 Price = context.Saga.Price,
    //                 Quantity = context.Saga.Quantity,
    //                 UserId = context.Saga.UserId,
    //                 Exists = true
    //             })
    //             .Finalize()
    //     );
    //     
    //     SetCompletedWhenFinalized();
    // }
}