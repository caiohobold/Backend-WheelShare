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
ENV CONNECTION_STRING="Host=monorail.proxy.rlwy.net;Port=43262;Database=railway;Username=postgres;Password=HONFodckquNwgGCmudPMsRNAMrTTznfj;"

# Executar migrações em sequência
CMD sh -c "until pg_isready -h db -p 5432 -U postgres; do echo waiting for postgres; sleep 2; done;"
CMD sh -c "dotnet ef database update 20240516211349_InitialMigration --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240516211349_InitialMigration --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240522205643_FixColumnName --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240523172529_AlterDateTime --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240523175021_AlterDateTime2 --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240523175154_AlterDateTime3 --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240523184324_AlterColumnEnum --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240523184400_AjustarEquipamento2 --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240523192551_AjustarManuseioEnumEquipamento --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240523194913_AjustarManuseioEnumEquipamento2 --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240524120025_AjustarManuseioToInt --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240524121421_AjustarManuseioToInt2 --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240524151828_AjusteTabelas --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240524151842_AjusteTabelas2 --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240527150040_AjustarDefaultValue --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240527153103_AjustarTimeStampColumn --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240527165751_AddRowVersionToEmprestimo --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240527170631_RemoveRowVersionColumn --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240527170826_RemoveRowVersionColumn2 --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240527182054_UpdateEmprestimoRelations --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240612102015_AddDataDevolucaoEmprestimoToEmprestimos --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240610224006_UpdatePessoaEmprestimo --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240610223656_UpdateEmprestimo2 --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 2024061022540_RemoveExtraPessoaColumns --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240612115812_AddColumnEndereco --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240613105014_AddFotosToEquipamentos --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240613120839_AddFotosToEquipamentos2 --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240617142731_AlterTableFotos --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240617175655_AddFotoPessoasColumn --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240617175565_AddLocaisTableAndForeignKey --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 202406180531_UpdateNotNullConstraintPessoa --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 202406180820_UpdateNotNullConstraintPessoa2 --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 202406180920_AddFeedbackTable --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240619114941_AlterFeedbackTable --project WheelShareAPI.csproj"
CMD sh -c "dotnet ef database update 20240620130119_AddFeedbackAssociacao --project WheelShareAPI.csproj"


CMD sh -c "dotnet WheelShareAPI.dll"

