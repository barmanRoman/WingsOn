using FluentValidation;

namespace WingsOn.WebApi.Validators
{
    public class IdValidator:AbstractValidator<int>
    {
        public IdValidator()
        {
            RuleFor(id=>id).GreaterThan(0).WithMessage("Must be greater than 0");
        }
    }
}