using System;
using System.Collections.Generic;
using System.Text;

namespace webstat
{
    public interface IServiceTickEvent
    {
        DateTime Timestamp { get; }
        Status Status { get; }
    }
}
