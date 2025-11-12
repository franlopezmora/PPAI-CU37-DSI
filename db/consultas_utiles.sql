-- Consultas útiles para verificar el estado de los sismógrafos

-- 1. Ver el estado actual de todos los sismógrafos
SELECT 
    s.identificador_sismografo,
    s.nro_serie,
    e.nombre_estado as estado_actual,
    s.estacion_id,
    est.nombre_estacion
FROM sismografos s
LEFT JOIN estados e ON s.estado_actual_id = e.id
LEFT JOIN estaciones_sismologicas est ON s.estacion_id = est.codigo_estacion;

-- 2. Ver el historial completo de cambios de estado de un sismógrafo específico
SELECT 
    ce.fecha_hora_inicio,
    ce.fecha_hora_fin,
    e.nombre_estado,
    ce.sismografo_id
FROM cambios_estado ce
JOIN estados e ON ce.estado_id = e.id
WHERE ce.sismografo_id = 'SISM001'  -- Cambiar por el ID del sismógrafo que quieras
ORDER BY ce.fecha_hora_inicio DESC;

-- 3. Ver los cambios de estado activos (sin fecha_hora_fin)
SELECT 
    ce.id,
    ce.fecha_hora_inicio,
    e.nombre_estado,
    ce.sismografo_id,
    s.nro_serie
FROM cambios_estado ce
JOIN estados e ON ce.estado_id = e.id
JOIN sismografos s ON ce.sismografo_id = s.identificador_sismografo
WHERE ce.fecha_hora_fin IS NULL
ORDER BY ce.fecha_hora_inicio DESC;

-- 4. Ver los motivos de fuera de servicio de un cambio de estado específico
SELECT 
    mfs.comentario,
    mt.descripcion as tipo_motivo,
    ce.fecha_hora_inicio,
    ce.sismografo_id
FROM motivos_fuera_servicio mfs
JOIN motivos_tipo mt ON mfs.motivo_tipo_id = mt.id_tipo
JOIN cambios_estado ce ON mfs.cambio_estado_id = ce.id
WHERE ce.sismografo_id = 'SISM001'  -- Cambiar por el ID del sismógrafo
  AND ce.estado_id = (SELECT id FROM estados WHERE nombre_estado = 'Fuera de Servicio')
ORDER BY ce.fecha_hora_inicio DESC;

-- 5. Ver todos los sismógrafos que están fuera de servicio
SELECT 
    s.identificador_sismografo,
    s.nro_serie,
    est.nombre_estacion,
    ce.fecha_hora_inicio as fecha_fuera_servicio,
    COUNT(mfs.id) as cantidad_motivos
FROM sismografos s
JOIN estados e ON s.estado_actual_id = e.id
JOIN estaciones_sismologicas est ON s.estacion_id = est.codigo_estacion
LEFT JOIN cambios_estado ce ON s.identificador_sismografo = ce.sismografo_id 
    AND ce.fecha_hora_fin IS NULL
LEFT JOIN motivos_fuera_servicio mfs ON ce.id = mfs.cambio_estado_id
WHERE e.nombre_estado = 'Fuera de Servicio'
GROUP BY s.identificador_sismografo, s.nro_serie, est.nombre_estacion, ce.fecha_hora_inicio;

-- 6. Ver el último cambio de estado de cada sismógrafo
SELECT 
    s.identificador_sismografo,
    s.nro_serie,
    e.nombre_estado as estado_actual,
    ce.fecha_hora_inicio as fecha_cambio,
    ce.fecha_hora_fin
FROM sismografos s
LEFT JOIN estados e ON s.estado_actual_id = e.id
LEFT JOIN cambios_estado ce ON s.identificador_sismografo = ce.sismografo_id
    AND ce.id = (
        SELECT id FROM cambios_estado 
        WHERE sismografo_id = s.identificador_sismografo 
        ORDER BY fecha_hora_inicio DESC 
        LIMIT 1
    );

