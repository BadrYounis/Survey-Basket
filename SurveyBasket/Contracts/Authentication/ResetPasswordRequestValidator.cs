using SurveyBasket.Abstractions.Consts;

namespace SurveyBasket.Contracts.Authentication;
public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Code)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
           .NotEmpty()
           .Matches(RegexPatterns.Password)
           .WithMessage("Passworsd should at least 8 digits and should contains Lowercase, Nonalphanumeric and Uppercase");
    }
}