FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SalaryCalculator.Web/SalaryCalculator.Web.csproj", "SalaryCalculator.Web/"]
COPY ["SalaryCalculator.Application/SalaryCalculator.Application.csproj", "SalaryCalculator.Application/"]
COPY ["SalaryCalculator.Infrastructure/SalaryCalculator.Infrastructure.csproj", "SalaryCalculator.Infrastructure/"]
RUN dotnet restore "SalaryCalculator.Web/SalaryCalculator.Web.csproj"
COPY . .
WORKDIR "/src/SalaryCalculator.Web"
RUN dotnet build "SalaryCalculator.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalaryCalculator.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalaryCalculator.Web.dll"]
