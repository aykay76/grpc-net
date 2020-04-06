FROM mcr.microsoft.com/dotnet/core/runtime:3.0

COPY bin/Release/netcoreapp3.1/publish/ app/

ENTRYPOINT ["dotnet", "app/grpc-net.dll"]

