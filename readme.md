Ensure Dockerfile references latest image version
`dotnet publish -c Release`
`docker build .`
`docker run --publish 10000:10000 --rm --name grpc-net localhost:5000/grpc-net`