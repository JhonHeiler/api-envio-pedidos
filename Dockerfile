# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar archivos y restaurar dependencias
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Etapa de producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Exponer el puerto 80 y ejecutar la API
EXPOSE 80
ENTRYPOINT ["dotnet", "ApiEnvioPedidos.dll"]