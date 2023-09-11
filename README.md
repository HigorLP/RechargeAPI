# Só Recarga

# Documentação do Projeto Recharge

Bem-vindo à documentação oficial do projeto Recharge, uma plataforma de comércio eletrônico altamente flexível e escalável.

## Visão Geral

O projeto Recharge é uma solução de comércio eletrônico que abrange o gerenciamento de produtos e a finalização das compras.

## Arquitetura

O Recharge é construído com base em uma arquitetura moderna e escalável, utilizando tecnologias de ponta para fornecer desempenho e confiabilidade excepcionais. Ele consiste em várias entidades principais:

- **Product**: Gerencia os detalhes dos produtos oferecidos pela loja.
- **User**: Registra e autentica os usuários que acessam a plataforma.
- **Purchase**: Rastreia as compras dos clientes, incluindo informações de pagamento e status.
- **CartItem**: Gerencia os itens do carrinho de compras dos usuários.

## Recursos Disponíveis

A plataforma Recharge oferece uma variedade de recursos essenciais para apoiar as operações de comércio eletrônico, incluindo:

- Cadastro e autenticação de usuários.
- Gerenciamento de produtos e categorias.
- Realização de compras, incluindo carrinhos de compras.
- Rastreamento de histórico de compras.
- Atualização e remoção de compras e itens do carrinho.

## Como Usar Esta Documentação

Esta documentação tem como objetivo fornecer uma visão geral abrangente do projeto Recharge, incluindo detalhes sobre os modelos de dados, os repositórios, os serviços e os controladores da API relacionados às principais entidades do sistema.

Você encontrará informações detalhadas sobre como consumir cada parte da API, incluindo endpoints, rotas, payloads JSON e exemplos de respostas.

## Começando

Para começar a usar o Recharge, siga as instruções nesta documentação para acessar e interagir com as diferentes partes da plataforma.

---

### **Classe User**

A classe `User` representa um usuário no sistema. Ela contém informações como nome, CPF, e-mail, telefone, senha, e outras propriedades relacionadas ao usuário. Além disso, possui propriedades para gerenciar compras (Purchases) e endereços (Addresses) associados a esse usuário.

#### Propriedades:

- `Id` (Guid): O identificador único do usuário.
- `Name` (string): O nome do usuário.
- `Cpf` (string opcional): O número de CPF do usuário.
- `Email` (string): O endereço de e-mail do usuário.
- `Phone` (string opcional): O número de telefone do usuário.
- `HashPassword` (string): A senha do usuário, armazenada de forma segura com hash.
- `Role` (string): O papel ou cargo do usuário (definido como "Admin").
- `IsLogged` (bool): Indica se o usuário está logado no sistema.
- `Purchases` (ICollection&lt;Purchase&gt; opcional): Uma coleção de compras associadas a esse usuário.
- `Addresses` (ICollection&lt;Address&gt; opcional): Uma coleção de endereços associados a esse usuário.

#### Construtores:

- `User(string name, string email, string password)`: Construtor que recebe o nome, e-mail e senha do usuário.
- `User(string name, string? cpf, string email, string? phone, string password)`: Construtor que recebe o nome, CPF (opcional), e-mail, telefone (opcional) e senha do usuário.
- `User(bool isLogged)`: Construtor que recebe um parâmetro booleano para definir o status de logado do usuário.
- `User()`: Construtor vazio.

### **Interface IUserRepository**

A interface `IUserRepository` define os métodos para interagir com o repositório de usuários.

#### Métodos:

- `Task<User> RegisterUser(User user)`: Registra um novo usuário.
- `Task<User> GetUserById(Guid id)`: Obtém um usuário pelo seu ID.
- `Task<User> GetUserByEmail(string email)`: Obtém um usuário pelo seu e-mail.
- `Task<User> GetUserByDocument(string cpf)`: Obtém um usuário pelo seu CPF.
- `Task<ICollection<User>> GetAllUsers()`: Obtém todos os usuários.
- `Task<User> CompleteRegister(User user)`: Completa o registro de um usuário.
- `Task<User> UpdateUser(User user)`: Atualiza as informações de um usuário.
- `Task<User> DeleteUser(User user)`: Exclui um usuário.
- `Task<User> LogIn(Guid userId, bool status)`: Realiza o login ou logout de um usuário.

### **Classe UserService**

A classe `UserService` é responsável por implementar a lógica de negócios relacionada a usuários. Ela utiliza o repositório de usuários (`IUserRepository`) para interagir com o banco de dados e realiza a validação dos dados dos usuários.

#### Métodos Públicos:

- `async Task<ResultService<UserResponseDTO>> RegisterUser(RegisterUserDTO userDTO)`: Registra um novo usuário com base nos dados fornecidos no objeto `RegisterUserDTO`.
- `async Task<ResultService<UserResponseDTO>> GetUserById(Guid id)`: Obtém um usuário pelo seu ID.
- `async Task<ResultService<UserResponseDTO>> GetUserByEmail(string email)`: Obtém um usuário pelo seu e-mail.
- `async Task<ResultService<UserResponseDTO>> GetUserByDocument(string cpf)`: Obtém um usuário pelo seu CPF.
- `async Task<ResultService<ICollection<UserResponseDTO>>> GetAllUsers()`: Obtém todos os usuários.
- `async Task<ResultService<UserUpdateDTO>> CompleteRegister(Guid id, UserUpdateDTO userDTO)`: Completa o registro de um usuário com base nos dados fornecidos no objeto `UserUpdateDTO`.
- `async Task<ResultService<UserUpdateDTO>> UpdateUser(Guid id, UserUpdateDTO userDTO)`: Atualiza as informações de um usuário com base nos dados fornecidos no objeto `UserUpdateDTO`.
- `async Task<ResultService<UserResponseDTO>> DeleteUser(Guid id, UserDTO userDTO)`: Exclui um usuário.
- `async Task<string> LogIn(LoginDTO loginDTO)`: Realiza o login de um usuário com base no e-mail e senha fornecidos no objeto `LoginDTO`.

### **Classe UserController**

A classe `UserController` é um controlador da API responsável por receber e responder às solicitações relacionadas a usuários.

#### Rotas:

- `POST /Users/register`: Registra um novo usuário.
- `GET /Users/{id}`: Obtém um usuário pelo seu ID.
- `GET /Users/email/{email}`: Obtém um usuário pelo seu e-mail.
- `GET /Users/cpf/{cpf}`: Obtém um usuário pelo seu CPF.
- `GET /Users`: Obtém todos os usuários.
- `PUT /Users/complete/{id}`: Completa o registro de um usuário.
- `PUT /Users/{id}`: Atualiza as informações de um usuário.
- `DELETE /Users/{id}`

: Exclui um usuário.

- `POST /Users/Login`: Realiza o login de um usuário.

### Autenticação

A autenticação na API é feita por meio de tokens JWT (JSON Web Tokens). Os tokens são gerados após um usuário fazer login com sucesso e devem ser incluídos no cabeçalho de autorização de todas as solicitações autenticadas.

Exemplo de cabeçalho de autorização:

```
Authorization: Bearer {seu_token_jwt}
```

### Endpoints

#### 1. Registro de Usuário

- **Rota:** `POST /Users/register`
- **Descrição:** Registra um novo usuário no sistema.
- **Payload JSON de entrada:**

```json
{
  "Name": "Nome do Usuário",
  "Email": "usuario@email.com",
  "HashPassword": "senha_segura"
}
```

- **Resposta de Sucesso (Status 200 OK):**

```json
{
  "isSuccess": true,
  "message": "Usuário registrado com sucesso.",
  "data": {
    "Id": "1a234bcd-5678-90ef-1234-567890abcdef",
    "Name": "Nome do Usuário",
    "Email": "usuario@email.com"
  }
}
```

- **Resposta de Erro (Status 400 Bad Request):**

```json
{
  "isSuccess": false,
  "message": "A senha não atende aos requisitos de segurança.",
  "data": null
}
```

#### 2. Login de Usuário

- **Rota:** `POST /Users/Login`
- **Descrição:** Realiza o login de um usuário e fornece um token JWT para autenticação subsequente.
- **Payload JSON de entrada:**

```json
{
  "Email": "usuario@email.com",
  "HashPassword": "senha_segura"
}
```

- **Resposta de Sucesso (Status 200 OK):**

```json
{
  "isSuccess": true,
  "message": "Login bem-sucedido.",
  "data": "seu_token_jwt"
}
```

- **Resposta de Erro (Status 400 Bad Request):**

```json
{
  "isSuccess": false,
  "message": "E-mail não cadastrado.",
  "data": null
}
```

#### 3. Obter Usuário por ID

- **Rota:** `GET /Users/{id}`
- **Descrição:** Obtém informações de um usuário com base no seu ID.
- **Parâmetros de URL:**
  - `id` (Guid): O ID único do usuário.
- **Resposta de Sucesso (Status 200 OK):**

```json
{
  "isSuccess": true,
  "message": "Usuário encontrado.",
  "data": {
    "Id": "1a234bcd-5678-90ef-1234-567890abcdef",
    "Name": "Nome do Usuário",
    "Email": "usuario@email.com"
  }
}
```

- **Resposta de Erro (Status 404 Not Found):**

```json
{
  "isSuccess": false,
  "message": "Usuário não encontrado.",
  "data": null
}
```

#### 4. Atualizar Informações do Usuário

- **Rota:** `PUT /Users/{id}`
- **Descrição:** Atualiza informações de um usuário com base no seu ID.
- **Parâmetros de URL:**
  - `id` (Guid): O ID único do usuário.
- **Payload JSON de entrada:**

```json
{
  "Name": "Novo Nome do Usuário",
  "Email": "novo_email@email.com"
}
```

- **Resposta de Sucesso (Status 200 OK):**

