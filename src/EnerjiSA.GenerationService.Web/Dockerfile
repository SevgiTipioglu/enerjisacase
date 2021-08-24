#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/EnerjiSA.GenerationService.Web/EnerjiSA.GenerationService.Web.csproj", "src/EnerjiSA.GenerationService.Web/"]
RUN dotnet restore "src/EnerjiSA.GenerationService.Web/EnerjiSA.GenerationService.Web.csproj"
COPY . .
WORKDIR "/src/src/EnerjiSA.GenerationService.Web"
RUN dotnet build "EnerjiSA.GenerationService.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EnerjiSA.GenerationService.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnerjiSA.GenerationService.Web.dll"]