## Meal Ticket Management

### Objetivo

API REST que gerencia a entrega de tickets de refeição para funcionários, permitindo visualizar o total de tickets entregues por pessoa e no geral em um determinado período.

### Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Docker Compose
- Swagger
- xUnit
- Moq

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

- Cadastro de funcionários
- Edição de funcionários
- CPF único com validação matemática
- Status sempre ativo na criação de funcionário
- Funcionário pode ser inativado, mas não excluído
- Data de alteração atualizada a cada edição
- Cadastro de tickets entregues
- Edição de tickets
- Funcionário obrigatório e deve estar ativo para criar ticket
- Quantidade obrigatória e maior que zero
- Data de Entrega automática e imutável
- Status do funcionário sempre ativo na criação do ticket, podendo ser alterado via edição
- Relatório por período com total por funcionário e total geral
- Nenhum registro pode ser excluído
- Docker Compose com PostgreSQL e API
- Migrations automáticas ao iniciar a aplicação
- Swagger para documentação
- Testes unitários com xUnit e Moq
- script.sql para criação manual das tabelas

### Endpoints

**Employees:**
- `POST /api/employees`
- `PUT /api/employees/{id}`
- `GET /api/employees`
- `GET /api/employees/{id}`

**MealTickets:**
- `POST /api/mealtickets`
- `PUT /api/mealtickets/{id}`
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

1. Clone o repositório
2. Navegue até a pasta do projeto
3. Execute `docker-compose up` para iniciar a aplicação e o banco de dados
4. Acesse `http://localhost:8080/swagger` para visualizar a documentação
5. Para rodar os testes, execute `dotnet test` na raiz do projeto
6. Para encerrar, execute `docker-compose down`