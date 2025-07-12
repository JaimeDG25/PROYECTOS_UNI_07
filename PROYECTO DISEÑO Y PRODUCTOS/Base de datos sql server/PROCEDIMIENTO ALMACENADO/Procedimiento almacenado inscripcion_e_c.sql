SELECT * FROM inscripciones_e_c
select * from estudiantes
select * from asignacion_d_c

SELECT Id_Inscripcion,
Nombre_Estudiante,Apellido_Estudiante,
Nombre_Usuario AS Nombre_Asistente,
Dia_Asignacion,Horario_Inicio_Asignacion,Horario_Fin_Asignacion,
c.Nombre_Curso,c.Descripcion_Curso
FROM inscripciones_e_c i
inner join estudiantes e On e.Id_Estudiante=i.Estudiante_Id
inner join asignacion_d_c a ON  a.Id_Asignacion=i.Asignacion_Id
INNER JOIN cursos c ON c.Id_Curso = a.Curso_Id
inner join usuarios u ON u.Id_Usuario=a.Asistente_Id

SELECT 
    i.Id_Inscripcion,
	i.Estudiante_Id,
    e.Nombre_Estudiante,
    e.Apellido_Estudiante,
    c.Nombre_Curso,
	c.Descripcion_Curso,
    u.Nombre_Usuario,
	i.Asignacion_I,
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