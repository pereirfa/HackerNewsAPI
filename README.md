## 📋 Informações do Projeto

| Campo                  | Descrição                                                                 |
|------------------------|---------------------------------------------------------------------------|
| **Nome do Candidato**  | Fabio Sarmento Pereira                                                    |
| **Repositório**        | [HackerNewsAPI](https://github.com/pereirfa/HackerNewsAPI)                |
| **Arquitetura**        | MVC (Controller / Model / Services)                                       |
| **Documentação Swagger** | Não implementada                                                        |
| **Logging/Monitoramento** | Não implementado                                                       |
| **Objetivo**           | Consumir dados da API pública do Hacker News e disponibilizar via rotas   |

---

# HackerNews API

## 📌 Descrição
Esta aplicação é uma API construída em ASP.NET Core que expõe endpoints para consultar dados da [Hacker News API](https://github.com/HackerNews/API).  
Ela utiliza `HttpClient` para comunicação externa e `MemoryCache` para otimizar chamadas repetidas.

---

## 🚀 Como rodar a aplicação

### Pré-requisitos
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) instalado
- Acesso à internet (para chamadas reais à API Hacker News)


## 🚀 Passos para executar a aplicação

| Nº | Etapa                        | Comando / Ação                                                                 |
|----|------------------------------|--------------------------------------------------------------------------------|
| 1  | Clonar o repositório         | `git clone https://github.com/pereirfa/HackerNewsAPI.git`                      |
| 2  | Acessar a pasta do projeto   | `cd HackerNewsAPI`                                                              |
| 3  | Restaurar dependências       | `dotnet restore`                                                                |
| 4  | Compilar o projeto           | `dotnet build`                                                                  |
| 5  | Executar a aplicação         | `dotnet run`                                                                    |
| 6  | Acessar a API localmente     | `http://localhost:5000` ou `https://localhost:5001`                             |
| 7  | Testar endpoints disponíveis | Exemplo: `GET /Item/{id}` para buscar item específico, `GET /TopStories` para histórias principais |


   
## 📂 Estrutura do Projeto
| Projeto/Folder            | Descrição                          |
|----------------------------|------------------------------------|
| **HackerNewsAPI/**         | Projeto principal da API           |
| ├── Controllers/           | Controladores da aplicação         |
| ├── Models/                | Modelos de dados                   |
| ├── Services/              | Serviços e lógica de negócio       |
| **HackerNewsAPI.Tests/**   | Projeto de testes automatizados    |



# 📌 Resumo das Rotas da API

Base das rotas:
## 🔹 ItemController

| Método | Rota            | Descrição                          |
|--------|-----------------|------------------------------------|
| GET    | /v0/item/{id}   | Retorna os detalhes de um item pelo seu **id**. |

---

## 🔹 StoriesController

| Método | Rota                      | Descrição                                      |
|--------|---------------------------|------------------------------------------------|
| GET    | /v0/stories/topstories    | Lista das histórias mais populares.            |
| GET    | /v0/stories/newstories    | Lista das histórias mais recentes.             |
| GET    | /v0/stories/beststories   | Lista das histórias com melhor avaliação.      |
| GET    | /v0/stories/askstories    | Lista das histórias do tipo *Ask HN*.          |
| GET    | /v0/stories/showstories   | Lista das histórias do tipo *Show HN*.         |
| GET    | /v0/stories/jobstories    | Lista das histórias relacionadas a empregos.   |

---

## 🔹 UserController

| Método | Rota            | Descrição                          |
|--------|-----------------|------------------------------------|
| GET    | /v0/user/{id}   | Retorna os detalhes de um usuário pelo seu **id**. |


```markdown
## ⚙️ Funcionamento do Cache na API

A API utiliza um serviço chamado **CachedHackerNewsService**, que encapsula o `HackerNewsService` e adiciona suporte a **cache em memória** através do `IMemoryCache` do ASP.NET Core.  

### 🔹 Fluxo de funcionamento (exemplo com `ItemController`)

1. **Primeira requisição**  
   - Quando o cliente chama `GET /v0/item/{id}`, o método `GetItemAsync(id)` é executado.  
   - O serviço verifica se o item já está armazenado no cache.  
   - Como é a primeira chamada, o item **não está no cache** → o serviço consulta o Hacker News via `HttpClient`.  
   - O resultado é armazenado no cache para futuras chamadas.

2. **Segunda requisição (mesmo id)**  
   - O cliente chama novamente `GET /v0/item/{id}`.  
   - O serviço encontra o item no cache e retorna imediatamente, **sem chamar o HttpClient**.  
   - Isso reduz a latência e evita chamadas repetidas à API externa.

3. **Validade do cache**  
   - O tempo de vida dos objetos no cache é configurado pelo `MemoryCacheOptions`.  
   - Enquanto o item estiver válido no cache, novas requisições para o mesmo `id` serão atendidas diretamente da memória.

### 🔹 Benefícios

- **Performance**: respostas mais rápidas após a primeira chamada.  
- **Eficiência**: menos chamadas externas ao Hacker News.  
- **Escalabilidade**: reduz carga em serviços externos e melhora a experiência do usuário.

### 🔹 Teste de validação

O projeto inclui testes unitários (`CachedHackerNewsServiceTests`) que garantem o funcionamento correto do cache:

- Na **primeira chamada**, o `HttpClient` é invocado.  
- Na **segunda chamada com o mesmo id**, o resultado vem do cache.  
- O teste confirma que o `HttpClient` foi chamado apenas **uma vez**, validando o uso do cache.

```csharp
Assert.Equal(1, handler.CallCount); // HttpClient chamado apenas uma vez
  
## Observações sobre o Projeto

| Aspecto                        | Situação Atual                                                                 |
|--------------------------------|-------------------------------------------------------------------------------|
| **Documentação Swagger**        | Não foi implementada documentação das APIs utilizando Swagger/OpenAPI.        |
| **Logging e Monitoramento**     | Não há processos de logging estruturado nem monitoramento das chamadas de API. |
| **Arquitetura**                 | O projeto segue o padrão **MVC** (Controller / Model / Services), focando na codificação e no tratamento das rotas. |

## Próximos Passos
Para tornar o projeto mais robusto e pronto para produção, sugerimos as seguintes melhorias:

- [ ] **Implementar documentação Swagger/OpenAPI**  
  - Facilitar testes e visualização das rotas.  
  - Gerar documentação automática das APIs.  

- [ ] **Adicionar logging estruturado**  
  - Utilizar bibliotecas como **Serilog** ou **NLog**.  
  - Registrar erros, métricas de desempenho e eventos importantes.  

- [ ] **Configurar monitoramento**  
  - Integrar ferramentas como **Application Insights**, **Prometheus** ou **Grafana**.  
  - Acompanhar disponibilidade, tempo de resposta e uso das APIs.  

- [ ] **Melhorar tratamento de erros**  
  - Implementar respostas padronizadas para falhas.  
  - Garantir mensagens claras para o cliente da API.  

- [ ] **Testes automatizados**  
  - Criar testes unitários e de integração.  
  - Validar o comportamento dos controllers, services e models.  

- [ ] **Documentação adicional**  
  - Explicar arquitetura MVC adotada.  
  - Detalhar fluxo de dados entre Controller → Service → Model.  
  


