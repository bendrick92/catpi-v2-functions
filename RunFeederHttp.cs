using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace CatPiV2.Functions
{
  public class RunFeederHttp
  {
    [FunctionName("RunFeederHttp")]
    [return: ServiceBus("run-feeder", Connection = "Azure:ServiceBus:ConnectionString")]
    public static Message Run(
      [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] dynamic input,
      ILogger log
    )
    {
      log.LogInformation("RunFeederHttp function processed a trigger");
      return new Message(null);
    }
  }
}