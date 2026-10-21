FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore --packages ./packages-cache \
 && dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/runtime:10.0
COPY --from=build /app /app
WORKDIR /work
ENTRYPOINT ["dotnet", "/app/FirstPdf.dll"]