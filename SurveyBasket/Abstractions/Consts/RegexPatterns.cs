namespace SurveyBasket.Abstractions.Consts;
public static class RegexPatterns
{
    public const string Password = "(?=.*[0-9])(?=.*[!@#$%^&*()\\\\[\\]{}\\\\\\-+=~`'\";:,.<>/?])(?=.*[a-z])(?=.*[A-Z])(?=(.*)).{8,}";
}