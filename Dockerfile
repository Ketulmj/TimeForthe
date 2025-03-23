FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app


COPY TimeForthe.csproj ./
RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /out ./

EXPOSE 5000

EXPOSE 5001

ENTRYPOINT ["dotnet", "TimeForthe.dll"]
