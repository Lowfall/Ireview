FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ireview.Web/Ireview.Web.csproj", "Ireview.Web/"]
COPY ["Ireview.Core/Ireview.Core.csproj", "Ireview.Core/"]
COPY ["Ireview.Infrastructure/Ireview.Infrastructure.csproj", "Ireview.Infrastructure/"]
RUN dotnet restore "Ireview.Web/Ireview.Web.csproj"
COPY . .
WORKDIR /src/Ireview.Web
RUN dotnet build "Ireview.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish  "Ireview.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Ireview.Web.dll