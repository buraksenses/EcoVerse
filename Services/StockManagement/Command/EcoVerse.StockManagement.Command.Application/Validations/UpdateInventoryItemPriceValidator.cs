using EcoVerse.StockManagement.Command.Application.Commands;
using EcoVerse.StockManagement.Command.Application.DTOs;
using FluentValidation;

namespace EcoVerse.StockManagement.Command.Application.Validations;

public class UpdateInventoryItemPriceValidator : AbstractValidator<UpdateInventoryItemPriceCommand>
{
    public UpdateInventoryItemPriceValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThan(0m)
            .WithMessage("Item price must be greater than zero!");
    }
}