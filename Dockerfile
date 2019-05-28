FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["/LightLinkAPI", "/Server"]
COPY ["/LightLinkModels", "/LightLinkModels"]
RUN dotnet restore "/Server/LightLinkAPI/LightLinkAPI.csproj"
RUN dotnet build "/Server/LightLinkAPI/LightLinkAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "/Server/LightLinkAPI/LightLinkAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LightLinkAPI.dll"]