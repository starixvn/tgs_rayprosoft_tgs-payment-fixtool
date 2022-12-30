#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM node:18.10 AS nodejs
WORKDIR /src
COPY ["frontend", "frontend/"]
WORKDIR /src/frontend
RUN npm ci
RUN npx ng build -c production --aot

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BuildConfiguration=Release
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["Tgs.Payment.FixTool/Tgs.Payment.FixTool.csproj", "Tgs.Payment.FixTool/"]
COPY ["FixTool.Core/FixTool.Core.csproj", "FixTool.Core/"]
COPY ["FixTool.Repositories/FixTool.Repositories.csproj", "FixTool.Repositories/"]
RUN dotnet restore "Tgs.Payment.FixTool/Tgs.Payment.FixTool.csproj"
COPY . .
COPY --from=nodejs /src/frontend/dist/frontend /src/Tgs.Payment.FixTool/wwwroot/
WORKDIR "/src/Tgs.Payment.FixTool"
RUN dotnet build "Tgs.Payment.FixTool.csproj" -c $BuildConfiguration -o /app/build

FROM build AS publish
ARG BuildConfiguration
RUN dotnet publish "Tgs.Payment.FixTool.csproj" -c $BuildConfiguration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tgs.Payment.FixTool.dll"]