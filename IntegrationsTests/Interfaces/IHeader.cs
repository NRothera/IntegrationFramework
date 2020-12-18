using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationsTests.Interfaces
{
    public interface IHeader
    {
        /// <summary>
        /// The Name of the header
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// The value to pass in to the header
        /// </summary>
        string Value { get; set; }
    }
}
