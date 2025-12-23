namespace SurveyBasket.Errors;
public static class RoleErrors
{
    public static readonly Error RoleNotFound =
        new("Role.RoleNotFound", "Role Is Not Found", StatusCodes.Status404NotFound);

    //public static readonly Error UserNotFound =
    //    new("User.UserNotFound", "No User Found With The Given ID", StatusCodes.Status401Unauthorized);
    //
    //public static readonly Error RefreshTokenNotFound =
    //    new("User.RefreshTokenNotFound", "Refresh Token Not Found", StatusCodes.Status401Unauthorized);
    //
    public static readonly Error DuplicatedRole =
        new("Role.DuplicatedRole", "This Role With The Same Name Is Already Exists", StatusCodes.Status409Conflict);
    //
    //public static readonly Error EmailNotConfirmed =
    //        new("User.EmailNotConfirmed", "Email Not Confirmed", StatusCodes.Status401Unauthorized);
    //
    public static readonly Error InvalidPermissions =
        new("Role.InvalidPermissions", "Invalid Permissions", StatusCodes.Status400BadRequest);
    //
    //public static readonly Error DuplicatedConfirmation =
    //    new("User.DuplicatedConfirmation", "This Email Already Confirmed Before", StatusCodes.Status400BadRequest);
}