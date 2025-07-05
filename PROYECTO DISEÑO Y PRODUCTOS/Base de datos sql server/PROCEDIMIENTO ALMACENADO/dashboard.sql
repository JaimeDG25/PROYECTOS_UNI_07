USE DBPORTALUTP
(select count(*) from usuarios) [TotalUsuarios],
(select count(*) from cursos) [TotalCursos],
(select count(*) from estudiantes) [TotalEstudiantes],
CREATE PROC sp_Total_Dashboard
AS
BEGIN
    SELECT 
        (SELECT COUNT(*) FROM usuarios) AS TotalUsuarios,
        (SELECT COUNT(*) FROM cursos) AS TotalCursos,
        (SELECT COUNT(*) FROM estudiantes) AS TotalEstudiantes
END