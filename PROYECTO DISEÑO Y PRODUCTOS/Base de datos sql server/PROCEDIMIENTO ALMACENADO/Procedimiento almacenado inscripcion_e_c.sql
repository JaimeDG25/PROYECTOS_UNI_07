select * from inscripciones_e_c	
select * from estudiantes
select * from asignacion_d_c
INSERT INTO inscripciones_e_c (Id_Inscripcion, Estudiante_Id, Asignacion_Id)
VALUES 
(8, 8, 38),
(9, 8, 39),
(10, 8, 40);
SELECT 
    i.Id_Inscripcion,
    i.Estudiante_Id,
    e.Nombre_Estudiante,
    e.Apellido_Estudiante,
    a.Curso_Id,
    c.Nombre_Curso,
	c.Descripcion_Curso,
    a.Asistente_Id,
    u.Nombre_Usuario,
    u.Apellido_Usuario,
    i.Asignacion_Id,
    a.Dia_Asignacion,
    a.Horario_Inicio_Asignacion,
    a.Horario_Fin_Asignacion
FROM inscripciones_e_c i
INNER JOIN estudiantes e ON e.Id_Estudiante = i.Estudiante_Id
INNER JOIN asignacion_d_c a ON a.Id_Asignacion = i.Asignacion_Id
INNER JOIN cursos c ON c.Id_Curso = a.Curso_Id
INNER JOIN usuarios u ON u.Id_Usuario = a.Asistente_Id
WHERE i.Estudiante_Id = 7;

SELECT 
    i.Id_Inscripcion,
	i.Estudiante_Id,
    e.Nombre_Estudiante,
    e.Apellido_Estudiante,
    c.Nombre_Curso,
	c.Descripcion_Curso,
    u.Nombre_Usuario,
	i.Asignacion_Id,
    a.Dia_Asignacion,
    a.Horario_Inicio_Asignacion,
    a.Horario_Fin_Asignacion
FROM inscripciones_e_c i
INNER JOIN estudiantes e ON e.Id_Estudiante = i.Estudiante_Id
INNER JOIN asignacion_d_c a ON a.Id_Asignacion = i.Asignacion_Id
INNER JOIN cursos c ON c.Id_Curso = a.Curso_Id
INNER JOIN usuarios u ON u.Id_Usuario = a.Asistente_Id;

sp_help inscripciones_e_c

create proc sp_RegistrarInscripcion_E_C(
    @Estudiante_Id int,
	@Asignacion_Id int,
    @Mensaje varchar(500) output,
    @Resultado int output
)
AS
BEGIN
    SET @Resultado = 0
    BEGIN TRY
        INSERT INTO inscripciones_e_c (
            Estudiante_Id,
            Asignacion_Id
        ) VALUES (
            @Estudiante_Id,
            @Asignacion_Id
        )
        SET @Resultado = SCOPE_IDENTITY()
        SET @Mensaje = 'Inscripcion registrada correctamente.'
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE()
    END CATCH
END