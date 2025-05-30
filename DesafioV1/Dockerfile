# Etapa 1: Base - Runtime do .NET
# Usa a imagem oficial do ASP.NET Core como base para o ambiente de execu��o final.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Exp�e as portas que o Kestrel (servidor web do ASP.NET Core) vai usar.
# A porta 8080 � para HTTP e a 8081 geralmente para HTTPS.
# Certifique-se de que o seu docker-compose.yml mapeia essas portas corretamente.
EXPOSE 8080
EXPOSE 8081

# Configura a vari�vel de ambiente ASPNETCORE_URLS para que o Kestrel
# escute em todas as interfaces na porta 8080 (para HTTP).
# Isso � crucial para que a aplica��o seja acess�vel dentro do container.
ENV ASPNETCORE_URLS="http://+:8080"

# Etapa 2: Build - SDK do .NET
# Usa a imagem oficial do SDK do .NET para compilar a aplica��o.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o arquivo .csproj primeiro para restaurar as depend�ncias.
# Isso otimiza o cache do Docker, pois as depend�ncias s� s�o restauradas
# se o .csproj mudar.
COPY ["DesafioV1.csproj", "."]
RUN dotnet restore "DesafioV1.csproj"

# Copia o restante do c�digo fonte para o container.
COPY . .

# Compila a aplica��o.
RUN dotnet build "DesafioV1.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa 3: Publica��o
# Publica a aplica��o para criar os bin�rios prontos para implanta��o.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Publica a aplica��o para o diret�rio /app/publish.
# /p:UseAppHost=false � usado para n�o criar um execut�vel nativo,
# o que � comum em ambientes Docker.
RUN dotnet publish "DesafioV1.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa 4: Final - Imagem de Produ��o
# Esta � a imagem final e otimizada, contendo apenas o runtime e os bin�rios publicados.
FROM base AS final
WORKDIR /app

# Copia os arquivos publicados da etapa 'publish' para a imagem final.
COPY --from=publish /app/publish .

# Define o ponto de entrada da aplica��o, ou seja, o comando que ser� executado
# quando o container for iniciado.
ENTRYPOINT ["dotnet", "DesafioV1.dll"]