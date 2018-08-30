using System;
using Amazon;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

namespace PCM.Configuration.Extentions.StackOutputs
{
    public class AwsStackConfigurationSource : IConfigurationSource
    {
        private readonly string _stack;
        private readonly AWSCredentials _credential;
        private readonly RegionEndpoint _regionEndpoint;

        public AwsStackConfigurationSource(string stack,string accessKey, string secretKey, string region)
        {
            _stack = stack;
            _credential = new BasicAWSCredentials(accessKey, secretKey);
            _regionEndpoint =  RegionEndpoint.GetBySystemName(region);
        }

        public AwsStackConfigurationSource(string stack, RegionEndpoint region, AWSCredentials credentials)
        {
            _regionEndpoint = region;
            _stack = stack;
            _credential = credentials;
        }

        public AwsStackConfigurationSource(string stack) : 
            this(stack, Environment.GetEnvironmentVariable("AWS_ACCESS_KEY"), Environment.GetEnvironmentVariable("AWS_SECRET_KEY"), Environment.GetEnvironmentVariable("AWS_REGION"))
        {}

        
        public  IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AwsStackConfigurationProvider(_stack, _credential, _regionEndpoint);
        }
    }
}