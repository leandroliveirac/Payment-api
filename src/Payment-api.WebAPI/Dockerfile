#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Payment-api.WebAPI/Payment-api.WebAPI.csproj", "Payment-api.WebAPI/"]
COPY ["src/Payment-api.Infra.IOC/Payment-api.Infra.IOC.csproj", "Payment-api.Infra.IOC/"]
COPY ["src/Payment-api.Infra.Data/Payment-api.Infra.Data.csproj", "Payment-api.Infra.Data/"]
COPY ["src/Payment-api.Domain/Payment-api.Domain.csproj", "Payment-api.Domain/"]
COPY ["src/Payment-api.Application/Payment-api.Application.csproj", "Payment-api.Application/"]
RUN dotnet restore "Payment-api.WebAPI/Payment-api.WebAPI.csproj"
COPY . .
# WORKDIR "/src/Payment-api.WebAPI"
RUN dotnet build "src/Payment-api.WebAPI/Payment-api.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/Payment-api.WebAPI/Payment-api.WebAPI.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment-api.WebAPI.dll"]