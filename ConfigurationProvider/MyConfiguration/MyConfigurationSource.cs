using Microsoft.Extensions.Configuration;

namespace ConfigurationProvider.MyConfiguration
{
    public class MyConfigurationSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new MyConfigurationProvider();
        }
    }
}