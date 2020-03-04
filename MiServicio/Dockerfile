#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-nanoserver-1903 AS base

ENV Audit:ConnectionString="ConnectionString FROM ENV DOCKERFILE"
ENV InstrumentationKey="InstrumentationKey FROM ENV DOCKERFILE"

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["MiServicio/MiServicio.csproj", "MiServicio/"]
RUN dotnet restore "MiServicio/MiServicio.csproj"
COPY . .
WORKDIR "/src/MiServicio"
RUN dotnet build "MiServicio.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MiServicio.csproj" -c Release -o /app/publish

FROM base AS final


WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiServicio.dll"]