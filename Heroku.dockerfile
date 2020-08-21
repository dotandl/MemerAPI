FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /MemerAPI

COPY MemerAPI/MemerAPI.csproj ./
RUN dotnet restore

COPY MemerAPI/* ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /MemerAPI
COPY --from=build /MemerAPI/out ./

# Heroku needs the server to run on host 0.0.0.0 at the random port saved in
# PORT environment variable
CMD dotnet MemerAPI.dll --urls=http://0.0.0.0:${PORT}
