# 💰 CashFlow

Projeto de controle financeiro pessoal com foco em registrar despesas e gerar relatórios financeiros mensais. Construído em **C#** com princípios da **Clean Architecture** para garantir organização, escalabilidade e fácil manutenção.

---

## 🚀 Funcionalidades já implementadas

- ✅ Registro de despesas/gastos do usuário  
- ✅ Geração de relatórios mensais:
  - 📄 PDF (usando **PDFsharp-MigraDoc**)
  - 📊 Excel (usando **ClosedXML**)
- ✅ Camada de domínio sólida (Core)
- ✅ Swagger UI para documentação e testes de endpoints
- ✅ Validações com **FluentValidation**
- ✅ Mapeamentos com **AutoMapper**
- ✅ Persistência com **Entity Framework**
- ✅ Configuração com **Docker**

---

## 🧱 Arquitetura

O projeto segue os princípios da **Clean Architecture**, com as seguintes camadas:

- **Domain** – regras de negócio e entidades
- **Application** – casos de uso e interfaces
- **Exception** - exceções personalizadas
- **Communication** -- dtos de entrada e saída de dados
- **Infrastructure** – implementação de repositórios e serviços externos
- **API** – camada de apresentação (controllers, Swagger, etc.)

---

## 🧪 Tecnologias & Ferramentas

| Tecnologia           | Função Principal                          |
|----------------------|--------------------------------------------|
| C# / .NET            | Linguagem e plataforma principal           |
| Entity Framework     | ORM e persistência de dados                |
| AutoMapper           | Mapeamento entre DTOs e Entidades          |
| ClosedXML            | Geração de relatórios em Excel             |
| PDFsharp / MigraDoc  | Geração de relatórios em PDF               |
| FluentValidation     | Validação de entrada                       |
| Swagger              | Documentação interativa da API             |
| Docker               | Empacotamento e execução isolada da aplicação |

---

## 📌 Em andamento / Futuras implementações

- 🔐 Autenticação (AuthN) com JWT
- 🔒 Autorização (AuthZ) por nível de acesso
- 👤 Associação dos gastos a um usuário autenticado
- 🧾 Melhorias na estrutura dos relatórios (filtros por categoria, etc.)
- ✅ Testes automatizados (unitários e de integração)
---
## 📦 Como executar localmente (via Docker)

Atualmente, o Docker está configurado apenas para subir o serviço de banco de dados **MySQL**. A aplicação deve ser iniciada separadamente via Visual Studio, CLI ou IDE de sua preferência.

### 1. Subir o banco de dados
```bash
docker-compose up -d
```

Isso iniciará o serviço de banco MySQL, normalmente acessível em:
- Host: localhost
- Porta: 3306 (ou a porta configurada no docker-compose.yml)
- Usuário/Senha: conforme definido no docker-compose.yml

### 2. Rodar a aplicação
Execute o projeto a partir da IDE ou linha de comando. Certifique-se de que a string de conexão da aplicação esteja apontando para o banco que foi iniciado com Docker.

Acesse a aplicação:
- http://localhost:7138/swagger — interface Swagger para testar a API
