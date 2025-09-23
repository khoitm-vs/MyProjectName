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
COPY ["services/projects/NuGet.Config", "services/projects/"]
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/projects/host/MyProjectName.Projects.HttpApi.Host/MyProjectName.Projects.HttpApi.Host.csproj", "services/projects/host/MyProjectName.Projects.HttpApi.Host/"]
COPY ["shared/MyProjectName.Microservice.Shared/MyProjectName.Microservice.Shared.csproj", "shared/MyProjectName.Microservice.Shared/"]
COPY ["services/administration/src/MyProjectName.Administration.EntityFrameworkCore/MyProjectName.Administration.EntityFrameworkCore.csproj", "services/administration/src/MyProjectName.Administration.EntityFrameworkCore/"]
COPY ["shared/MyProjectName.Shared/MyProjectName.Shared.csproj", "shared/MyProjectName.Shared/"]
COPY ["services/administration/src/MyProjectName.Administration.Domain/MyProjectName.Administration.Domain.csproj", "services/administration/src/MyProjectName.Administration.Domain/"]
COPY ["services/administration/src/MyProjectName.Administration.Domain.Shared/MyProjectName.Administration.Domain.Shared.csproj", "services/administration/src/MyProjectName.Administration.Domain.Shared/"]
COPY ["shared/MyProjectName.Hosting.Shared/MyProjectName.Hosting.Shared.csproj", "shared/MyProjectName.Hosting.Shared/"]
COPY ["shared/MyProjectName.ServiceDefaults/MyProjectName.ServiceDefaults.csproj", "shared/MyProjectName.ServiceDefaults/"]
COPY ["services/projects/src/MyProjectName.Projects.HttpApi/MyProjectName.Projects.HttpApi.csproj", "services/projects/src/MyProjectName.Projects.HttpApi/"]
COPY ["services/projects/src/MyProjectName.Projects.Application.Contracts/MyProjectName.Projects.Application.Contracts.csproj", "services/projects/src/MyProjectName.Projects.Application.Contracts/"]
COPY ["services/projects/src/MyProjectName.Projects.Domain.Shared/MyProjectName.Projects.Domain.Shared.csproj", "services/projects/src/MyProjectName.Projects.Domain.Shared/"]
COPY ["services/projects/src/MyProjectName.Projects.Application/MyProjectName.Projects.Application.csproj", "services/projects/src/MyProjectName.Projects.Application/"]
COPY ["services/projects/src/MyProjectName.Projects.Domain/MyProjectName.Projects.Domain.csproj", "services/projects/src/MyProjectName.Projects.Domain/"]
COPY ["services/projects/src/MyProjectName.Projects.EntityFrameworkCore/MyProjectName.Projects.EntityFrameworkCore.csproj", "services/projects/src/MyProjectName.Projects.EntityFrameworkCore/"]
RUN dotnet restore "./services/projects/host/MyProjectName.Projects.HttpApi.Host/MyProjectName.Projects.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/projects/host/MyProjectName.Projects.HttpApi.Host"
RUN dotnet build "./MyProjectName.Projects.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MyProjectName.Projects.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyProjectName.Projects.HttpApi.Host.dll"]