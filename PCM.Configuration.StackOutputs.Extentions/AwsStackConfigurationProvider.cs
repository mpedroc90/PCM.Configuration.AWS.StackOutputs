using System.Collections.Generic;
using System.Linq;
using Amazon;
using Amazon.CloudFormation;
using Amazon.CloudFormation.Model;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace GDExpress.Tools.Configuration.Extentions.StackOutputs
{
    public class AwsStackConfigurationProvider : ConfigurationProvider
    {
        private List<Output> _outputs;

        private readonly string _stack;
        private readonly string _accessKey;
        private readonly string _secretKey;
        private readonly string _region;

        public AwsStackConfigurationProvider(string stack, string accessKey, string secretKey, string region)
        {
            _stack = stack;
            _accessKey = accessKey;
            _secretKey = secretKey;
            _region = region;
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
            var client = new AmazonCloudFormationClient(new BasicAWSCredentials(_accessKey, _secretKey), RegionEndpoint.GetBySystemName(_region));

            var request = new DescribeStacksRequest()
            {
                StackName = _stack
            };

            var response = client.DescribeStacksAsync(request).GetAwaiter().GetResult();
            _outputs = response.Stacks.FirstOrDefault()?.Outputs.Where(t=> !string.IsNullOrEmpty(t.ExportName)).ToList();

        }

    }
}