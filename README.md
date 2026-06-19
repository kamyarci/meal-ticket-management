## Meal Ticket Management

### Objetivo

API REST que gerencia a entrega de tickets de refeição para funcionários, permitindo visualizar o total de tickets entregues por pessoa e no geral em um determinado período.

### Stack

Backend:
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Docker Compose
- Swagger
- xUnit
- Moq

Frontend:
- Next.js 15 + React 19 + TypeScript
- shadcn/ui + Tailwind CSS 4
- Axios
- Lucide React

### Arquitetura

A aplicação foi desenvolvida seguindo os princípios da Clean Architecture, dividida nas seguintes camadas:

- **Domain**: Entidades, enums, interfaces dos repositórios, guards e exceptions. Não possui dependências externas.
- **Application**: Use cases, DTOs e interfaces dos use cases. Depende apenas do Domain.
- **Infrastructure**: Implementação dos repositórios e configuração do EF Core com PostgreSQL. Depende do Domain.
- **API**: Controllers, middlewares e configuração da aplicação. Depende de Application e Infrastructure.
- **Tests**: Testes unitários das entidades e use cases com xUnit e Moq.

### Decisões de Arquitetura

Clean Arch com projetos separados: Optei por separar cada camada em um projeto ao invés de pastas dentro de um único projeto, haja vista que isso me garante que as dependências entre camadas sejam respeitadas em tempo de compilação. Ou seja, a camada de infra não pode referenciar a api, a camada de domínio não pode referenciar nada externo, etc.

Use Cases e não services: Optei dessa forma porque nos meus use cases eu consigo ter apenas um método que implementa uma interface própria, e isso facilita os testes unitários, porque cada use case é mockável individualmente. Já em uma arquitetura com services, muitas vezes um service tem vários métodos e isso pode dificultar a criação de mocks específicos para cada funcionalidade.

Entidades do domain diretamente no EF Core: Em vez de criar entidades de persistência separadas com mappers, optei que as entidades do domínio são usadas diretamente com o EF Core via Fluent API. Para uma aplicação desse porte, essa abordagem reduz duplicação sem comprometer o encapsulamento.

Guard nas entidades: As validações ficam de maneira centralizada, garantindo que uma entidade inválida nunca seja criada, seja via API ou diretamente no código. Isso mantém a integridade do domínio e facilita a manutenção.

Injeçao de dependência por camada: a ideia é que cada camada registra suas próprias dependências, mantendo o Program.cs limpo e deixando que cada camada seja responsável por suas dependências.

### Funcionalidades

- Cadastro e edição de funcionários
- Cadastro e edição de tickets de refeição
- Relatório por período com total por funcionário e geral

### Regras de Negócio

- CPF único com validação matemática
- Status sempre ativo na criação de funcionário e ticket
- Funcionário pode ser inativado, mas não excluído
- Funcionário deve estar ativo para criar ticket
- Quantidade obrigatória e maior que zero
- Data de entrega automática e imutável
- Nenhum registro pode ser excluído

### Endpoints

**Employees:**
- `POST /api/employees`
- `PUT /api/employees/{id}`
- `GET /api/employees`
- `GET /api/employees/{id}`

**MealTickets:**
- `POST /api/mealtickets`
- `PUT /api/mealtickets/{id}`
- `GET /api/mealtickets`
- `GET /api/mealtickets/{id}`
- `GET /api/mealtickets/report?startDate={startDate}&endDate={endDate}`


### Diagrama

employees
├── Id (uuid, PK)   
├── Name (varchar 100)
├── Cpf (varchar 11, unique)
├── Status (varchar 1)
└── UpdatedAt (timestamptz)

meal_tickets
├── Id (uuid, PK)
├── EmployeeId (uuid, FK → employees.Id)                                                                                                                                                                                                                                                                                                                                                                                                                  
├── Quantity (integer)
├── DeliveredAt (timestamptz)
├── Status (varchar 1)
└── UpdatedAt (timestamptz)

### Como rodar

#### Com Docker
1. Clone o repositório
2. Navegue até a pasta do projeto
3. Execute `docker-compose up --build`
4. Frontend estará disponível em `http://localhost:3000`
5. API estará disponível em `http://localhost:8080`
6. Acesse `http://localhost:8080/swagger` para visualizar a documentação
7. Para encerrar, execute `docker-compose down`

#### Localmente
1. Copie `backend/MealTicketManagement.Api/appsettings.Development.example.json` para `appsettings.Development.json` e preencha os dados do PostgreSQL
2. Rode o backend: `cd backend/MealTicketManagement.Api && dotnet run`
3. Rode o frontend: `cd frontend && yarn install && yarn dev`
4. Frontend disponível em `http://localhost:3000`

#### Testes
1. Execute `dotnet test` na pasta `backend`