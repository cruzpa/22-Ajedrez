create proc producto_insertar
@nombre varchar(50), @precio float
as
begin
declare @id int = (select isnull(max(ID_PRODUCTO),0)+1 from producto)
insert into producto values ( @id, @nombre, @precio)
end
go


create proc producto_editar
@id int, @nombre varchar(50), @precio float
as
begin
update producto set
nombre = @nombre,
precio = @precio
where ID_PRODUCTO = @id
end 

go

create proc producto_borrar
@id int
as
begin
delete from PRODUCTO where ID_PRODUCTO = @id
end 

go



create proc producto_listar
as
begin
select * from producto
end