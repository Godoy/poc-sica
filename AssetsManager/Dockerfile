ARG CORE_SDK=mcr.microsoft.com/dotnet/core/sdk
ARG ASPNET=mcr.microsoft.com/dotnet/core/aspnet
ARG VERSION=3.1

FROM $CORE_SDK:$VERSION AS build-env
WORKDIR /app
COPY . ./
RUN dotnet restore 
RUN dotnet publish -c Release -o ./out

FROM $ASPNET:$VERSION AS runtime
WORKDIR /app
COPY Sica.Assets.Api/appsettings*.json ./
COPY Sica.Assets.Api/Sica.Assets.Api.XML ./
COPY --from=build-env /app/out ./

EXPOSE 80

ENTRYPOINT dotnet Sica.Assets.Api.dll