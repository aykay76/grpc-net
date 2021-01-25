using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcEnvironment;

namespace grpc_net
{
    class Program
    {
        const int Port = 10000;

        static async Task<int> Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "-server") {
                Server server = new Server
                {
                    Services = { GrpcEnvironment.EnvironmentService.BindService(new GrpcEnvironmentServer())},
                    Ports = { new ServerPort("0.0.0.0", Port, ServerCredentials.Insecure) }
                };
                server.Start();

                Console.WriteLine("Server listening on port " + Port);
                Console.WriteLine("CTRL+C to stop the server...");
                Thread.Sleep(Timeout.Infinite);

                server.ShutdownAsync().Wait();   
            }
            else
            {
                var channel = new Channel("localhost", Port, ChannelCredentials.Insecure);
                var client = new GrpcEnvironment.EnvironmentService.EnvironmentServiceClient(channel);
                AsyncServerStreamingCall<GrpcEnvironment.KeyValuePair> stream = client.GetEnvironmentVariables(new global::Google.Protobuf.WellKnownTypes.Empty());
                while (await stream.ResponseStream.MoveNext()) 
                {
                    KeyValuePair kvp = stream.ResponseStream.Current;
                    Console.WriteLine($"{kvp.Key} = {kvp.Value}");
                }

                // KeyValuePair request = new KeyValuePair() {
                //     Key = "HOSTNAME"
                // };
                // KeyValuePair result = client.GetEnvironmentVariable(request);
                // Console.WriteLine($"{result.Key} = {result.Value}");
            }

            return 0;         
        }
    }
}
