using ApplicationLogic.Jobs.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationLogic.Jobs
{
    public interface IJobBusinessLogic
    {
        Task CreateJob(CreateJobRequest job);
        Task<GetJobReponseForUpdate> GetJobById(string id);
        Task UpdateJob(UpdateJobRequest job);
        Task DeleteJob(string jobId);
        Task<List<GetJobReponse>> GetJobs(GetJobsRequest request);
        Task<List<GetJobReponse>> GetJobsByCompanyId(string companyId);
        Task<List<GetJobLocationsResponse>> GetJobLocations();
        Task<List<GetJobCategoriesResponse>> GetJobCategories();
        Task<IList<GetLocationResponse>> GetLocations();
        Task<IList<GetCategoryResponse>> GetCategories();
    }
}
