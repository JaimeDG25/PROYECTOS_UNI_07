	create database DBPORTALUTP
	use DBPORTALUTP

-- Crear tabla de roles
CREATE TABLE roles (
    Id_Rol int primary key identity,
    Roles_Descripcion VARCHAR(100) not null
);

-- Insertar roles de ejemplo
INSERT INTO roles (Roles) VALUES ('administrador'), ('profesor');

-- Crear tabla de carreras
CREATE TABLE carreras (
    Id_Carrera int primary key identity,
    Nombre_Carrera VARCHAR(255) not null
);

-- Crear tabla de estudiantes
CREATE TABLE estudiantes (
    Id_Estudiante int primary key identity,
    Nombre_Estudiante VARCHAR(100) not null,
    Apellido_Estudiante VARCHAR(100) not null,
    Correo_Electronico_Estudiante VARCHAR(255) unique not null,
    Inscripcion int not null,
    Carrera_Id int references carreras(Id_Carrera)
);

-- Crear tabla de cursos
CREATE TABLE cursos (
    Id_Curso int primary key identity,
    Codigo_Curso VARCHAR(20) unique not null,
    Nombre_Curso VARCHAR(255) not null,
    Creditos int not null
);

-- Crear tabla de inscripciones
CREATE TABLE inscripciones (
    Id_Inscripcion int primary key identity,
    Estudiante_Id int references estudiantes(Id_Estudiante),
    Curso_Id int references cursos(Id_Curso),
    Fecha_Inscripcion date not null
);

-- Crear tabla de usuarios
CREATE TABLE usuarios (
    Id_Usuario int primary key identity,
    Nombre_Usuario VARCHAR(100) not null,
    Apellido_Usuario VARCHAR(100) not null,
    Correo_electronico_Usuario VARCHAR(255) unique not null,
    Rol_Id int references roles(Id_Rol),
    Clave VARCHAR(255) not null,
	Activo bit default 1
);
INSERT INTO usuarios (Nombre_Usuario, Apellido_Usuario, Correo_electronico_Usuario, Rol_Id, Clave, Activo)
VALUES ('testuser', 'testfull', 'test@gmail.com', 1, 'testpassword',1);

-- Crear tabla de asignaciones de cursos
CREATE TABLE asignaciones_cursos (
    Id_Asignacion int primary key identity,
    Curso_Id int references cursos(Id_Curso),
    Profesor_Id int references usuarios(Id_Usuario),
    Semestre VARCHAR(50) not null
);

select * from roles