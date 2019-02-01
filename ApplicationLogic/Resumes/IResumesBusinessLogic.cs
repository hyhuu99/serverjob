using System.Collections.Generic;
using ApplicationLogic.Resumes.Messages;
using System.Threading.Tasks;

namespace ApplicationLogic.Resumes
{
    public interface IResumesBusinessLogic
    {
        Task<string> CreateResumes(ResumeRequest resumeStep1);
        Task UpdateResumes(ResumeRequest resumeStep);
        Task<List<ResumeRequest>> GetResumesByUser(string email);
        Task<ResumeRequest> GetResumesById(string id);
        Task<List<ResumeRequest>> SearchResume(SearchResumeRequest searchRequest);
    }
}
