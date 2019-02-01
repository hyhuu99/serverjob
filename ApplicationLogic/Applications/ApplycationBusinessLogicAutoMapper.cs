using ApplicationLogic.Applications.Messages;
using AutoMapper;
using Domain.Models;

namespace ApplicationLogic.Applications
{
    public class ApplycationBusinessLogicAutoMapper :Profile
    {
        public ApplycationBusinessLogicAutoMapper()
        {
            CreateMap<Application, CreateApplycationRequest>().ReverseMap();
            CreateMap<Application, GetApplycationReponse>().ReverseMap();
            CreateMap<Application, UpdateApplycationRequest>().ReverseMap();
        }
    }
}
