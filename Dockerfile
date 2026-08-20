# Stage 1: Build the app using the full .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy just the project file first, restore packages
# (this ordering helps Docker cache the restore step efficiently)
COPY *.csproj ./
RUN dotnet restore

# Now copy everything else and build
COPY . .
RUN dotnet publish -c Release -o /out

# Stage 2: Runtime-only image, much smaller than the SDK image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "CRUD_WEBAPI.dll"]