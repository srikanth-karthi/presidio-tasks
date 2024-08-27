FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Job_Portal_Application.csproj", "."]
RUN dotnet restore "./Job_Portal_Application.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "./Job_Portal_Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Job_Portal_Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY wwwroot ./wwwroot
ENTRYPOINT ["dotnet", "Job_Portal_Application.dll"]

