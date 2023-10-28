FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

COPY Ticketer.sln .
COPY Ticketer.Business/Ticketer.Business.csproj ./Ticketer.Business/Ticketer.Business.csproj 
COPY Ticketer.WebApp/Ticketer.WebApp.csproj ./Ticketer.WebApp/Ticketer.WebApp.csproj
RUN dotnet restore ./Ticketer.Business/Ticketer.Business.csproj 
RUN dotnet restore ./Ticketer.WebApp/Ticketer.WebApp.csproj

COPY Ticketer.Business/. ./Ticketer.Business/
COPY Ticketer.WebApp/. ./Ticketer.WebApp/
WORKDIR /source/Ticketer.WebApp
RUN dotnet publish -c release /p:EnvironmentName=docker-compose-dev -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_ENVIRONMENT=DockerDev
ENV ASPNETCORE_URLS=http://+:5001
EXPOSE 5001
ENTRYPOINT ["dotnet", "Ticketer.WebApp.dll"]