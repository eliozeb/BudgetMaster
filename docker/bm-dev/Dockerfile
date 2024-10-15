# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and project files
COPY BudgetMaster.sln ./
COPY src/ src/

# Restore dependencies
RUN dotnet restore

# Build and publish the app
RUN dotnet publish src/BudgetMaster.WebUI/BudgetMaster.WebUI.csproj -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /app/out ./

# Expose ports
EXPOSE 80

# Set environment variable for ASP.NET Core
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "BudgetMaster.WebUI.dll"]