```json
{
  "isSuccess": true,
  "message": "Informações do usuário atualizadas com sucesso.",
  "data": {
    "Id": "1a234bcd-5678-90ef-1234-567890abcdef",
    "Name": "Novo Nome do Usuário",
    "Email": "novo_email@email.com"
  }
}
```

- **Resposta de Erro (Status 404 Not Found):**

```json
{
  "isSuccess": false,
  "message": "Usuário não encontrado.",
  "data": null
}
```

#### 5. Excluir Usuário

- **Rota:** `DELETE /Users/{id}`
- **Descrição:** Exclui um usuário com base no seu ID.
- **Parâmetros de URL:**
  - `id` (Guid): O ID único do usuário.
- **Resposta de Sucesso (Status 200 OK):**

```json
{
  "isSuccess": true,
  "message": "Usuário excluído com sucesso.",
  "data": {
    "Id": "1a234bcd-5678-90ef-1234-567890abcdef",
    "Name": "Nome do Usuário",
    "Email": "usuario@email.com"
  }
}
```

- **Resposta de Erro (Status 404 Not Found):**

```json
{
  "isSuccess": false,
  "message": "Usuário não encontrado.",
  "data": null
}
```

#### 6. Obter Todos os Usuários

- **Rota:** `GET /Users`
- **Descrição:** Obtém uma lista de todos os usuários registrados no sistema.
- **Resposta de Sucesso (Status 200 OK):**

```json
{
  "isSuccess": true,
  "message": "Lista de usuários obtida com sucesso.",
  "data": [
    {
      "Id": "1a234bcd-5678-90ef-1234-567890abcdef",
      "Name": "Usuário 1",
      "Email": "usuario1@email.com"
    },
    {
      "Id": "2b345cde-6789-01fg-2345-678901bcdefg",
      "Name": "Usuário 2",
      "Email": "usuario2@email.com"
    }
  ]
}
```

- **Resposta de Erro (Status 404 Not Found):**

```json
{
  "isSuccess": false,
  "message": "Nenhum usuário encontrado.",
  "data": null
}
```

#### 7. Completar Registro de Usuário

- **Rota:** `PUT /Users/complete/{id}`

- **Descrição:** Completa o registro de um usuário após o registro inicial.
- **Parâmetros de URL:**
  - `id` (Guid): O ID único do usuário.
- **Payload JSON de entrada:**

```json
{
  "Name": "Nome Completo do Usuário",
  "Cpf": "12345678901",
  "Phone": "1234567890"
}
```

- **Resposta de Sucesso (Status 200 OK):**

```json
{
  "isSuccess": true,
  "message": "Registro do usuário completado com sucesso.",
  "data": {
    "Id": "1a234bcd-5678-90ef-1234-567890abcdef",
    "Name": "Nome Completo do Usuário",
    "Cpf": "12345678901",
    "Phone": "1234567890",
    "Email": "usuario@email.com"
  }
}
```

- **Resposta de Erro (Status 404 Not Found):**

```json
{
  "isSuccess": false,
  "message": "Usuário não encontrado.",
  "data": null
}
```

---

### Modelo de Endereço (`Address`)

A classe `Address` representa um endereço de usuário no sistema. Ela possui os seguintes atributos:

- `Id` (Guid): O ID único do endereço.
- `Cep` (string): O CEP do endereço.
- `Logradouro` (string): O logradouro do endereço.
- `Complemento` (string opcional): Informações adicionais sobre o endereço.
- `Bairro` (string): O bairro do endereço.
- `Localidade` (string): A localidade (cidade) do endereço.
- `Uf` (string): O estado (UF) do endereço.
- `User` (User): Referência ao usuário ao qual este endereço pertence.
- `UserId` (Guid): O ID do usuário ao qual este endereço pertence.

### Repositório de Endereço (`IAddressRepository`)

A interface `IAddressRepository` define os métodos para interagir com o banco de dados em relação aos endereços dos usuários. Os métodos incluem:

- `CreateAddress`: Cria um novo endereço.
- `GetAddressById`: Obtém um endereço com base no seu ID.
- `GetAddress`: Obtém uma lista de endereços com base em um modelo de endereço.
- `UpdateAddress`: Atualiza um endereço existente.
- `DeleteAddress`: Exclui um endereço.

### Mapeamento de Entidade de Endereço (`AddressMap`)

A classe `AddressMap` define como a entidade de endereço deve ser mapeada para a base de dados por meio do Entity Framework Core. Isso inclui a configuração das chaves primárias e propriedades obrigatórias.

### Repositório de Endereço (`AddressRepository`)

A classe `AddressRepository` implementa a interface `IAddressRepository` e fornece a implementação concreta dos métodos para interagir com o banco de dados em relação aos endereços dos usuários.

### DTO de Endereço (`AddressDTO`)

O DTO (Objeto de Transferência de Dados) `AddressDTO` é usado para transferir dados relacionados a endereços entre a API e o cliente. Ele possui campos correspondentes aos atributos do modelo de endereço.

### Integração com ViaCep (`IViaCepRefit` e `ViaCepService`)

A interface `IViaCepRefit` define um contrato de integração com o serviço ViaCep usando a biblioteca Refit. Ela inclui um método para buscar dados de CEP.

A classe `ViaCepService` implementa a interface `IViaCep` e é responsável por realizar a integração com o serviço ViaCep para buscar dados de endereço com base em um CEP fornecido.

### Controlador de Endereço (`AddressController`)

O controlador `AddressController` define os endpoints relacionados aos endereços dos usuários na API. Estes incluem:

- `GET /Address/viacep/{cep}`: Obtém informações de endereço com base em um CEP fornecido.
- `POST /Address`: Cria um novo endereço.
- `GET /Address/{id}`: Obtém um endereço com base no seu ID.
- `GET /Address`: Obtém uma lista de endereços com base em um modelo de endereço.
- `PUT /Address`: Atualiza um endereço existente.
- `DELETE /Address/{id}`: Exclui um endereço.

Cada endpoint possui sua descrição, payload JSON de entrada (quando aplicável) e respostas de sucesso ou erro esperadas.

Esta documentação fornece uma visão geral completa das classes relacionadas aos endereços dos usuários na API Recharge, incluindo modelos, repositórios, serviços e endpoints. Certifique-se de seguir as convenções de nomenclatura das rotas e os métodos HTTP corretos ao acessar os endpoints da API.

**Documentação Detalhada da API - Endpoints e Consumo**

Nesta documentação, você encontrará informações detalhadas sobre o consumo da API relacionada aos endereços (`Address`) dos usuários no sistema Recharge.

### Endpoint `GET /Address/viacep/{cep}`

**Descrição:**

Este endpoint é usado para buscar informações de endereço com base em um CEP fornecido. Ele faz uso da integração com o serviço ViaCep para obter os detalhes do endereço.

**Requisição:**

- Método: GET
- Rota: `/Address/viacep/{cep}`
- Parâmetros de Rota:
  - `{cep}` (string): O CEP do endereço desejado.

**Exemplo de Requisição:**

```http
GET /Address/viacep/12345000
```

**Resposta de Sucesso:**

- Código de Status: 200 OK
- Corpo da Resposta: Um objeto JSON contendo informações detalhadas do endereço.

**Exemplo de Resposta de Sucesso:**

```json
{
  "cep": "12345-000",
  "logradouro": "Rua das Flores",
  "complemento": "Apto 101",
  "bairro": "Centro",
  "localidade": "Cidade Exemplo",
  "uf": "SP"
}
```

**Resposta de Erro:**

- Código de Status: 400 Bad Request
- Corpo da Resposta: Uma mensagem de erro indicando que o CEP não foi encontrado.

**Exemplo de Resposta de Erro:**

```json
{
  "message": "CEP não encontrado."
}
```

### Endpoint `POST /Address`

**Descrição:**

Este endpoint é usado para criar um novo endereço para um usuário no sistema Recharge.

**Requisição:**

- Método: POST
- Rota: `/Address`
- Corpo da Requisição: Um objeto JSON contendo os dados do novo endereço.

**Exemplo de Corpo da Requisição:**

```json
{
  "cep": "12345-000",
  "logradouro": "Rua das Flores",
  "complemento": "Apto 101",
  "bairro": "Centro",
  "localidade": "Cidade Exemplo",
  "uf": "SP",
  "userId": "4b55b512-1e85-4c0c-9c58-e96db6bb3a84"
}
```

**Resposta de Sucesso:**

- Código de Status: 200 OK
- Corpo da Resposta: Um objeto JSON contendo os detalhes do novo endereço criado.

**Exemplo de Resposta de Sucesso:**

```json
{
  "id": "e5e0576b-6de4-44dd-8e9b-62a9e9db6c8c",
  "cep": "12345-000",
  "logradouro": "Rua das Flores",
  "complemento": "Apto 101",
  "bairro": "Centro",
  "localidade": "Cidade Exemplo",
  "uf": "SP",
  "userId": "4b55b512-1e85-4c0c-9c58-e96db6bb3a84"
}
```

**Resposta de Erro:**

- Código de Status: 400 Bad Request
- Corpo da Resposta: Uma mensagem de erro indicando a falha na criação do endereço.

**Exemplo de Resposta de Erro:**

```json
{
  "message": "Erro ao criar endereço: Detalhes do erro."
}
```

### Endpoint `GET /Address/{id}`

**Descrição:**

Este endpoint é usado para obter os detalhes de um endereço com base no seu ID.

**Requisição:**

- Método: GET
- Rota: `/Address/{id}`
- Parâmetros de Rota:
  - `{id}` (Guid): O ID único do endereço desejado.

**Exemplo de Requisição:**

```http
GET /Address/e5e0576b-6de4-44dd-8e9b-62a9e9db6c8c
```

**Resposta de Sucesso:**

- Código de Status: 200 OK
- Corpo da Resposta: Um objeto JSON contendo os detalhes do endereço.

**Exemplo de Resposta de Sucesso:**

