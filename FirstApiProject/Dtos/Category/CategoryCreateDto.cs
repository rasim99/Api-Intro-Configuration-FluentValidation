using FluentValidation;

namespace FirstApiProject.Dtos.Category
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("bosh qoyma")
                .MaximumLength(20).WithMessage("20 den yuxari olmaz");
        }
    }
}
