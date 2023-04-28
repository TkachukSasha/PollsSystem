#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/PollsSystem.Api/PollsSystem.Api.csproj", "src/PollsSystem.Api/"]
COPY ["src/PollsSystem.Application/PollsSystem.Application.csproj", "src/PollsSystem.Application/"]
COPY ["src/PollsSystem.Domain/PollsSystem.Domain.csproj", "src/PollsSystem.Domain/"]
COPY ["src/PollsSystem.Shared/PollsSystem.Shared.csproj", "src/PollsSystem.Shared/"]
COPY ["src/PollsSystem.Utils/PollsSystem.Utils.csproj", "src/PollsSystem.Utils/"]
COPY ["src/PollsSystem.Persistence/PollsSystem.Persistence.csproj", "src/PollsSystem.Persistence/"]
COPY ["src/PollsSystem.Presentation/PollsSystem.Presentation.csproj", "src/PollsSystem.Presentation/"]
RUN dotnet restore "src/PollsSystem.Api/PollsSystem.Api.csproj"
COPY . .
WORKDIR "/src/src/PollsSystem.Api"
RUN dotnet build "PollsSystem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PollsSystem.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:80
ENV ASPNETCORE_ENVIRONMENT Production
ENTRYPOINT ["dotnet", "PollsSystem.Api.dll"]