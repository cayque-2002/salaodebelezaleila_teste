# 💇‍♀️ Sistema de Gerenciamento de Salão de Beleza

Sistema completo para gestão de salão de beleza, incluindo cadastro de clientes, serviços, agendamentos e dashboard com indicadores.

Projeto desenvolvido com arquitetura separada em backend e frontend.

---

## 🚀 Tecnologias Utilizadas

### Backend
- ASP.NET Core 9
- Entity Framework Core
- SQLite (ou SQL Server, dependendo da configuração)
- Arquitetura em camadas (Domain, Application, Infra, API)

### Frontend
- React
- TypeScript
- Axios
- React Router
- Lucide Icons
- CSS modular

---

## 📁 Estrutura do Projeto
backend/
├── SalaoDeBelezaLeila.Api
├── SalaoDeBelezaLeila.Application
├── SalaoDeBelezaLeila.Domain
├── SalaoDeBelezaLeila.Infra

frontend/
├── salao-front
├── src
├── pages
├── components
├── services
├── layouts


---

## ⚙️ Funcionalidades

### 👤 Clientes
- Cadastro de clientes
- Edição e remoção
- Listagem completa

### ✂️ Serviços
- Cadastro de serviços
- Definição de preços
- Gerenciamento

### 📅 Agendamentos
- Criação de agendamentos
- Seleção múltipla de serviços
- Filtro por cliente (usuário logado)
- Status: Pendente, Confirmado, Cancelado
- Edição e cancelamento

### 📊 Dashboard
- Total de atendimentos
- Faturamento total
- Serviços mais realizados
- Faturamento por dia da semana

### 🔐 Controle de Usuário
- Tipo de usuário:
  - Admin
  - Comum (cliente)
- Controle de acesso por perfil

---

## 🧠 Arquitetura

O sistema segue separação em camadas:

- **Domain** → Entidades e enums
- **Application** → Regras de negócio e DTOs
- **Infra** → Acesso a dados (EF Core)
- **API** → Controllers

---

## ▶️ Como executar o projeto

---

# 🔧 BACKEND

### 1. Acessar a pasta do backend

```bash
cd backend/SalaoDeBelezaLeila.Api
```

### 2. Restaurar dependências

```bash
dotnet restore
```

### 4. Executar API

```bash
dotnet run
```

📍 Backend rodará em:

```bash
https://localhost:7229
```

🎨 FRONTEND

### 1. Acessar pasta

```bash
cd frontend/salao-front
```

### 2. Instalar dependências

```bash
npm install
```

### 3. Rodar projeto

```bash
npm run dev
```


📍 Frontend rodará em:
```bash
http://localhost:5173
```
🔗 Comunicação Front + Back

O frontend consome a API via Axios:

```bash
https://localhost:7229/api
```

🔐 Tipos de Usuário
Tipo	Permissões
Admin	Acesso total (clientes, serviços, dashboard, agendamentos)
Comum	Apenas seus próprios agendamentos
📊 Dashboard

O dashboard é baseado em dados reais da semana:

Total de atendimentos
Faturamento
Ranking de serviços
Faturamento diário
⚠️ Observações
O sistema ainda não possui autenticação JWT (usa controle simples por usuário)
Validação de conflito de horário não implementada (versão futura)
Projeto em constante evolução
👨‍💻 Autor

Desenvolvido por Cayque Guilherme
Projeto de estudo e evolução fullstack (.NET + React)

📌 Status

🚧 Em desenvolvimento contínuo
