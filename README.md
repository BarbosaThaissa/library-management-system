# Library Management System

Sistema de gerenciamento de autores, gêneros e livros.  
Backend em .NET C#, frontend em React. API RESTful com CRUD completo e SPA funcional.

## Tecnologias

- **Backend:** .NET 10, C#, Entity Framework, PostgreSQL
- **Frontend:** React, TypeScript, Axios, Vite
- **Testes:** xUnit, Moq, FluentAssertions
- **Documentação:** Swagger

## Estrutura do Projeto

```

backend/
├─ LibraryManagement.Api # API principal
├─ LibraryManagement.Application
├─ LibraryManagement.Domain
├─ LibraryManagement.Infrastructure
└─ LibraryManagement.Application.Tests

frontend/
├─ src/
│ ├─ pages/ # Páginas React (Authors, Genres, Books)
│ ├─ services/ # Serviços para consumir API
│ ├─ models/ # Interfaces/Types
│ ├─ api/ # Configuração do Axios
│ └─ App.tsx # Configuração das rotas
└─ package.json

```

## Backend

### Configuração

1. Navegue até a pasta do backend:

```bash
cd backend/LibraryManagement.Api
```

2. Restaurar pacotes:

```bash
dotnet restore
```

3. Configurar o banco de dados e rodar migrations:

```bash
dotnet ef database update
```

4. Rodar a API:

```bash
dotnet run
```

A API ficará disponível em:
`http://localhost:5120/api/v1`

### Endpoints da API

**Authors**

- `GET /api/v1/Authors` - Listar todos
- `GET /api/v1/Authors/{id}` - Obter por id
- `POST /api/v1/Authors` - Criar
- `PUT /api/v1/Authors/{id}` - Atualizar
- `DELETE /api/v1/Authors/{id}` - Deletar

**Genres**

- `GET /api/v1/Genres`
- `GET /api/v1/Genres/{id}`
- `POST /api/v1/Genres`
- `PUT /api/v1/Genres/{id}`
- `DELETE /api/v1/Genres/{id}`

**Books**

- `GET /api/v1/Books`
- `GET /api/v1/Books/{id}`
- `POST /api/v1/Books`
- `PUT /api/v1/Books/{id}`
- `DELETE /api/v1/Books/{id}`

> A documentação completa pode ser acessada via Swagger ao rodar a API.

---

## Frontend

### Configuração

1. Navegue até a pasta do frontend:

```bash
cd frontend
```

2. Instalar dependências:

```bash
npm install
```

3. Rodar a aplicação:

```bash
npm run dev
```

A aplicação ficará disponível em:
`http://localhost:5173`

### Rotas da SPA

- `/authors` - Authors
- `/genres` - Genres
- `/books` - Books

### Funcionalidades

- CRUD completo de Authors, Genres e Books
- Mini navbar para navegação entre páginas
- Layout padronizado com cards e formulários
- Deleção com confirmação
- Mensagens de carregamento (loading) durante operações

---

## Testes Backend

Os testes unitários estão localizados em:
`LibraryManagement.Application.Tests`

Para rodar:

```bash
dotnet test
```

- Cobre os Services (AuthorService, GenreService, BookService)
- Utiliza Moq para mock de repositórios
- FluentAssertions para asserções legíveis

---

## Observações

- Certifique-se de configurar a variável de ambiente `VITE_API_BASE_URL` no frontend, apontando para a URL do backend.
- A aplicação utiliza DTOs, migrations e versionamento da API.
