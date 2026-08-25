
-- <<<< 1 Criar banco e tabela >>>>
CREATE DATABASE loja_exercicios;
USE loja_exercicios;

create table Produtos (
	id int auto_increment primary key,
    nome varchar(100) not null,
    preco decimal(10,2) not null,
    estoque int not null
);



-- <<<< 2 Inserir dados >>>>
insert into Produtos(nome, preco, estoque) VALUES
('Amaciante',250.00, 10),
('Teclado',350.00, 22),
('Bola de Futebol',400.00, 9),
('Coleira',100.00, 21),
('Escova de Dente',20.50, 34),
('Mixer',321.45, 20);



-- <<<< 3 Select e Where >>>>
select * from Produtos;

select * from Produtos
where Produtos.preco > 100;

select * from Produtos
where Produtos.estoque < 10;

select * from Produtos
where Produtos.preco > 50 && Produtos.preco < 300;



-- <<<< 4 Order by e Funções >>>>
select * from Produtos
order by Produtos.preco desc;

select * from Produtos
order by Produtos.preco;

select * from Produtos
where preco = (select max(preco) from Produtos);

select * from Produtos
order by Produtos.preco
limit 1;

select count(*) from Produtos;



-- <<<< 5 Calculando valores >>>>
select nome, preco, estoque, (preco * estoque) as valor_total
from Produtos;

select sum(preco * estoque) as valor_total	
from Produtos;

select avg(preco) from Produtos;



-- <<<< 6 Criar relacionamento >>>>
create table categorias(
	id int auto_increment primary key,
    nome varchar(45) not null
);

insert into categorias (nome) VALUES
('Periféricos'),
('Monitores'),
('Acessórios');

alter table Produtos
add column id_categoria int,
add constraint fk_categoria
foreign key (id_categoria) references categorias(id);

show tables;
select * from Produtos;
select * from categorias;

UPDATE produtos
SET id_categoria = 3
WHERE id = 4;



-- <<<< 7 Inner Join >>>>
select Produtos.nome, produtos.preco, categorias.nome from Produtos
inner join categorias on Produtos.Id = Produtos.id;

select Produtos.nome, produtos.preco, categorias.nome from Produtos
INNER JOIN categorias ON Produtos.id_categoria = categorias.id
where categorias.nome = 'Periféricos';



-- <<<< 8 Left Join >>>>
select categorias.id,categorias.nome, produtos.nome from categorias
left join Produtos on categorias.id = Produtos.id_categoria;

insert into categorias (nome) VALUES
('Limpeza');



-- <<<< 9 Right Join >>>>
select categorias.id, categorias.nome as 'Nome da Categoria', Produtos.nome from categorias
right join Produtos on categorias.id = Produtos.id_categoria;

insert into Produtos (nome, preco, estoque) VALUES
('Maquina de lavar', 499.90, 5);



-- <<<< 10 MINI SISTEMA DE VENDAS >>>>
create table vendas (
	id_venda int auto_increment primary key,
    qtd int not null,
    data_venda date not null
);

alter table vendas
add column id_produto int,
add constraint fk_produto
foreign key (id_produto) references Produtos(id);

select * from vendas;

insert into vendas (qtd, data_venda, id_produto) values
(7,'2026-12-29', 2),
(10,'2026-09-24',1),
(20, '2026-04-30',6),
(15, '2026-03-10', 7),
(9, '2026-07-13', 4),
(8, '2026-10-21',5),
(14, '2026-08-03',3),
(10, '2026-05-09',1),
(17, '2026-04-09',3),
(5, '2026-09-04', 4);

select sum(vendas.qtd * Produtos.preco) as valor_total from vendas
join Produtos on vendas.id_produto = Produtos.id;

select Produtos.nome, sum(vendas.qtd) as qtd_vendidas, Produtos.preco,
sum(vendas.qtd * Produtos.preco) as valor_vendas_total from vendas
join Produtos on vendas.id_produto = Produtos.id
group by Produtos.id, Produtos.nome;

select sum(qtd) from vendas;

select Produtos.nome, sum(vendas.qtd) as total_vendido from vendas
join Produtos on vendas.id_produto = Produtos.id
group by Produtos.id
order by total_vendido desc
limit 1;

select avg(vendas.qtd * Produtos.preco) as valor_medio from vendas
join Produtos on vendas.id_produto = Produtos.id;

select Produtos.nome, sum(vendas.qtd) as total_vendido, sum(vendas.qtd * Produtos.preco) as valor_produto from vendas
join Produtos on vendas.id_produto = Produtos.id
group by Produtos.id
order by valor_produto desc
limit 1;
