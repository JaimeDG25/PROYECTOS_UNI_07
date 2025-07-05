--EMPEZANDO A AHCER MI BASE DE DATOS PARA EL PROYECTO DE DISEÑO DE PRODUCTOS Y SERVICIOS
create database DBPORTALUTPMAS
use DBPORTALUTPMAS

-- Crear tabla de roles
CREATE TABLE roles (
    Id_Rol int primary key identity,
    Roles_Descripcion VARCHAR(100) not null
); select * from roles
-- Insertar roles de ejemplo
INSERT INTO roles (Roles_Descripcion) VALUES ('Administrador'), ('Asistente');

-- Crear tabla de carreras
CREATE TABLE carreras (
    Id_Carrera int primary key identity,
    Nombre_Carrera VARCHAR(255) not null
);
-- Tabla usuarios
CREATE TABLE usuarios (
    Id_Usuario INT PRIMARY KEY,
    Nombre_Usuario VARCHAR(100),
    Apellido_Usuario VARCHAR(100),
    Correo_electronico_Usuario VARCHAR(100),
    Rol_Id INT,
    Clave VARCHAR(100),
    Activo BIT,
    FOREIGN KEY (Rol_Id) REFERENCES roles(Id_Rol)
);
-- Tabla estudiantes
CREATE TABLE estudiantes (
    Id_Estudiante INT PRIMARY KEY,
    Nombre_Estudiante VARCHAR(100),
    Apellido_Estudiante VARCHAR(100),
    Correo_Electronico_Estudiante VARCHAR(100),
    Carrera_Id INT,
    DNL_Estudiante VARCHAR(50),
    Inscripcion_Estudiante DATE,
    Contraseña_Estudiante VARCHAR(100),
    FOREIGN KEY (Carrera_Id) REFERENCES carreras(Id_Carrera)
);
-- Tabla cursos
CREATE TABLE cursos (
    Id_Curso INT PRIMARY KEY,
    Codigo_Curso VARCHAR(50),
    Nombre_Curso VARCHAR(100),
    Creditos INT,
    Carrera_Id INT,
    Descripcion_Curso TEXT,
    Activo_Curso BIT,
    FOREIGN KEY (Carrera_Id) REFERENCES carreras(Id_Carrera)
);
-- Tabla inscripciones
CREATE TABLE inscripciones (
    Id_Inscripcion INT PRIMARY KEY,
    Estudiante_Id INT,
    Curso_Id INT,
    Fecha_Inscripcion DATE,
    FOREIGN KEY (Estudiante_Id) REFERENCES estudiantes(Id_Estudiante),
    FOREIGN KEY (Curso_Id) REFERENCES cursos(Id_Curso)
);

-- Tabla asignacion_d_c
CREATE TABLE asignacion_d_c (
    Id_Asignacion INT PRIMARY KEY,
    Curso_Id INT,
    Asistente_Id INT,
    FOREIGN KEY (Curso_Id) REFERENCES cursos(Id_Curso),
    FOREIGN KEY (Asistente_Id) REFERENCES usuarios(Id_Usuario)
);