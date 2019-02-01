using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Common
{
    public class Dropdown: IAggregateRoot
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
