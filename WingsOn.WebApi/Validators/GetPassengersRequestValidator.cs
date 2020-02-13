using FluentValidation;
using WingsOn.WebApi.Requests;

namespace WingsOn.WebApi.Validators
{
    public class GetPassengersRequestValidator:AbstractValidator<GetPassengersRequest>
    {
        public GetPassengersRequestValidator()
        {
            RuleFor(r => r.Number).NotEmpty().WithMessage("Cannot be null or empty");
        }
    }
}