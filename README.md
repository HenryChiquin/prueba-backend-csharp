# Prueba de backend en C#

API REST en ASP.NET Core y .NET 10 que consulta la tabla `MarcasAutos` en PostgreSQL. 
La primera migración crea la tabla e inserta Toyota, Honda y Ford.

## Ejecutar con Docker

Abre Docker Desktop en modo de contenedores Linux y ejecuta desde la raíz:

```powershell
docker compose up --build --detach --wait
Invoke-RestMethod http://localhost:8080/api/marcasautos
```

La API se inicia cuando PostgreSQL está listo y aplica la migración antes de atender solicitudes. El puerto de la API es `8080` y el de la base es `5432` en el equipo local. Las credenciales de Compose son para desarrollo local. Para detener los servicios y conservar los datos del volumen, ejecuta `docker compose down`.

## Ejecutar las pruebas

Con el SDK de .NET 10 o un SDK posterior compatible, ejecuta:

```powershell
dotnet restore
dotnet test --settings coverage.runsettings --collect:"XPlat Code Coverage"
```

Las pruebas usan EF Core InMemory. Los resultados de cobertura están en `PruebaBackend.Tests/TestResults/*/coverage.cobertura.xml`.

## Ejecutar la API desde Windows

```powershell
docker compose up -d --wait db
dotnet tool restore
dotnet ef database update --project PruebaBackend.Api -- --environment Development
dotnet run --project PruebaBackend.Api --launch-profile http
```
