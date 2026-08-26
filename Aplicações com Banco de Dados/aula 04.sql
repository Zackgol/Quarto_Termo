create database if not exists loja_aula4;
use loja_aula4;

create table produtos (
	id_produto INT auto_increment primary key,
    nome varchar(50) not null,
    preco decimal(10,2) not null,
    estoque int default 0
);

insert into produtos (nome, preco, estoque) VALUES
('Teclado Mecânico', 250.00, 15),
('Mouse Gamer', 120.00, 30),
('Monito 24', 890.00, 8);

select * from produtos;

delimiter //

create function fn_calcula_desconto(
	p_preco decimal(10,2),
    p_pct decimal(5,2)
)
returns decimal(10,2)
deterministic
begin
	return p_preco *(1 - (p_pct / 100));
end //

delimiter ;

-- Teste com valor fixo
select fn_calcula_desconto(10, 20) as preco_com_desconto;

-- Aplicando a função em uma consulta na tabela
select 
	nome, 
	preco,
	fn_calcula_desconto(preco, 10) as preco_promocional
from produtos;

delimiter //

create procedure sp_adiciona_estoque (
	in p_id int,
    in p_qtd int
)
begin
	update produtos
	set estoque = estoque + p_qtd
    where id_produto = p_id;
end //
delimiter ;

select * from produtos;

-- Adicionando 10 unidades ao produto ID 1 (teclado)
call sp_adiciona_estoque(1, 10);

-- Verificando o resultado no banco
select id_produto, nome, estoque
from produtos
where id_produto = 1;