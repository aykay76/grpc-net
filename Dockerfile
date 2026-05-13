FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ENV PROTOBUF_TOOLS_OS=macosx
ENV PROTOBUF_TOOLS_CPU=arm64
WORKDIR /source
COPY . ./
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "grpc-net.dll"]