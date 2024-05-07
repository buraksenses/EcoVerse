using EcoVerse.Shared;
using EcoVerse.StockManagement.Command.Application.Commands;
using EcoVerse.StockManagement.Command.Application.DTOs;
using FluentValidation;

namespace EcoVerse.StockManagement.Command.Application.Validations;

public class RemoveInventoryItemValidator : AbstractValidator<RemoveInventoryItemCommand>
{
    public RemoveInventoryItemValidator()
    {
        RuleFor(x => x.Id)
            .Must(Utils.BeAValidGuid)
            .WithMessage("Item id must be a valid guid!");
    }
}