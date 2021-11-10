FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
HEALTHCHECK --interval=5s --timeout=10s --retries=3 CMD curl --fail https://localhost:80/healthz || exit 1
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MasAcademyLab.Web/MasAcademyLab.Web.csproj", "MasAcademyLab.Web/"]
COPY ["MasAcademyLab.Service/MasAcademyLab.Service.csproj", "MasAcademyLab.Service/"]
COPY ["MasAcademyLab.Data/MasAcademyLab.Data.csproj", "MasAcademyLab.Data/"]
COPY ["MasAcademyLab.Domain/MasAcademyLab.Domain.csproj", "MasAcademyLab.Domain/"]
RUN dotnet restore "MasAcademyLab.Web/MasAcademyLab.Web.csproj"
COPY . .
WORKDIR "/src/MasAcademyLab.Web"
RUN dotnet build "MasAcademyLab.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MasAcademyLab.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MasAcademyLab.Web.dll"]