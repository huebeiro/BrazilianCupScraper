FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["src/Huebeiro.BrazilianCup.API/Huebeiro.BrazilianCup.API.csproj", "src/Huebeiro.BrazilianCup.API/"]
COPY ["src/Huebeiro.BrazilianCup.Application/Huebeiro.BrazilianCup.Application.csproj", "src/Huebeiro.BrazilianCup.Application/"]
COPY ["src/Huebeiro.BrazilianCup.Domain/Huebeiro.BrazilianCup.Domain.csproj", "src/Huebeiro.BrazilianCup.Domain/"]
COPY ["src/Huebeiro.BrazilianCup.Infrastructure/Huebeiro.BrazilianCup.Infrastructure.csproj", "src/Huebeiro.BrazilianCup.Infrastructure/"]
COPY ["src/Huebeiro.BrazilianCup.Scraper/Huebeiro.BrazilianCup.Scraper.csproj", "src/Huebeiro.BrazilianCup.Scraper/"]

RUN dotnet restore "src/Huebeiro.BrazilianCup.API/Huebeiro.BrazilianCup.API.csproj"

COPY . .

WORKDIR "/src/src/Huebeiro.BrazilianCup.API"

RUN dotnet publish "Huebeiro.BrazilianCup.API.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

# Instalação Chrome para utilização do Selenium conteinerizado
RUN apt-get update \
    && apt-get install -y wget gnupg \
    && wget -q -O - https://dl.google.com/linux/linux_signing_key.pub \
        | gpg --dearmor -o /usr/share/keyrings/google-chrome.gpg \
    && echo "deb [arch=amd64 signed-by=/usr/share/keyrings/google-chrome.gpg] http://dl.google.com/linux/chrome/deb/ stable main" \
        > /etc/apt/sources.list.d/google-chrome.list \
    && apt-get update \
    && apt-get install -y google-chrome-stable \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Huebeiro.BrazilianCup.API.dll"]