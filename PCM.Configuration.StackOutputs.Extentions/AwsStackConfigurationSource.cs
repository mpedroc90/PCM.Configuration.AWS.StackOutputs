using System;
using Microsoft.Extensions.Configuration;

namespace GDExpress.Tools.Configuration.Extentions.StackOutputs
{
    public class AwsStackConfigurationSource : IConfigurationSource
    {
        private readonly string _stack;
        private readonly string _accessKey;
        private readonly string _secretKey;
        private readonly string _region;
        public AwsStackConfigurationSource(string stack,string accessKey, string secretKey, string region)
        {
            _stack = stack;
            _accessKey = accessKey;
            _secretKey = secretKey;
            _region = region;
        }


        public AwsStackConfigurationSource(string stack)
        {
            _stack = stack;
            _accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY");
            _secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY"); ;
            _region = Environment.GetEnvironmentVariable("AWS_REGION");
        }

        public  IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            
            return new AwsStackConfigurationProvider(_stack, _accessKey, _secretKey, _region);
        }
    }
}