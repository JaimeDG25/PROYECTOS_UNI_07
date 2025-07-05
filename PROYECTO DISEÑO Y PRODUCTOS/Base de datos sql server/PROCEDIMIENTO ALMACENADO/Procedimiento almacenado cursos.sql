use DBPORTALUTP;
select * from cursos
select * from carreras
insert into cursos(Codigo_Curso, Nombre_Curso, Creditos, Carrera_Id, Descripcion_Curso, Activo_Curso) 
        values ('UTP-123456-1', 'Matematica 1', 3, 1,'Un curso de matematica basica', 1) 
create proc sp_RegistrarCurso(
    @Codigo_Curso varchar(100),
    @Nombre_Curso varchar(100),
    @Creditos int,
	@Carrera_Id varchar(100),
	@Descripcion_Curso varchar(100),
    @Activo_Curso bit,
    @Mensaje varchar(500) output,
    @Resultado int output
)
as
begin
    SET @Resultado = 0
    IF NOT EXISTS (SELECT * FROM cursos WHERE Codigo_Curso = @Codigo_Curso)
    begin
        insert into cursos(Codigo_Curso, Nombre_Curso, Creditos, Carrera_Id, Descripcion_Curso, Activo_Curso) 
        values (@Codigo_Curso, @Nombre_Curso, @Creditos, @Carrera_Id,@Descripcion_Curso, @Activo_Curso)

        SET @Resultado = scope_identity()
    end
    else
        set @Mensaje = 'El codigo del curso ya existe'
end

create proc sp_EditarCurso(
    @Id_Curso int,
    @Codigo_Curso varchar(100),
    @Nombre_Curso varchar(100),
    @Creditos int,
	@Carrera_Id varchar(100),
	@Descripcion_Curso varchar(100),
    @Activo_Curso bit,
    @Mensaje varchar(500) output,
    @Resultado int output
)
as
begin
    SET @Resultado = 0
    IF NOT EXISTS (SELECT * FROM cursos WHERE Codigo_Curso = @Codigo_Curso and Id_Curso != @Id_Curso)
    begin
        update top (1) cursos set
            Codigo_Curso = @Codigo_Curso,
			Nombre_Curso = @Nombre_Curso,
            Creditos = @Creditos,
			Carrera_Id = @Carrera_Id,
            Descripcion_Curso = @Descripcion_Curso,
            Activo_Curso = @Activo_Curso
        where Id_Curso = @Id_Curso

        SET @Resultado = 1
    end
    else
        set @Mensaje = 'El codigo del codigo ya existe'
end
