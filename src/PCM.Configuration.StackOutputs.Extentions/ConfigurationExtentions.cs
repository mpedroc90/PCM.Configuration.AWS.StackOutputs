using Amazon;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

namespace PCM.Configuration.Extentions.StackOutputs
{
    public static class ConfigurationExtentions

    {
        public static IConfigurationBuilder AddStackConfig(this IConfigurationBuilder builder, string stack,string accessKey, string secretKey, string region)
        {
            builder.Add(new AwsStackConfigurationSource(stack,accessKey, secretKey, region));
            return builder;

        }



        public static IConfigurationBuilder AddStackConfig(this IConfigurationBuilder builder, string stack, AWSCredentials credentials, RegionEndpoint region = null)
        {
            builder.Add(new AwsStackConfigurationSource(stack, credentials, region));
            return builder;

        }

        public static IConfigurationBuilder AddStackConfig(this IConfigurationBuilder builder, string stack)
        {
            builder.Add(new AwsStackConfigurationSource(stack));
            return builder;
        }
    }
}