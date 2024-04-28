using EcoVerse.BasketManagement.Application.DTOs;
using FluentValidation;

namespace EcoVerse.BasketManagement.Application.Validations;

public class UpdateCartValidator : AbstractValidator<UpdateCartDto>
{
    public UpdateCartValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0");
    }
}