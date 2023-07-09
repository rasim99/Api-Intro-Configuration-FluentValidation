using FluentValidation;

namespace FirstApiProject.Dtos.Product
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
    }
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("bosh qoyma")
                .MaximumLength(20).WithMessage("20den yuxari ola bilmez");
            RuleFor(p => p.SalePrice).NotNull().WithMessage("bosh ola bilmez")
                .GreaterThanOrEqualTo(0).WithMessage("0den boyuk olmalidir");
            RuleFor(p => p.CostPrice).NotNull().WithMessage("bosh ola bilmez")
                .GreaterThanOrEqualTo(0).WithMessage("0den boyuk olmalidir");
            RuleFor(p => p).Custom((p, context) =>
            {
                if (p.CostPrice > p.SalePrice)
                {
                    context.AddFailure("CostPrice" ,"CostPrice SalesPriceden kicik olmalidir");
                }
            });
        }
    }
}
