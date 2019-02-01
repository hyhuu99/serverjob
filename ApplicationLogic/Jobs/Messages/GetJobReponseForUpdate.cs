using Domain.Models.Common;
using Domain.Models.Enum;
using System;

namespace ApplicationLogic.Jobs.Messages
{
    public class GetJobReponseForUpdate: GetJobReponse
    {
        public string LocationId { get; set; }
        public Dropdown Level { get; set; }
        public DateTime StartDay { get; set; }
        public bool isShowSalary { get; set; }
    }
}
