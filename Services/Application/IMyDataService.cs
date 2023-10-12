using YourProjectName.ViewModels.MyData;

namespace YourProjectName.Services.Application
{
    public interface IMyDataService
    {
        Task<IEnumerable<MyDataViewModel>> GetAllMyDataAsync( bool trackChanges);
        Task<MyDataViewModel> GetMyDataAsync(int id, bool trackChanges);
        Task<MyDataViewModel> CreateMyDataAsync(MyDataViewModel myDataViewModel, bool trackChanges);
        Task DeleteMyDataAsync(int id, bool trackChanges);
        Task UpdateMyDataAsync(MyDataViewModel myDataViewModel, bool trackChanges);
   
    }
}
