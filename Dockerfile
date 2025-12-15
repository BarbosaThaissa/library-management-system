FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY backend/ ./

WORKDIR /app/LibraryManagement.Api
RUN dotnet restore LibraryManagement.Api.csproj
RUN dotnet publish LibraryManagement.Api.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/LibraryManagement.Api/out ./

EXPOSE 5120
CMD ["dotnet", "LibraryManagement.Api.dll"]
