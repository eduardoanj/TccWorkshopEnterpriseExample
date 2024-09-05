## Serviço de UserRegistrationEnterpriseExample

Esta aplicação ... // TODO

---

## Requerimentos mínimos para desenvolvimento

- [.NET 6.0 sdk](https://dotnet.microsoft.com/download/dotnet/6.0)
- Uma IDE de desenvolvimento em C# / .NET (recomendações: [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/)
  ou [Visual Studio Code](https://code.visualstudio.com/))
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

## Como gerar um Migration?

1. Execute o comando `dotnet tool install --global dotnet-ef` (apenas na primeira vez)
2. No diretório `src`, execute os comandos:

```
dotnet ef -p Registration.UserRegistrationEnterpriseExample.Infrastructure -s Registration.UserRegistrationEnterpriseExample.Presentation migrations add NOME_MIGRATION
dotnet ef -p Registration.UserRegistrationEnterpriseExample.Infrastructure -s Registration.UserRegistrationEnterpriseExample.Presentation database update
```
remove
dotnet ef -p Registration.UserRegistrationEnterpriseExample.Infrastructure -s Registration.UserRegistrationEnterpriseExample.Presentation migrations
---

Uma classe de migration será gerada no diretório `Migrations` do
projeto `Registration.UserRegistrationEnterpriseExample.Infrastructure`.

## Como executar a aplicação localmente?

Há duas formas:

1. Execução da aplicação com suas dependências:
    * No diretório `docker` execute o comando `docker-compose up`
    * Acesse o [Swagger da API](http://localhost:5001/swagger/index.html)
2. Execução apenas das dependências (útil para depuração):
    * No diretório `docker` execute o comando `docker-compose up postgres pgadmin`
    * Execute o projeto `Presentation` com o perfil `ServiceLocal` da aplicação na IDE
    * Acesse o [Swagger da API](https://localhost:5001/swagger/index.html)
