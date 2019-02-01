using Domain.Models.Common;
using Domain.Models.Enum;
using System;

namespace ApplicationLogic.Jobs.Messages
{
    public class GetJobReponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public JobType JobType { get; set; }
        public int? Vacancies { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Salary { get; set; }
        public JobStatus Status { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string LocationId { get; set; }
    }
}
