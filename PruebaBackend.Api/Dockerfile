FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY PruebaBackend.Api/PruebaBackend.Api.csproj PruebaBackend.Api/
RUN dotnet restore PruebaBackend.Api/PruebaBackend.Api.csproj
COPY PruebaBackend.Api/ PruebaBackend.Api/
RUN dotnet publish PruebaBackend.Api/PruebaBackend.Api.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
USER $APP_UID
EXPOSE 8080
ENTRYPOINT ["dotnet", "PruebaBackend.Api.dll"]
