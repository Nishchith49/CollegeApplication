# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the project files into the container
COPY *.csproj ./ 

# Restore dependencies
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Publish the application
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory for the runtime container
WORKDIR /app

# Copy the published application from the build container
COPY --from=build /app/publish .

# Expose the port that the app will run on
EXPOSE 80

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "CollegeApplication.dll"]
