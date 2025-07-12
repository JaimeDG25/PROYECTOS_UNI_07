SELECT * FROM asignacion_d_c
sp_help asignacion_d_c
EXEC sp_rename 'asignacion_d_c.Profesor_Id', 'Asistente_Id', 'COLUMN';
EXEC sp_rename 'asignaciones_cursos', 'asignacion_d_c';
delete asignacion_d_c where Id_Asignacion between 1 and 27

select a.Id_Asignacion, 
a.Asistente_Id,
u.Nombre_Usuario,u.Apellido_Usuario, 
a.Curso_Id, 
c.Nombre_Curso,c.Descripcion_Curso, 
c.Creditos,c.Codigo_Curso,
a.Dia_Asignacion,
a.Horario_inicio_asignacion,
a.horario_fin_asignacion
from asignacion_d_c a
inner join cursos c ON c.Id_Curso=a.Curso_Id
inner join usuarios u ON u.Id_Usuario=a.Asistente_Id 


CREATE PROC sp_RegistrarAsignacion_d_c (
    @Curso_Id INT,
    @Asistente_Id INT,
    @Dia_Asignacion VARCHAR(100),
    @Horario_Inicio_Asignacion TIME,
    @Horario_Fin_Asignacion TIME,
    @Mensaje VARCHAR(500) OUTPUT,
    @Resultado INT OUTPUT
)
AS
BEGIN
    SET @Resultado = 0

    BEGIN TRY
        INSERT INTO asignacion_d_c (
            Curso_Id,
            Asistente_Id,
            Dia_Asignacion,
            Horario_Inicio_Asignacion,
            Horario_Fin_Asignacion
        )
        VALUES (
            @Curso_Id,
            @Asistente_Id,
            @Dia_Asignacion,
            @Horario_Inicio_Asignacion,
            @Horario_Fin_Asignacion
        )

        SET @Resultado = SCOPE_IDENTITY()
        SET @Mensaje = 'Asignación registrada correctamente.'
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE()
    END CATCH
END

create proc sp_EditarAsignacion_d_c(
    @Id_Asignacion int,
    @Curso_Id varchar(100),
    @Asistente_Id varchar(100),
	@dia_asignacion varchar(100),
	@hora_inicio TIME,
    @hora_fin TIME,
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

