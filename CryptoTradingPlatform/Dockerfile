#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CryptoTradingPlatform/CryptoTradingPlatform.csproj", "CryptoTradingPlatform/"]
RUN dotnet restore "CryptoTradingPlatform/CryptoTradingPlatform.csproj"
COPY . .
WORKDIR "/src/CryptoTradingPlatform"
RUN dotnet build "CryptoTradingPlatform.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CryptoTradingPlatform.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CryptoTradingPlatform.dll"]