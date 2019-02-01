using Domain.Models.GeneralModel.ResumesModel;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models.Enum;

namespace Domain.Models
{
    public class Resume : IAggregateRoot
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public ExpInfo ExpInfo { get; set; }
        public Purpose Purpose { get; set; }
        public string Email { get; set; }
        public ResumeStatus Status { get; set; }
        public bool CanSearch { get; set; }
    }
}
