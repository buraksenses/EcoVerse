using EcoVerse.ProductManagement.Application.DTOs.Category;
using FluentValidation;

namespace EcoVerse.ProductManagement.Application.Validations.Category;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(10, 100).WithMessage("Description must be between 3 and 100 characters.");
    }
}