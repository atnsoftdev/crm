using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace CRM.Shared.ValidationModel
{
    public class ValidationResultModel
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;
        public string Message { get; set; } = "Validation Failed";
        public List<ValidationError> Errors { get; }
        public ValidationResultModel(ValidationResult result = null)
        {
            Errors = result.Errors
                .Select(err => new ValidationError(err.PropertyName, err.ErrorMessage))
                .ToList()
                ?? new List<ValidationError>();
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}