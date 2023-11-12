FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
COPY ../certs/aspnetapp.pfx /https/
EXPOSE 37360
EXPOSE 443

ENV ASPNETCORE_URLS="https://+;http://+"
ENV ASPNETCORE_HTTPS_PORT=443

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:3.1 AS build
ARG configuration=Release
WORKDIR /src
COPY ["HCMClient/HCMClient.csproj", "HCMClient/"]
RUN dotnet restore "HCMClient/HCMClient.csproj"
COPY . .
WORKDIR "/src/HCMClient"
RUN dotnet build "HCMClient.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "HCMClient.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HCMClient.dll"]
