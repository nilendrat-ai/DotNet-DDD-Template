using FluentValidation;

namespace DotNet.DDD.Template.Application.Orders.Commands.CreateOrder;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required.")
            .MaximumLength(100).WithMessage("CustomerId must not exceed 100 characters.");
    }
}