```json
{
  "id": "e5e0576b-6de4-44dd-8e9b-62a9e9db6c8c",
  "cep": "12345-000",
  "logradouro": "Rua das Flores",
  "complemento": "Apto 101",
  "bairro": "Centro",
  "localidade": "Cidade Exemplo",
  "uf": "SP",
  "userId": "4b55b512-1e85-4c0c-9c58-e96db6bb3a84"
}
```

**Resposta de Erro:**

- Código de Status: 404 Not Found
- Corpo da Resposta: Uma mensagem de erro indicando que o endereço não foi encontrado.

**Exemplo de Resposta de Erro:**

```json
{
  "message": "Endereço não encontrado."
}
```

### Endpoint `GET /Address`

**Descrição:**

Este endpoint é usado para obter uma lista de endereços com base em um modelo de endereço fornecido. Todos os campos do modelo são opcionais, permitindo consultas flexíveis.

**Requisição:**

- Método: GET
- Rota: `/Address`
- Parâmetros de Consulta (Query Parameters): Os campos do modelo de endereço são usados como parâmetros de consulta.

**Exemplo de Requisição:**

```http
GET /Address?cep=12345-000&localidade=Cidade Exemplo
```

**Resposta de Sucesso:**

- Código de Status: 200 OK
- Corpo da Resposta: Uma lista de objetos JSON contendo os detalhes dos endereços que correspondem aos critérios de consulta.

**Exemplo de Resposta de Sucesso:**

```json
[
  {
    "id": "e5e0576b-6de4-44dd-8e9b-62a9e9db6c8c",
    "cep": "12345-000",
    "logradouro": "Rua das Flores",
    "complemento": "Apto 101",
    "bairro": "Centro",
    "localidade": "Cidade Exemplo",
    "uf": "SP",
    "userId": "4b55b512-1e85-4c0c-9c58-e96db6bb3a84"
  },
  {
    "id": "c4f95219-042d-4cb7-afdb-542e09e13868",
    "cep": "54321-000",
    "logradouro": "Avenida Principal",
    "complemento": null,
    "bairro": "Centro",
    "localidade": "Cidade Exemplo",
    "uf": "SP",
    "userId": "6f8d8499-1f3e-4cf9-8b13-29bfc72d1f61"
  }
]
```

**Resposta de Erro:**

- Código de Status: 400 Bad Request
- Corpo da Resposta: Uma mensagem de erro indicando a falha na consulta.

**Exemplo de Resposta de Erro:**

```json
{
  "message": "Erro ao obter endereços: Detalhes do erro."
}
```

### Endpoint `PUT /Address`

**Descrição:**

Este endpoint é usado para atualizar um endereço existente com base nos dados fornecidos.

**Requisição:**

- Método: PUT
- Rota: `/Address`
- Corpo da Requisição: Um objeto JSON contendo os novos dados do endereço a ser atualizado.

**Exemplo de Corpo da Requisição:**

```json
{
  "id": "e5e0576b-6de4-44dd-8e9b-62a9e9db6c8c",
  "cep": "54321-000",
  "logradouro": "Avenida Principal",
  "bairro": "Centro",
  "localidade": "Cidade Exemplo",
  "uf": "SP"
}
```

**Resposta de Sucesso:**

- Código de Status: 200 OK
- Corpo da Resposta: Um objeto JSON contendo os detalhes do endereço atualizado.

**Exemplo de Resposta de Sucesso:**

```json
{
  "id": "e5e0576b-6de4-44dd-8e9b-62a9e9db6c8c",
  "cep": "54321-000",
  "logradouro": "Avenida Principal",
  "bairro": "Centro",
  "localidade": "Cidade Exemplo",
  "uf": "SP",
  "userId": "4b55b512-1e85-4c0c-9c58-e96db6bb3a84"
}
```

**Resposta de Erro:**

- Código de Status: 400 Bad Request
- Corpo da Resposta: Uma mensagem de erro indicando a falha na atualização do endereço.

**Exemplo de Resposta de Erro:**

```json
{
  "message": "Erro ao atualizar endereço: Detalhes do erro."
}
```

### Endpoint `DELETE /Address/{id}`

**Descrição:**

Este endpoint é usado para excluir um endereço com base no seu ID.

**Requisição:**

- Método: DELETE
- Rota: `/Address/{id}`
- Parâmetros de Rota:
  - `{id}` (Guid): O ID único do endereço a ser excluído.

**Exemplo de Requisição:**

```http
DELETE /Address/e5e0576b-6de4-44dd-8e9b-62a9e9db6c8c
```

**Resposta de Sucesso:**

- Código de Status: 200 OK
- Corpo da Resposta: Um objeto JSON contendo os detalhes do endereço excluído.

**Exemplo de Resposta de Sucesso:**

```json
{
  "id": "e5e0576b-6de4-44dd-8e9b-62a9e9db6c8c",
  "cep": "54321-000",
  "logradouro": "Avenida Principal",
  "bairro": "Centro",
  "localidade": "Cidade Exemplo",
  "uf": "SP",
  "userId": "4b55b512-1e85-4c0c-9c58-e96db6bb3a84"
}
```

**Resposta de Erro:**

- Código de Status: 400 Bad Request
- Corpo da Resposta: Uma mensagem de erro indicando a falha na exclusão do endereço.

**Exemplo de Resposta de Erro:**

```json
{
  "message": "Erro ao remover endereço: Detalhes do erro."
}
```

---

### Modelo da Categoria (`Category`)

A classe `Category` representa uma categoria de produto no sistema Recharge. Abaixo estão os detalhes da classe:

```csharp
namespace Recharge.Domain.Models.Products
{
    public sealed class Category : Entity
    {
        public string Name { get; private set; }
        public ICollection<Product> Products { get; set; }

        public Category(string name)
        {
            Name = name;
            Products = new List<Product>();
        }
    }
}
```

- **Name (string):** O nome da categoria de produto.
- **Products (ICollection<Product>):** Uma coleção de produtos associados a esta categoria.

### Repositório da Categoria (`ICategoryRepository`)

A interface `ICategoryRepository` define os métodos que devem ser implementados para interagir com as categorias no banco de dados. Abaixo estão os detalhes da interface:

```csharp
namespace Recharge.Domain.Repositories.Products
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategory(Category category);
        Task<Category> GetCategoryById(Guid id);
        Task<Category> GetCategoryByName(string name);
        Task<ICollection<Category>> GetAllCategories();
        Task<Category> UpdateCategory(Category category);
        Task<Category> DeleteCategory(Category category);
    }
}
```

- **CreateCategory:** Cria uma nova categoria no banco de dados.
- **GetCategoryById:** Obtém uma categoria com base em seu ID.
- **GetCategoryByName:** Obtém uma categoria com base em seu nome.
- **GetAllCategories:** Obtém uma lista de todas as categorias.
- **UpdateCategory:** Atualiza os detalhes de uma categoria existente.
- **DeleteCategory:** Exclui uma categoria existente.

### Mapeamento da Categoria (`CategoryMap`)

A classe `CategoryMap` é responsável pelo mapeamento da classe `Category` para o banco de dados. Abaixo estão os detalhes da classe:

```csharp
namespace Recharge.Infra.Data.Maps.Products
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.HasMany(x => x.Products).WithOne(c => c.Category).HasForeignKey(x => x.CategoryId);
        }
    }
}
```

- Realiza o mapeamento das propriedades da classe `Category` para as colunas do banco de dados.

### Repositório de Implementação da Categoria (`CategoryRepository`)

A classe `CategoryRepository` implementa a interface `ICategoryRepository` e é responsável pela interação com o banco de dados para as operações relacionadas às categorias. Abaixo estão os detalhes da classe:

```csharp
namespace Recharge.Infra.Data.Repositories.Products
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Implementa os métodos definidos na interface ICategoryRepository.
    }
}
```

- Implementa os métodos definidos na interface `ICategoryRepository` para criar, obter, atualizar e excluir categorias no banco de dados.

### DTO da Categoria (`CategoryDTO`)

A classe `CategoryDTO` é usada para transferir dados relacionados à categoria entre o controlador e os serviços da API. Abaixo estão os detalhes da classe:

```csharp
namespace Recharge.Application.DTOs.Products
{
    public class CategoryDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}
```

- **Id (Guid?):** O ID único da categoria (pode ser nulo).
- **Name (string):** O nome da categoria.

### Validador da Categoria (`CategoryValidator`)

A classe `CategoryValidator` define as regras de validação para objetos do tipo `Category`. Abaixo estão os detalhes da classe:

```csharp
namespace Recharge.Application.Validators.Products
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.Name)
                .NotEmpty().WithMessage("O nome da categoria não pode estar vazio.")
                .MaximumLength(255).WithMessage("O nome da categoria não pode exceder 255 caracteres.");
        }
    }
}
```

- Define regras de validação para o campo `Name` da classe `Category`.

### Serviço da Categoria (`CategoryService`)

A classe `CategoryService` é responsável por executar operações relacionadas à categoria, como criação, atualização e exclusão. Abaixo estão os detalhes da classe:

```csharp
namespace Recharge.Application.Services.Products
{
    public class CategoryService : ICategoryService
    {
        // Implementa os métodos definidos na interface ICategoryService.
    }
}
```

- Implementa os métodos definidos na interface `ICategoryService` para criar, obter, atualizar e excluir categorias.

### Controlador da Categoria (`CategoryController`)

O controlador `CategoryController` define os endpoints da API relacionados à categoria e interage com o serviço da categoria. Abaixo estão os detalhes da classe:

```csharp
namespace Recharge.API.Controllers.Products
{
    [Route("Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        // Define os endpoints da API para a entidade Category.
    }
}
```

- Define os endpoints da API para criar, obter, atualizar e excluir categorias.

### Endpoint `POST /Category`

**Descrição:**

Este endpoint é usado para criar uma nova categoria de produto no sistema Recharge.

**Requisição:**

- Método: POST
- Rota: `/Category`
- Corpo da Requisição: Um objeto JSON contendo os dados da nova categoria.

