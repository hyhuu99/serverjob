using ApplicationLogic.Applications.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationLogic.Applications
{
    public interface IApplycationBusinessLogic
    {
        Task CreateApplication(CreateApplycationRequest apply);

        Task<bool> IsExistApplyByJob(string jobId,string email);

        Task<List<GetApplycationReponse>> GetApplyByJob(string jobId);

        Task<GetApplycationReponse> GetApplyById(string applyId);
    }
}
