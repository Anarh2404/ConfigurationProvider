using System;
using Microsoft.Extensions.Configuration;

namespace ConfigurationProvider.MyConfiguration
{
    public static class MyConfigurationExtension
    {
        public static IConfigurationBuilder AddMyConfiguration(this IConfigurationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Add<MyConfigurationSource>(s => {});
        }
    }
}