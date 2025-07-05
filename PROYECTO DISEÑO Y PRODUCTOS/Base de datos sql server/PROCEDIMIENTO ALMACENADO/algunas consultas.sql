select * from asignaciones_cursos
select * from usuarios
select * from cursos
SELECT TOP 1 
    cu.Carrera_Id, 
    ca.Nombre_Carrera, 
    COUNT(*) AS TotalCursos
FROM cursos cu
INNER JOIN carreras ca ON cu.Carrera_Id = ca.Id_Carrera
GROUP BY cu.Carrera_Id, ca.Nombre_Carrera
ORDER BY TotalCursos DESC;

insert into asignaciones_cursos values(12,21,02)

ALTER TABLE asignaciones_cursos
DROP COLUMN Semestre;


select a.Id_Asignacion, a.Asistente_Id,u.Nombre_Usuario+' '+u.Apellido_Usuario as NombreCompleto, 
a.Curso_Id, c.Nombre_Curso,c.Descripcion_Curso, 
c.Creditos,c.Codigo_Curso
from asignacion_d_c a
inner join cursos c ON c.Id_Curso=a.Curso_Id
inner join usuarios u ON u.Id_Usuario=a.Asistente_Id 