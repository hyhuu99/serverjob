using ApplicationLogic.Companys;
using ApplicationLogic.Jobs;
using ApplicationLogic.Users;
using ApplicationLogic.Commons;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationLogic
{
    public class AutoMapperConfig
    {
        public static void Configure(IServiceCollection services)
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<UserBusinessLogicAutoMapper>();
                c.AddProfile<CompanyBusinessLogicAutoMapper>(); 
                c.AddProfile<JobBusinessLogicAutoMapper>();
                c.AddProfile<CommonBusinessLogicAutoMapper>();
            });

            services.AddAutoMapper(n => config.CreateMapper());
        }
    }
}
