FROM mcr.microsoft.com/dotnet/core/runtime:2.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /src
COPY ["AnyJob.Api/AnyJob.Api.csproj", "AnyJob.Api/"]
RUN dotnet restore "AnyJob.Api/AnyJob.Api.csproj"
COPY . .
WORKDIR "/src/AnyJob.Api"
RUN dotnet build "AnyJob.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnyJob.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnyJob.Api.dll"]
