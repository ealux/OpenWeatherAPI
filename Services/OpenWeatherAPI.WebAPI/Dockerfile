#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/OpenWeatherAPI.WebAPI/OpenWeatherAPI.WebAPI.csproj", "Services/OpenWeatherAPI.WebAPI/"]
RUN dotnet restore "Services/OpenWeatherAPI.WebAPI/OpenWeatherAPI.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/OpenWeatherAPI.WebAPI"
RUN dotnet build "OpenWeatherAPI.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpenWeatherAPI.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenWeatherAPI.WebAPI.dll"]