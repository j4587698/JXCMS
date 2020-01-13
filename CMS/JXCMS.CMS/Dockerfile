FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CMS/JXCMS.CMS/JXCMS.CMS.csproj", "CMS/JXCMS.CMS/"]
RUN dotnet restore "CMS/JXCMS.CMS/JXCMS.CMS.csproj"
COPY . .
WORKDIR "/src/CMS/JXCMS.CMS"
RUN dotnet build "JXCMS.CMS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JXCMS.CMS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JXCMS.CMS.dll"]