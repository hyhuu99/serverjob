using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class GroupCategory : IAggregateRoot
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string[] CategoryIds { get; set; }
    }
}
