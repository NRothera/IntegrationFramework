using System.Collections.Generic;

namespace IntegrationsTests.Interfaces
{
    public interface IHeaders
    {
        List<IHeader> Headers { get; set; }
    }
}