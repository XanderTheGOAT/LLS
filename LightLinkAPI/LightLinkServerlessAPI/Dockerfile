FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["LightLinkAPI/LightLinkAPI.csproj", "LightLinkAPI/"]
RUN dotnet restore "LightLinkAPI/LightLinkAPI.csproj"
COPY . .
WORKDIR "/src/LightLinkAPI"
RUN dotnet build "LightLinkAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "LightLinkAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LightLinkAPI.dll"]