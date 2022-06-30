FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app
COPY . .
RUN ["dotnet", "restore"]
COPY . ./
RUN dotnet build "StudentAPI.csproj" -c Release -o /app/build
RUN dotnet publish "StudentAPI.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
COPY --from=base /app/publish .
ENTRYPOINT ["dotnet", "StudentAPI.dll"]