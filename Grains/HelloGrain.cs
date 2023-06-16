using GrainAbstaction;
using Microsoft.Extensions.Logging;

namespace Grains
{
    public class HelloGrain : Grain, IHelloGrain
    {
        private readonly ILogger<HelloGrain> _logger;

        public HelloGrain(ILogger<HelloGrain> logger)
        {
            _logger = logger;
        }

        public ValueTask<string> SayHello(string greeting)
        {
            _logger.LogInformation($"{nameof(SayHello)} message received: greeting = '{greeting}'");

            return ValueTask.FromResult($"Client said: '{greeting}', so HelloGrain says: Hello!");
        }
    }
}
