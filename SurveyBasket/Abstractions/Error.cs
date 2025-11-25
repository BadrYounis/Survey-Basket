namespace SurveyBasket.Abstractions;
public record Error(string Code, string Describtion)
{
    public static readonly Error None = new(string.Empty, string.Empty);
}