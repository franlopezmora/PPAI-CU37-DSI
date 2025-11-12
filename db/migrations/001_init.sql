-- Migración 001: Inicialización
-- Este es un ejemplo de migración incremental
-- Las migraciones se aplican en orden numérico (001, 002, 003, ...)

-- Ejemplo: Agregar una columna a una tabla existente
-- ALTER TABLE alumnos ADD COLUMN telefono TEXT;

-- Ejemplo: Crear una nueva tabla
-- CREATE TABLE IF NOT EXISTS notas (
--     id INTEGER PRIMARY KEY AUTOINCREMENT,
--     alumno_id INTEGER NOT NULL,
--     curso_id INTEGER NOT NULL,
--     nota REAL NOT NULL,
--     fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
--     FOREIGN KEY (alumno_id) REFERENCES alumnos(id),
--     FOREIGN KEY (curso_id) REFERENCES cursos(id)
-- );

-- Esta migración está vacía por defecto, pero puedes agregar cambios aquí
-- cuando necesites modificar el esquema después de la versión inicial

