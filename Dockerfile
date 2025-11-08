# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia a solução e o projeto (a solução está na raiz do repositório)
# REMOVIDO O ../ DAQUI
COPY CrudSolucao.sln .
COPY CrudProjeto/*.csproj ./CrudProjeto/

# Restaura as dependências
RUN dotnet restore CrudSolucao.sln

# Copia todo o código da solução
COPY . .

# Faz o build + publish do projeto principal
WORKDIR /src/CrudProjeto
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: imagem final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CrudProjeto.dll"]