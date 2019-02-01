using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.GeneralModel.ResumesModel
{
    public class ContactInfo
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
