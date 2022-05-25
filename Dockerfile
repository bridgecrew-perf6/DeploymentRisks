#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY DeploymentRisks/DeploymentRisks.csproj DeploymentRisks/
RUN dotnet restore DeploymentRisks/DeploymentRisks.csproj
COPY . .
WORKDIR /src/DeploymentRisks
RUN dotnet build DeploymentRisks.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish DeploymentRisks.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DeploymentRisks.dll"]


ARG buildno
ARG gitcommithash

RUN echo "Build number: $buildno"
RUN echo "Based on commit: $gitcommithash"