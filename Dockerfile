FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY src/MyApi/MyApi.csproj src/MyApi/
RUN dotnet restore src/MyApi/MyApi.csproj

COPY src/MyApi/ src/MyApi/
RUN dotnet publish src/MyApi/MyApi.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .

USER $APP_UID

EXPOSE 8080
ENTRYPOINT ["dotnet", "MyApi.dll"]