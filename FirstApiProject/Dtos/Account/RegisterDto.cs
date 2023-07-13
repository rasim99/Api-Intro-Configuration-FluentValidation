using FluentValidation;

namespace FirstApiProject.Dtos.Account
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage("dont empty")
                .MaximumLength(20).WithMessage("cant high 20 ");
            
            RuleFor(r => r.FullName)
                .NotEmpty().WithMessage("dont empty")
                .MaximumLength(20).WithMessage("cant high 20 ");

            RuleFor(r => r.Password)
               .NotEmpty().WithMessage("dont empty")
               .MaximumLength(20).WithMessage("cant high 20 ")
               .MinimumLength(8).WithMessage("cant less 8 "); 

            RuleFor(r => r.RePassword)
               .NotEmpty().WithMessage("dont empty")
               .MaximumLength(20).WithMessage("cant high 20 ")
               .MinimumLength(8).WithMessage("cant less 8 ");

            RuleFor(z => z).Custom((z, context) =>
            {
                if (z.Password!=z.RePassword)
                {
                    context.AddFailure("RePassword", "passwords are not the same");
                }
            });
        }
    }
}
