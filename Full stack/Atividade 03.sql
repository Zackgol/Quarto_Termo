
CREATE DATABASE IF NOT EXISTS biblioteca_db;
USE biblioteca_db;

-- 1. Criação das Tabelas
CREATE TABLE alunos (
    id_aluno INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    curso VARCHAR(50) NOT NULL
);

CREATE TABLE livros (
    id_livro INT AUTO_INCREMENT PRIMARY KEY,
    titulo VARCHAR(150) NOT NULL,
    autor VARCHAR(100) NOT NULL
);

CREATE TABLE emprestimos (
    id_emprestimo INT AUTO_INCREMENT PRIMARY KEY,
    id_aluno INT,
    id_livro INT,
    data_emprestimo DATE,
    FOREIGN KEY (id_aluno) REFERENCES alunos(id_aluno),
    FOREIGN KEY (id_livro) REFERENCES livros(id_livro)
);

-- 2. Povoamento de Dados
INSERT INTO alunos (nome, curso) VALUES 
('Hermilo Lunas', 'ADS'), 
('Bruna Lima', 'ADS'), 
('Diego Costa', 'Direito');

INSERT INTO livros (titulo, autor) VALUES 
('Entendendo Algoritmos', 'Aditya Bhargava'), 
('Engenharia de Software', 'Pressman'), 
('Direito Civil', 'Maria Helena');

INSERT INTO emprestimos (id_aluno, id_livro, data_emprestimo) VALUES 
(1, 1, '2026-08-01'), 
(1, 2, '2026-08-10'), 
(2, 1, '2026-08-12');

-- <<<< 1 >>>>
select alunos.nome as 'Nome do aluno', alunos.curso , livros.titulo as 'Titulo do Livro', emprestimos.data_emprestimo as 'Data do Emprestimo'
 from emprestimos 
 inner join alunos on alunos.id_aluno = emprestimos.id_aluno
 inner join livros on livros.id_livro = emprestimos.id_livro;
 
 -- <<<< 2 >>>>
 select alunos.nome as 'Nome dos Alunos' , livros.titulo as 'Titulos dos Livros'
 from alunos
 left join emprestimos on alunos.id_aluno = emprestimos.id_aluno
 left join livros on livros.id_livro = emprestimos.id_livro;

-- <<<< 3 >>>>
select livros.titulo as 'Titulos dos Livros'
from livros
left join emprestimos on livros.id_livro = emprestimos.id_livro
where emprestimos.id_livro is null;

-- <<<< 4 >>>>
select alunos.nome as 'Nome do Aluno', count(emprestimos.id_emprestimo) as 'Quantidade de emprestimos'
from alunos
left join emprestimos on emprestimos.id_aluno = alunos.id_aluno
left join livros on livros.id_livro = emprestimos.id_livro
group by alunos.id_aluno, alunos.nome;
