using AutoMapper;
using YourProjectName.Models;
using YourProjectName.ViewModels.MyData;

namespace YourProjectName.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<MyData, MyDataViewModel>();
            CreateMap<MyData, MyDataViewModel>().ReverseMap();
            CreateMap<MyDataViewModel, MyData>();
        }
    }
}
