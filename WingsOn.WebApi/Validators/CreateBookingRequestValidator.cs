using FluentValidation;
using WingsOn.WebApi.Requests;

namespace WingsOn.WebApi.Validators
{
    public class CreateBookingRequestValidator:AbstractValidator<CreateBookingRequest>
    {
        public CreateBookingRequestValidator()
        {
            RuleFor(r => r.Model).NotNull().WithMessage("Cannot be null");
            RuleFor(r => r.Model.Customer).SetValidator(new IdValidator()).OverridePropertyName("Customer");
            RuleFor(r => r.Model.Flight).SetValidator(new IdValidator()).OverridePropertyName("Flight");
            RuleFor(r => r.Model.Passengers).NotEmpty().WithMessage("Cannot be empty").OverridePropertyName("Passengers");
            RuleForEach(r => r.Model.Passengers).SetValidator(new IdValidator()).OverridePropertyName("Passengers");
            RuleFor(r => r.Model.Number).NotEmpty().WithMessage("Cannot be null or empty").OverridePropertyName("Number");
        }
    }
}