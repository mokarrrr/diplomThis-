FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["diplom.csproj", "."]
RUN dotnet restore "./././diplom.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./diplom.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "./diplom.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "diplom.dll"]
