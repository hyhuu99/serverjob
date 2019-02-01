using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Infrastructure.Mongodb
{
    public interface IAggregateRoot
    {
        string Id { get; set; }
    }
}