**Exemplo de Corpo da Requisição:**

```json
{
  "name": "Eletrônicos"
}
```

**Resposta de Sucesso:**

- Código de Status: 200 OK
- Corpo da Resposta: Um objeto JSON contendo os detalhes da nova categoria criada.

**Exemplo de Resposta de Sucesso:**

```json
{
  "id": "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6",
  "name": "Eletrônicos"
}
```

**Resposta de Erro:**

- Código de Status: 400 Bad Request
- Corpo da Resposta: Uma mensagem de erro indicando a falha na criação da categoria.

**Exemplo de Resposta de Erro:**

```json
{
  "message": "Erro ao criar categoria: Categoria já cadastrada."
}
```

### Endpoint `GET /Category/{id}`

**Descrição:**

Este endpoint é usado para obter os detalhes de uma categoria de produto com base no seu ID.

**Requisição:**

- Método: GET
- Rota: `/Category/{id}`
- Parâmetros de Rota:
  - `{id}` (Guid): O ID único da categoria desejada.

**Exemplo de Requisição:**

```http
GET /Category/a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6
```

**Resposta de Sucesso:**

- Código de Status: 200 OK
- Corpo da Resposta: Um objeto JSON contendo os detalhes da categoria.

**Exemplo de Resposta de Sucesso:**

```json
{
  "id": "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6",
  "name": "Eletrônicos"
}
```

**Resposta de Erro:**

- Código de Status: 404 Not Found
- Corpo da Resposta: Uma mensagem de erro indicando que a categoria não foi encontrada.

**Exemplo de Resposta de Erro:**

```json
{
  "message": "Categoria não encontrada."
}
```

### Endpoint `GET /Category/{name}`

**Descrição:**

Este endpoint é usado para obter os detalhes de uma categoria de produto com base no seu nome.

**Requisição:**

- Método: GET
- Rota: `/Category/{name}`
- Parâmetros de Rota:
  - `{name}` (string): O nome da categoria desejada.

**Exemplo de Requisição:**

```http
GET /Category/Eletrônicos
```

**Resposta de Sucesso:**

- Código de Status: 200 OK
- Corpo da Resposta: Um objeto JSON contendo os detalhes da categoria.

**Exemplo de Resposta de Sucesso:**

```json
{
  "id": "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6",
  "name": "Eletrônicos"
}
```

**Resposta de Erro:**

- Código de Status: 404 Not Found
- Corpo da Resposta: Uma mensagem de erro indicando que a categoria não foi encontrada.

**Exemplo de Resposta de Erro:**

```json
{
  "message": "Categoria não encontrada."
}
```

### Endpoint `GET /Category`

**Descrição:**

Este endpoint é usado para obter uma lista de todas as categorias de produtos no sistema Recharge.

**Requisição:**

- Método: GET
- Rota: `/Category`

**Exemplo de Requisição:**

```http
GET /Category
```

**Resposta de Sucesso:**

- Código de Status: 200 OK
- Corpo da Resposta: Uma lista de objetos JSON contendo os detalhes das categorias de produtos.

**Exemplo de Resposta de Sucesso:**

```json
[
  {
    "id": "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6",
    "name": "Eletrônicos"
  },
  {
    "id": "b2c3d4e5-f6g7-h8i9-j0k1-l2m3n4o5p6q7",
    "name": "Roupas"
  }
]
```

**Resposta de Erro:**

- Código de Status: 400 Bad Request
- Corpo da Resposta: Uma mensagem de erro indicando a falha na consulta.

**Exemplo de Resposta de Erro:**

```json
{
  "message": "Erro ao obter categorias: Detalhes do erro."
}
```

### Endpoint `PUT /Category/{id}`

**Descrição:**

Este endpoint é usado para atualizar os detalhes de uma categoria de produto existente com base no seu ID.

**Requisição:**

- Método: PUT
- Rota: `/Category/{id}`
- Parâmetros de Rota:
  - `{id}` (Guid): O ID único da categoria a ser atualizada.
- Corpo da Requisição: Um objeto JSON contendo os novos dados da categoria.

**Exemplo de Corpo da Requisição:**

```json
{
  "name": "Eletrônicos Atualizados"
}
```

**Resposta de Sucesso:**

- Código de Status: 200 OK
- Corpo da Resposta: Um objeto JSON contendo os detalhes da categoria atualizada.

**Exemplo de Resposta de Sucesso:**

```json
{
  "id": "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6",
  "name": "Eletrônicos Atualizados"
}
```

**Resposta de Erro:**

- Código de Status: 404 Not Found
- Corpo da Resposta: Uma mensagem de erro indicando que a categoria não foi encontrada.

**Exemplo de Resposta de Erro:**

```json
{
  "message": "Categoria não encontrada."
}
```

### Endpoint `DELETE /Category

/{id}`

**Descrição:**

Este endpoint é usado para excluir uma categoria de produto com base no seu ID.

**Requisição:**

- Método: DELETE
- Rota: `/Category/{id}`
- Parâmetros de Rota:
  - `{id}` (Guid): O ID único da categoria a ser excluída.

**Exemplo de Requisição:**

```http
DELETE /Category/a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6
```

**Resposta de Sucesso:**

- Código de Status: 200 OK
- Corpo da Resposta: Um objeto JSON contendo os detalhes da categoria excluída.

**Exemplo de Resposta de Sucesso:**

```json
{
  "id": "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6",
  "name": "Eletrônicos Atualizados"
}
```

**Resposta de Erro:**

- Código de Status: 404 Not Found
- Corpo da Resposta: Uma mensagem de erro indicando que a categoria não foi encontrada.

**Exemplo de Resposta de Erro:**

```json
{
  "message": "Categoria não encontrada."
}
```

---

### Modelo da Marca (`Brand`)

A classe `Brand` representa uma marca de produto no sistema Recharge. Abaixo estão os detalhes da classe:

```csharp
namespace Recharge.Domain.Models.Products
{
    public sealed class Brand : Entity
    {
        public string Name { get; private set; }
        public ICollection<Product> Products { get; set; }

        public Brand(string name)
        {
            Name = name;
            Products = new List<Product>();
        }
    }
}
```

- **Name (string):** O nome da marca de produto.
- **Products (ICollection<Product>):** Uma coleção de produtos associados a esta marca.

### Repositório da Marca (`IBrandRepository`)

A interface `IBrandRepository` define os métodos que devem ser implementados para interagir com as marcas no banco de dados. Abaixo estão os detalhes da interface:

```csharp
namespace Recharge.Domain.Repositories.Products
{
    public interface IBrandRepository
    {
        Task<Brand> CreateBrand(Brand brand);
        Task<Brand> GetBrandByName(string name);
        Task<Brand> GetBrandById(Guid id);
        Task<ICollection<Brand>> GetAllBrands();
        Task<Brand> UpdateBrand(Brand brand);
        Task<Brand> DeleteBrand(Brand brand);
    }
}
```

- **CreateBrand:** Cria uma nova marca no banco de dados.
- **GetBrandByName:** Obtém uma marca com base em seu nome.
- **GetBrandById:** Obtém uma marca com base em seu ID.
- **GetAllBrands:** Obtém uma lista de todas as marcas.
- **UpdateBrand:** Atualiza os detalhes de uma marca existente.
- **DeleteBrand:** Exclui uma marca existente.

### Mapeamento da Marca (`BrandMap`)

A classe `BrandMap` é responsável pelo mapeamento da classe `Brand` para o banco de dados. Abaixo estão os detalhes da classe:

```csharp
namespace Recharge.Infra.Data.Maps.Products
{
    public class BrandMap : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.HasMany(x => x.Products).WithOne(c => c.Brand).HasForeignKey(x => x.BrandId);
        }
    }
}
```

- Realiza o mapeamento das propriedades da classe `Brand` para as colunas do banco de dados.

### Repositório de Implementação da Marca (`BrandRepository`)

A classe `BrandRepository` implementa a interface `IBrandRepository` e é responsável pela interação com o banco de dados para as operações relacionadas às marcas. Abaixo estão os detalhes da classe:

```csharp
namespace Recharge.Infra.Data.Repositories.Products
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BrandRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Implementa os métodos definidos na interface IBrandRepository.
    }
}
```

- Implementa os métodos definidos na interface `IBrandRepository` para criar, obter, atualizar e excluir marcas no banco de dados.

### DTO da Marca (`BrandDTO`)

A classe `BrandDTO` é usada para transferir dados relacionados à marca entre o controlador e os serviços da API. Abaixo estão os detalhes da classe:

```csharp
namespace Recharge.Application.DTOs.Products
{
    public class BrandDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}
