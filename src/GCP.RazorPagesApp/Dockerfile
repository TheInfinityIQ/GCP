#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

ARG buildConfiguration=Release

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_ENVIRONMENT=Production

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GCP.RazorPagesApp/GCP.RazorPagesApp.csproj", "GCP.RazorPagesApp/"]
RUN dotnet restore "GCP.RazorPagesApp/GCP.RazorPagesApp.csproj"
COPY . .
WORKDIR "/src/GCP.RazorPagesApp"
RUN dotnet build "GCP.RazorPagesApp.csproj" -c "$buildConfiguration" -o /app/build

FROM build AS script
RUN dotnet tool install -g dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet ef migrations script -i -o "./Data/Scripts/migrations.sql" -c "$buildConfiguration" 

FROM build AS publish
RUN dotnet publish "GCP.RazorPagesApp.csproj" -c "$buildConfiguration" -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=script /src/GCP.RazorPagesApp/Data/Scripts/ ./Scripts/
ENTRYPOINT ["dotnet", "GCP.RazorPagesApp.dll"]