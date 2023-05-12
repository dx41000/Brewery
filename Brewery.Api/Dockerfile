#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Brewery.Api/Brewery.Api.csproj", "Brewery.Api/"]
COPY ["Brewery.Datalayer/Brewery.Datalayer.csproj", "Brewery.Datalayer/"]
COPY ["Brewery.Models/Brewery.Models.csproj", "Brewery.Models/"]
COPY ["Brewery.Repositories.Interfaces/Brewery.Repositories.Interfaces.csproj", "Brewery.Repositories.Interfaces/"]
COPY ["Brewery.Repositories/Brewery.Repositories.csproj", "Brewery.Repositories/"]
COPY ["Brewery.Services.Interfaces/Brewery.Services.Interfaces.csproj", "Brewery.Services.Interfaces/"]
COPY ["Brewery.Services/Brewery.Services.csproj", "Brewery.Services/"]
RUN dotnet restore "Brewery.Api/Brewery.Api.csproj"
COPY . .
WORKDIR "/src/Brewery.Api"
RUN dotnet build "Brewery.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Brewery.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Brewery.Api.dll"]