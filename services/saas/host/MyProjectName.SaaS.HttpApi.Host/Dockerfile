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
COPY ["services/saas/NuGet.Config", "services/saas/"]
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/saas/host/MyProjectName.SaaS.HttpApi.Host/MyProjectName.SaaS.HttpApi.Host.csproj", "services/saas/host/MyProjectName.SaaS.HttpApi.Host/"]
COPY ["shared/MyProjectName.Microservice.Shared/MyProjectName.Microservice.Shared.csproj", "shared/MyProjectName.Microservice.Shared/"]
COPY ["services/administration/src/MyProjectName.Administration.EntityFrameworkCore/MyProjectName.Administration.EntityFrameworkCore.csproj", "services/administration/src/MyProjectName.Administration.EntityFrameworkCore/"]
COPY ["shared/MyProjectName.Shared/MyProjectName.Shared.csproj", "shared/MyProjectName.Shared/"]
COPY ["services/administration/src/MyProjectName.Administration.Domain/MyProjectName.Administration.Domain.csproj", "services/administration/src/MyProjectName.Administration.Domain/"]
COPY ["services/administration/src/MyProjectName.Administration.Domain.Shared/MyProjectName.Administration.Domain.Shared.csproj", "services/administration/src/MyProjectName.Administration.Domain.Shared/"]
COPY ["shared/MyProjectName.Hosting.Shared/MyProjectName.Hosting.Shared.csproj", "shared/MyProjectName.Hosting.Shared/"]
COPY ["shared/MyProjectName.ServiceDefaults/MyProjectName.ServiceDefaults.csproj", "shared/MyProjectName.ServiceDefaults/"]
COPY ["services/saas/src/MyProjectName.SaaS.HttpApi/MyProjectName.SaaS.HttpApi.csproj", "services/saas/src/MyProjectName.SaaS.HttpApi/"]
COPY ["services/saas/src/MyProjectName.SaaS.Application.Contracts/MyProjectName.SaaS.Application.Contracts.csproj", "services/saas/src/MyProjectName.SaaS.Application.Contracts/"]
COPY ["services/administration/src/MyProjectName.Administration.Application.Contracts/MyProjectName.Administration.Application.Contracts.csproj", "services/administration/src/MyProjectName.Administration.Application.Contracts/"]
COPY ["services/saas/src/MyProjectName.SaaS.Domain.Shared/MyProjectName.SaaS.Domain.Shared.csproj", "services/saas/src/MyProjectName.SaaS.Domain.Shared/"]
COPY ["services/saas/src/MyProjectName.SaaS.Application/MyProjectName.SaaS.Application.csproj", "services/saas/src/MyProjectName.SaaS.Application/"]
COPY ["services/administration/src/MyProjectName.Administration.Application/MyProjectName.Administration.Application.csproj", "services/administration/src/MyProjectName.Administration.Application/"]
COPY ["services/saas/src/MyProjectName.SaaS.Domain/MyProjectName.SaaS.Domain.csproj", "services/saas/src/MyProjectName.SaaS.Domain/"]
COPY ["services/saas/src/MyProjectName.SaaS.EntityFrameworkCore/MyProjectName.SaaS.EntityFrameworkCore.csproj", "services/saas/src/MyProjectName.SaaS.EntityFrameworkCore/"]
RUN dotnet restore "./services/saas/host/MyProjectName.SaaS.HttpApi.Host/MyProjectName.SaaS.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/saas/host/MyProjectName.SaaS.HttpApi.Host"
RUN dotnet build "./MyProjectName.SaaS.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MyProjectName.SaaS.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyProjectName.SaaS.HttpApi.Host.dll"]