using YourProjectName.Infrastructure.DataContext;
using YourProjectName.Infrastructure.Repositories.Application;


namespace YourProjectName.Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly YourProjectNameDbContext _repositoryContext;
        private readonly Lazy<IMyDataRepository> _myDataRepository;

        public RepositoryManager(YourProjectNameDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _myDataRepository = new Lazy<IMyDataRepository>(() => new MyDataRepository(repositoryContext));
            
        }

        public IMyDataRepository MyData => _myDataRepository.Value;
        public async Task SaveAsync()
        {
            await _repositoryContext.SaveChangesAsync();
        }
    }
}
