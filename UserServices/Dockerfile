FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["UserServices/UserServices.csproj", "UserServices/"]
RUN dotnet restore "UserServices/UserServices.csproj"
COPY . .
WORKDIR "/src/UserServices"
RUN dotnet build "UserServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserServices.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserServices.dll"]
