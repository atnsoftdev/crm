FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/crm.web/crm.web.csproj", "src/crm.web/"]
RUN dotnet restore "src/crm.web/crm.web.csproj"
COPY . .
WORKDIR "/src/src/crm.web"
RUN dotnet build "crm.web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "crm.web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "crm.web.dll"]