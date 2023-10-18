using System.Threading;
using System.Threading.Tasks;
using Entities.Models.User;

namespace Contracts;

public interface IUserRepository
{
    Task<User> LoginAsync(
        string username,
        string password,
        bool tracking,
        CancellationToken cancellationToken = default);
}
