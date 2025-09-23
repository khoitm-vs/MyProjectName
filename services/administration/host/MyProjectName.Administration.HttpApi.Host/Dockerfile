# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/identity/NuGet.Config", "services/identity/"]
COPY ["services/saas/NuGet.Config", "services/saas/"]
COPY ["services/administration/host/MyProjectName.Administration.HttpApi.Host/MyProjectName.Administration.HttpApi.Host.csproj", "services/administration/host/MyProjectName.Administration.HttpApi.Host/"]
COPY ["shared/MyProjectName.Microservice.Shared/MyProjectName.Microservice.Shared.csproj", "shared/MyProjectName.Microservice.Shared/"]
COPY ["services/administration/src/MyProjectName.Administration.EntityFrameworkCore/MyProjectName.Administration.EntityFrameworkCore.csproj", "services/administration/src/MyProjectName.Administration.EntityFrameworkCore/"]
COPY ["shared/MyProjectName.Shared/MyProjectName.Shared.csproj", "shared/MyProjectName.Shared/"]
COPY ["services/administration/src/MyProjectName.Administration.Domain/MyProjectName.Administration.Domain.csproj", "services/administration/src/MyProjectName.Administration.Domain/"]
COPY ["services/administration/src/MyProjectName.Administration.Domain.Shared/MyProjectName.Administration.Domain.Shared.csproj", "services/administration/src/MyProjectName.Administration.Domain.Shared/"]
COPY ["shared/MyProjectName.Hosting.Shared/MyProjectName.Hosting.Shared.csproj", "shared/MyProjectName.Hosting.Shared/"]
COPY ["shared/MyProjectName.ServiceDefaults/MyProjectName.ServiceDefaults.csproj", "shared/MyProjectName.ServiceDefaults/"]
COPY ["services/identity/src/MyProjectName.IdentityService.Application.Contracts/MyProjectName.IdentityService.Application.Contracts.csproj", "services/identity/src/MyProjectName.IdentityService.Application.Contracts/"]
COPY ["services/identity/src/MyProjectName.IdentityService.Domain.Shared/MyProjectName.IdentityService.Domain.Shared.csproj", "services/identity/src/MyProjectName.IdentityService.Domain.Shared/"]
COPY ["services/identity/src/MyProjectName.IdentityService.EntityFrameworkCore/MyProjectName.IdentityService.EntityFrameworkCore.csproj", "services/identity/src/MyProjectName.IdentityService.EntityFrameworkCore/"]
COPY ["services/identity/src/MyProjectName.IdentityService.Domain/MyProjectName.IdentityService.Domain.csproj", "services/identity/src/MyProjectName.IdentityService.Domain/"]
COPY ["services/saas/src/MyProjectName.SaaS.Application.Contracts/MyProjectName.SaaS.Application.Contracts.csproj", "services/saas/src/MyProjectName.SaaS.Application.Contracts/"]
COPY ["services/administration/src/MyProjectName.Administration.Application.Contracts/MyProjectName.Administration.Application.Contracts.csproj", "services/administration/src/MyProjectName.Administration.Application.Contracts/"]
COPY ["services/saas/src/MyProjectName.SaaS.Domain.Shared/MyProjectName.SaaS.Domain.Shared.csproj", "services/saas/src/MyProjectName.SaaS.Domain.Shared/"]
COPY ["services/administration/src/MyProjectName.Administration.HttpApi/MyProjectName.Administration.HttpApi.csproj", "services/administration/src/MyProjectName.Administration.HttpApi/"]
COPY ["services/administration/src/MyProjectName.Administration.Application/MyProjectName.Administration.Application.csproj", "services/administration/src/MyProjectName.Administration.Application/"]
RUN dotnet restore "./services/administration/host/MyProjectName.Administration.HttpApi.Host/MyProjectName.Administration.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/administration/host/MyProjectName.Administration.HttpApi.Host"
RUN dotnet build "./MyProjectName.Administration.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MyProjectName.Administration.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyProjectName.Administration.HttpApi.Host.dll"]