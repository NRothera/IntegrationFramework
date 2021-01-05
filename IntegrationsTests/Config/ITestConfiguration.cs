
namespace IntegrationsTests.Interfaces
{
    public interface ITestConfiguration
    {
        string BasePostUrl { get; }
        string BaseCommentUrl { get; }
        string ExampleName { get; }
        string ExampleTwo { get; }
        string GetResource(string key);
     
    }
}
