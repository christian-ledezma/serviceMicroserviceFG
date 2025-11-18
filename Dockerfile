# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar toda la solución
COPY ./serviceMicroservice.sln ./

# Copiar cada proyecto
COPY ./serviceMicroservice.API/serviceMicroservice.API.csproj ./serviceMicroservice.API/
COPY ./serviceMicroservice.Application/serviceMicroservice.Application.csproj ./serviceMicroservice.Application/
COPY ./serviceMicroservice.Domain/serviceMicroservice.Domain.csproj ./serviceMicroservice.Domain/
COPY ./serviceMicroservice.Infrastructure/serviceMicroservice.Infrastructure.csproj ./serviceMicroservice.Infrastructure/

# Restaurar dependencias
RUN dotnet restore

# Copiar todo el resto del código
COPY . .

# Publicar la API
WORKDIR /src/serviceMicroservice.API
RUN dotnet publish -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:80

COPY --from=build /app .

EXPOSE 80
ENTRYPOINT ["dotnet", "serviceMicroservice.API.dll"]
