using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Common
{
    public class CommonData : IAggregateRoot
    {
        public string Id { get; set; }
        public Dropdown[] Country { get; set; }
        public City[] City { get; set; }
        public Dropdown[] Gender { get; set; }
        public Dropdown[] Relationship { get; set; }
        public Dropdown[] Nationality { get; set; }
        public Dropdown[] Qualification { get; set; }
        public Dropdown[] Language { get; set; }
        public Dropdown[] Level { get; set; }
        public Dropdown[] YearOfWork { get; set; }
        public Dropdown[] LevelOfWork { get; set; }
        public Dropdown[] TypeOfWork { get; set; }
        public Dropdown[] Scale { get; set; }
    }
}
