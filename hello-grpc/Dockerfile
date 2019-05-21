FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 50031

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["hello-grpc/hello-grpc.csproj", "hello-grpc/"]
RUN dotnet restore "hello-grpc/hello-grpc.csproj"
COPY . .
WORKDIR "/src/hello-grpc"
RUN dotnet build "hello-grpc.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "hello-grpc.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "hello-grpc.dll"]
