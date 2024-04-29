using EcoVerse.BasketManagement.Application.Interfaces;
using EcoVerse.ProductManagement.Application.Sagas;
using EcoVerse.Shared.Messages;
using MassTransit;

namespace EcoVerse.BasketManagement.Application.Sagas;

public class AddToCartSaga : MassTransitStateMachine<AddToCartSagaState>
{
    public State AwaitingProductVerification { get; private set; }
    public State AwaitingInventoryCheck { get; private set; }

    public Event<ProductCheckResponseEvent> ProductVerified { get; private set; }
    //public Event<InventoryChecked> InventoryChecked { get; private set; }
    public Event<AddItemToCartCommand> AddToCart { get; private set; }

    public AddToCartSaga()
    {
        Event(() => AddToCart, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => ProductVerified, x => x.CorrelateById(context => context.Message.CorrelationId));
        
        InstanceState(x => x.CurrentState);

        Initially(
            When(AddToCart)
                .Then(context =>
                {
                    context.Saga.ProductId = context.Message.ProductId;
                    context.Saga.Quantity = context.Message.Quantity;
                    context.Saga.UserId = context.Message.UserId;
                })
                .Publish(context => new VerifyProductCommand
                {
                    ProductId = context.Saga.ProductId,
                    Price = context.Saga.Price,
                    Quantity = context.Saga.Quantity,
                    UserId = context.Saga.UserId
                })
                .TransitionTo(AwaitingProductVerification)
        );

        During(AwaitingProductVerification,
            When(ProductVerified)
                .Then(context =>
                {
                    if (!context.Message.Exists)
                        throw new InvalidOperationException("Product does not exist.");
                })
                .Publish(context => new ProductVerified
                {
                    ProductId = context.Saga.ProductId,
                    Price = context.Saga.Price,
                    Quantity = context.Saga.Quantity,
                    UserId = context.Saga.UserId,
                    Exists = true
                })
                .Finalize()
        );

        SetCompletedWhenFinalized();
        // During(AwaitingInventoryCheck,
        //     When(InventoryChecked)
        //         .Then(context =>
        //         {
        //             if (!context.Message.IsInStock)
        //                 throw new InvalidOperationException("Insufficient stock.");
        //         })
        //         .Finalize()
        // );

    }
}