namespace SurveyBasket.Abstractions.Consts;
public static class DefaultRoles
{
    public partial class Admin
    {
        public const string Name = nameof(Admin);
        public const string Id = "019b3459-1d08-7535-ac15-577491d6c0e7";
        public const string ConcurrencyStamp = "019b3459-1d08-7535-ac15-577371ef735c";
    }
    public partial class Member
    {
        public const string Name = nameof(Member);
        public const string Id = "019b3459-1d08-7535-ac15-5772ce43ae2a";
        public const string ConcurrencyStamp = "019b3459-1d08-7535-ac15-577191a38d8e";
    }
}