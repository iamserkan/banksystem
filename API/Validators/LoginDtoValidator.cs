using Core.DTOs;
using FluentValidation;

namespace API.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş olamaz")
            .EmailAddress().WithMessage("Geçersiz email formatı");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre boş olamaz");
    }
}