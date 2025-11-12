# Consultas SQL - Sistema de Gestión de Estaciones Sismológicas

## Resumen

Este documento explica las consultas SQL utilizadas para poner un sismógrafo fuera de servicio y las operaciones relacionadas.

## Flujo: Poner Sismógrafo Fuera de Servicio

### 1. Cargar Datos Iniciales

Al iniciar la aplicación, se cargan todos los datos desde la base de datos:

```sql
-- Cargar Estados
SELECT id, nombre_estado, ambito FROM estados;

-- Cargar Sismógrafos con su estado actual
SELECT s.identificador_sismografo, s.nro_serie, s.fecha_adquisicion, 
       s.estacion_id, s.estado_actual_id, e.nombre_estado as estado_nombre
FROM sismografos s
LEFT JOIN estados e ON s.estado_actual_id = e.id;

-- Cargar Historial de Cambios de Estado
SELECT ce.id, ce.fecha_hora_inicio, ce.fecha_hora_fin, ce.estado_id, e.nombre_estado
FROM cambios_estado ce
JOIN estados e ON ce.estado_id = e.id
WHERE ce.sismografo_id = @sismografo_id
ORDER BY ce.fecha_hora_inicio DESC;
```

### 2. Operación: Poner Sismógrafo Fuera de Servicio

Cuando se ejecuta la operación de poner un sismógrafo fuera de servicio, se realizan las siguientes operaciones en una **transacción**:

#### Paso 1: Finalizar el cambio de estado actual (si existe)

```sql
UPDATE cambios_estado 
SET fecha_hora_fin = @fecha_hora
WHERE sismografo_id = @sismografo_id 
  AND fecha_hora_fin IS NULL;
```

**Explicación**: Cierra el cambio de estado activo (donde `fecha_hora_fin IS NULL`) estableciendo la fecha de finalización.

#### Paso 2: Crear nuevo cambio de estado

```sql
INSERT INTO cambios_estado (fecha_hora_inicio, fecha_hora_fin, estado_id, sismografo_id)
VALUES (@fecha_hora_inicio, NULL, @estado_id, @sismografo_id);
SELECT last_insert_rowid();
```

**Explicación**: 
- Crea un nuevo registro en `cambios_estado` con el estado "Fuera de Servicio"
- `fecha_hora_fin` es NULL porque es el estado actual
- Retorna el ID del cambio de estado creado para asociar los motivos

#### Paso 3: Insertar motivos de fuera de servicio

```sql
INSERT INTO motivos_fuera_servicio (cambio_estado_id, motivo_tipo_id, comentario)
VALUES (@cambio_estado_id, @motivo_tipo_id, @comentario);
```

**Explicación**: 
- Se ejecuta una vez por cada motivo seleccionado
- Relaciona el cambio de estado con los tipos de motivo y sus comentarios

#### Paso 4: Actualizar estado actual del sismógrafo

```sql
UPDATE sismografos 
SET estado_actual_id = @estado_id
WHERE identificador_sismografo = @sismografo_id;
```

**Explicación**: Actualiza la referencia al estado actual en la tabla `sismografos` para consultas rápidas.

### 3. Operación: Cerrar Orden de Inspección

```sql
UPDATE ordenes_inspeccion 
SET fecha_hora_cierre = @fecha_hora_cierre,
    observacion_cierre = @observacion_cierre,
    estado_id = @estado_id
WHERE numero_orden = @numero_orden;
```

**Explicación**: Actualiza la orden de inspección con la fecha de cierre, observación y estado "Cerrada".

## Estructura de Datos

### Tabla: `cambios_estado`
Almacena el historial de cambios de estado de cada sismógrafo.

- `id`: Identificador único
- `fecha_hora_inicio`: Cuándo comenzó este estado
- `fecha_hora_fin`: Cuándo terminó (NULL = estado actual)
- `estado_id`: Referencia al estado
- `sismografo_id`: Referencia al sismógrafo

### Tabla: `motivos_fuera_servicio`
Almacena los motivos asociados a un cambio de estado.

- `id`: Identificador único
- `cambio_estado_id`: Referencia al cambio de estado
- `motivo_tipo_id`: Referencia al tipo de motivo
- `comentario`: Comentario adicional del motivo

### Tabla: `sismografos`
Almacena los sismógrafos con su estado actual.

- `identificador_sismografo`: PK
- `estado_actual_id`: Referencia al estado actual (para consultas rápidas)

## Ventajas de este Diseño

1. **Historial Completo**: Se mantiene un registro de todos los cambios de estado
2. **Transacciones**: Todas las operaciones se realizan en una transacción para garantizar consistencia
3. **Consultas Rápidas**: El campo `estado_actual_id` en `sismografos` permite consultas rápidas sin necesidad de buscar en el historial
4. **Integridad Referencial**: Las claves foráneas garantizan que los datos sean consistentes

## Ejemplo de Uso en Código

```csharp
// En Sismografo.cs
public string ponerSismografoFueraDeServicio(
    DateTime fechaHora, 
    List<Tuple<string, MotivoTipo>> listaMotivosTipoComentario, 
    Estado estadoFueraServicio)
{
    // Persistir en base de datos (transacción automática)
    DataAccess.PonerSismografoFueraDeServicio(
        identificadorSismografo,
        fechaHora,
        listaMotivosTipoComentario,
        estadoFueraServicio
    );
    
    // Actualizar en memoria
    estadoActual = estadoFueraServicio;
    // ...
}
```

## Notas Importantes

- Todas las operaciones de escritura se realizan dentro de transacciones para garantizar atomicidad
- El estado actual se mantiene tanto en `sismografos.estado_actual_id` como en el último registro de `cambios_estado` con `fecha_hora_fin IS NULL`
- Los motivos pueden ser múltiples por cada cambio de estado

