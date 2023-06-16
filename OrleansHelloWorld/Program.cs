using GrainAbstaction;
using Grains;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// Main Method
{
    try
    {
        using var host = await StartSiloAsync();

        var client = host.Services.GetRequiredService<IClusterClient>();

        Console.WriteLine("\n\n Press Enter to terminate...\n\n");
        Console.ReadLine();

        await DoClientWorkAsync(client);
        Console.ReadKey();

        await host.StopAsync();

        return 0;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return 1;
    }
}

// Creating silo server inside localhost cluster to hold grains
static async Task<IHost> StartSiloAsync()
{
    var builder = new HostBuilder()
     .UseOrleans(silo =>
     {
         silo.UseLocalhostClustering()
         .ConfigureLogging(logging => logging.AddConsole());
     });

    builder.ConfigureServices(collection =>
    {
        collection.AddTransient<IHelloGrain, HelloGrain>();
    });

    var host = builder.Build();
    await host.StartAsync();

    return host;
}

static async Task DoClientWorkAsync(IClusterClient client)
{
    var friend = client.GetGrain<IHelloGrain>(0);
    var response = await friend.SayHello("Good morning, HelloGrain!");

    Console.WriteLine($"\n\n{response}\n\n");
}