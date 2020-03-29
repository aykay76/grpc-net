using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrpcEnvironment;
using Grpc.Core;
using Google.Protobuf;
using System.Collections;

public class GrpcEnvironmentServer : EnvironmentService.EnvironmentServiceBase
{
    public override async Task<GrpcEnvironment.KeyValuePair> GetEnvironmentVariable(GrpcEnvironment.KeyValuePair request, ServerCallContext context)
    {
        GrpcEnvironment.KeyValuePair kvp = new GrpcEnvironment.KeyValuePair();
        kvp.Key = request.Key;
        kvp.Value = System.Environment.GetEnvironmentVariable(request.Key);
        return kvp;
    }

    public override async Task<Google.Protobuf.WellKnownTypes.Empty> SetEnvironmentVariable(GrpcEnvironment.KeyValuePair request, ServerCallContext context)
    {
        System.Environment.SetEnvironmentVariable(request.Key, request.Value);
        return new Google.Protobuf.WellKnownTypes.Empty();
    }

    public override async Task GetEnvironmentVariables(Google.Protobuf.WellKnownTypes.Empty request, IServerStreamWriter<GrpcEnvironment.KeyValuePair> responseStream, ServerCallContext context)
    {
        var env = Environment.GetEnvironmentVariables();
        foreach (DictionaryEntry entry in env)
        {
            GrpcEnvironment.KeyValuePair kvp = new GrpcEnvironment.KeyValuePair();
            kvp.Key = entry.Key.ToString();
            kvp.Value = entry.Value.ToString();
            await responseStream.WriteAsync(kvp);
        }
    }
}