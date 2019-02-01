using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Category : IAggregateRoot
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
