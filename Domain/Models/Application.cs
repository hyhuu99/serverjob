using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Application : IAggregateRoot
    {
        public string Id { get; set; }
        public string JobId { get; set; }
        public DateTime AppliedDate { get; set; }
        public string Email { get; set; }
        public Resume Resume { get; set; }
    }
}
