# Build stage using Microsoft .NET SDK image for build environment.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Sets working directory in container.
WORKDIR /src
# Copies all files into the container.
COPY . .
# Restores the build and publishes config outputting compiled application into /app/publish in container.
RUN dotnet publish BlazorProjectApp1/BlazorProjectApp1.csproj -c Release -o /app/publish

# Runtime stage using the ASP.NET runtime image for running the application.
FROM mcr.microsoft.com/dotnet/aspnet:8.0
# Adding non-root appuser due to SonarQube security alerts regarding default user with root access.
USER appuser
# Sets the directory for the application.
WORKDIR /app
# Copies the output from the builstage into the runtime.
COPY --from=build /app/publish .
# Defines the entrypoint for how to run the program.
ENTRYPOINT ["dotnet", "BlazorProjectApp1.dll"]