using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Amelia.CEXPOC.DipendencyInjection;
using Amelia.CEXPOC.RunnerNamespace;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = DependencyInjection.Initialize();
        var runner = serviceProvider.GetService(typeof(Runner)) as Runner;
        if (runner != null)
        {
            await runner.Run();
        }
        else
        {
            throw new Exception("Failed to retrieve Runner instance from the dependency injection container.");
        }
    }
}