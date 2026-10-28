# CineDiário

Diário de filmes em WPF com Entity Framework Core e MySQL.

## Como rodar

1. Ter o MySQL 8 rodando em `localhost:3306`.
2. Em `CineDiario/Data/CineContext.cs`, ajustar `user` e `password` na string de conexão.
3. Abrir `CineDiario.sln` no Visual Studio 2022 e apertar F5.
   O app aplica a migration `InitialCreate` sozinho (`Database.Migrate()`), cria o banco `cinediario`
   e as tabelas, e insere o usuário admin e alguns filmes.
4. Entrar com **admin** / **cinema123**, ou criar uma conta nova na tela de login.

## Migrations e SQL

- Migrations: pasta `CineDiario/Migrations` (migration `InitialCreate`).
- Script SQL do banco: `cinediario.sql` (também pode ser gerado com `Script-Migration -Output cinediario.sql`).
- Criar o banco manualmente pelo EF: `Update-Database` no Package Manager Console.
- Se alterar os modelos: `Add-Migration NomeDaMudanca` e depois `Update-Database`.

## Telas

- **Login**: usuário e senha, com senha guardada em hash (PBKDF2 + salt) e bloqueio de 30s depois de 3 erros.
- **Painel**: 4 cards com números da coleção, cada um abre uma tela.
- **Estante**: CRUD completo de filmes.
- **Busca**: pesquisa por título/diretor, gênero, faixa de anos e situação.
- **Pódio**: os 3 filmes com melhor nota, gênero mais assistido, diretor com melhor média e o filme mais longo.
- **Roleta**: sorteia um filme da fila que caiba no tempo que você tem.

## Estrutura

- `Models`: classes Filme e Usuario
- `Data`: CineContext (EF com MySQL) e carga inicial do banco
- `Repositories`: acesso aos dados com LINQ
- `Services`: regras de negócio, validações e tratamento de exceções
- `Views`: telas WPF
