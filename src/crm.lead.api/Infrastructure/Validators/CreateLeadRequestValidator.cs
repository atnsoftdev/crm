using FluentValidation;
using LeadApi;

namespace CRM.Lead.Api.Infrastructure.Validators
{
    public class CreateLeadRequestValidator : AbstractValidator<CreateLeadRequest>
    {
        public CreateLeadRequestValidator()
        {
            RuleFor(x => x.LeadOwner)
                .NotNull()
                .NotEmpty()
                .WithMessage("Lead owner not be null or empty");
        }
    }
}