using System;

namespace CRM.Shared.ValidationModel
{
    public class ValidationException : Exception
    {
        public ValidationResultModel ValidationResultModel { get; private set; }
        public ValidationException(ValidationResultModel validationResultModel)
        {
            ValidationResultModel = validationResultModel;
        }
    }
}