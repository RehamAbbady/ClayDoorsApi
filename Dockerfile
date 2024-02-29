FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY *.sln ./

COPY DoorManagementSystem.API/*.csproj ./DoorManagementSystem.API/
COPY DoorManagementSystem.Infrastructure/*.csproj ./DoorManagementSystem.Infrastructure/
COPY DoorManagementSystem.Domain/*.csproj ./DoorManagementSystem.Domain/
COPY DoorManagementSystem.Application/*.csproj ./DoorManagementSystem.Application/
COPY DoorManagementSystem.Test/*.csproj ./DoorManagementSystem.Test/

RUN dotnet restore --verbosity detailed /p:RestoreMaxCpus=1

COPY DoorManagementSystem.API/. ./DoorManagementSystem.API/
COPY DoorManagementSystem.Infrastructure/. ./DoorManagementSystem.Infrastructure/
COPY DoorManagementSystem.Domain/. ./DoorManagementSystem.Domain/
COPY DoorManagementSystem.Application/. ./DoorManagementSystem.Application/
COPY DoorManagementSystem.Test/. ./DoorManagementSystem.Test/

WORKDIR /app/DoorManagementSystem.API
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/DoorManagementSystem.API/out .
EXPOSE 7063
EXPOSE 5123
ENTRYPOINT ["dotnet", "DoorManagementSystem.API.dll"]
