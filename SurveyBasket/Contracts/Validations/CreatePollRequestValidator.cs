namespace SurveyBasket.Contracts.Validations;
public class CreatePollRequestValidator : AbstractValidator<CreatePollRequest>
{
    public CreatePollRequestValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty()
            .Length(3, 100);

        RuleFor(p => p.Description)
            .NotEmpty()
            .Length(3, 1000);
    }
}