using AutoMapper;
using YourProjectName.Infrastructure.Repositories;
using YourProjectName.Services.Application;

namespace YourProjectName.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IMyDataService> _myDataService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _myDataService = new Lazy<IMyDataService>(() =>
                new MyDataService(repositoryManager,mapper));
           
        }

        public IMyDataService MyDataService => _myDataService.Value;
    }
}
