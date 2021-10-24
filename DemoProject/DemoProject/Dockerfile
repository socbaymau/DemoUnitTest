#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["DemoProject/DemoProject.csproj", "DemoProject/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Servers/Services.csproj", "Servers/"]
RUN dotnet restore "DemoProject/DemoProject.csproj"
COPY . .
WORKDIR "/src/DemoProject"
RUN apt-get update && apt-get install -y openjdk-11-jdk
RUN dotnet tool install dotnet-sonarscanner --global
ENV PATH="${PATH}:/root/.dotnet/tools"
# analyze using SQ
RUN dotnet sonarscanner begin  /o:"socbaymau" \
  /k:"socbaymau_DemoUnitTest" \
  /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="b827dd86adc16a2475d44ef5707e2cd0b2b01500"
RUN dotnet build "DemoProject.csproj" -c Release -o /app/build
RUN dotnet sonarscanner end /d:sonar.login="b827dd86adc16a2475d44ef5707e2cd0b2b01500"

FROM build AS publish
RUN dotnet publish "DemoProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoProject.dll"]