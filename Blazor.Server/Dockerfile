FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Blazor.Server/Blazor.Server.csproj", "Blazor.Server/"]
RUN dotnet restore "Blazor.Server/Blazor.Server.csproj"
COPY . .
WORKDIR "/src/Blazor.Server"
RUN dotnet build "Blazor.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blazor.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blazor.Server.dll"]
