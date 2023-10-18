using System.Threading.Tasks;

namespace Contracts;

public interface IRepositoryManager
{
    IUserRepository User { get; }
    Task SaveAsync();
}
