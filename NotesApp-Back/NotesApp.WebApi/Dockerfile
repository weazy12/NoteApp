FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5134
EXPOSE 7241

# ---- BUILD ----
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src

COPY ["NotesApp-Back/NotesApp.WebApi/NotesApp.WebApi.csproj", "NotesApp.WebApi/"]
COPY ["NotesApp-Back/NotesApp.BLL/NotesApp.BLL.csproj", "NotesApp.BLL/"]
COPY ["NotesApp-Back/NotesApp.DAL/NotesApp.DAL.csproj", "NotesApp.DAL/"]

RUN dotnet restore "NotesApp.WebApi/NotesApp.WebApi.csproj"

COPY NotesApp-Back/ ./NotesApp-Back/
WORKDIR "/src/NotesApp-Back/NotesApp.WebApi"
RUN dotnet build "NotesApp.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build


# ---- PUBLISH ----
FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "NotesApp.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ---- FINAL IMAGE ----
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotesApp.WebApi.dll"]
