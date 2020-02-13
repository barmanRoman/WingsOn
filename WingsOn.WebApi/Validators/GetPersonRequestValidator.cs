using FluentValidation;
using WingsOn.WebApi.Requests;

namespace WingsOn.WebApi.Validators
{
    public class GetPersonRequestValidator:AbstractValidator<GetPersonRequest>
    {
        public GetPersonRequestValidator()
        {
            RuleFor(r => r.Id).SetValidator(new IdValidator());
        }
    }
}