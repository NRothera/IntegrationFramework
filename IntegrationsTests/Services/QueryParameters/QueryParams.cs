using System;
using System.Collections.Generic;
using System.Text;
using  IntegrationsTests.Interfaces;

namespace  IntegrationsTests.Services.QueryParameters
{
    /// <summary>
    /// Abstract class that implements IQueryParams to be extended
    /// </summary>
    public abstract class QueryParams : IQueryParams
    {
        /// <summary>
        /// Checks if the query string is populated
        /// </summary>
        /// <param name="stringToCheck"></param>
        /// <returns>Boolean</returns>
        protected bool isPopulated(String stringToCheck)
        {
            return stringToCheck != null && stringToCheck != "";
        }

        /// <summary>
        /// Gets all the specified query parameters
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<string, string> getParameters();
    }
}
