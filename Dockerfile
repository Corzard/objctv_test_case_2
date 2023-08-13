# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /src
COPY objctv_test_case_2.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "objctv_test_case_2.dll"]

# FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
# WORKDIR /src
# COPY src/*.csproj .
# RUN dotnet restore
# COPY . ./
# RUN dotnet publish -c Release -o /publish
# FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
# WORKDIR /publish
# COPY --from=build-env /publish .
# EXPOSE 80
# ENTRYPOINT ["dotnet", "objctv_test_case_2.dll"]