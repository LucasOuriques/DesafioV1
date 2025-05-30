# Etapa 1: Base - Runtime do .NET
# Usa a imagem oficial do ASP.NET Core como base para o ambiente de execução final.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Expõe as portas que o Kestrel (servidor web do ASP.NET Core) vai usar.
# A porta 8080 é para HTTP e a 8081 geralmente para HTTPS.
# Certifique-se de que o seu docker-compose.yml mapeia essas portas corretamente.
EXPOSE 8080
EXPOSE 8081

# Configura a variável de ambiente ASPNETCORE_URLS para que o Kestrel
# escute em todas as interfaces na porta 8080 (para HTTP).
# Isso é crucial para que a aplicação seja acessível dentro do container.
ENV ASPNETCORE_URLS="http://+:8080"

# Etapa 2: Build - SDK do .NET
# Usa a imagem oficial do SDK do .NET para compilar a aplicação.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o arquivo .csproj primeiro para restaurar as dependências.
# Isso otimiza o cache do Docker, pois as dependências só são restauradas
# se o .csproj mudar.
COPY ["DesafioV1.csproj", "."]
RUN dotnet restore "DesafioV1.csproj"

# Copia o restante do código fonte para o container.
COPY . .

# Compila a aplicação.
RUN dotnet build "DesafioV1.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa 3: Publicação
# Publica a aplicação para criar os binários prontos para implantação.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Publica a aplicação para o diretório /app/publish.
# /p:UseAppHost=false é usado para não criar um executável nativo,
# o que é comum em ambientes Docker.
RUN dotnet publish "DesafioV1.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa 4: Final - Imagem de Produção
# Esta é a imagem final e otimizada, contendo apenas o runtime e os binários publicados.
FROM base AS final
WORKDIR /app

# Copia os arquivos publicados da etapa 'publish' para a imagem final.
COPY --from=publish /app/publish .

# Define o ponto de entrada da aplicação, ou seja, o comando que será executado
# quando o container for iniciado.
ENTRYPOINT ["dotnet", "DesafioV1.dll"]