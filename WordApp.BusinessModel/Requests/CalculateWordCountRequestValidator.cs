using FluentValidation;

namespace WordApp.BusinessModel.Requests
{
    public class CalculateWordCountRequestValidator : AbstractValidator<CalculateWordCountRequest>
    {
        public CalculateWordCountRequestValidator()
        {
            RuleFor(request => request.InputText).NotNull().WithMessage("InputTextMandatoryValidationMessage");
           
        }
    }
}
