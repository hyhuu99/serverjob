using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Applications.Messages
{
    public class GetApplycationReponse
    {
        public string Id { get; set; }
        public string JobId { get; set; }
        public DateTime AppliedDate { get; set; }
        public string Email { get; set; }
        public Resume Resume { get; set; }
    } 
}
