using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionAllInOne
{
    public class Function1
    {
        [FunctionName("Schedhuler")]
        public void Run([TimerTrigger("* */10 * * * *")]TimerInfo myTimer, ILogger log)
        {
            //CRON Format
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
