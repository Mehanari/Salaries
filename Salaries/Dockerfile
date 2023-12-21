FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Salaries/Salaries.csproj", "Salaries/"]
RUN dotnet restore "Salaries/Salaries.csproj"
COPY . .
WORKDIR "/src/Salaries"
RUN dotnet build "Salaries.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Salaries.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Salaries.dll"]
