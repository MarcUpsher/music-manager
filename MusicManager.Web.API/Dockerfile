FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
WORKDIR /src

COPY MusicManager.Web.API/MusicManager.Web.API.sln .
COPY MusicManager.Web.API/MusicManager.Web.API/*.csproj ./MusicManager.Web.API/
COPY MusicManager.Web.API/MusicManager.DAL/*.csproj ./MusicManager.DAL/
COPY MusicManager.Web.API/MusicManager.Tests/*.csproj ./MusicManager.Tests/

RUN dotnet restore
COPY . .
WORKDIR /src/MusicManager.Web.API
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "MusicManager.Web.API.dll"]

ENTRYPOINT [ "dotnet", "MusicManager.Web.API.dll", "--server.urls", "https://0.0.0.0:5000"]
