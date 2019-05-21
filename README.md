# Prerequisites
- dot net core 3.0, preview 5
- docker
- Visual Studio 16.1, preview 3
- Docker Desktop (on Windows), run using `linux containers`

# To Run
- Press F5
- Navigate to : https://localhost:44310/api/values

# Learnings

## dot net project creation
Dotnet can be used to create a `webapi` service using the command 

`dotnet new webapi`

and a `gRPC` service can be created with the command:

`dotnet new grpc`.

## docker networking
Use the default bridge mode provided by docker. A service can be referenced using it's name, e.g. `hello-grpc` in `ValuesController.cs(20)`.


## docker config
There is a file called `docker-compose.override.yaml`, which contains the port mapping for the services.

