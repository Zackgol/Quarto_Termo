CREATE TABLE `alunos` (
  `id_aluno` int(11) NOT NULL,
  `nome_aluno` varchar(45) NOT NULL,
  `dt_nasc_aluno` date DEFAULT NULL,
  `email_aluno` varchar(45) DEFAULT NULL,
  `tel_aluno` varchar(45) DEFAULT NULL,
  `cep_aluno` char(8) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura da tabela `emprestimos`
--

CREATE TABLE `emprestimos` (
  `id_emprestimo` int(11) NOT NULL,
  `dt_emprestimo` date DEFAULT NULL,
  `livros_id_livro` int(11) NOT NULL,
  `alunos_id_aluno` int(11) NOT NULL,
  `funcionarios_id_funcionario` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura da tabela `funcionarios`
--

CREATE TABLE `funcionarios` (
  `id_funcionario` int(11) NOT NULL,
  `nome_funcionario` varchar(45) NOT NULL,
  `dt_nasc_funcionario` date DEFAULT NULL,
  `email_funcionario` varchar(45) DEFAULT NULL,
  `tel_funcionario` varchar(15) DEFAULT NULL,
  `cep_funcionario` char(8) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura da tabela `livros`
--

CREATE TABLE `livros` (
  `id_livro` int(11) NOT NULL,
  `titulo_livro` varchar(60) NOT NULL,
  `autor_livro` varchar(45) NOT NULL,
  `paginas_livros` int(11) DEFAULT NULL,
  `qtd_livro` int(11) DEFAULT NULL,
  `editora_livro` varchar(45) DEFAULT NULL,
  `dt_lancamento` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `alunos`
--
ALTER TABLE `alunos`
  ADD PRIMARY KEY (`id_aluno`);

--
-- Índices para tabela `emprestimos`
--
ALTER TABLE `emprestimos`
  ADD PRIMARY KEY (`id_emprestimo`),
  ADD KEY `fk_emprestimos_livros` (`livros_id_livro`),
  ADD KEY `fk_emprestimos_alunos` (`alunos_id_aluno`),
  ADD KEY `fk_emprestimos_funcionarios` (`funcionarios_id_funcionario`);

--
-- Índices para tabela `funcionarios`
--
ALTER TABLE `funcionarios`
  ADD PRIMARY KEY (`id_funcionario`);

--
-- Índices para tabela `livros`
--
ALTER TABLE `livros`
  ADD PRIMARY KEY (`id_livro`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `alunos`
--
ALTER TABLE `alunos`
  MODIFY `id_aluno` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `emprestimos`
--
ALTER TABLE `emprestimos`
  MODIFY `id_emprestimo` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `funcionarios`
--
ALTER TABLE `funcionarios`
  MODIFY `id_funcionario` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `livros`
--
ALTER TABLE `livros`
  MODIFY `id_livro` int(11) NOT NULL AUTO_INCREMENT;

--
-- Restrições para despejos de tabelas
--

--
-- Limitadores para a tabela `emprestimos`
--
ALTER TABLE `emprestimos`
  ADD CONSTRAINT `fk_emprestimos_alunos` FOREIGN KEY (`alunos_id_aluno`) REFERENCES `alunos` (`id_aluno`),
  ADD CONSTRAINT `fk_emprestimos_funcionarios` FOREIGN KEY (`funcionarios_id_funcionario`) REFERENCES `funcionarios` (`id_funcionario`),
  ADD CONSTRAINT `fk_emprestimos_livros` FOREIGN KEY (`livros_id_livro`) REFERENCES `livros` (`id_livro`);
COMMIT;


-- 10 alunos
INSERT INTO alunos
(nome_aluno, dt_nasc_aluno, email_aluno, tel_aluno, cep_aluno)
VALUES
('João Silva', '2005-03-15', 'joao.silva@email.com', '11987654321', '01310100'),
('Maria Oliveira', '2004-07-22', 'maria.oliveira@email.com', '11987654322', '01310900'),
('Pedro Santos', '2006-01-10', 'pedro.santos@email.com', '11987654323', '02045000'),
('Ana Costa', '2005-11-05', 'ana.costa@email.com', '11987654324', '04002000'),
('Lucas Souza', '2004-09-18', 'lucas.souza@email.com', '11987654325', '05010000'),
('Juliana Lima', '2006-04-27', 'juliana.lima@email.com', '11987654326', '06016000'),
('Gabriel Rocha', '2005-12-12', 'gabriel.rocha@email.com', '11987654327', '07010000'),
('Beatriz Alves', '2004-02-08', 'beatriz.alves@email.com', '11987654328', '08010000'),
('Rafael Martins', '2006-06-30', 'rafael.martins@email.com', '11987654329', '09010000'),
('Camila Ferreira', '2005-08-14', 'camila.ferreira@email.com', '11987654330', '10010000');


-- 10 funcionários
INSERT INTO funcionarios
(nome_funcionario, dt_nasc_funcionario, email_funcionario, tel_funcionario, cep_funcionario)
VALUES
('Carlos Mendes', '1985-02-10', 'carlos.mendes@biblioteca.com', '11911112222', '01310100'),
('Fernanda Ribeiro', '1990-06-15', 'fernanda.ribeiro@biblioteca.com', '11911113333', '01310900'),
('Ricardo Gomes', '1982-09-20', 'ricardo.gomes@biblioteca.com', '11911114444', '02045000'),
('Patricia Nunes', '1988-12-03', 'patricia.nunes@biblioteca.com', '11911115555', '04002000'),
('Marcos Teixeira', '1979-04-25', 'marcos.teixeira@biblioteca.com', '11911116666', '05010000'),
('Larissa Barbosa', '1992-07-11', 'larissa.barbosa@biblioteca.com', '11911117777', '06016000'),
('André Carvalho', '1986-10-30', 'andre.carvalho@biblioteca.com', '11911118888', '07010000'),
('Renata Castro', '1991-03-17', 'renata.castro@biblioteca.com', '11911119999', '08010000'),
('Eduardo Moreira', '1984-05-09', 'eduardo.moreira@biblioteca.com', '11911110000', '09010000'),
('Sabrina Freitas', '1989-11-21', 'sabrina.freitas@biblioteca.com', '11911112223', '10010000');


-- 10 livros
INSERT INTO livros
(titulo_livro, autor_livro, paginas_livros, qtd_livro, editora_livro, dt_lancamento)
VALUES
('Dom Casmurro', 'Machado de Assis', 256, 5, 'Editora Moderna', '1899-01-01'),
('O Cortiço', 'Aluísio Azevedo', 320, 4, 'Editora Ática', '1890-01-01'),
('Capitães da Areia', 'Jorge Amado', 280, 6, 'Companhia das Letras', '1937-01-01'),
('Vidas Secas', 'Graciliano Ramos', 176, 3, 'Record', '1938-01-01'),
('A Hora da Estrela', 'Clarice Lispector', 96, 5, 'Rocco', '1977-10-01'),
('Memórias Póstumas de Brás Cubas', 'Machado de Assis', 240, 4, 'Penguin', '1881-01-01'),
('Grande Sertão: Veredas', 'João Guimarães Rosa', 624, 2, 'Nova Fronteira', '1956-01-01'),
('Iracema', 'José de Alencar', 160, 5, 'Martin Claret', '1865-01-01'),
('O Alienista', 'Machado de Assis', 112, 4, 'L&PM', '1882-01-01'),
('Sagarana', 'João Guimarães Rosa', 368, 3, 'Nova Fronteira', '1946-01-01');


-- 10 empréstimos
INSERT INTO emprestimos
(dt_emprestimo, livros_id_livro, alunos_id_aluno, funcionarios_id_funcionario)
VALUES
('2026-08-01', 1, 1, 1),
('2026-08-03', 2, 2, 2),
('2026-08-05', 3, 3, 3),
('2026-08-07', 4, 4, 4),
('2026-08-10', 5, 5, 5),
('2026-08-12', 6, 6, 6),
('2026-08-15', 7, 7, 7),
('2026-08-18', 8, 8, 8),
('2026-08-20', 9, 9, 9),
('2026-08-22', 10, 10, 10);


create view relatorio_alun_func AS
select livros.titulo_livro as 'Titulo do Livro', alunos.nome_aluno as 'Nome do Aluno', funcionarios.nome_funcionario as 'Nome do Funcionario'
from livros 
inner join emprestimos on emprestimos.livros_id_livro = livros.id_livro
join funcionarios on funcionarios.id_funcionario = emprestimos.funcionarios_id_funcionario
join alunos on alunos.id_aluno = emprestimos.alunos_id_aluno;

select * from relatorio_alun_func where
`Titulo do Livro` = 'Sagarana';


create view relatorio_data_especf AS
select alunos.nome_aluno, emprestimos.dt_emprestimo 
from alunos 
inner join emprestimos on emprestimos.alunos_id_aluno = alunos.id_aluno
where emprestimos.dt_emprestimo between '2026-08-01' and '2026-08-10';

select * from relatorio_data_especf;
