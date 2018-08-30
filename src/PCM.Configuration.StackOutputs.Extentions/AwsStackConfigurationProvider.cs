using System.Collections.Generic;
using System.Linq;
using Amazon;
using Amazon.CloudFormation;
using Amazon.CloudFormation.Model;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

namespace PCM.Configuration.Extentions.StackOutputs
{
    public class AwsStackConfigurationProvider : ConfigurationProvider
    {
        private List<Output> _outputs;
        private readonly string _stack;

        private readonly AmazonCloudFormationClient _client;


        public AwsStackConfigurationProvider(string stack, AWSCredentials credential, RegionEndpoint region = null)
        {
            _stack = stack;
            _client = region == null ?
            new AmazonCloudFormationClient(credential):
            new AmazonCloudFormationClient(credential, region);
        }


        
        public override bool TryGet(string key, out string value)
        {
            var aux =_outputs.FirstOrDefault(t => t.ExportName == key);

            value = null;
            if (aux == null)
                return false;
            value = aux.OutputValue;
            return true;
        }

     

        public override void Load()
        {
          

            var request = new DescribeStacksRequest()
            {
                StackName = _stack
            };

            var response = _client.DescribeStacksAsync(request).GetAwaiter().GetResult();
            _outputs = response.Stacks.FirstOrDefault()?.Outputs.Where(t=> !string.IsNullOrEmpty(t.ExportName)).ToList();

        }

    }
}