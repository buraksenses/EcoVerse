using EcoVerse.BasketManagement.Application.DTOs;
using EcoVerse.Shared;
using FluentValidation;

namespace EcoVerse.BasketManagement.Application.Validations;

public class AddToCartValidator : AbstractValidator<AddToCartDto>
{
    public AddToCartValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThan(0m).WithMessage("Price must be greater than zero!");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero!");
        
        RuleFor(x => x.ProductId)
            .Must(Utils.BeAValidGuid).WithMessage("Invalid ID.");
    }
}