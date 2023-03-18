using Financify_Api.Models;

namespace Financify_Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User Get(string username, string password);
    }
}
