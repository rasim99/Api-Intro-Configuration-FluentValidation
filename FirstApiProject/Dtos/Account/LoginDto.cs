using FluentValidation;

namespace FirstApiProject.Dtos.Account
{
    public class LoginDto
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(l => l.UserNameOrEmail)
              .NotEmpty().WithMessage("dont be empty");
            
            RuleFor(l => l.Password)
              .NotEmpty().WithMessage("dont be empty");
        }
    }

}
