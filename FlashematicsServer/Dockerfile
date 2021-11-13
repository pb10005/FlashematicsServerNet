#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FlashematicsServer/FlashematicsServer.csproj", "FlashematicsServer/"]
RUN dotnet restore "FlashematicsServer/FlashematicsServer.csproj"
COPY . .
WORKDIR "/src/FlashematicsServer"
RUN dotnet build "FlashematicsServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FlashematicsServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlashematicsServer.dll"]