using FluentValidation;
using WingsOn.WebApi.Requests;

namespace WingsOn.WebApi.Validators
{
    public class UpdatePersonAddressRequestValidator:AbstractValidator<UpdatePersonAddressRequest>
    {
        public UpdatePersonAddressRequestValidator()
        {
            RuleFor(r => r.Id).SetValidator(new IdValidator());
            RuleFor(r => r.Address).NotNull().WithMessage("Cannot be null")
                .EmailAddress().WithMessage("Not valid email address");
        }
    }
}