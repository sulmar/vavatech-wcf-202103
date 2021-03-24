#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["08_DotNetCoreApi/WebApiService/WebApiService.csproj", "08_DotNetCoreApi/WebApiService/"]
COPY ["08_DotNetCoreApi/FakeServices/FakeServices.csproj", "08_DotNetCoreApi/FakeServices/"]
COPY ["08_DotNetCoreApi/Models/Models.csproj", "08_DotNetCoreApi/Models/"]
COPY ["08_DotNetCoreApi/IServices/IServices.csproj", "08_DotNetCoreApi/IServices/"]
RUN dotnet restore "08_DotNetCoreApi/WebApiService/WebApiService.csproj"
COPY . .
WORKDIR "/src/08_DotNetCoreApi/WebApiService"
RUN dotnet build "WebApiService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApiService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiService.dll"]