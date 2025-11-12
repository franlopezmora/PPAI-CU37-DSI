-- Schema para el sistema de gestión de estaciones sismológicas
-- Este archivo contiene la estructura completa de las tablas

-- Tabla: Roles
CREATE TABLE IF NOT EXISTS roles (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nombre_rol TEXT NOT NULL UNIQUE,
    descripcion_rol TEXT
);

-- Tabla: Empleados
CREATE TABLE IF NOT EXISTS empleados (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nombre TEXT NOT NULL,
    apellido TEXT NOT NULL,
    mail TEXT NOT NULL,
    telefono TEXT,
    rol_id INTEGER,
    FOREIGN KEY (rol_id) REFERENCES roles(id)
);

-- Tabla: Usuarios
CREATE TABLE IF NOT EXISTS usuarios (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nombre_usuario TEXT NOT NULL UNIQUE,
    contrasena TEXT NOT NULL,
    empleado_id INTEGER NOT NULL,
    FOREIGN KEY (empleado_id) REFERENCES empleados(id)
);

-- Tabla: Estaciones Sismológicas
CREATE TABLE IF NOT EXISTS estaciones_sismologicas (
    codigo_estacion TEXT PRIMARY KEY,
    nombre_estacion TEXT NOT NULL,
    latitud REAL,
    longitud REAL
);

-- Tabla: Sismógrafos
CREATE TABLE IF NOT EXISTS sismografos (
    identificador_sismografo TEXT PRIMARY KEY,
    nro_serie TEXT NOT NULL,
    fecha_adquisicion DATETIME NOT NULL,
    estacion_id TEXT NOT NULL,
    estado_actual_id INTEGER NOT NULL,
    FOREIGN KEY (estacion_id) REFERENCES estaciones_sismologicas(codigo_estacion)
    FOREIGN KEY (estado_actual_id) REFERENCES estados(id)
);

-- Tabla: Estados (para almacenar los diferentes estados del sistema)
-- Nota: Los estados son clases abstractas en el código, aquí almacenamos el nombre
CREATE TABLE IF NOT EXISTS estados (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nombre_estado TEXT NOT NULL UNIQUE,
    ambito TEXT NOT NULL CHECK(ambito IN ('OrdenInspeccion', 'Sismografo'))
);

-- Tabla: Órdenes de Inspección
CREATE TABLE IF NOT EXISTS ordenes_inspeccion (
    numero_orden INTEGER PRIMARY KEY,
    fecha_hora_inicio DATETIME NOT NULL,
    fecha_hora_finalizacion DATETIME,
    fecha_hora_cierre DATETIME,
    observacion_cierre TEXT,
    estado_id INTEGER,
    empleado_id INTEGER NOT NULL,
    estacion_id TEXT NOT NULL,
    FOREIGN KEY (estado_id) REFERENCES estados(id),
    FOREIGN KEY (empleado_id) REFERENCES empleados(id),
    FOREIGN KEY (estacion_id) REFERENCES estaciones_sismologicas(codigo_estacion)
);

-- Tabla: Cambios de Estado (historial de estados de sismógrafos)
CREATE TABLE IF NOT EXISTS cambios_estado (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    fecha_hora_inicio DATETIME NOT NULL,
    fecha_hora_fin DATETIME,
    estado_id INTEGER NOT NULL,
    sismografo_id TEXT NOT NULL,
    FOREIGN KEY (estado_id) REFERENCES estados(id),
    FOREIGN KEY (sismografo_id) REFERENCES sismografos(identificador_sismografo)
);

-- Tabla: Tipos de Motivo
CREATE TABLE IF NOT EXISTS motivos_tipo (
    id_tipo INTEGER PRIMARY KEY,
    descripcion TEXT NOT NULL
);

-- Tabla: Motivos de Fuera de Servicio (relación entre CambioEstado y MotivoTipo)
CREATE TABLE IF NOT EXISTS motivos_fuera_servicio (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    cambio_estado_id INTEGER NOT NULL,
    motivo_tipo_id INTEGER NOT NULL,
    comentario TEXT,
    FOREIGN KEY (cambio_estado_id) REFERENCES cambios_estado(id),
    FOREIGN KEY (motivo_tipo_id) REFERENCES motivos_tipo(id_tipo)
);

-- Índices para mejorar el rendimiento
CREATE INDEX IF NOT EXISTS idx_empleados_rol ON empleados(rol_id);
CREATE INDEX IF NOT EXISTS idx_usuarios_empleado ON usuarios(empleado_id);
CREATE INDEX IF NOT EXISTS idx_sismografos_estacion ON sismografos(estacion_id);
CREATE INDEX IF NOT EXISTS idx_ordenes_empleado ON ordenes_inspeccion(empleado_id);
CREATE INDEX IF NOT EXISTS idx_ordenes_estacion ON ordenes_inspeccion(estacion_id);
CREATE INDEX IF NOT EXISTS idx_ordenes_estado ON ordenes_inspeccion(estado_id);
CREATE INDEX IF NOT EXISTS idx_cambios_estado_sismografo ON cambios_estado(sismografo_id);
CREATE INDEX IF NOT EXISTS idx_cambios_estado_estado ON cambios_estado(estado_id);
CREATE INDEX IF NOT EXISTS idx_cambios_estado_fecha ON cambios_estado(fecha_hora_inicio);
CREATE INDEX IF NOT EXISTS idx_motivos_cambio_estado ON motivos_fuera_servicio(cambio_estado_id);
CREATE INDEX IF NOT EXISTS idx_motivos_tipo ON motivos_fuera_servicio(motivo_tipo_id);
CREATE INDEX IF NOT EXISTS idx_estados_ambito ON estados(ambito);
