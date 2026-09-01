CREATE DATABASE escola;
USE escola;

CREATE TABLE cursos (
    id_curso INT PRIMARY KEY,
    nome_curso VARCHAR(100),
    area VARCHAR(50),
    mensalidade DECIMAL(10,2)
);

CREATE TABLE professores (
    id_professor INT PRIMARY KEY,
    nome_professor VARCHAR(100),
    titulacao VARCHAR(50),
    salario DECIMAL(10,2)
);

CREATE TABLE alunos (
    id_aluno INT PRIMARY KEY,
    nome_aluno VARCHAR(100),
    cpf VARCHAR(14),
    cidade VARCHAR(60),
    status_aluno VARCHAR(20)
);

CREATE TABLE turmas (
    id_turma INT PRIMARY KEY,
    nome_turma VARCHAR(50),
    ano_semestre VARCHAR(10),
    id_curso INT,
    id_professor INT,

    FOREIGN KEY (id_curso)
        REFERENCES cursos(id_curso),

    FOREIGN KEY (id_professor)
        REFERENCES professores(id_professor)
);

CREATE TABLE matriculas (
    id_matricula INT PRIMARY KEY,
    id_aluno INT,
    id_turma INT,
    data_matricula DATE,
    nota_final DECIMAL(4,2),

    FOREIGN KEY (id_aluno)
        REFERENCES alunos(id_aluno),

    FOREIGN KEY (id_turma)
        REFERENCES turmas(id_turma)
);

INSERT INTO cursos (id_curso, nome_curso, area, mensalidade) VALUES
(1, 'Desenvolvimento de Sistemas', 'Tecnologia', 650.00),
(2, 'Administração', 'Gestão', 520.00),
(3, 'Enfermagem', 'Saúde', 780.00),
(4, 'Contabilidade', 'Gestão', 490.00),
(5, 'Design Gráfico', 'Comunicação', 590.00),
(6, 'Redes de Computadores', 'Tecnologia', 620.00),
(7, 'Marketing', 'Comunicação', 550.00),
(8, 'Recursos Humanos', 'Gestão', 480.00),
(9, 'Segurança do Trabalho', 'Segurança', 570.00),
(10, 'Logística', 'Gestão', 510.00);



INSERT INTO professores (id_professor, nome_professor, titulacao, salario) VALUES
(1, 'Carlos Eduardo Mendes', 'Mestre', 5800.00),
(2, 'Fernanda Oliveira Santos', 'Especialista', 4600.00),
(3, 'Ricardo Almeida Souza', 'Doutor', 7200.00),
(4, 'Juliana Martins Ferreira', 'Mestre', 6100.00),
(5, 'Marcos Vinicius Costa', 'Especialista', 4300.00),
(6, 'Patricia Rodrigues Lima', 'Mestre', 5900.00),
(7, 'André Luiz Barbosa', 'Especialista', 4500.00),
(8, 'Camila Fernandes Rocha', 'Doutora', 7500.00),
(9, 'Gustavo Henrique Alves', 'Mestre', 5700.00),
(10, 'Mariana Cristina Gomes', 'Especialista', 4400.00),
(11, 'Rafael Martins Pereira', 'Mestre', 6200.00),
(12, 'Aline Beatriz Carvalho', 'Doutora', 7800.00);



INSERT INTO alunos
(id_aluno, nome_aluno, cpf, cidade, status_aluno)
VALUES
(1, 'João Pedro Silva', '123.456.789-01', 'Presidente Prudente', 'Ativo'),
(2, 'Maria Eduarda Santos', '234.567.890-12', 'Presidente Prudente', 'Ativo'),
(3, 'Lucas Gabriel Oliveira', '345.678.901-23', 'Álvares Machado', 'Ativo'),
(4, 'Ana Clara Souza', '456.789.012-34', 'Presidente Prudente', 'Ativo'),
(5, 'Gabriel Henrique Costa', '567.890.123-45', 'Regente Feijó', 'Ativo'),
(6, 'Beatriz Almeida', '678.901.234-56', 'Presidente Prudente', 'Ativo'),
(7, 'Rafael Martins', '789.012.345-67', 'Presidente Prudente', 'Ativo'),
(8, 'Larissa Fernandes', '890.123.456-78', 'Pirapozinho', 'Ativo'),
(9, 'Matheus Rodrigues', '901.234.567-89', 'Presidente Prudente', 'Ativo'),
(10, 'Isabela Ferreira', '012.345.678-90', 'Presidente Prudente', 'Ativo'),
(11, 'Pedro Henrique Lima', '111.222.333-44', 'Álvares Machado', 'Ativo'),
(12, 'Mariana Carvalho', '222.333.444-55', 'Presidente Prudente', 'Ativo'),
(13, 'Gustavo Ribeiro', '333.444.555-66', 'Regente Feijó', 'Ativo'),
(14, 'Letícia Gomes', '444.555.666-77', 'Presidente Prudente', 'Inativo'),
(15, 'Thiago Barbosa', '555.666.777-88', 'Pirapozinho', 'Ativo'),
(16, 'Amanda Vitória Martins', '666.777.888-99', 'Presidente Prudente', 'Ativo'),
(17, 'Bruno Henrique Lopes', '777.888.999-00', 'Presidente Prudente', 'Ativo'),
(18, 'Carolina Mendes', '888.999.000-11', 'Álvares Machado', 'Ativo'),
(19, 'Diego Fernandes', '999.000.111-22', 'Presidente Prudente', 'Inativo'),
(20, 'Sofia Rodrigues', '000.111.222-33', 'Presidente Prudente', 'Ativo');



