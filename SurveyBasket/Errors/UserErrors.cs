namespace SurveyBasket.Errors;
public static class UserErrors
{
    public static readonly Error InvalidCredentials =
        new("User.InvalidCredentials", "Invalid Credentials", StatusCodes.Status400BadRequest);

    public static readonly Error UserNotFound =
        new("User.UserNotFound", "No User Found With The Given ID", StatusCodes.Status404NotFound);

    public static readonly Error RefreshTokenNotFound =
        new("User.RefreshTokenNotFound", "Refresh Token Not Found", StatusCodes.Status404NotFound);
}