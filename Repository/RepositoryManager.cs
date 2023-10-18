using System;
using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext repositoryContext;
    private IUserRepository userRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        this.repositoryContext = repositoryContext ??
            throw new ArgumentNullException(nameof(repositoryContext));
    }

    public IUserRepository User
    {
        get
        {
            if(userRepository is null) 
            {
                userRepository = new UserRepository(repositoryContext);
            }

            return userRepository;
        }
    }

    public Task SaveAsync() => repositoryContext.SaveChangesAsync();
}

