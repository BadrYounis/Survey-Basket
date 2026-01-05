namespace SurveyBasket.Errors;
public record UserErrors
{
    public static readonly Error InvalidCredentials =
        new("User.InvalidCredentials", "Invalid Credentials", StatusCodes.Status401Unauthorized);

    public static readonly Error DisabledUser =
        new("User.DisabledUser", "Disabled user, Please contact your administrator", StatusCodes.Status401Unauthorized);

    public static readonly Error LockedUser =
        new("User.LockedUser", "Locked user, Please contact your administrator", StatusCodes.Status401Unauthorized);

    public static readonly Error UserNotFound =
        new("User.UserNotFound", "No User Found With The Given ID", StatusCodes.Status401Unauthorized);

    public static readonly Error RefreshTokenNotFound =
        new("User.RefreshTokenNotFound", "Refresh Token Not Found", StatusCodes.Status401Unauthorized);

    public static readonly Error DuplicatedEmail =
        new("User.DuplicatedEmail", "This Email Is Already Taken", StatusCodes.Status409Conflict);

    public static readonly Error EmailNotConfirmed =
            new("User.EmailNotConfirmed", "Email Not Confirmed", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidCode =
        new("User.InvalidCode", "Invalid Code", StatusCodes.Status401Unauthorized);

    public static readonly Error DuplicatedConfirmation =
        new("User.DuplicatedConfirmation", "This Email Already Confirmed Before", StatusCodes.Status400BadRequest);

    public static readonly Error InvalidRoles =
        new("User.InvalidRoles", "Invalid Roles", StatusCodes.Status400BadRequest);
}