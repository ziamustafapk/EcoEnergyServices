using YourProjectName.Services.Application;

namespace YourProjectName.Services
{
    public interface IServiceManager
    {
        IMyDataService MyDataService { get; }
    }
}
