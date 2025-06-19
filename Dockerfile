# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything
COPY . .

# Restore and publish the app
RUN dotnet restore
RUN dotnet publish -c Release -o out

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy from build stage
COPY --from=build /app/out .

# Expose port 80 (Render uses this)
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "EmployeeAPI.dll"]
