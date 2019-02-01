
using ApplicationLogic.Resumes.Messages;
using AutoMapper;
using Domain.Models;
using Domain.Models.GeneralModel.ResumesModel;

namespace ApplicationLogic.GroupCategorys
{
    public class ResumesBusinessLogicAutoMapper : Profile
    {
        public ResumesBusinessLogicAutoMapper()
        {
            CreateMap<ResumeRequest, Resume>().ReverseMap()
                .ForMember(dest => dest.ContactInfo,
                    opt => opt.MapFrom(src => src.ContactInfo))
                .ForMember(dest => dest.ExpInfo,
                    opt => opt.MapFrom(src => src.ExpInfo))
                .ForMember(dest => dest.Purpose,
                    opt => opt.MapFrom(src => src.Purpose));
            CreateMap<Resume, ResumeRequest>().ReverseMap()
                .ForMember(dest => dest.ContactInfo,
                    opt => opt.MapFrom(src => src.ContactInfo))
                .ForMember(dest => dest.ExpInfo,
                    opt => opt.MapFrom(src => src.ExpInfo))
                .ForMember(dest => dest.Purpose,
                    opt => opt.MapFrom(src => src.Purpose));
            //CreateMap<GroupCategory, GetGroupCategoryReponse>().ReverseMap();
            //CreateMap<GroupCategory, UpdateGroupCategoryRequest>().ReverseMap();
        }
    }
}
