using YourProjectName.Infrastructure.DataContext;
using YourProjectName.Infrastructure.Repositories.Base;

using YourProjectName.Models;

namespace YourProjectName.Infrastructure.Repositories.Application
{
    public  class MyDataRepository : RepositoryBase<MyData>, IMyDataRepository
    {
        public MyDataRepository(YourProjectNameDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
