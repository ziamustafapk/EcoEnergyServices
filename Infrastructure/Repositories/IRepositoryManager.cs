using YourProjectName.Infrastructure.Repositories.Application;


namespace YourProjectName.Infrastructure.Repositories
{
    public interface IRepositoryManager
    {
        IMyDataRepository MyData { get; }
        Task SaveAsync();
    }
}
