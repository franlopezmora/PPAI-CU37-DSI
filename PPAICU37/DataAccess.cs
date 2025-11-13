using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;

namespace PPAICU37
{
    public static class DataAccess
    {
        private static string GetConnectionString()
        {
            return DbInit.GetConnectionString();
        }

        // ========== CARGAR DATOS ==========

        public static List<Estado> CargarEstados()
        {
            var estados = new List<Estado>();
            using var con = new SqliteConnection(GetConnectionString());
            con.Open();

            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT id, nombre_estado, ambito FROM estados";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var nombreEstado = reader.GetString("nombre_estado");
                var ambito = reader.GetString("ambito");

                Estado estado = nombreEstado switch
                {
                    "Fuera de Servicio" => new FueraDeServicio(),
                    "Inhabilitado por Inspección" => new InhabilitadoPorInspeccion(),
                    _ => CrearEstadoGenerico(nombreEstado, ambito)
                };

                estados.Add(estado);
            }

            return estados;
        }

        private static Estado CrearEstadoGenerico(string nombreEstado, string ambito)
        {
            // Para estados que aún no tienen clase específica, creamos una clase genérica
            return new EstadoGenerico(nombreEstado, ambito);
        }

        public static List<Rol> CargarRoles()
        {
            var roles = new List<Rol>();
            using var con = new SqliteConnection(GetConnectionString());
            con.Open();

            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT id, nombre_rol, descripcion_rol FROM roles";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                roles.Add(new Rol
                {
                    NombreRol = reader.GetString("nombre_rol"),
                    DescripcionRol = reader.IsDBNull("descripcion_rol") ? null : reader.GetString("descripcion_rol")
                });
            }

            return roles;
        }

        public static List<Empleado> CargarEmpleados(List<Rol> roles)
        {
            var empleados = new List<Empleado>();
            using var con = new SqliteConnection(GetConnectionString());
            con.Open();

            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
                SELECT e.id, e.nombre, e.apellido, e.mail, e.telefono, e.rol_id, r.nombre_rol, r.descripcion_rol
                FROM empleados e
                LEFT JOIN roles r ON e.rol_id = r.id";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var empleado = new Empleado
                {
                    nombre = reader.GetString("nombre"),
                    apellido = reader.GetString("apellido"),
                    mail = reader.GetString("mail"),
                    telefono = reader.IsDBNull("telefono") ? null : reader.GetString("telefono")
                };

                if (!reader.IsDBNull("rol_id"))
                {
                    var rolId = reader.GetInt32("rol_id");
                    empleado.Rol = roles.FirstOrDefault(r => r.NombreRol == reader.GetString("nombre_rol"));
                    if (empleado.Rol == null)
                    {
                        empleado.Rol = new Rol
                        {
                            NombreRol = reader.GetString("nombre_rol"),
                            DescripcionRol = reader.IsDBNull("descripcion_rol") ? null : reader.GetString("descripcion_rol")
                        };
                    }
                }

                empleados.Add(empleado);
            }

            return empleados;
        }

        public static List<Usuario> CargarUsuarios(List<Empleado> empleados)
        {
            var usuarios = new List<Usuario>();
            using var con = new SqliteConnection(GetConnectionString());
            con.Open();

            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
                SELECT u.id, u.nombre_usuario, u.contrasena, u.empleado_id, 
                       e.nombre, e.apellido, e.mail, e.telefono
                FROM usuarios u
                JOIN empleados e ON u.empleado_id = e.id";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var empleadoId = reader.GetInt32("empleado_id");
                var empleado = empleados.FirstOrDefault(emp => 
                    emp.nombre == reader.GetString("nombre") && 
                    emp.apellido == reader.GetString("apellido"));

                if (empleado != null)
                {
                    usuarios.Add(new Usuario
                    {
                        nombreUsuario = reader.GetString("nombre_usuario"),
                        contrasena = reader.GetString("contrasena"),
                        Empleado = empleado
                    });
                }
            }

            return usuarios;
        }

        public static List<EstacionSismologica> CargarEstaciones()
        {
            var estaciones = new List<EstacionSismologica>();
            using var con = new SqliteConnection(GetConnectionString());
            con.Open();

            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT codigo_estacion, nombre_estacion, latitud, longitud FROM estaciones_sismologicas";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                estaciones.Add(new EstacionSismologica
                {
                    codigoEstacion = reader.GetString("codigo_estacion"),
                    nombreEstacion = reader.GetString("nombre_estacion"),
                    latitud = reader.IsDBNull("latitud") ? 0 : reader.GetDouble("latitud"),
                    longitud = reader.IsDBNull("longitud") ? 0 : reader.GetDouble("longitud")
                });
            }

            return estaciones;
        }

        public static List<Sismografo> CargarSismografos(List<EstacionSismologica> estaciones, List<Estado> estados)
        {
            var sismografos = new List<Sismografo>();
            using var con = new SqliteConnection(GetConnectionString());
            con.Open();

            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
                SELECT s.identificador_sismografo, s.nro_serie, s.fecha_adquisicion, 
                       s.estacion_id, s.estado_actual_id, e.nombre_estado as estado_nombre
                FROM sismografos s
                LEFT JOIN estados e ON s.estado_actual_id = e.id";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var estacionId = reader.GetString("estacion_id");
                var estacion = estaciones.FirstOrDefault(e => e.codigoEstacion == estacionId);

                var sismografo = new Sismografo
                {
                    identificadorSismografo = reader.GetString("identificador_sismografo"),
                    nroSerie = reader.GetString("nro_serie"),
                    fechaAdquisicion = reader.GetDateTime("fecha_adquisicion"),
                    EstacionSismologica = estacion
                };

                // Cargar estado actual
                if (!reader.IsDBNull("estado_nombre"))
                {
                    var estadoNombre = reader.GetString("estado_nombre");
                    sismografo.estadoActual = estados.FirstOrDefault(e => e.nombreEstado == estadoNombre);
                }

                // Cargar historial de cambios de estado
                sismografo.CambioEstado = CargarCambiosEstado(sismografo.identificadorSismografo, estados);

                sismografos.Add(sismografo);
            }

            return sismografos;
        }

        public static List<CambioEstado> CargarCambiosEstado(string sismografoId, List<Estado> estados)
        {
            var cambios = new List<CambioEstado>();
            using var con = new SqliteConnection(GetConnectionString());
            con.Open();

            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
                SELECT ce.id, ce.fecha_hora_inicio, ce.fecha_hora_fin, ce.estado_id, e.nombre_estado
                FROM cambios_estado ce
                JOIN estados e ON ce.estado_id = e.id
                WHERE ce.sismografo_id = @sismografo_id
                ORDER BY ce.fecha_hora_inicio DESC";
            cmd.Parameters.AddWithValue("@sismografo_id", sismografoId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var estadoNombre = reader.GetString("nombre_estado");
                var estado = estados.FirstOrDefault(e => e.nombreEstado == estadoNombre);

                if (estado != null)
                {
                    var cambio = new CambioEstado
                    {
                        fechaHoraInicio = reader.GetDateTime("fecha_hora_inicio"),
                        fechaHoraFin = reader.IsDBNull("fecha_hora_fin") ? null : reader.GetDateTime("fecha_hora_fin"),
                        Estado = estado
                    };

                    // Cargar motivos asociados
                    cambio.MotivoFueraServicio = CargarMotivosFueraServicio(reader.GetInt32("id"));

                    cambios.Add(cambio);
                }
            }

            return cambios;
        }

        public static List<MotivoFueraServicio> CargarMotivosFueraServicio(int cambioEstadoId)
        {
            var motivos = new List<MotivoFueraServicio>();
            using var con = new SqliteConnection(GetConnectionString());
            con.Open();

            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
                SELECT mfs.id, mfs.comentario, mt.id_tipo, mt.descripcion
                FROM motivos_fuera_servicio mfs
                JOIN motivos_tipo mt ON mfs.motivo_tipo_id = mt.id_tipo
                WHERE mfs.cambio_estado_id = @cambio_estado_id";
            cmd.Parameters.AddWithValue("@cambio_estado_id", cambioEstadoId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var motivoTipo = new MotivoTipo(
                    reader.GetInt32("id_tipo"),
                    reader.GetString("descripcion")
                );

                motivos.Add(new MotivoFueraServicio(
                    motivoTipo,
                    reader.IsDBNull("comentario") ? null : reader.GetString("comentario")
                ));
            }

            return motivos;
        }

        public static List<MotivoTipo> CargarMotivosTipo()
        {
            var motivos = new List<MotivoTipo>();
            using var con = new SqliteConnection(GetConnectionString());
            con.Open();

            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT id_tipo, descripcion FROM motivos_tipo ORDER BY id_tipo";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                motivos.Add(new MotivoTipo(
                    reader.GetInt32("id_tipo"),
                    reader.GetString("descripcion")
                ));
            }

            return motivos;
        }

        public static List<OrdenDeInspeccion> CargarOrdenesInspeccion(
            List<Empleado> empleados, 
            List<EstacionSismologica> estaciones, 
            List<Estado> estados)
        {
            var ordenes = new List<OrdenDeInspeccion>();
            using var con = new SqliteConnection(GetConnectionString());
            con.Open();

            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
                SELECT oi.numero_orden, oi.fecha_hora_inicio, oi.fecha_hora_finalizacion, 
                       oi.fecha_hora_cierre, oi.observacion_cierre, oi.estado_id, 
                       oi.empleado_id, oi.estacion_id, e.nombre_estado as estado_nombre,
                       emp.nombre, emp.apellido, emp.mail, emp.telefono
                FROM ordenes_inspeccion oi
                LEFT JOIN estados e ON oi.estado_id = e.id
                JOIN empleados emp ON oi.empleado_id = emp.id";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var orden = new OrdenDeInspeccion
                {
                    numeroOrden = reader.GetInt32("numero_orden"),
                    fechaHoraInicio = reader.GetDateTime("fecha_hora_inicio"),
                    fechaHoraFinalizacion = reader.IsDBNull("fecha_hora_finalizacion") ? null : reader.GetDateTime("fecha_hora_finalizacion"),
                    fechaHoraCierre = reader.IsDBNull("fecha_hora_cierre") ? null : reader.GetDateTime("fecha_hora_cierre"),
                    observacionCierre = reader.IsDBNull("observacion_cierre") ? null : reader.GetString("observacion_cierre")
                };

                // Asignar estado
                if (!reader.IsDBNull("estado_nombre"))
                {
                    var estadoNombre = reader.GetString("estado_nombre");
                    orden.Estado = estados.FirstOrDefault(e => e.nombreEstado == estadoNombre);
                }

                // Asignar empleado
                var empleadoId = reader.GetInt32("empleado_id");
                orden.Empleado = empleados.FirstOrDefault(emp => 
                    emp.nombre == reader.GetString("nombre") && 
                    emp.apellido == reader.GetString("apellido"));

                // Asignar estación
                var estacionId = reader.GetString("estacion_id");
                orden.EstacionSismologica = estaciones.FirstOrDefault(e => e.codigoEstacion == estacionId);

                ordenes.Add(orden);
            }

            return ordenes;
        }

        // ========== OPERACIONES DE ESCRITURA ==========

        public static int ObtenerIdEstado(string nombreEstado)
        {
            using var con = new SqliteConnection(GetConnectionString());
            con.Open();

            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT id FROM estados WHERE nombre_estado = @nombre_estado";
            cmd.Parameters.AddWithValue("@nombre_estado", nombreEstado);

            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public static int PersistirCambioEstado(
            string sismografoId,
            CambioEstado cambioEstadoAnterior,
            CambioEstado nuevoCambioEstado)
        {
            using var con = new SqliteConnection(GetConnectionString());
            con.Open();
            using var transaction = con.BeginTransaction();

            try
            {
                // 1. Finalizar el cambio de estado anterior si existe
                if (cambioEstadoAnterior != null && cambioEstadoAnterior.fechaHoraFin.HasValue)
                {
                    // Buscar el ID del cambio de estado anterior en BD para actualizarlo
                    // Nota: Si no tenemos el ID, buscamos por sismógrafo y fecha
                    using var cmdFinalizar = con.CreateCommand();
                    cmdFinalizar.Transaction = transaction;
                    cmdFinalizar.CommandText = @"
                        UPDATE cambios_estado 
                        SET fecha_hora_fin = @fecha_hora_fin
                        WHERE sismografo_id = @sismografo_id 
                          AND fecha_hora_inicio = @fecha_hora_inicio
                          AND fecha_hora_fin IS NULL";
                    cmdFinalizar.Parameters.AddWithValue("@fecha_hora_fin", cambioEstadoAnterior.fechaHoraFin.Value);
                    cmdFinalizar.Parameters.AddWithValue("@sismografo_id", sismografoId);
                    cmdFinalizar.Parameters.AddWithValue("@fecha_hora_inicio", cambioEstadoAnterior.fechaHoraInicio);
                    cmdFinalizar.ExecuteNonQuery();
                }

                // 2. Persistir el nuevo cambio de estado (ya creado en memoria)
                var estadoId = ObtenerIdEstado(nuevoCambioEstado.Estado.nombreEstado);
                using var cmdCambio = con.CreateCommand();
                cmdCambio.Transaction = transaction;
                cmdCambio.CommandText = @"
                    INSERT INTO cambios_estado (fecha_hora_inicio, fecha_hora_fin, estado_id, sismografo_id)
                    VALUES (@fecha_hora_inicio, @fecha_hora_fin, @estado_id, @sismografo_id);
                    SELECT last_insert_rowid();";
                cmdCambio.Parameters.AddWithValue("@fecha_hora_inicio", nuevoCambioEstado.fechaHoraInicio);
                cmdCambio.Parameters.AddWithValue("@fecha_hora_fin", nuevoCambioEstado.fechaHoraFin ?? (object)DBNull.Value);
                cmdCambio.Parameters.AddWithValue("@estado_id", estadoId);
                cmdCambio.Parameters.AddWithValue("@sismografo_id", sismografoId);
                
                var cambioEstadoId = Convert.ToInt32(cmdCambio.ExecuteScalar());

                // 3. Persistir motivos de fuera de servicio (ya creados en memoria)
                foreach (var motivo in nuevoCambioEstado.MotivoFueraServicio)
                {
                    using var cmdMotivo = con.CreateCommand();
                    cmdMotivo.Transaction = transaction;
                    cmdMotivo.CommandText = @"
                        INSERT INTO motivos_fuera_servicio (cambio_estado_id, motivo_tipo_id, comentario)
                        VALUES (@cambio_estado_id, @motivo_tipo_id, @comentario)";
                    cmdMotivo.Parameters.AddWithValue("@cambio_estado_id", cambioEstadoId);
                    cmdMotivo.Parameters.AddWithValue("@motivo_tipo_id", motivo.Tipo.IdTipo);
                    cmdMotivo.Parameters.AddWithValue("@comentario", motivo.Comentario ?? (object)DBNull.Value);
                    cmdMotivo.ExecuteNonQuery();
                }

                // 4. Actualizar estado actual del sismógrafo
                using var cmdActualizar = con.CreateCommand();
                cmdActualizar.Transaction = transaction;
                cmdActualizar.CommandText = @"
                    UPDATE sismografos 
                    SET estado_actual_id = @estado_id
                    WHERE identificador_sismografo = @sismografo_id";
                cmdActualizar.Parameters.AddWithValue("@estado_id", estadoId);
                cmdActualizar.Parameters.AddWithValue("@sismografo_id", sismografoId);
                cmdActualizar.ExecuteNonQuery();

                transaction.Commit();
                return cambioEstadoId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        // [Obsolete("Usar PersistirCambioEstado en su lugar")]
        // public static int PonerSismografoFueraDeServicio(
        //     string sismografoId,
        //     DateTime fechaHora,
        //     List<Tuple<string, MotivoTipo>> listaMotivosTipoComentario,
        //     Estado estadoFueraServicio,
        //     Empleado responsableLogueado)
        // {
        //     // Crear el cambio de estado en memoria primero
        //     var nuevoCambio = CambioEstado.crear(fechaHora, listaMotivosTipoComentario, estadoFueraServicio, responsableLogueado);
        //     
        //     // Usar el nuevo método
        //     return PersistirCambioEstado(sismografoId, null, nuevoCambio);
        // }

        public static void ActualizarOrdenInspeccion(
            int numeroOrden,
            DateTime fechaHoraCierre,
            string observacion,
            Estado estadoCerrado)
        {
            using var con = new SqliteConnection(GetConnectionString());
            con.Open();

            var estadoId = ObtenerIdEstado(estadoCerrado.nombreEstado);

            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
                UPDATE ordenes_inspeccion 
                SET fecha_hora_cierre = @fecha_hora_cierre,
                    observacion_cierre = @observacion_cierre,
                    estado_id = @estado_id
                WHERE numero_orden = @numero_orden";
            cmd.Parameters.AddWithValue("@fecha_hora_cierre", fechaHoraCierre);
            cmd.Parameters.AddWithValue("@observacion_cierre", observacion ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@estado_id", estadoId);
            cmd.Parameters.AddWithValue("@numero_orden", numeroOrden);
            cmd.ExecuteNonQuery();
        }
    }

    // Clase auxiliar para estados que aún no tienen implementación específica
    internal class EstadoGenerico : Estado
    {
        private readonly string _nombreEstado;
        private readonly string _ambito;

        public EstadoGenerico(string nombreEstado, string ambito)
        {
            _nombreEstado = nombreEstado;
            _ambito = ambito;
        }

        public override string nombreEstado => _nombreEstado;
        public override string ambito => _ambito;

        public override bool esCompletamenteRealizada()
        {
            return _nombreEstado == "Completamente Realizada";
        }

        public override bool esCerrado()
        {
            return _nombreEstado == "Cerrada";
        }

        public override bool esFueraDeServicio()
        {
            return _nombreEstado == "Fuera de Servicio";
        }
    }
}

