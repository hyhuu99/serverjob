using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Companys.Messages
{
    public class GetCompanyReponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dropdown Scale { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Summary { get; set; }
        public string Email { get; set; }
        public bool IsShow { get; set; }
    }
}
