using Domain.Models;
using Domain.Models.Common;

namespace ApplicationLogic.Resumes.Messages
{
    public class CreateResumeStep1Request
    {
        public string FullName { get; set; }
        public Dropdown Gender { get; set; }
        public string BirthDay { get; set; }
        public Dropdown RelationShip { get; set; }
        public Dropdown Nationality { get; set; }
        public string Address { get; set; }
        public Dropdown Country { get; set; }
        public City City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
    }
}
