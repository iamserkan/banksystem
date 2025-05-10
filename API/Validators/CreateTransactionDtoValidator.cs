using Core.DTOs;
using FluentValidation;

namespace API.Validators;

public class CreateTransactionDtoValidator : AbstractValidator<CreateTransactionDto>
{
    public CreateTransactionDtoValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Tutar 0'dan büyük olmalı");

        RuleFor(x => x.Type)
            .NotEmpty()
            .Must(t => new[] { "Deposit", "Withdraw", "Transfer" }.Contains(t))
            .WithMessage("Geçerli bir işlem türü seçilmelidir");
    }
}