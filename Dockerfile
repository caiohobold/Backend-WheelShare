# Usar a imagem SDK para construção e publicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Instalar o dotnet-ef globalmente durante a fase de build
RUN dotnet tool install --global dotnet-ef

# Adicionar .dotnet/tools ao PATH
ENV PATH="$PATH:/root/.dotnet/tools"

# Copiar e restaurar dependências
COPY WheelShareAPI.csproj .
RUN dotnet restore "WheelShareAPI.csproj"

# Copiar o restante do código e construir
COPY . .
RUN dotnet build "WheelShareAPI.csproj" -c Release -o /app/build

# Publicar a aplicação
RUN dotnet publish "WheelShareAPI.csproj" -c Release -o /app/publish

# Usar a imagem SDK para a etapa final
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
# COPY --from=build /app/publish .
# COPY --from=build /app/WheelShareAPI.csproj .
COPY --from=build /root/.dotnet /root/.dotnet

# Adicionar .dotnet/tools ao PATH na imagem final
ENV PATH="$PATH:/root/.dotnet/tools"

# Instalar o cliente PostgreSQL para usar o pg_isready
RUN apt-get update && apt-get install -y postgresql-client

# Definir a variável de ambiente para a string de conexão
ENV CONNECTION_STRING="Host=localhost;Port=5432;Database=SistemaEmprestimo;Username=postgres;Password=1234;"

CMD sh -c "dotnet ef database update --project WheelShareAPI.csproj && dotnet WheelShareAPI.dll"

