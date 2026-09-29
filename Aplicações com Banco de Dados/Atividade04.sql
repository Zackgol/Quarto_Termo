CREATE table produtos_loja(
		id_produto int primary key auto_increment,
        nome varchar(100) not null,
        categoria varchar(50) not null,
        preco decimal(10,2) not null        
);

insert into produtos_loja(nome, categoria, preco) values
('Celular','Eletronicos', 399.90),
('Notebook','Eletronicos', 999.90),
('Percy Jackson','Livros', 249.90),
('Harry Potter', 'Livros', 49.90);

delimiter //

create procedure sp_reajustar_preco_categoria(
		in p_categoria varchar(50),
        in p_porcentagem decimal(5,2)
)
begin
	update produtos_loja
    set preco = preco * (1 + p_porcentagem / 100)
    where categoria = p_categoria;
end //

create procedure sp_contar_produtos(
		in p_categoria varchar(50),
        out p_total int
)
begin
	select count(*)
    into p_total
    from produtos_loja
    where categoria = p_categoria;
end //

delimiter ;

call sp_contar_produtos('Eletronicos', @qtdTotal);
CALL sp_reajustar_preco_categoria('Livros', 20);

select @qtdTotal;
select * from produtos_loja
where categoria = 'Livros';

insert into produtos_loja(nome, categoria, preco) values
('Carregador Iphone','Eletronicos', 99.90);