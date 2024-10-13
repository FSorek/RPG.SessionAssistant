FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Blazor.WASM/Blazor.WASM.csproj", "Blazor.WASM/"]
RUN dotnet restore "Blazor.WASM/Blazor.WASM.csproj"
COPY . .
WORKDIR "/src/Blazor.WASM"
RUN dotnet build "Blazor.WASM.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blazor.WASM.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blazor.WASM.dll"]
