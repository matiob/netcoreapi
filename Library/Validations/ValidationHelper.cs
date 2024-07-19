namespace Library.Validations
{
    public static class ValidationHelper
    {
        public static bool BeValidDate(string date)
        {
            return DateTime.TryParse(date, out _);
        }
    }
}
