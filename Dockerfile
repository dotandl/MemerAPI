FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /MemerAPI

COPY MemerAPI/MemerAPI.csproj ./
RUN dotnet restore

COPY MemerAPI/* ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /MemerAPI
COPY --from=build /MemerAPI/out ./

EXPOSE 80
CMD dotnet MemerAPI.dll --urls=http://localhost:80
