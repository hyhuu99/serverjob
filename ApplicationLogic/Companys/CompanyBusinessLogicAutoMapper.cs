using ApplicationLogic.Companys.Messages;
using AutoMapper;
using Domain.Models;

namespace ApplicationLogic.Companys
{
    public class CompanyBusinessLogicAutoMapper : Profile
    {
        public CompanyBusinessLogicAutoMapper()
        {
            CreateMap<Company, CreateCompanyRequest>().ReverseMap();
            CreateMap<Company, GetCompanyReponse>().ReverseMap();
            CreateMap<Company, UpdateCompanyRequest>().ReverseMap();
        }
    }
}
