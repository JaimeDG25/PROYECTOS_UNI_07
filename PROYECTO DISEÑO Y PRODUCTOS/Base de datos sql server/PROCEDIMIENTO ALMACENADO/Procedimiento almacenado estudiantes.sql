use DBPORTALUTP;
ALTER TABLE estudiantes
ADD Contraseña_Estudiante varchar(120);

select * from estudiantes
SELECT u.Id_Estudiante, u.Nombre_Estudiante, u.Apellido_Estudiante, 
       u.Correo_Electronico_Estudiante, u.Contraseña_Estudiante, 
       c.Id_Carrera, c.Nombre_Carrera
FROM estudiantes u
         INNER JOIN carreras c ON u.Carrera_Id = c.Id_Carrera


select DNI_Estudiante from estudiantes
create proc sp_RegistrarEstudiante(
    @Nombre_Estudiante varchar(100),
    @Apellido_Estudiante varchar(100),
    @Correo_Electronico_Estudiante varchar(100),
	@Carrera_Id varchar(100),
    @DNI_Estudiante int,
	@Contraseña_Estudiante varchar(100),
    @Mensaje varchar(500) output,
    @Resultado int output
)
as
begin
    SET @Resultado = 0
    IF NOT EXISTS (SELECT * FROM estudiantes WHERE Correo_Electronico_Estudiante = @Correo_Electronico_Estudiante)
    begin
        insert into estudiantes(Nombre_Estudiante, Apellido_Estudiante, Correo_Electronico_Estudiante,Carrera_Id,DNI_Estudiante,Contraseña_Estudiante) 
        values (@Nombre_Estudiante, @Apellido_Estudiante, @Correo_Electronico_Estudiante,@Carrera_Id, @DNI_Estudiante, @Contraseña_Estudiante)

        SET @Resultado = scope_identity()
    end
    else
        set @Mensaje = 'El correo del estudiante ya existe'
end

create proc sp_EditarEstudiante(
    @Id_Estudiante int,
    @Nombre_Estudiante varchar(100),
    @Apellido_Estudiante varchar(100),
    @Correo_Electronico_Estudiante varchar(100),
	@Carrera_Id varchar(100),
    @DNI_Estudiante int,
    @Mensaje varchar(500) output,
    @Resultado int output
)
as
begin
    SET @Resultado = 0
    IF NOT EXISTS (SELECT * FROM estudiantes WHERE Correo_Electronico_Estudiante = @Correo_Electronico_Estudiante and Id_Estudiante != @Id_Estudiante and DNI_Estudiante = @DNI_Estudiante)
    begin
        update top (1) estudiantes set
            Nombre_Estudiante = @Nombre_Estudiante,
            Apellido_Estudiante = @Apellido_Estudiante,
            Correo_Electronico_Estudiante = @Correo_Electronico_Estudiante,
			DNI_Estudiante = @DNI_Estudiante,
			Carrera_Id = @Carrera_Id
        where Id_Estudiante = @Id_Estudiante

        SET @Resultado = 1
    end
    else
        set @Mensaje = 'El correo o el dni del estudiante ya existe'
end

INSERT INTO estudiantes (
    Nombre_Estudiante, 
    Apellido_Estudiante, 
    Correo_Electronico_Estudiante,
    Carrera_Id,
    DNI_Estudiante,
    Contraseña_Estudiante
) 
VALUES (
    'Luis', 
    'Ramírez', 
    'luis.ramirez@example.com', 
    1, 
    12345678, 
    'luis123'
),
(
    'Ana', 
    'Gómez', 
    'ana.gomez@example.com', 
    2, 
    87654321, 
    'ana456'
),
(
    'Carlos', 
    'Martínez', 
    'carlos.martinez@example.com', 
    3, 
    11223344, 
    'carlos789'
);