```

- **Id (Guid?):** O ID único da marca (pode ser nulo).
- **Name (string):** O nome da marca.

### Validador da Marca (`BrandValidator`)

A classe `BrandValidator` define as regras de validação para objetos do tipo `Brand`. Abaixo estão os detalhes da classe:

```csharp
namespace Recharge.Application.Validator.Products
{
    public class BrandValidator : AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(brand => brand.Name)
                .NotEmpty().WithMessage("O nome da marca não pode estar vazio.")
                .MaximumLength(255).WithMessage("O nome da marca não pode exceder 255 caracteres.");
        }
    }
}
```

- Define regras de validação para o campo `Name` da classe `Brand`.

### Serviço da Marca (`BrandService`)

A classe `BrandService` é responsável por executar operações relacionadas à marca, como criação, atualização e exclusão. Abaixo estão os detalhes da classe:

```csharp
namespace Recharge.Application.Services.Products
{
    public class BrandService : IBrandService
    {
        // Implementa os métodos definidos na interface IBrandService.
    }
}
```

- Implementa os métodos definidos na interface `IBrandService` para criar, obter, atualizar e excluir marcas.

### Controlador da Marca (`BrandController`)

O controlador `BrandController` define os endpoints da API relacionados à marca e interage com o serviço da marca. Abaixo estão os detalhes da classe:

```csharp
namespace Recharge.API.Controllers.Products
{
    [Route("Brand")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        // Define os endpoints da API para a entidade Brand.
    }
}
```

- Define os endpoints da API para criar, obter, atualizar e excluir marcas.

### Endpoints da API da Marca

A API da Marca oferece os seguintes endpoints para executar operações relacionadas à marca:

- **Criar uma Nova Marca:**

  - **Método:** POST
  - **Rota:** `/Brand/CreateBrand`
  - **Payload JSON de Exemplo:**
    ```json
    {
      "name": "MarcaNova"
    }
    ```
  - **Descrição:** Cria uma nova marca com o nome especificado.

- **Obter uma Marca por ID:**

  - **Método:** GET
  - **Rota:** `/Brand/{id}`
  - **Descrição:** Obtém uma marca pelo seu ID único.

- **Obter uma Marca por Nome:**

  - **Método:** GET
  - **Rota:** `/Brand/name/{name}`
  - **Descrição:** Obtém uma marca pelo seu nome.

- **Obter Todas as Marcas:**

  - **Método:** GET
  - **Rota:** `/Brand`
  - **Descrição:** Obtém uma lista de todas as marcas cadastradas.

- **Atualizar uma Marca por ID:**

  - **Método:** PUT
  - **Rota:** `/Brand/{id}`
  - **Payload JSON de Exemplo:**
    ```json
    {
      "name": "MarcaAtualizada"
    }
    ```
  - **Descrição:** Atualiza os detalhes de uma marca existente com o ID especificado.

- **Excluir uma Marca por ID:**
  - **Método:** DELETE
  - **Rota:** `/Brand/{id}`
  - **Descrição:** Exclui uma marca existente com o ID especificado.

### Exemplos de Uso da API

Aqui estão alguns exemplos de como você pode consumir a API da Marca:

#### Exemplo 1: Criar uma Nova Marca

- **Método:** POST
- **Rota:** `/Brand/CreateBrand`
- **Payload JSON:**
  ```json
  {
    "name": "MarcaNova"
  }
  ```
- **Resposta de Sucesso (Código 200):**
  ```json
  {
    "isSuccess": true,
    "data": {
      "id": "12345678-1234-1234-1234-1234567890ab",
      "name": "MarcaNova"
    }
  }
  ```

#### Exemplo 2: Obter uma Marca por ID

- **Método:** GET
- **Rota:** `/Brand/12345678-1234-1234-1234-1234567890ab`
- **Resposta de Sucesso (Código 200):**
  ```json
  {
    "isSuccess": true,
    "data": {
      "id": "12345678-1234-1234-1234-1234567890ab",
      "name": "MarcaNova"
    }
  }
  ```

#### Exemplo 3: Atualizar uma Marca por ID

- **Método:** PUT
- **Rota:** `/Brand/12345678-1234-1234-1234-1234567890ab`
- **Payload JSON:**
  ```json
  {
    "name": "MarcaAtualizada"
  }
  ```
- **Resposta de Sucesso (Código 200):**
  ```json
  {
    "isSuccess": true,
    "data": {
      "id": "12345678-1234-1234-1234-1234567890ab",
      "name": "MarcaAtualizada"
    }
  }
  ```

#### Exemplo 4: Excluir uma Marca por ID

- **Método:** DELETE
- **Rota:** `/Brand/12345678-1234-1234-1234-1234567890ab`
- **Resposta de Sucesso (Código 200):**
  ```json
  {
    "isSuccess": true,
    "data": {
      "id": "12345678-1234-1234-1234-1234567890ab",
      "name": "MarcaAtualizada"
    }
  }
  ```

### Observações Importantes

- Para autenticação e autorização, verifique se você está incluindo os cabeçalhos apropriados nas solicitações, conforme exigido pela sua implementação de segurança.

- Sempre valide os dados de entrada e trate os erros apropriadamente em seu cliente ao consumir a API.

- Os IDs são gerados automaticamente e são exclusivos para cada marca.

---

### Modelo de Produto (Product)

O modelo `Product` representa um produto no sistema Recharge. Ele inclui detalhes como nome, SKU, código de barras, descrição, preço, quantidade, categoria, marca e folha de dados associados.

```csharp
public sealed class Product : Entity
{
    public string Name { get; set; }
    public string Sku { get; set; }
    public string BarCode { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public long? Amount { get; set; }
    public Category Category { get; set; }
    public Guid? CategoryId { get; set; }
    public Brand Brand { get; set; }
    public Guid? BrandId { get; set; }
    public Datasheet Datasheet { get; set; }
    public Guid? DatasheetId { get; set; }
    public ICollection<CartItem> CartItems { get; set; }

    // Construtores e métodos de validação também estão incluídos.
}
```

### Repositório de Produto (IProductRepository)

A interface `IProductRepository` define os métodos que permitem interagir com os produtos no banco de dados. Esses métodos incluem criar, obter, atualizar e excluir produtos, bem como recuperar listas de produtos com base em critérios específicos.

```csharp
public interface IProductRepository
{
    Task<Product> CreateProduct(Product product);
    Task<Product> GetProductById(Guid id);
    Task<Product> GetProductByName(string productName);
    Task<Product> GetProductBySku(string sku);
    Task<Product> GetProductByBarCode(string barCode);
    Task<ICollection<Product>> GetAllProducts();
    Task<ICollection<Product>> GetAllProductsInTheCategory(Guid categoryId);
    Task<ICollection<Product>> GetAllProductsInTheBrand(Guid brandId);
    Task<Product> UpdateProduct(Product product);
    Task<Product> RemoveProduct(Product product);
}
```

### Mapeamento do Modelo de Produto (ProductMap)

O mapeamento do modelo `Product` para o banco de dados é definido na classe `ProductMap`, usando o Entity Framework Core. Isso inclui a definição de chaves primárias, propriedades obrigatórias e relacionamentos com outras entidades.

```csharp
public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Amount).IsRequired();
        builder.Property(x => x.Sku).IsRequired();
        builder.Property(x => x.BarCode).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.HasOne(x => x.Datasheet).WithOne(c => c.Product).HasForeignKey<Datasheet>(x => x.ProductId);
    }
}
```

### Serviço de Produto (IProductService e ProductService)

O serviço de produto `IProductService` define métodos para criar, obter, atualizar e excluir produtos, bem como recuperar listas de produtos com base em critérios específicos. O serviço também executa validações e mapeia DTOs (Data Transfer Objects) para modelos.

```csharp
public interface IProductService
{
    Task<ResultService<ProductDTO>> CreateProduct(ProductDTO productDTO);
    Task<ResultService<ProductDTO>> GetProductById(Guid id);
    Task<ResultService<ProductDTO>> GetProductByName(string productName);
    Task<ResultService<ProductDTO>> GetProductBySku(string sku);
    Task<ResultService<ProductDTO>> GetProductByBarCode(string barCode);
    Task<ResultService<ICollection<ProductDTO>>> GetAllProducts();
    Task<ResultService<ICollection<ProductDTO>>> GetAllProductsInTheCategory(Guid categoryId);
    Task<ResultService<ICollection<ProductDTO>>> GetAllProductsInTheBrand(Guid brandId);
    Task<ResultService<ProductDTO>> UpdateProduct(Guid id, ProductDTO productDTO);
    Task<ResultService<ProductDTO>> RemoveProduct(Guid id);
}
```

O serviço de produto é implementado na classe `ProductService`, que lida com a lógica de negócios, validações e interações com o repositório.

### Controlador da API de Produto (ProductController)

O controlador da API `ProductController` define os endpoints para consumir os serviços relacionados a produtos. Ele recebe solicitações HTTP e chama os métodos apropriados no serviço de produto.

```csharp
[Route("Product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // Implementação de endpoints HTTP para criar, obter, atualizar e excluir produtos.
}
```

### Observações Importantes

- O sistema Recharge segue as melhores práticas de desenvolvimento .NET Core, incluindo a separação de preocupações entre modelos, serviços, repositórios e controladores.

- As validações são realizadas tanto no nível do serviço quanto no nível do modelo usando a biblioteca FluentValidation.

- O Entity Framework Core é usado para mapear objetos para tabelas de banco de dados.

- O controlador da API lida com as solicitações HTTP e chama os métodos apropriados no serviço de produto.

### Endpoints da API de Produtos

A API de Produtos oferece os seguintes endpoints para executar operações relacionadas a produtos:

- **Criar um Novo Produto:**

  - **Método:** POST
  - **Rota:** `/Product/CreateProduct`
  - **Payload JSON de Exemplo:**
    ```json
    {
      "name": "ProdutoNovo",
      "sku": "SKU123",
      "barCode": "1234567890",
      "description": "Descrição do produto",
      "price": 19.99,
      "amount": 100,
      "categoryId": "12345678-1234-1234-1234-1234567890ab",
      "brandId": "12345678-1234-1234-1234-1234567890cd"
    }
    ```
  - **Descrição:** Cria um novo produto com os detalhes especificados.

- **Obter um Produto por ID:**

  - **Método:** GET
  - **Rota:** `/Product/{id}`
  - **Descrição:** Obtém um produto pelo seu ID único.

- **Obter um Produto por Nome:**

  - **Método:** GET
  - **Rota:** `/Product/name/{productName}`
  - **Descrição:** Obtém um produto pelo seu nome.

- **Obter um Produto por SKU:**

  - **Método:** GET
  - **Rota:** `/Product/sku/{sku}`
  - **Descrição:** Obtém um produto pelo seu SKU (Stock Keeping Unit).

- **Obter um Produto por Código de Barras:**

  - **Método:** GET
  - **Rota:** `/Product/barcode/{barCode}`
  - **Descrição:** Obtém um produto pelo seu código de barras.

- **Obter Todos os Produtos:**

  - **Método:** GET
  - **Rota:** `/Product`
  - **Descrição:** Obtém uma lista de todos os produtos cadastrados.

- **Obter Todos os Produtos em uma Categoria:**

  - **Método:** GET
  - **Rota:** `/Product/category/{categoryId}`
  - **Descrição:** Obtém uma lista de todos os produtos em uma categoria específica com base no ID da categoria.

- **Obter Todos os Produtos em uma Marca:**

  - **Método:** GET
  - **Rota:** `/Product/brand/{brandId}`
  - **Descrição:** Obtém uma lista de todos os produtos em uma marca específica com base no ID da marca.

- **Atualizar um Produto por ID:**

  - **Método:** PUT
  - **Rota:** `/Product/{id}`
  - **Payload JSON de Exemplo:**
    ```json
    {
      "name": "ProdutoAtualizado",
      "sku": "SKU321",
      "barCode": "0987654321",
      "description": "Descrição atualizada",
      "price": 29.99,
      "amount": 200,
      "categoryId": "12345678-1234-1234-1234-1234567890ab",
      "brandId": "12345678-1234-1234-1234-1234567890cd"
    }
    ```
  - **Descrição:** Atualiza os detalhes de um produto existente com o ID especificado.

- **Remover um Produto por ID:**
  - **Método:** DELETE
  - **Rota:** `/Product/{id}`
  - **Descrição:** Remove um produto existente com o ID especificado.

### Exemplos de Uso da API

Aqui estão alguns exemplos de como você pode consumir a API de Produtos:

#### Exemplo 1: Criar um Novo Produto

- **Método:** POST
- **Rota:** `/Product/CreateProduct`
- **Payload JSON:**
  ```json
  {
    "name": "ProdutoNovo",
    "sku": "SKU123",
    "barCode": "1234567890",
    "description": "Descrição do produto",
    "price": 19.99,
    "amount": 100,
    "categoryId": "12345678-1234-1234-1234-1234567890ab",
    "brandId": "12345678-1234-1234-1234-1234567890cd"
  }
  ```
- **Resposta de Sucesso (Código 200):**
  ```json
  {
      "isSuccess": true,
      "
  ```

data": {
"id": "12345678-1234-1234-1234-1234567890ef",
"name": "ProdutoNovo",
"sku": "SKU123",
"barCode": "1234567890",
"description": "Descrição do produto",
"price": 19.99,
"amount": 100,
"categoryId": "12345678-1234-1234-1234-1234567890ab",
"brandId": "12345678-1234-1234-1234-1234567890cd"
}
}

````

#### Exemplo 2: Obter um Produto por ID

- **Método:** GET
- **Rota:** `/Product/12345678-1234-1234-1234-1234567890ef`
- **Resposta de Sucesso (Código 200):**
```json
{
    "isSuccess": true,
    "data": {
        "id": "12345678-1234-1234-1234-1234567890ef",
        "name": "ProdutoNovo",
        "sku": "SKU123",
        "barCode": "1234567890",
        "description": "Descrição do produto",
        "price": 19.99,
        "amount": 100,
        "categoryId": "12345678-1234-1234-1234-1234567890ab",
        "brandId": "12345678-1234-1234-1234-1234567890cd"
    }
}
````

#### Exemplo 3: Atualizar um Produto por ID

- **Método:** PUT
- **Rota:** `/Product/12345678-1234-1234-1234-1234567890ef`
- **Payload JSON:**
  ```json
  {
    "name": "ProdutoAtualizado",
    "sku": "SKU321",
    "barCode": "0987654321",
    "description": "Descrição atualizada",
    "price": 29.99,
    "amount": 200,
    "categoryId": "12345678-1234-1234-1234-1234567890ab",
    "brandId": "12345678-1234-1234-1234-1234567890cd"
  }
  ```
- **Resposta de Sucesso (Código 200):**
  ```json
  {
    "isSuccess": true,
    "data": {
      "id": "12345678-1234-1234-1234-1234567890ef",
      "name": "ProdutoAtualizado",
      "sku": "SKU321",
      "barCode": "0987654321",
      "description": "Descrição atualizada",
      "price": 29.99,
      "amount": 200,
      "categoryId": "12345678-1234-1234-1234-1234567890ab",
      "brandId": "12345678-1234-1234-1234-1234567890cd"
    }
  }
  ```

#### Exemplo 4: Remover um Produto por ID

- **Método:** DELETE
- **Rota:** `/Product/12345678-1234-1234-1234-1234567890ef`
- **Resposta de Sucesso (Código 200):**
  ```json
  {
    "isSuccess": true,
    "data": {
      "id": "12345678-1234-1234-1234-1234567890ef",
      "name": "ProdutoAtualizado",
      "sku": "SKU321",
      "barCode": "0987654321",
      "description": "Descrição atualizada",
      "price": 29.99,
      "amount": 200,
      "categoryId": "12345678-1234-1234-1234-1234567890ab",
      "brandId": "12345678-1234-1234-1234-1234567890cd"
    }
  }
  ```

### Observações Importantes

- Para autenticação e autorização, verifique se você está incluindo os cabeçalhos apropriados nas solicitações, conforme exigido pela sua implementação de segurança.

- Sempre valide os dados de entrada e trate os erros apropriadamente em seu cliente ao consumir a API.

- Os IDs são gerados automaticamente e são exclusivos para cada produto.

---

### Modelo de Datasheet (Datasheet)

O modelo `Datasheet` representa um datasheet associado a um produto no sistema Recharge. Ele inclui detalhes como o modelo e a garantia.

```csharp
namespace Recharge.Domain.Models.Products
{
    public sealed class Datasheet : Entity
    {
        public string Model { get; set; }
        public string Warranty { get; set; }

        public Product Product { get; set; }
        public Guid ProductId { get; set; }

        public Datasheet(string model, string warranty)
        {
            Model = model;
            Warranty = warranty;
        }
    }
}
```

### Repositório de Datasheet (IDatasheetRepository)

A interface `IDatasheetRepository` define os métodos que permitem interagir com os datasheets no banco de dados. Esses métodos incluem criar, obter, atualizar e excluir datasheets, bem como recuperar listas de datasheets.

```csharp
namespace Recharge.Domain.Repositories.Products
{
    public interface IDatasheetRepository
    {
        Task<Datasheet> CreateDatasheet(Datasheet datasheet);
        Task<Datasheet> GetDatasheetById(Guid id);
        Task<ICollection<Datasheet>> GetAllDatasheets();
        Task<Datasheet> UpdateDatasheet(Datasheet datasheet);
        Task<Datasheet> DeleteDatasheet(Datasheet datasheet);
    }
}
```

### Mapeamento do Modelo de Datasheet (DatasheetMap)

O mapeamento do modelo `Datasheet` para o banco de dados é definido na classe `DatasheetMap` usando o Entity Framework Core. Isso inclui a definição de chaves primárias, propriedades obrigatórias e relacionamentos com outras entidades, como o relacionamento com `Product`.

```csharp
namespace Recharge.Infra.Data.Maps.Products
{
    public class DatasheetMap : IEntityTypeConfiguration<Datasheet>
    {
        public void Configure(EntityTypeBuilder<Datasheet> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Model).IsRequired();
            builder.Property(x => x.Warranty).IsRequired();
            builder.HasOne(x => x.Product).WithOne(c => c.Datasheet).HasForeignKey<Product>(x => x.DatasheetId);
        }
    }
}
```

### Serviço de Datasheet (IDatasheetService e DatasheetService)

O serviço de datasheet `IDatasheetService` define métodos para criar, obter, atualizar e excluir datasheets, bem como recuperar listas de datasheets. Ele também inclui validações usando `DatasheetValidator`.

```csharp
namespace Recharge.Application.Interfaces.Products
{
    public interface IDatasheetService
    {
        Task<ResultService<DatasheetDTO>> CreateDatasheet(DatasheetDTO datasheetDTO);
        Task<ResultService<DatasheetDTO>> GetDatasheetById(Guid id);
        Task<ResultService<ICollection<DatasheetDTO>>> GetAllDatasheets();
        Task<ResultService<DatasheetDTO>> UpdateDatasheet(Guid id, DatasheetDTO datasheetDTO);
        Task<ResultService<DatasheetDTO>> DeleteDatasheet(Guid id);
    }
}
```

A classe `DatasheetService` implementa a lógica de negócios do serviço, incluindo validações, mapeamento DTO-modelo e interações com o repositório.

### Controlador da API de Datasheet (DatasheetController)

O controlador da API `DatasheetController` define endpoints para criar, obter, atualizar e excluir datasheets. Ele recebe solicitações HTTP e chama os métodos apropriados no serviço de datasheet.

```csharp
namespace Recharge.API.Controllers.Products
{
    [Route("Datasheet")]
    [ApiController]
    public class DatasheetController : ControllerBase
    {
        private readonly IDatasheetService _datasheetService;

        public DatasheetController(IDatasheetService datasheetService)
        {
            _datasheetService = datasheetService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateDatasheet([FromBody] DatasheetDTO datasheetDTO)
        {
            // Implementação do endpoint de criação de datasheet.
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetDatasheetById(Guid id)
        {
            // Implementação do endpoint para obter um datasheet por ID.
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDatasheets()
        {
            // Implementação do endpoint para obter todos os datasheets.
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDatasheet(Guid id, [FromBody] DatasheetDTO datasheetDTO)
        {
            // Implementação do endpoint de atualização de datasheet.
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDatasheet(Guid id)
        {
            // Implementação do endpoint de exclusão de datasheet.
        }
    }
}
```

### Endpoint Base da API

O endpoint base para acessar os recursos relacionados à entidade `Datasheet` é `/Datasheet`.

### Endpoints Disponíveis

A API fornece os seguintes endpoints para interagir com os datasheets:

1. **Criar Datasheet**

   - **Método HTTP:** POST
   - **Rota:** `/Datasheet`
   - **Payload JSON de Requisição:**
     ```json
     {
       "Model": "Modelo do Datasheet",
       "Warranty": "Garantia do Datasheet"
     }
     ```
   - **Descrição:** Cria um novo datasheet com o modelo e a garantia especificados.

2. **Obter Datasheet por ID**

   - **Método HTTP:** GET
   - **Rota:** `/Datasheet/{id}`
   - **Descrição:** Obtém os detalhes de um datasheet específico com base em seu ID.

3. **Obter Todos os Datasheets**

   - **Método HTTP:** GET
   - **Rota:** `/Datasheet`
   - **Descrição:** Obtém uma lista de todos os datasheets disponíveis no sistema.

4. **Atualizar Datasheet por ID**

   - **Método HTTP:** PUT
   - **Rota:** `/Datasheet/{id}`
   - **Payload JSON de Requisição:**
     ```json
     {
       "Model": "Novo Modelo do Datasheet",
       "Warranty": "Nova Garantia do Datasheet"
     }
     ```
   - **Descrição:** Atualiza os detalhes de um datasheet específico com base em seu ID.

5. **Excluir Datasheet por ID**

   - **Método HTTP:** DELETE
   - **Rota:** `/Datasheet/{id}`
   - **Descrição:** Exclui um datasheet específico com base em seu ID.

### Respostas da API

A API retorna respostas em formato JSON. As respostas podem conter informações sobre o resultado da solicitação, bem como os dados solicitados, se aplicável.

#### Exemplo de Resposta de Sucesso:

```json
{
  "isSuccess": true,
  "message": "Operação bem-sucedida.",
  "data": {
    "Id": "12345678-1234-1234-1234-123456789012",
    "Model": "Novo Modelo do Datasheet",
    "Warranty": "Nova Garantia do Datasheet"
  }
}
```

#### Exemplo de Resposta de Erro:

```json
{
  "isSuccess": false,
  "message": "Erro ao criar datasheet.",
  "errors": [
    "O Modelo do Datasheet não pode estar vazio.",
    "A Garantia do Datasheet não pode estar vazia."
  ]
}
```

### Autenticação e Autorização

Certifique-se de que a API tenha autenticação e autorização adequadas implementadas, dependendo dos requisitos de segurança do seu sistema. Os endpoints devem ser protegidos contra acesso não autorizado, se necessário.

### Boas Práticas

Ao consumir a API de Datasheet, siga estas boas práticas:

- **Tratamento de Erros:** Lide adequadamente com as respostas de erro retornadas pela API, exibindo mensagens úteis para os usuários e tratando exceções, se aplicável.

- **Validações de Dados:** Certifique-se de fornecer dados válidos nos payloads JSON de solicitação, seguindo as regras de validação definidas pela API.

- **Testes:** Realize testes de unidade e integração para garantir que sua integração com a API de Datasheet funcione conforme o esperado.

- **Segurança:** Mantenha a segurança em mente ao lidar com dados confidenciais, especialmente se a API exigir autenticação.

- **Documentação da API:** Consulte a documentação da API fornecida pelo desenvolvedor para obter informações adicionais sobre os endpoints e suas operações.

Certifique-se de adaptar a documentação de consumo da API às necessidades específicas do seu aplicativo e seguir as melhores práticas de segurança e desenvolvimento.

---

### Modelo da Classe `Purchase`

A classe `Purchase` representa uma compra no sistema Recharge. Ela contém as seguintes propriedades:

- `Id`: Identificador exclusivo da compra.
- `BuyDate`: Data da compra.
- `Payment`: Forma de pagamento da compra.
- `Status`: Status da compra (por exemplo, "Em processamento", "Concluída").
- `UserId`: ID do usuário que fez a compra.
- `CartItems`: Uma coleção de itens do carrinho relacionados a esta compra.

```csharp
public sealed class Purchase : Entity
{
    public DateTime BuyDate { get; set; }
    public string Payment { get; set; }
    public string Status { get; set; }
    public Guid UserId { get; set; }
    public ICollection<CartItem> CartItems { get; set; }

    public Purchase(DateTime buyDate, string payment, string status)
    {
        BuyDate = buyDate;
        Payment = payment;
        Status = status;
        CartItems = new List<CartItem>();
    }
}
```

### Repositório da Classe `Purchase`

O repositório da classe `Purchase` define operações para acessar e manipular dados relacionados a compras no banco de dados. Ele inclui métodos como criar, atualizar, excluir e consultar compras.

```csharp
public interface IPurchaseRepository
{
    Task<Purchase> CreatePurchase(Purchase purchase);
    Task<Purchase> GetPurchaseById(Guid id);
    Task<ICollection<Purchase>> GetPurchasesByUserId(Guid userId);
    Task<ICollection<Purchase>> GetAllPurchases();
    Task<Purchase> UpdatePurchase(Purchase purchase);
    Task<Purchase> RemovePurchase(Purchase purchase);
}
```

### Mapeamento da Classe `Purchase`

O mapeamento da classe `Purchase` define como a entidade `Purchase` deve ser mapeada para as tabelas do banco de dados usando o Entity Framework Core. Ele especifica chaves primárias, propriedades obrigatórias e relacionamentos com outras entidades.

```csharp
public class PurchaseMap : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.BuyDate).IsRequired();
        builder.Property(x => x.Payment).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.HasMany(x => x.CartItems).WithOne(c => c.Purchase).HasForeignKey(x => x.PurchaseId);
    }
}
```

### Serviço da Classe `Purchase`

O serviço da classe `Purchase` fornece métodos para criar, atualizar, consultar e excluir compras. Ele utiliza o repositório da classe `Purchase` para interagir com o banco de dados e mapeia os dados entre objetos DTO e objetos de modelo.

```csharp
public interface IPurchaseService
{
    Task<ResultService<PurchaseDTO>> CreatePurchase(PurchaseDTO purchaseDTO);
    Task<ResultService<PurchaseDTO>> GetPurchaseById(Guid id);
    Task<ResultService<ICollection<PurchaseDTO>>> GetPurchasesByUserId(Guid userId);
    Task<ResultService<ICollection<PurchaseDTO>>> GetAllPurchases();
    Task<ResultService<PurchaseDTO>> UpdatePurchase(Guid id, PurchaseDTO purchaseDTO);
    Task<ResultService<PurchaseDTO>> RemovePurchase(Guid id, PurchaseDTO purchaseDTO);
}
```

### Controlador da API para `Purchase`

O controlador da API define os endpoints HTTP para interagir com compras. Ele utiliza o serviço da classe `Purchase` para processar solicitações e respostas. A autenticação e a autorização devem ser implementadas conforme necessário.

```csharp
[Route("Purchase")]
[ApiController]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public PurchaseController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePurchase([FromBody] PurchaseDTO purchaseDTO)
    {
        // Implemente a lógica para criar uma compra.
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPurchaseById(Guid id)
    {
        // Implemente a lógica para obter detalhes de uma compra por ID.
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetPurchasesByUserId(Guid userId)
    {
        // Implemente a lógica para obter todas as compras de um usuário.
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPurchases()
    {
        // Implemente a lógica para obter todas as compras.
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePurchase(Guid id, [FromBody] PurchaseDTO purchaseDTO)
    {
        // Implemente a lógica para atualizar uma compra por ID.
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemovePurchase(Guid id)
    {
        // Implemente a lógica para remover uma compra por ID.
    }
}
```

### Endpoint Base da API

O endpoint base para acessar os recursos relacionados à entidade `Purchase` é `/Purchase`.

### Endpoints Disponíveis

A API fornece os seguintes endpoints para interagir com as compras (`Purchase`):

1. **Criar Compra**

   - **Método HTTP:** POST
   - **Rota:** `/Purchase`
   - **Payload JSON de Requisição:**
     ```json
     {
       "Payment": "Forma de Pagamento",
       "Status": "Status da Compra",
       "UserId": "ID do Usuário",
       "CartItems": []
     }
     ```
   - **Descrição:** Cria uma nova compra com a forma de pagamento, status, ID do usuário e itens do carrinho especificados.

2. **Obter Compra por ID**

   - **Método HTTP:** GET
   - **Rota:** `/Purchase/{id}`
   - **Descrição:** Obtém os detalhes de uma compra específica com base em seu ID.

3. **Obter Compras por ID de Usuário**

   - **Método HTTP:** GET
   - **Rota:** `/Purchase/user/{userId}`
   - **Descrição:** Obtém uma lista de todas as compras feitas por um usuário com base em seu ID.

4. **Obter Todas as Compras**

   - **Método HTTP:** GET
   - **Rota:** `/Purchase`
   - **Descrição:** Obtém uma lista de todas as compras disponíveis no sistema.

5. **Atualizar Compra por ID**

   - **Método HTTP:** PUT
   - **Rota:** `/Purchase/{id}`
   - **Payload JSON de Requisição:**
     ```json
     {
       "Payment": "Nova Forma de Pagamento",
       "Status": "Novo Status da Compra",
       "UserId": "Novo ID do Usuário",
       "CartItems": []
     }
     ```
   - **Descrição:** Atualiza os detalhes de uma compra específica com base em seu ID.

6. **Remover Compra por ID**

   - **Método HTTP:** DELETE
   - **Rota:** `/Purchase/{id}`
   - **Descrição:** Exclui uma compra específica com base em seu ID.

### Respostas da API

A API retorna respostas em formato JSON. As respostas podem conter informações sobre o resultado da solicitação, bem como os dados solicitados, se aplicável.

#### Exemplo de Resposta de Sucesso:

```json
{
  "isSuccess": true,
  "message": "Operação bem-sucedida.",
  "data": {
    "Id": "12345678-1234-1234-1234-123456789012",
    "Payment": "Nova Forma de Pagamento",
    "Status": "Novo Status da Compra",
    "UserId": "98765432-9876-9876-9876-987654321098",
    "CartItems": []
  }
}
```

#### Exemplo de Resposta de Erro:

```json
{
  "isSuccess": false,
  "message": "Erro ao criar compra.",
  "errors": [
    "A forma de pagamento não pode estar vazia.",
    "O status da compra não pode estar vazio."
  ]
}
```

### Autenticação e Autorização

Certifique-se de que a API tenha autenticação e autorização adequadas implementadas, dependendo dos requisitos de segurança do seu sistema. Os endpoints devem ser protegidos contra acesso não autorizado, se necessário.

---

### Modelo da Classe `CartItem`

A classe `CartItem` representa um item do carrinho no sistema Recharge. Ela contém as seguintes propriedades:

- `Id`: Identificador exclusivo do item do carrinho.
- `Amount`: A quantidade de produtos no carrinho.
- `PriceUn`: O preço unitário do produto.
- `ProductId`: O ID do produto associado a este item do carrinho.
- `PurchaseId`: O ID da compra à qual este item do carrinho pertence.

```csharp
public sealed class CartItem : Entity
{
    public int Amount { get; set; }
    public decimal PriceUn { get; set; }
    public Guid ProductId { get; set; }
    public Guid PurchaseId { get; set; }
    public Product Product { get; set; }
    public Purchase Purchase { get; set; }

    public CartItem(int amount, decimal priceUn)
    {
        Amount = amount;
        PriceUn = priceUn;
    }
}
```

### Repositório da Classe `CartItem`

O repositório da classe `CartItem` define operações para acessar e manipular dados relacionados a itens do carrinho no banco de dados. Ele inclui métodos como criar, consultar e remover itens do carrinho.

```csharp
public interface ICartItemRepository
{
    Task<CartItem> CreateCartItem(CartItem cartItem);
    Task<CartItem> GetCartItemById(Guid id);
    Task<ICollection<CartItem>> GetCartItensByPurchase(Guid purchaseId);
    Task<CartItem> RemoveCartItem(CartItem cartItem);
}
```

### Mapeamento da Classe `CartItem`

O mapeamento da classe `CartItem` define como a entidade `CartItem` deve ser mapeada para as tabelas do banco de dados usando o Entity Framework Core. Ele especifica chaves primárias, propriedades obrigatórias e relacionamentos com outras entidades.

```csharp
public class CartItemMap : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Amount).IsRequired();
        builder.Property(x => x.PriceUn).IsRequired();
        builder.HasOne(x => x.Product).WithMany(x => x.CartItems).HasForeignKey(x => x.ProductId);
    }
}
```

### Serviço da Classe `CartItem`

O serviço da classe `CartItem` fornece métodos para criar, consultar e remover itens do carrinho. Ele utiliza o repositório da classe `CartItem` para interagir com o banco de dados e mapeia os dados entre objetos DTO e objetos de modelo.

```csharp
public interface ICartItemService
{
    Task<ResultService<CartItemDTO>> CreateCartItem(CartItemDTO cartItemDTO);
    Task<ResultService<CartItemDTO>> GetCartItemById(Guid id);
    Task<ResultService<ICollection<CartItemDTO>>> GetCartItensByPurchase(Guid purchaseId);
    Task<ResultService<CartItemDTO>> RemoveCartItem(Guid id, CartItemDTO cartItemDTO);
}
```

### Controlador da API para `CartItem`

O controlador da API define os endpoints HTTP para interagir com itens do carrinho. Ele utiliza o serviço da classe `CartItem` para processar solicitações e respostas. A autenticação e a autorização devem ser implementadas conforme necessário.

```csharp
[Route("Cart")]
[ApiController]
public class CartItemController : ControllerBase
{
    private readonly ICartItemService _cartItemService;

    public CartItemController(ICartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCartItem([FromBody] CartItemDTO cartItemDTO)
    {
        // Implemente a lógica para criar um item do carrinho.
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCartItemById(Guid id)
    {
        // Implemente a lógica para obter detalhes de um item do carrinho por ID.
    }

    [HttpGet("purchase/{purchaseId}")]
    public async Task<IActionResult> GetCartItensByPurchase(Guid purchaseId)
    {
        // Implemente a lógica para obter todos os itens do carrinho de uma compra.
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveCartItem(Guid id)
    {
        // Implemente a lógica para remover um item do carrinho por ID.
    }
}
```

### Modelo da Classe `CartItem`

A classe `CartItem` representa um item do carrinho no sistema Recharge. Ela contém as seguintes propriedades:

- `Id`: Identificador exclusivo do item do carrinho.
- `Amount`: A quantidade de produtos no carrinho.
- `PriceUn`: O preço unitário do produto.
- `ProductId`: O ID do produto associado a este item do carrinho.
- `PurchaseId`: O ID da compra à qual este item do carrinho pertence.

```csharp
public sealed class CartItem : Entity
{
    public int Amount { get; set; }
    public decimal PriceUn { get; set; }
    public Guid ProductId { get; set; }
    public Guid PurchaseId { get; set; }
    public Product Product { get; set; }
    public Purchase Purchase { get; set; }

    public CartItem(int amount, decimal priceUn)
    {
        Amount = amount;
        PriceUn = priceUn;
    }
}
```

### Repositório da Classe `CartItem`

O repositório da classe `CartItem` define operações para acessar e manipular dados relacionados a itens do carrinho no banco de dados. Ele inclui métodos como criar, consultar e remover itens do carrinho.

```csharp
public interface ICartItemRepository
{
    Task<CartItem> CreateCartItem(CartItem cartItem);
    Task<CartItem> GetCartItemById(Guid id);
    Task<ICollection<CartItem>> GetCartItensByPurchase(Guid purchaseId);
    Task<CartItem> RemoveCartItem(CartItem cartItem);
}
```

### Mapeamento da Classe `CartItem`

O mapeamento da classe `CartItem` define como a entidade `CartItem` deve ser mapeada para as tabelas do banco de dados usando o Entity Framework Core. Ele especifica chaves primárias, propriedades obrigatórias e relacionamentos com outras entidades.

```csharp
public class CartItemMap : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Amount).IsRequired();
        builder.Property(x => x.PriceUn).IsRequired();
        builder.HasOne(x => x.Product).WithMany(x => x.CartItems).HasForeignKey(x => x.ProductId);
    }
}
```

### Serviço da Classe `CartItem`

O serviço da classe `CartItem` fornece métodos para criar, consultar e remover itens do carrinho. Ele utiliza o repositório da classe `CartItem` para interagir com o banco de dados e mapeia os dados entre objetos DTO e objetos de modelo.

```csharp
public interface ICartItemService
{
    Task<ResultService<CartItemDTO>> CreateCartItem(CartItemDTO cartItemDTO);
    Task<ResultService<CartItemDTO>> GetCartItemById(Guid id);
    Task<ResultService<ICollection<CartItemDTO>>> GetCartItensByPurchase(Guid purchaseId);
    Task<ResultService<CartItemDTO>> RemoveCartItem(Guid id, CartItemDTO cartItemDTO);
}
```

### Controlador da API para `CartItem`

O controlador da API define os endpoints HTTP para interagir com itens do carrinho. Ele utiliza o serviço da classe `CartItem` para processar solicitações e respostas. A autenticação e a autorização devem ser implementadas conforme necessário.

```csharp
[Route("Cart")]
[ApiController]
public class CartItemController : ControllerBase
{
    private readonly ICartItemService _cartItemService;

    public CartItemController(ICartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCartItem([FromBody] CartItemDTO cartItemDTO)
    {
        // Implemente a lógica para criar um item do carrinho.
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCartItemById(Guid id)
    {
        // Implemente a lógica para obter detalhes de um item do carrinho por ID.
    }

    [HttpGet("purchase/{purchaseId}")]
    public async Task<IActionResult> GetCartItensByPurchase(Guid purchaseId)
    {
        // Implemente a lógica para obter todos os itens do carrinho de uma compra.
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveCartItem(Guid id)
    {
        // Implemente a lógica para remover um item do carrinho por ID.
    }
}
```

---

# Conclusão

Parabéns por explorar a documentação do projeto Recharge! Esperamos que esta documentação tenha fornecido uma visão completa e detalhada de nossa plataforma de comércio eletrônico. O Recharge foi projetado para simplificar e otimizar suas operações de vendas online, tornando a experiência de compra do cliente mais fácil e eficiente.

Ao longo deste documento, você aprendeu sobre as principais entidades do Recharge, incluindo **Product**, **User**, **Purchase** e **CartItem**, juntamente com seus modelos de dados, repositórios, serviços e controladores de API associados. Você descobriu como consumir os endpoints da API para executar uma variedade de ações, desde criar e atualizar produtos até gerenciar compras de clientes.

Lembre-se de que a documentação do Recharge é uma ferramenta valiosa para orientar o desenvolvimento e a integração da plataforma em seu ambiente de negócios. Use-a como referência enquanto constrói e expande sua loja online.

## Próximos Passos

Agora que você está familiarizado com o Recharge, aqui estão alguns próximos passos que você pode considerar:

1. **Integração**: Comece a integrar o Recharge em sua infraestrutura existente de comércio eletrônico. Use os recursos fornecidos nesta documentação para criar uma experiência de compra perfeita para seus clientes.

2. **Personalização**: Aproveite a flexibilidade do Recharge para personalizar a plataforma de acordo com as necessidades exclusivas de sua empresa. Adicione recursos adicionais, ajuste o design e melhore a funcionalidade.

3. **Suporte**: Se você encontrar desafios ou precisar de ajuda ao longo do caminho, nossa equipe de suporte está disponível para ajudar. Não hesite em entrar em contato conosco para obter assistência.

4. **Feedback**: Valorizamos muito seus comentários e sugestões. Se você tiver alguma observação sobre como podemos melhorar o Recharge ou esta documentação, ficaríamos gratos em ouvir.

Mais uma vez, obrigado por escolher o Recharge como sua solução de comércio eletrônico. Estamos ansiosos para ver o sucesso de sua loja online e estamos aqui para apoiar sua jornada.

Boa sorte e ótimos negócios com o Recharge!

---
