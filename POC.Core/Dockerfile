FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DSPMediatRPOC/DSPMediatRPOC.csproj", "DSPMediatRPOC/"]
RUN dotnet restore "DSPMediatRPOC/DSPMediatRPOC.csproj"
COPY . .
WORKDIR "/src/DSPMediatRPOC"
RUN dotnet build "DSPMediatRPOC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DSPMediatRPOC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DSPMediatRPOC.dll"]
