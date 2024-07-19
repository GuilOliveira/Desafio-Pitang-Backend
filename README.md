# Desafio Pitang Backend

## Descrição

O **Desafio Pitang Backend** é uma API que desenvolvi para o desafio final do programa de estágio da Pitang. A API permite criar, consultar, atualizar e excluir agendamentos, além de autenticar usuários e registrar novos usuários.

## Funções de destaque:

- Testes que garantem validações sólidas nos endpoints da aplicação.
- Testes com banco de dados em memória.
- Login com Token JWT e com Refresh Token.
- Controle de transação para proteger a aplicação de inconsistência de dados
- **Diferentes niveis de autênticação**, um usuário comum pode apenas alterar seus registros, um administrador pode alterar todos.
- Responsabilidades bem divididas graças a arquitetura robusta do projeto.
- todas as operações do CRUD, com destaque no Patch para alterar apenas uma parte do conteúdo.
- Uso do padrão [Conventional Commits](https://www.conventionalcommits.org/pt-br/v1.0.0-beta.4/) em conjunto com [Git Flow](https://dev.to/gabrielduete/gitflow-4dok) para uma melhor organização de Repositório.
- Endpoints adicionais para melhorar a experiência do usuário.

# Como Rodar o Projeto

## Pré-requisitos

Antes de começar, certifique-se de que você tem os seguintes itens instalados:

- SQL Server
- Visual Studio Community
- .NET 6.0

## Configuração do Backend

### 1. Clonar o Repositório

Clone o repositório do backend:

```bash
git clone https://github.com/GuilOliveira/Desafio-Pitang-Backend.git
cd Desafio-Pitang-Backend
```

### 2. Configurar o Banco de Dados

- Crie um banco de dados no SQL Server.
- Execute o script SQL presente no arquivo **script.sql** localizado na raiz do projeto para criar as tabelas necessárias.

### 3. Atualizar a Connection String

Abra o arquivo _appsettings.json_ na camada de WebApi do projeto e atualize a connection string com as informações do seu banco de dados:

```json
"ConnectionStrings": {
  "DefaultConnection": "database=[NOME DO SEU BANCO DE DADOS]; server=[SEU SERVIDOR SQL];Trusted_Connection=True;Trust Server Certificate=true "
}
```

### 4. Executar o Projeto

- Abra o repositório clonado no Visual Studio Community.
- Compile e execute o projeto para garantir que tudo esteja funcionando corretamente.
- Use o [Frontend](https://github.com/GuilOliveira/Desafio-Pitang-Frontend) da aplicação para uma melhor experiência!

# Requisitos do Sistema

| Requisito                                                                                                                                                                                                              | Tipo             | UI  | Usável | Totalmente Concluído / Validado |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------- | --- | ------ | ------------------------------- |
| O agendamento deve ser feito em uma página por um formulário.                                                                                                                                                          | Regra de uso     | Yes | Yes    | Yes                             |
| A disponibilidade das vagas são de 20 por dia.                                                                                                                                                                         | Regra de uso     | Yes | Yes    | Yes                             |
| Cada horário só tem a disponibilidade de 2 agendamentos para o mesmo horário.                                                                                                                                          | Regra de uso     | Yes | Yes    | Yes                             |
| Deve ser criada uma página para consultar os agendamentos.                                                                                                                                                             | Regra de uso     | Yes | Yes    | Yes                             |
| O resultado dos agendamentos deve ser agrupado por dia e hora do agendamento.                                                                                                                                          | Regra de uso     | Yes | Yes    | Yes                             |
| O intervalo de tempo entre um agendamento e outro é de 1 hora.                                                                                                                                                         | Regra de uso     | Yes | Yes    | Yes                             |
| O paciente deve informar seu nome, data de nascimento e dia e horário para o agendamento.                                                                                                                              | Regra de negócio | Yes | Yes    | Yes                             |
| Deverá ser checado se o formulário foi preenchido.                                                                                                                                                                     | Regra de negócio | Yes | Yes    | Yes                             |
| Os dados do paciente/agendamentos devem ser armazenados em memória e dentro de um BehaviorSubject.                                                                                                                     | Regra de negócio | Yes | Yes    | Yes                             |
| Exibir mensagem de agendamento criado com sucesso dentro de um modal/popup, usando um service para controlar o estado do modal.                                                                                        | Regra de negócio | Yes | Yes    | Yes                             |
| Dentro da página para consultar os agendamentos deve ser possível visualizar a listagem de agendamentos feitos e informar se o agendamento foi realizado ou não, e qual a conclusão do atendimento (se foi realizado). | Regra de negócio | Yes | Yes    | Yes                             |
| Quando o usuário der F5 ou recarregar a página os dados não podem ser perdidos, devem ser salvadas no localStorage.                                                                                                    | Regra de negócio | Yes | Yes    | Yes                             |
| Criar um ícone de notificação que será responsável por mostrar a quantidade de agendamentos do usuário e deverá ser atualizado sempre que um novo agendamento for realizado.                                           | Regra de negócio | Yes | Yes    | Yes                             |

# Regras projeto

- [x] Uso de observables no Angular.
- [x] Utilizar DatePicker do Angular Material.
- [x] Evitar any no typescript.
- [x] Usar Pipe para tratamento de datas.
- [x] Separar responsabilidades de Template, Componente e Services.
- [x] Implementar componente de Loading nas chamadas a API.
- [x] Banco de dados: SQL Server Express
- [x] Versão dotnet core: 6
- [x] Versão do angular: 17 ou 18
- [x] Implementar os controllers que disponibilizam as APIs para o
      frontend.
- [x] Criar endpoints para criar, ler, atualizar e deletar (CRUD).
- [x] Criar as interfaces de todos os serviços de negócios.
- [x] Implementar as regras de negócio necessárias.
- [x] Criar as entidades de negócio, bem como os DTOs e Models
      necessários para a aplicação.
- [x] Criar as validações de negócio para garantir a integridade dos
      dados.
- [x] Utilizar o fluentvalidation ou realizar validações “manuais”.
- [x] Criar as interfaces de todos os repositórios.
- [x] Mapear as entidades para o banco de dados utilizando Entity
- [x] Implementar os repositórios e suas consultas.
- [x] Não utilizar migrations.
- [x] Implementação de testes unitários em sua respectiva camada

## Critérios de destaque:

- [x] Implementar testes unitários com banco de dados em memória.
- [x] Autenticação JWT.
- [x] Controle de transação.
- [x] Criar validação customizada.

## Critérios de avaliação:

Organização do código.

Utilização dos princípios do SOLID.

• Organização dos commits.

o Commits por features, utilizando os princípios do gitflow.

• Organização do repositório.

o Criação de um repositório para Front e outro Back-end.

• Seguir todas as regras estabelecidas.

# Endpoints

### Agendamentos

- **Obter todos os agendamentos**

  - `GET /api/Appointment/GetAll`
  - Resposta: `200 OK` com uma lista de agendamentos.

- **Obter agendamentos por intervalo de datas**

  - `GET /api/Appointment/GetByDate`
  - Parâmetros:
    - `initialDate` (string, formato: date-time)
    - `finalDate` (string, formato: date-time)
  - Resposta: `200 OK` com uma lista de agendamentos.

- **Obter agendamentos por usuário**

  - `GET /api/Appointment/GetByUser`
  - Resposta: `200 OK` com uma lista de agendamentos.

- **Atualizar o status de um agendamento**

  - `PATCH /api/Appointment/Update/Status`
  - Corpo da Requisição: `application/json` com Id e Status.
  - Resposta: `200 OK` com o agendamento atualizado.

- **Deletar um agendamento**
  - `DELETE /api/Appointment/Delete`
  - Parâmetros:
    - `id` (integer)
  - Resposta: `200 OK`.

### Autenticação

- **Autenticar usuário**

  - `GET /api/Authentication/Login`
  - Parâmetros:
    - `email` (string)
    - `password` (string)
  - Resposta: `200 OK` com um `Token JWT e Refresh Token`.

- **Atualizar token de autenticação**
  - `GET /api/Authentication/RefreshToken`
  - Resposta: `200 OK` com um `Token JWT e Refresh Token`.

### Registro

- **Registrar um novo agendamento**

  - `POST /api/Scheduling/Register`
  - Corpo da Requisição: `application/json` com `SchedulingModel`.
  - Resposta: `200 OK` com o agendamento registrado.

- **Registrar um novo usuário**
  - `POST /api/User/Register`
  - Corpo da Requisição: `application/json` com `UserRegistrationModel`.
  - Resposta: `200 OK`.
