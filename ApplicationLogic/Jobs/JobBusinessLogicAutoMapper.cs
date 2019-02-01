using ApplicationLogic.Jobs.Messages;
using AutoMapper;
using Domain.Models;

namespace ApplicationLogic.Jobs
{
    public class JobBusinessLogicAutoMapper : Profile
    {
        public JobBusinessLogicAutoMapper()
        {
            CreateMap<Job, CreateJobRequest>().ReverseMap();
            CreateMap<Job, GetJobReponse>().ReverseMap();
            CreateMap<Job, UpdateJobRequest>().ReverseMap();
            CreateMap<Job, GetJobReponseForUpdate>().ReverseMap();
        }
    }
}
