namespace SurveyBasket.Errors;
public static class PollErrors
{
    public static readonly Error PollNotFound =
        new("Poll.NotFound", "No Poll Found With The Given ID");
}