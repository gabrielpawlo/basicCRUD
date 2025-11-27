# Stage 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o .csproj primeiro
COPY CrudProjeto/CrudProjeto.csproj CrudProjeto/
RUN dotnet restore CrudProjeto/CrudProjeto.csproj

# Agora copia todo o restante
COPY . .

# Publica o projeto
RUN dotnet publish CrudProjeto/CrudProjeto.csproj -c Release -o /app/out

# Stage 2 — Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

# Render usa a variável PORT
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "CrudProjeto.dll"]
