namespace Domain.Guards;

public static class ValidationConstants
{
    public static class Email
    {
        public const string EmailRegex = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$";
        public const int MaxLength = 256;
    }
    public static class Password
    {
        public const int MinLength = 8;
        public const int MaxLength = 100;
    }
}
