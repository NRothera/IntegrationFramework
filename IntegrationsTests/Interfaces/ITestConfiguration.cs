
namespace IntegrationsTests.Interfaces
{
    public interface ITestConfiguration
    {
        string BasePostUrl { get; }
        string BaseCommentUrl { get; }
        string GetResource(string key);
    }
}
