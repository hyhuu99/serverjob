
using Domain.Models.Enum;

namespace ApplicationLogic.Resumes.Messages
{
    public class ResumeRequest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public CreateResumeStep1Request ContactInfo { get; set; }
        public CreateResumeStep2Request ExpInfo { get; set; }
        public CreateResumeStep3Request Purpose { get; set; }
        public string Email { get; set; }
        public ResumeStatus Status { get; set; }
        public bool CanSearch { get; set; }
    }
}
