using EcoVerse.Shared;
using EcoVerse.StockManagement.Command.Application.Commands;
using FluentValidation;

namespace EcoVerse.StockManagement.Command.Application.Validations;

public class AddInventoryItemValidator : AbstractValidator<AddInventoryItemCommand>
{
    public AddInventoryItemValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0");
        
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.Description)
            .Length(10, 200)
            .WithMessage(
                "Item description must be minimum length of 10 characters and maximum length of 200 characters!");
        
        RuleFor(x => x.Name)
            .Length(3, 100)
            .WithMessage(
                "Item name must be minimum length of 3 characters and maximum length of 100 characters!");
        
        RuleFor(x => x.ProductId)
            .Must(Utils.BeAValidGuid)
            .WithMessage("Item id must be a valid guid!");
    }
}