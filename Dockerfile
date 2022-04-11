FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GraphQL.NET/GraphQL.NET.csproj", "GraphQL.NET/"]
RUN dotnet restore "GraphQL.NET.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "GraphQL.NET.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GraphQL.NET.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GraphQL.NET.dll"]
