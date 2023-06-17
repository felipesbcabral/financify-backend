namespace Financify_Api.Repositories.Interfaces
{
    public interface ITokenService
    {
        string GenerateRandomToken();
        void CreatePasswordHash(string password, out string passwordHash);
    }
}
