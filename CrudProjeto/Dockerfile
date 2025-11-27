# Etapa 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY CrudProjeto/*.csproj ./CrudProjeto/
RUN dotnet restore CrudProjeto/CrudProjeto.csproj

COPY . .
RUN dotnet publish CrudProjeto/CrudProjeto.csproj -c Release -o /out

# Etapa 2 — Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /out .

ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

EXPOSE 8080

ENTRYPOINT ["dotnet", "CrudProjeto.dll"]
