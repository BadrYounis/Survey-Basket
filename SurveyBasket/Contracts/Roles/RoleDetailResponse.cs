namespace SurveyBasket.Contracts.Roles;
public record RoleDetailResponse(
    string Id,
    string Name,
    bool isDeleted,
    IEnumerable<string> Permissions
);