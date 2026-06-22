FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["InThePocket.Assignment.GardenManager.Ports/InThePocket.Assignment.GardenManager.Ports.csproj", "InThePocket.Assignment.GardenManager.Ports/"]
RUN dotnet restore "InThePocket.Assignment.GardenManager.Ports/InThePocket.Assignment.GardenManager.Ports.csproj"
COPY . .
WORKDIR "/src/InThePocket.Assignment.GardenManager.Ports"
RUN dotnet build "InThePocket.Assignment.GardenManager.Ports.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InThePocket.Assignment.GardenManager.Ports.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InThePocket.Assignment.GardenManager.Ports.dll"]
