FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["NotificationServices/NotificationServices.csproj", "NotificationServices/"]
RUN dotnet restore "NotificationServices/NotificationServices.csproj"
COPY . .
WORKDIR "/src/NotificationServices"
RUN dotnet build "NotificationServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationServices.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationServices.dll"]
