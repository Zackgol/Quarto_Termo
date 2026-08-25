create database if not exists FullStackDB;
use FullStackDB;

create table if not exists Produtos (
	Id int auto_increment primary key,
    Nome varchar(100) not null, 
    Preco decimal(10,2) not null,
    Estoque int not null
);

insert into Produtos (Nome, Preco, Estoque) VALUES
('Teclado Mecanido RGB (MySQL)', 250.00, 15),
('Mouse Óptico 7200 DPI (MySQL)', 120.50, 30),
('Monitor 24 IPS (MySQL)', 899.90, 8);

show tables;
select * from produtos;
select * from movimentacoes;

insert into Produtos(Nome, Preco, Estoque) VALUES
('Memoria RAM 8GB', 200.00, 10);

insert into Movimentacoes (data, tipo_movimentacao, qtd_movimentacao, produtos_Id) VALUES
('2026-08-19', 'Entrada', 15, 3),
('2026-08-23', 'Saida', 20, 2),
('2026-08-28', 'Saida', 10, 1);

