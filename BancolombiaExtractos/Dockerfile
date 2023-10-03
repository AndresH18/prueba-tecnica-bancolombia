FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BancolombiaExtractos/BancolombiaExtractos.csproj", "BancolombiaExtractos/"]
RUN dotnet restore "BancolombiaExtractos/BancolombiaExtractos.csproj"
COPY . .
WORKDIR "/src/BancolombiaExtractos"
RUN dotnet build "BancolombiaExtractos.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BancolombiaExtractos.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BancolombiaExtractos.dll"]
