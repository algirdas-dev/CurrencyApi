FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Currency.App/Currency.App.csproj", "Currency.App/"]
RUN dotnet restore "Currency.App/Currency.App.csproj"
COPY . .
WORKDIR "/src/Currency.App"
RUN dotnet build "Currency.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Currency.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Currency.App.dll"]
