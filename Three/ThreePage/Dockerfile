#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 6688
EXPOSE 443 

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["ThreePage/ThreePage.csproj", "ThreePage/"]
RUN dotnet restore "ThreePage/ThreePage.csproj"
COPY . .
WORKDIR "/src/ThreePage"
RUN dotnet build "ThreePage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ThreePage.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThreePage.dll"]