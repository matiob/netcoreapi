namespace Library.Services.Auth
{
    public interface ILoginService
    {
        Task<string> ValidateUserAsync(string username, string password);

    }
}
