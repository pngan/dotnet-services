FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 44300
EXPOSE 44301

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["hello-controller/hello-controller.csproj", "hello-controller/"]
RUN dotnet restore "hello-controller/hello-controller.csproj"
COPY . .
WORKDIR "/src/hello-controller"
RUN dotnet build "hello-controller.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "hello-controller.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "hello-controller.dll"]
