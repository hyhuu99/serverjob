using Domain.Models;
using Domain.Models.Common;

namespace ApplicationLogic.Resumes.Messages
{
    public class CreateResumeStep3Request
    {
        public City[] PlaceOfWorks { get; set; }
        public Dropdown[] Categorys { get; set; }
        public Dropdown LevelOfWork { get; set; }
        public Dropdown TypeOfWork { get; set; }
        public string Salary { get; set; }
        public string Ambition { get; set; }
        public bool CanChangePlace { get; set; }
    }
}
