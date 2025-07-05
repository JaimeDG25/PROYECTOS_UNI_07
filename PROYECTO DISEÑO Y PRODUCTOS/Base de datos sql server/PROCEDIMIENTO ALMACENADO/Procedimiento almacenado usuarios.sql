use DBPORTALUTP;
select * from usuarios
create proc sp_RegistrarUsuario(
    @Nombre_Usuario varchar(100),
    @Apellido_Usuario varchar(100),
    @Correo_electronico_Usuario varchar(100),
	@Rol_Id varchar(100),
    @Clave varchar(100),
    @Activo bit,
    @Mensaje varchar(500) output,
    @Resultado int output
)
as
begin
    SET @Resultado = 0
    IF NOT EXISTS (SELECT * FROM usuarios WHERE Correo_electronico_Usuario = @Correo_electronico_Usuario)
    begin
        insert into usuarios(Nombre_Usuario, Apellido_Usuario, Correo_electronico_Usuario,Rol_Id, Clave, Activo) 
        values (@Nombre_Usuario, @Apellido_Usuario, @Correo_electronico_Usuario,@Rol_Id, @Clave, @Activo)

        SET @Resultado = scope_identity()
    end
    else
        set @Mensaje = 'El correo del usuario ya existe'
end

create proc sp_EditarUsuario(
    @Id_Usuario int,
    @Nombre_Usuario varchar(100),
    @Apellido_Usuario varchar(100),
    @Correo_electronico_Usuario varchar(100),
	@Rol_Id varchar(100),
    @Activo bit,
    @Mensaje varchar(500) output,
    @Resultado int output
)
as
begin
    SET @Resultado = 0
    IF NOT EXISTS (SELECT * FROM usuarios WHERE Correo_electronico_Usuario = @Correo_electronico_Usuario and Id_Usuario != @Id_Usuario)
    begin
        update top (1) usuarios set
            Nombre_Usuario = @Nombre_Usuario,
            Apellido_Usuario = @Apellido_Usuario,
            Correo_electronico_Usuario = @Correo_electronico_Usuario,
            Activo = @Activo,
			Rol_Id = @Rol_Id
        where Id_Usuario = @Id_Usuario

        SET @Resultado = 1
    end
    else
        set @Mensaje = 'El correo del usuario ya existe'
end
