FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore ./ForumBackend.sln
RUN dotnet publish -c release -o build ./ForumBackend.sln

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/build ./
EXPOSE 80
ENTRYPOINT ["dotnet", "ForumBackendApi.dll"]
