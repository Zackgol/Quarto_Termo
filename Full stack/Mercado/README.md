# Mercado — Trabalho 3 (Banco de Dados)

Sistema WPF + Entity Framework Core (SQLite) simples: cadastro de produtos
de mercado com listagem, adição, edição, exclusão e pesquisa parametrizada.

## Estrutura

```
Mercado/
  Mercado.sln
  Mercado/
    Mercado.csproj
    App.xaml / App.xaml.cs
    Models/Produto.cs
    Data/MercadoContext.cs        -> DbContext + seed inicial
    Exceptions/MercadoExceptions.cs
    Repositories/ProdutoRepository.cs   -> acesso a dados (LINQ parametrizado)
    Services/ProdutoService.cs          -> validações / regras de negócio
    MainWindow.xaml / .cs               -> tela única: grid + formulário
```

## Como rodar

1. Instale o **.NET 8 SDK** (https://dotnet.microsoft.com/download).
2. Abra `Mercado.sln` no Visual Studio 2022 e rode (F5) — os pacotes NuGet
   (`Microsoft.EntityFrameworkCore.Sqlite` e `.Design`) são restaurados
   automaticamente.
3. Na primeira execução, o `MercadoContext` cria `mercado.db` (SQLite) e
   popula com 8 produtos de exemplo.

Ou pelo terminal:
```bash
cd Mercado
dotnet restore
dotnet run --project Mercado
```

## Onde está cada requisito

| Requisito | Onde está |
|---|---|
| Projeto WPF com Entity Framework | `MercadoContext.cs`, `.csproj` |
| Tela inicial listando dados | `MainWindow.xaml` — `GridProdutos` |
| Botão de add | "➕ Adicionar Produto" |
| Adicionar novo dado no banco | `ProdutoService.Salvar` + `ProdutoRepository.Adicionar` |
| Camada de serviço/repositório | `Repositories/`, `Services/` |
| Tratamento de exceções | `Exceptions/`, try/catch nas duas camadas |
| Pesquisa parametrizada | `ProdutoRepository.Buscar(termoNome, categoria)` |

Edição e exclusão foram incluídas além do mínimo pedido, pra deixar o CRUD
completo — é só clicar numa linha do grid pra carregar no formulário.
