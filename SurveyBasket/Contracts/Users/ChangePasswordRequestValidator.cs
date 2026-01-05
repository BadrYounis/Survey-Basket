namespace SurveyBasket.Contracts.Users;
public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.CurrentPassword)
           .NotEmpty();

        RuleFor(x => x.NewPassword)
           .NotEmpty()
           .Matches(RegexPatterns.Password)
           .WithMessage("Passworsd should at least 8 digits and should contains Lowercase, Nonalphanumeric and Uppercase")
           .NotEqual(x => x.CurrentPassword)
           .WithMessage("New password cannot be the same as the current password");
    }
}