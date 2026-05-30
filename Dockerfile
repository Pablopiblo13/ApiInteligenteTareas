FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ApiInteligenteTareas.API/*.csproj ./ApiInteligenteTareas.API/
RUN dotnet restore ./ApiInteligenteTareas.API/ApiInteligenteTareas.API.csproj

COPY . .
RUN dotnet publish ApiInteligenteTareas.API/ApiInteligenteTareas.API.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000

ENTRYPOINT ["dotnet", "ApiInteligenteTareas.API.dll"]
