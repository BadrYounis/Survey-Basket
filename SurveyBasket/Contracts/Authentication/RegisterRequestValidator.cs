using SurveyBasket.Abstractions.Consts;

namespace SurveyBasket.Contracts.Authentication;
public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
           .NotEmpty()
           .Matches(RegexPatterns.Password)
           .WithMessage("Passworsd should at least 8 digits and should contains Lowercase, Nonalphanumeric and Uppercase");

        RuleFor(x => x.FirstName)
          .NotEmpty()
          .Length(3,100);

        RuleFor(x => x.LastName)
           .NotEmpty()
           .Length(3, 100);
    }
}