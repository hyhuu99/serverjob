using ApplicationLogic.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using ApplicationLogic.Applications;
using ApplicationLogic.Companys;
using ApplicationLogic.Jobs;
using ApplicationLogic.Commons;
using ApplicationLogic.Resumes;

namespace JobBoard.Service.Configs
{
    public class DIConfig
    {
        public static void Configure(IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IUserBusinessLogic, UserBusinessLogic>();
            services.AddTransient<IApplycationBusinessLogic, ApplycationBusinessLogic>();
            services.AddTransient<ICompanyBusinessLogic, CompanyBusinessLogic>();
            services.AddTransient<IJobBusinessLogic, JobBusinessLogic>();
            services.AddTransient<ICommonBusinessLogic, CommonBusinessLogic>();
            services.AddTransient<IResumesBusinessLogic, ResumesBusinessLogic>();
            services.AddTransient<IMongoDbRepository, MongoDbRepository>(n => new MongoDbRepository(config.GetValue<string>("MongoDb:DefaultConnectionString")));
        }
    }
}
