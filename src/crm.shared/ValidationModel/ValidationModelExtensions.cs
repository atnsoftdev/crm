using System.Threading.Tasks;
using FluentValidation;

namespace CRM.Shared.ValidationModel
{
    public static class ValidationModelExtensions
    {
        public static async Task HandleValidation<TRequest>(this IValidator<TRequest> validator, TRequest request)
        {
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ValidationException(new ValidationResultModel(result));
            }
        }
    }
}