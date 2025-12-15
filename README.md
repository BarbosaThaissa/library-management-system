# Library Management System

Sistema de gerenciamento de autores, gêneros e livros.
Backend em .NET C#, frontend em React. API RESTful com CRUD completo e SPA funcional.

## Tecnologias

- **Backend:** .NET 8, C#, Entity Framework, PostgreSQL
- **Frontend:** React, TypeScript, Axios, Vite
- **Testes:** xUnit, Moq, FluentAssertions
- **Documentação:** Swagger

## Estrutura do Projeto

```
backend/
├─ LibraryManagement.Api        # API principal
├─ LibraryManagement.Application
├─ LibraryManagement.Domain
├─ LibraryManagement.Infrastructure
└─ LibraryManagement.Application.Tests

frontend/
├─ src/
│ ├─ pages/                    # Páginas React (Authors, Genres, Books)
│ ├─ services/                 # Serviços para consumir API
│ ├─ models/                   # Interfaces/Types
│ ├─ api/                      # Configuração do Axios
│ └─ App.tsx                    # Configuração das rotas
└─ package.json
```

---

## Configuração com Docker (recomendado)

1. Certifique-se de ter Docker e Docker Compose instalados.
2. Na raiz do projeto, rode:

```bash
docker-compose up --build
```

3. Isso irá subir **banco de dados + backend + frontend** automaticamente.
4. URLs disponíveis:

- Frontend: `http://localhost:5173/books`, `http://localhost:5173/genres`, `http://localhost:5173/authors`
- Backend / Swagger: `http://localhost:5120/swagger/index.html`

> O frontend, quando rodando via Docker, já está configurado para acessar o backend corretamente usando `http://backend:5120/api/v1`.

---

## Configuração Local (sem Docker)

1. Copie o arquivo `.env.example` para `.env` na pasta `frontend`:

```bash
cp frontend/.env.example frontend/.env
```

2. Escolha a URL do backend no `.env`:

```env
VITE_API_BASE_URL=http://localhost:5120/api/v1
```

3. Navegue até a pasta do backend e rode:

```bash
cd backend/LibraryManagement.Api
dotnet restore
dotnet ef database update
dotnet run
```

4. Navegue até o frontend e rode:

```bash
cd frontend
npm install
npm run dev
```

5. URLs disponíveis:

- Frontend: `http://localhost:5173/books`
- Backend / Swagger: `http://localhost:5120/swagger/index.html`

---

## Endpoints da API

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

> A documentação completa pode ser acessada via Swagger.

---

## Testes Backend

Os testes unitários estão em `LibraryManagement.Application.Tests`.

Para rodar:

```bash
dotnet test
```

- Cobre os Services (AuthorService, GenreService, BookService)
- Usa Moq para mock de repositórios
- FluentAssertions para asserções legíveis

---

## Observações

- Use sempre o arquivo `.env.example` como referência para configurar o frontend.
- Docker já resolve as URLs automaticamente; local precisa usar `localhost`.
- A aplicação utiliza DTOs, migrations e versionamento da API.
