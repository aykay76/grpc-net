FROM mcr.microsoft.com/dotnet/runtime:5.0

COPY bin/Release/netcoreapp5.0/publish/ app/

ENTRYPOINT ["dotnet", "app/grpc-net.dll", "-server"]

