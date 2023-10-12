using YourProjectName.Infrastructure.Repositories.Base;
using YourProjectName.Models;

namespace YourProjectName.Infrastructure.Repositories.Application
{
    public interface IMyDataRepository : IRepositoryBase<MyData>
    {
    }
}
