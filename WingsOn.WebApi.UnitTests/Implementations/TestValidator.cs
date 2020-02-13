using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace WingsOn.WebApi.Tests.Implementations
{
    public class TestValidator<T>:IValidator<T>
    {
        public ValidationResult Validate(object instance)
        {
            return new ValidationResult();
        }

        public Task<ValidationResult> ValidateAsync(object instance, CancellationToken cancellation = new CancellationToken())
        {
            return Task.FromResult(new ValidationResult());
        }

        public ValidationResult Validate(ValidationContext context)
        {
            return new ValidationResult();
        }

        public Task<ValidationResult> ValidateAsync(ValidationContext context, CancellationToken cancellation = new CancellationToken())
        {
            return Task.FromResult(new ValidationResult());
        }

        public IValidatorDescriptor CreateDescriptor()
        {
            throw new NotImplementedException();
        }

        public bool CanValidateInstancesOfType(Type type)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Validate(T instance)
        {
            return new ValidationResult();
        }

        public Task<ValidationResult> ValidateAsync(T instance, CancellationToken cancellation = new CancellationToken())
        {
            return Task.FromResult(new ValidationResult());
        }

        public CascadeMode CascadeMode { get; set; }
    }
}