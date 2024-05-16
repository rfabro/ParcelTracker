#syntax=docker/dockerfile:1.2
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
RUN mkdir /app
ADD . /app
WORKDIR /app

# Restore project
RUN dotnet restore "./API.csproj"

# Publish dotnet project
RUN dotnet publish "./API.csproj" out "./publish" --configuration Release

RUN cd "./publish"

# Enable globalization and time zones:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/enable-globalization.md
# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
EXPOSE 8080
WORKDIR /app
COPY --from=build "./publish" .
# Uncomment to enable non-root user
# USER $APP_UID
ENTRYPOINT ["dotnet", "API.dll"]