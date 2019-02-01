using Domain.Models.Common;
using Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Job : IAggregateRoot
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public JobType JobType { get; set; }
        public int? Vacancies { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Salary { get; set; }
        public JobStatus Status { get; set; }
        public string CompanyId { get; set; }
        public string LocationId { get; set; }
        public Dropdown Level { get; set; }
        public bool IsShowSalary { get; set; }
    }
}
