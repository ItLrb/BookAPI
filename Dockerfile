FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /backend
COPY BookStoreDB.csproj .
RUN dotnet restore "BookStoreDB.csproj"
COPY . .
RUN dotnet publish 'BookStoreDB.csproj' -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT [ "dotnet", "BookStoreDB.dll" ]