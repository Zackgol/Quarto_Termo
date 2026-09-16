create database techstore_db;
use techstore_db;

-- 1. Tabela de Clientes
CREATE TABLE IF NOT EXISTS clientes (
    id_cliente INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    cidade VARCHAR(60) NOT NULL,
    uf CHAR(2) NOT NULL,
    limite_credito DECIMAL(10,2) NOT NULL
);

-- 2. Tabela de Categorias
CREATE TABLE IF NOT EXISTS categorias (
    id_categoria INT AUTO_INCREMENT PRIMARY KEY,
    nome_categoria VARCHAR(50) NOT NULL,
    setor VARCHAR(50) NOT NULL
);

-- 3. Tabela de Produtos
CREATE TABLE IF NOT EXISTS produtos (
    id_produto INT AUTO_INCREMENT PRIMARY KEY,
    descricao VARCHAR(120) NOT NULL,
    id_categoria INT NOT NULL,
    preco_unitario DECIMAL(10,2) NOT NULL,
    estoque INT NOT NULL,
    CONSTRAINT fk_produtos_categorias FOREIGN KEY (id_categoria) REFERENCES categorias(id_categoria)
);

-- 4. Tabela de Vendas
CREATE TABLE IF NOT EXISTS vendas (
    id_venda INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente INT NOT NULL,
    id_produto INT NOT NULL,
    data_venda DATE NOT NULL,
    quantidade INT NOT NULL,
    desconto_aplicado DECIMAL(5,2) DEFAULT 0.00,
    CONSTRAINT fk_vendas_clientes FOREIGN KEY (id_cliente) REFERENCES clientes(id_cliente),
    CONSTRAINT fk_vendas_produtos FOREIGN KEY (id_produto) REFERENCES produtos(id_produto)
);

-- Inserindo 8 Clientes
INSERT INTO clientes (nome, cidade, uf, limite_credito) VALUES
('Carlos Eduardo Lima', 'Presidente Prudente', 'SP', 5000.00),
('Mariana Souza Santos', 'Álvares Machado', 'SP', 3500.00),
('Roberto Silva Prado', 'Regente Feijó', 'SP', 4200.00),
('Aline Castro Fernandes', 'Presidente Prudente', 'SP', 8000.00),
('Lucas Gabriel Mendes', 'Pirapozinho', 'SP', 2500.00),
('Juliana Barbosa Nogueira', 'Martinópolis', 'SP', 6000.00),
('Fernando Henrique Maia', 'Presidente Prudente', 'SP', 4500.00),
('Patrícia Gomes Rocha', 'Santo Anastácio', 'SP', 3000.00);

-- Inserindo 8 Categorias
INSERT INTO categorias (nome_categoria, setor) VALUES
('Hardware', 'Informática'),
('Periféricos', 'Informática'),
('Monitores', 'Vídeo e Imagem'),
('Redes', 'Infraestrutura'),
('Acessórios Gamer', 'Games'),
('Armazenamento', 'Informática'),
('Impressão', 'Escritório'),
('Automação', 'Eletrônicos');

-- Inserindo 8 Produtos
INSERT INTO produtos (descricao, id_categoria, preco_unitario, estoque) VALUES
('Processador Octa Core 4.2GHz', 1, 1450.00, 25),
('Teclado Mecânico RGB', 2, 280.00, 60),
('Monitor Ultrawide 29 Polegadas', 3, 1150.00, 18),
('Roteador Wi-Fi 6 Gigabit', 4, 390.00, 40),
('Headset 7.1 Surround', 5, 320.00, 35),
('SSD NVMe M.2 1TB', 6, 420.00, 50),
('Impressora Tanque de Tinta', 7, 890.00, 12),
('Módulo Relé Programável', 8, 45.00, 100);

-- Inserindo 8 Vendas
INSERT INTO vendas (id_cliente, id_produto, data_venda, quantidade, desconto_aplicado) VALUES
(1, 1, '2026-08-10', 1, 50.00),
(2, 2, '2026-08-12', 2, 10.00),
(3, 6, '2026-08-15', 2, 20.00),
(4, 3, '2026-08-20', 1, 0.00),
(5, 4, '2026-08-22', 1, 15.00),
(6, 5, '2026-09-02', 2, 25.00),
(7, 7, '2026-09-05', 1, 40.00),
(8, 8, '2026-09-10', 4, 0.00);

create view relatorio_total_vendas AS
SELECT 
    v.id_venda,
    v.data_venda,
    c.nome AS cliente,
    c.cidade,
    p.descricao AS produto,
    cat.nome_categoria AS categoria,
    v.quantidade,
    p.preco_unitario,
    v.desconto_aplicado,
    ((v.quantidade * p.preco_unitario) - v.desconto_aplicado) AS valor_total_liquido
FROM vendas v
INNER JOIN clientes c ON v.id_cliente = c.id_cliente
INNER JOIN produtos p ON v.id_produto = p.id_produto
INNER JOIN categorias cat ON p.id_categoria = cat.id_categoria
ORDER BY v.data_venda DESC;


-- <<<< 1 >>>>
create view relatorio_vendas_detalhadas AS
select vendas.quantidade as quantidade_de_vendas, produtos.descricao as Produto, (vendas.quantidade * produtos.preco_unitario) as valor_total  
from vendas
inner join produtos on produtos.id_produto = vendas.id_produto;

select * from relatorio_vendas_detalhadas;
-- <<<< 2 >>>>
select 
from vendas
inner join produtos on produtos.id_produto = vendas.id_produto 
inner join categorias on categorias.id_categoria = produtos.id_categoria
group by categorias.nome_categoria;