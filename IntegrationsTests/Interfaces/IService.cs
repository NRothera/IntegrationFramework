using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationsTests.Interfaces
{
    public interface IService
    {
        /// <summary>
        /// Invokes the service call
        /// </summary>
        //public abstract 
        void Invoke(bool expectSuccess = true);

        /// <summary>
        /// Cast the response on return, eg "basket.getResponse().CastAs&ltBasketResponse&gt()"
        /// </summary>
        /// <returns>IResponse object</returns>
        //public abstract 
        IResponse GetResponse();

        /// <summary>
        /// Chains the Errors Assertion for fluent syntax
        /// </summary>
        /// <returns></returns>
        //public abstract 
        T AssertThatErrors<T>();

        /// <summary>
        /// Return the errors
        /// </summary>
        /// <returns></returns>
        //public abstract 
        /// <summary>
        /// Chains the Assertions for the service for fluent syntax
        /// </summary>
        /// <returns></returns>
        //public abstract 
        //IAssertion assertThat();
        T AssertThat<T>();
    }
}
