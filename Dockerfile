#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /SOURCE
COPY . . 

RUN dotnet restore  
RUN dotnet publish "./Api/Api.csproj" -c release -o ./../App


#Build Servi
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build /App ./ 

EXPOSE 5024

ENTRYPOINT ["dotnet","Api.dll"]
