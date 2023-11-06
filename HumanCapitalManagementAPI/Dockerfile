FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 50187

ENV ASPNETCORE_URLS=http://+:50187

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:3.1 AS build
ARG configuration=Release
WORKDIR /src
COPY ["HumanCapitalManagementAPI/HumanCapitalManagementAPI.csproj", "HumanCapitalManagementAPI/"]
RUN dotnet restore "HumanCapitalManagementAPI/HumanCapitalManagementAPI.csproj"
COPY . .
WORKDIR "/src/HumanCapitalManagementAPI"
RUN dotnet build "HumanCapitalManagementAPI.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "HumanCapitalManagementAPI.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HumanCapitalManagementAPI.dll"]
