SELECT * FROM asignacion_d_c
sp_help asignacion_d_c;
EXEC sp_rename 'asignacion_d_c.Profesor_Id', 'Asistente_Id', 'COLUMN';
EXEC sp_rename 'asignaciones_cursos', 'asignacion_d_c';
select a.Id_Asignacion, a.Asistente_Id,u.Nombre_Usuario+' '+u.Apellido_Usuario as NombreCompleto, 
a.Curso_Id, c.Nombre_Curso,c.Descripcion_Curso, 
c.Creditos,c.Codigo_Curso
from asignacion_d_c a
inner join cursos c ON c.Id_Curso=a.Curso_Id
inner join usuarios u ON u.Id_Usuario=a.Asistente_Id 

select * from cursos
create proc sp_RegistrarAsignacion_d_c(
    @Curso_Id varchar(100),
    @Asistente_Id varchar(100),
    @Mensaje varchar(500) output,
    @Resultado int output
)
as
begin
    SET @Resultado = 0
    begin
        insert into asignacion_d_c(Curso_Id, Asistente_Id) 
        values (@Curso_Id, @Asistente_Id)

        SET @Resultado = scope_identity()
    end
end

create proc sp_EditarAsignacion_d_c(
    @Id_Asignacion int,
    @Curso_Id varchar(100),
    @Asistente_Id varchar(100),
    @Mensaje varchar(500) output,
    @Resultado int output
)
as
begin
    SET @Resultado = 0
    IF NOT EXISTS (SELECT * FROM asignacion_d_c WHERE Id_Asignacion != @Id_Asignacion)
    begin
        update top (1) asignacion_d_c set
            Curso_Id = @Curso_Id,
            Asistente_Id = @Asistente_Id
        where Id_Asignacion = @Id_Asignacion

        SET @Resultado = 1
    end
    else
        set @Mensaje = 'La asignacion ya existe'
end

