using EcoVerse.StockManagement.Command.Application.Commands;
using EcoVerse.StockManagement.Command.Application.DTOs;
using FluentValidation;

namespace EcoVerse.StockManagement.Command.Application.Validations;

public class UpdateInventoryItemQuantityValidator : AbstractValidator<UpdateInventoryItemQuantityCommand>
{
    public UpdateInventoryItemQuantityValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Item quantity must be greater than zero!");
    }
}