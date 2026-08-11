# BrazilianCupScraper

Uma solução **.Net** conteinerizada para disponibilização de dados da *Tabela Brasileirão 2026* através de **API REST MVC**, com raspagem de dados através de **Selenium** e persistência em **PostgreSQL**.

---
## Execução

### Pré-requisitos

Para executar o projeto localmente é necessário ter instalado:

- Docker
- Docker Compose

O restante das bibliotecas e frameworks mencionados são obtidos pelo próprio Docker Compose.

### Executando a aplicação

Navegue até a raiz do repositório e execute:

```bash
docker compose up --build
```

Após o primeiro build, que pode levar de **3-4 minutos**, comportamento esperado é:

1. Criação do container PostgreSQL e criação do banco de dados;
2. Espera pelo healthcheck do container PostgreSQL;
3. Criação e inicialização do container da API;
4. Execução da raspagem de dados inicial dos Times do Brasileirão;
5. Inicialização da API.

Em execuções posteriores, caso os times já estejam cadastrados, a raspagem não será executada novamente.

A API está documentada com a biblioteca [**Swagger**](https://swagger.io/), então após o levantamento bem-sucedido é possível visualizar e executar os endpoints da aplicação através da URL:

```text
http://localhost:8080/swagger
```

### Reset de ambiente

Caso seja necessário remover os containers e o volume de dados PostgreSQL basta executar:

```bash
docker compose down -v
```

E para iniciar novamente:

```bash
docker compose up --build
```

Caso haja alterações no código, é necessário a utilização da flag `--no-cache`:

```bash
docker compose up --build --no-cache
```

---
## Arquitetura

A solução foi desenvolvida seguindo princípios de **Domain-Driven Design**, mantendo as responsabilidades distribuídas entre os seguintes projetos:

```text
src/
├── Huebeiro.BrazilianCup.Domain
├── Huebeiro.BrazilianCup.Application
├── Huebeiro.BrazilianCup.Infrastructure
├── Huebeiro.BrazilianCup.Scraper
└── Huebeiro.BrazilianCup.API

tests/
└── Huebeiro.BrazilianCup.Tests
```

### API

Camada principal responsável pela configuração da aplicação, injeção de dependências e exposição dos endpoints através da **API Rest MVC**.

### Domain

Contém as entidades e regras de domínio, sendo elas:

- `Team`
- `Match`

As regras de negócio sobre as estatísticas dos times estão nesta camada.

### Application

Contém os casos de uso da aplicação, abstração de interfaces, contratos de requisição e resposta (DTOs) e a implementação dos serviços utilizados:

- Registro de partidas - `RegisterMatchService` ;
- Consulta da classificação - `GetStandingsService`;
- Cadastro dos times após raspagem - `InitializeTeamsService`.

### Infrastructure

Responsável pela implementação dos repositórios e persistência dos dados utilizando [**Entity Framework Core**](https://github.com/dotnet/efcore) e **PostgreSQL**.

### Scraper

Responsável pela coleta dos dados de classificação através do [**Selenium**](http://selenium.dev/). Como a execução da raspagem também foi conteinerizada, foi necessária a instalação do navegador Chrome durante o processo de build do container.

A raspagem é executada uma única vez, quando a tabela de Times está vazia, para a utilização completa da API.

### Tests

Contém os testes unitários das principais regras e comportamentos da aplicação utilizando as bibliotecas [**xUnit**](https://xunit.net/) e [**Moq**](https://github.com/devlooped/moq).

---