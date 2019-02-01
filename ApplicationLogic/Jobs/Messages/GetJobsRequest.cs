using Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Jobs.Messages
{
    public class GetJobsRequest
    {
        public string SearchText { get; set; }
        public JobType? JobType { get; set; }
        public string LocationId { get; set; }
        public string CategoryId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public bool IsActive { get; set; }
    }
}
