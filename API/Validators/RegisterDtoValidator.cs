using Core.DTOs;
using FluentValidation;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Ad boş olamaz.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Geçerli bir e-posta girin.");
        RuleFor(x => x.Password).MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalı.");
        RuleFor(x => x.Role).NotEmpty().WithMessage("Rol boş olamaz.");
    }
}