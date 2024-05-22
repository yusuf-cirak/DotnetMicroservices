FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet build "PlatformService.csproj" -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet publish -c $BUILD_CONFIGURATION -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet","PlatformService.dll"]
