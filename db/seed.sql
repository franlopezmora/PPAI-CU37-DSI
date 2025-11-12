-- Datos semilla para el sistema de gestión de estaciones sismológicas
-- Este archivo se ejecuta después de schema.sql si existe

-- Insertar Roles
INSERT OR IGNORE INTO roles (id, nombre_rol, descripcion_rol) VALUES
    (1, 'Inspector', 'Realiza inspecciones'),
    (2, 'Responsable de Reparaciones', 'Repara sismógrafos');

-- Insertar Estados
INSERT OR IGNORE INTO estados (nombre_estado, ambito) VALUES
    ('Pendiente de Inspección', 'OrdenInspeccion'),
    ('Completamente Realizada', 'OrdenInspeccion'),
    ('Cerrada', 'OrdenInspeccion'),
    ('Fuera de Servicio', 'Sismografo'),
    ('Operativo', 'Sismografo'),
    ('Inhabilitado por Inspección', 'Sismografo');

-- Insertar Empleados
INSERT OR IGNORE INTO empleados (id, nombre, apellido, mail, telefono, rol_id) VALUES
    (1, 'Juan', 'Perez', 'juan.perez@example.com', '123456', 2),
    (2, 'Ana', 'Lopez', 'ana.lopez@example.com', '789012', 2);

-- Insertar Usuarios
INSERT OR IGNORE INTO usuarios (id, nombre_usuario, contrasena, empleado_id) VALUES
    (1, 'jperez', '123', 1),
    (2, 'alopez', '456', 2);

-- Insertar Estaciones Sismológicas
INSERT OR IGNORE INTO estaciones_sismologicas (codigo_estacion, nombre_estacion, latitud, longitud) VALUES
    ('EST001', 'Central Cordoba', -31.4201, -64.1888);

-- Insertar Sismógrafos (con estado Inhabilitado por Inspección - id 6)
INSERT OR IGNORE INTO sismografos (identificador_sismografo, nro_serie, fecha_adquisicion, estacion_id, estado_actual_id) VALUES
    ('SISM001', 'SN111', datetime('now', '-2 years'), 'EST001', 6),
    ('SISM002', 'SN222', datetime('now', '-1 year'), 'EST001', 6);

-- Insertar Tipos de Motivo
INSERT OR IGNORE INTO motivos_tipo (id_tipo, descripcion) VALUES
    (1, 'Falla de sensor'),
    (2, 'Problema de alimentación'),
    (3, 'Mantenimiento programado'),
    (4, 'Otro');

-- Insertar Órdenes de Inspección de ejemplo
-- Nota: Los IDs de estado deben coincidir con los insertados arriba
INSERT OR IGNORE INTO ordenes_inspeccion (numero_orden, fecha_hora_inicio, fecha_hora_finalizacion, fecha_hora_cierre, observacion_cierre, estado_id, empleado_id, estacion_id) VALUES
    (101, datetime('now', '-10 days'), datetime('now', '-8 days'), NULL, NULL, 2, 1, 'EST001'),
    (102, datetime('now', '-5 days'), datetime('now', '-3 days'), NULL, NULL, 2, 1, 'EST001'),
    (103, datetime('now', '-2 days'), NULL, NULL, NULL, 1, 1, 'EST001'),
    (104, datetime('now', '-15 days'), datetime('now', '-12 days'), NULL, NULL, 2, 1, 'EST001');

-- Insertar Cambios de Estado iniciales para los sismógrafos (estado Inhabilitado por Inspeccion)
INSERT OR IGNORE INTO cambios_estado (id, fecha_hora_inicio, fecha_hora_fin, estado_id, sismografo_id) VALUES
    (1, datetime('now', '-2 years'), NULL, 6, 'SISM001'),  -- SISM001 inhabilitado por inspeccion desde hace 2 años
    (2, datetime('now', '-1 year'), NULL, 6, 'SISM002');     -- SISM002 inhabilitado por inspeccion desde hace 1 año
