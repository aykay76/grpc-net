using System;
using System.Threading;
using Grpc.Core;

namespace grpc_net
{
    class Program
    {
        const int Port = 50051;

        static void Main(string[] args)
        {
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
    }
}
