using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using YourProjectName.Infrastructure.Repositories;
using YourProjectName.Models;
using YourProjectName.ViewModels.MyData;

namespace YourProjectName.Services.Application
{
    public class MyDataService : IMyDataService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public MyDataService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<MyDataViewModel>> GetAllMyDataAsync(bool trackChanges)
        {
            var myData = await _repository.MyData.FindAll(trackChanges).ToListAsync();
            var myDataViewModels = _mapper.Map<IEnumerable<MyDataViewModel>>(myData);
            return myDataViewModels;
        }

        public async Task<MyDataViewModel> GetMyDataAsync(int id, bool trackChanges)
        {
            var myData = await GetMyDataAndCheckIfExists(id, trackChanges);
            var myDataViewModel = _mapper.Map<MyDataViewModel>(myData);
            return myDataViewModel;
        }

        public async Task<MyDataViewModel> CreateMyDataAsync(MyDataViewModel myDataViewModel, bool trackChanges)
        {
            var myDataEntity = _mapper.Map<MyData>(myDataViewModel);
            _repository.MyData.Create(myDataEntity);
            await _repository.SaveAsync();
            return myDataViewModel;
        }



        public async Task DeleteMyDataAsync(int id, bool trackChanges)
        {
            var myDataDb = await GetMyDataAndCheckIfExists(id, trackChanges);

            _repository.MyData.Delete(myDataDb);
            await _repository.SaveAsync();
        }

        public async Task UpdateMyDataAsync(MyDataViewModel myDataViewModel, bool trackChanges)
        {

            var myDataDb = await GetMyDataAndCheckIfExists(myDataViewModel.Id,trackChanges);

            _mapper.Map(myDataViewModel, myDataDb);

            await _repository.SaveAsync();
        }

        private async Task<MyData> GetMyDataAndCheckIfExists(int id, bool trackChanges)
        {
            var myData = await _repository.MyData.FindByCondition(md => md.Id.Equals(id), trackChanges).FirstOrDefaultAsync();
            if (myData is null)
                throw new Exception("My Data Not Found");

            return myData;
        }

    }
}
