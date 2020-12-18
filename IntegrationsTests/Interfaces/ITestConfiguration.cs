
namespace IntegrationsTests.Interfaces
{
    public interface ITestConfiguration
    {
        string BasePostUrl { get; }
        string BaseCountryUrl { get; }
        string GetResource(string key);
    }
}
