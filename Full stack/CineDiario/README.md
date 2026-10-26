# CineDiário

Diário de filmes em WPF com Entity Framework Core e MySQL.

## Como rodar

1. Abrir `CineDiario.sln` no Visual Studio 2022.
2. Em `Data/CineContext.cs`, colocar a senha do seu MySQL no lugar de `SUA_SENHA`.
3. Apertar F5. O banco `cinediario` é criado sozinho no MySQL na primeira execução, já com alguns filmes.
4. Entrar com **admin** / **cinema123**, ou criar uma conta nova na tela de login.

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