INSERT INTO turmas
(id_turma, nome_turma, ano_semestre, id_curso, id_professor)
VALUES
(1, 'DS-01', '2026-1', 1, 1),
(2, 'DS-02', '2026-1', 1, 7),
(3, 'ADM-01', '2026-1', 2, 2),
(4, 'ADM-02', '2026-1', 2, 10),
(5, 'ENF-01', '2026-1', 3, 8),
(6, 'CONT-01', '2026-1', 4, 3),
(7, 'DG-01', '2026-1', 5, 4),
(8, 'REDES-01', '2026-1', 6, 5),
(9, 'MKT-01', '2026-2', 7, 6),
(10, 'RH-01', '2026-2', 8, 9),
(11, 'SEG-01', '2026-2', 9, 11),
(12, 'LOG-01', '2026-2', 10, 12),
(13, 'DS-03', '2026-2', 1, 1),
(14, 'ADM-03', '2026-2', 2, 2),
(15, 'REDES-02', '2026-2', 6, 5);


INSERT INTO matriculas
(id_matricula, id_aluno, id_turma, data_matricula, nota_final)
VALUES
(1, 1, 1, '2026-02-02', 8.50),
(2, 2, 1, '2026-02-02', 9.20),
(3, 3, 1, '2026-02-03', 7.80),
(4, 4, 1, '2026-02-03', 6.50),

(5, 5, 2, '2026-02-04', 8.90),
(6, 6, 2, '2026-02-04', 7.40),
(7, 7, 2, '2026-02-05', 9.50),

(8, 8, 3, '2026-02-05', 8.00),
(9, 9, 3, '2026-02-06', 7.20),
(10, 10, 3, '2026-02-06', 9.10),

(11, 11, 4, '2026-02-07', 6.80),
(12, 12, 4, '2026-02-07', 8.70),
(13, 13, 4, '2026-02-08', 7.90),

(14, 14, 5, '2026-02-08', 5.50),
(15, 15, 5, '2026-02-09', 8.30),
(16, 16, 5, '2026-02-09', 9.40),

(17, 17, 6, '2026-02-10', 7.60),
(18, 18, 6, '2026-02-10', 8.80),
(19, 19, 6, '2026-02-11', 6.20),

(20, 20, 7, '2026-02-11', 9.00),
(21, 1, 7, '2026-02-12', 8.40),
(22, 2, 7, '2026-02-12', 7.70),

(23, 3, 8, '2026-02-13', 8.60),
(24, 4, 8, '2026-02-13', 7.30),
(25, 5, 8, '2026-02-14', 9.20),

(26, 6, 9, '2026-08-03', 8.90),
(27, 7, 9, '2026-08-03', 7.50),
(28, 8, 9, '2026-08-04', 9.30),

(29, 9, 10, '2026-08-04', 8.10),
(30, 10, 10, '2026-08-05', 7.60),
(31, 11, 10, '2026-08-05', 9.00),

(32, 12, 11, '2026-08-06', 8.70),
(33, 13, 11, '2026-08-06', 6.90),
(34, 14, 11, '2026-08-07', 5.80),

(35, 15, 12, '2026-08-07', 8.40),
(36, 16, 12, '2026-08-08', 9.10),
(37, 17, 12, '2026-08-08', 7.80),

(38, 18, 13, '2026-08-10', 8.90),
(39, 19, 13, '2026-08-10', 6.40),
(40, 20, 13, '2026-08-11', 9.50),

(41, 1, 14, '2026-08-11', 7.90),
(42, 2, 14, '2026-08-12', 8.60),
(43, 3, 14, '2026-08-12', 9.20),

(44, 4, 15, '2026-08-13', 7.40),
(45, 5, 15, '2026-08-13', 8.80),
(46, 6, 15, '2026-08-14', 9.00);




SELECT t.nome_turma, t.ano_semestre, ROUND(AVG(m.nota_final), 2) AS media FROM turmas t INNER 
JOIN matriculas m ON t.id_turma = m.id_turma GROUP BY t.nome_turma, t.ano_semestre HAVING 
AVG(m.nota_final) >= 7.0; 

SELECT c.nome_curso, SUM(c.mensalidade) AS total_receita FROM cursos c INNER JOIN 
turmas t ON c.id_curso = t.id_curso INNER JOIN matriculas m ON t.id_turma = m.id_turma GROUP 
BY c.nome_curso ORDER BY total_receita DESC; 