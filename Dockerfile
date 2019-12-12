FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 9000

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY pagozap.Pessoas/pagozap.sln ./
COPY pagozap.Pessoas/Api.Pessoas/Api.Pessoa.csproj Api.Pessoa/
COPY pagozap.Pessoas/2.Domain.Pessoa/Domain.Pessoa.csproj Domain.Pessoa/
COPY pagozap.Pessoas/1.Infra/Infra.Pessoa.csproj Infra.Pessoa/
COPY pagozap.Pessoas/App.Pessoa/App.Pessoa.csproj App.Pessoa/
COPY pagozap.Pessoas/CrossCutting.Pessoas/CrossCutting.Pessoa.csproj CrossCutting.Pessoas/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY pagozap.Pessoas .
WORKDIR /src/Api.Pessoas
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Api.Pessoa.dll"]