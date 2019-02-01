
using Domain.Models.Enum;

namespace ApplicationLogic.Resumes.Messages
{
    public class SearchResumeRequest
    {
        public string Title { get; set; }
        public string Level { get; set; }
        public string[] CategoryIds { get; set; }
        public string[] LocationIds { get; set; }
    }
}
