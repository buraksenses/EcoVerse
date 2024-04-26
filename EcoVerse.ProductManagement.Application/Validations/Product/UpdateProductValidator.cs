using EcoVerse.ProductManagement.Application.DTOs.Product;
using FluentValidation;

namespace EcoVerse.ProductManagement.Application.Validations.Product;

public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");

        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Quantity is required.")
            .GreaterThan(0).WithMessage("Quantity must be greater than zero!");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required.")
            .GreaterThan(0).WithMessage("Price must be greater than zero!")
            .LessThan(1000000m).WithMessage("Price must be less than 1000000!");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(10, 100).WithMessage("Description must be between 3 and 100 characters.");
    }
}