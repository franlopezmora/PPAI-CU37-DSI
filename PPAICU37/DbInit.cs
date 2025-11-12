using System.Data;
using Microsoft.Data.Sqlite;

namespace PPAICU37
{
    public static class DbInit
    {
        private static string DataDir => Path.Combine(AppContext.BaseDirectory, "data");
        private static string DbPath => Path.Combine(DataDir, "estaciones_sismologicas.sqlite");

        public static void EnsureDatabase()
        {
            Directory.CreateDirectory(DataDir);

            // Si la base de datos existe pero está vacía o corrupta, eliminarla
            if (File.Exists(DbPath))
            {
                try
                {
                    using var testCon = new SqliteConnection($"Data Source={DbPath}");
                    testCon.Open();
                    using var testCmd = testCon.CreateCommand();
                    testCmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='estados'";
                    var tableExists = testCmd.ExecuteScalar();
                    if (tableExists == null)
                    {
                        // La base de datos existe pero no tiene las tablas, eliminarla
                        testCon.Close();
                        File.Delete(DbPath);
                    }
                }
                catch
                {
                    // Si hay error al verificar, eliminar y recrear
                    try { File.Delete(DbPath); } catch { }
                }
            }

            if (!File.Exists(DbPath))
            {
                // Opción A: copiar base
                var basePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "db", "base", "escuela.v1.sqlite"));
                if (File.Exists(basePath))
                {
                    File.Copy(basePath, DbPath);
                }
                else
                {
                    // Opción B: crear desde scripts
                    using var con = new SqliteConnection($"Data Source={DbPath}");
                    con.Open();
                    
                    // Intentar diferentes paths posibles (usando GetFullPath para normalizar)
                    // Buscar desde el directorio del ejecutable hacia arriba hasta encontrar db/schema.sql
                    var schemaPaths = new List<string>();
                    var currentDir = AppContext.BaseDirectory;
                    
                    // Buscar hasta 5 niveles hacia arriba
                    for (int i = 0; i < 5; i++)
                    {
                        var path = Path.GetFullPath(Path.Combine(currentDir, "db", "schema.sql"));
                        schemaPaths.Add(path);
                        currentDir = Path.GetDirectoryName(currentDir);
                        if (string.IsNullOrEmpty(currentDir)) break;
                    }
                    
                    // También buscar desde el directorio actual
                    currentDir = Directory.GetCurrentDirectory();
                    for (int i = 0; i < 5; i++)
                    {
                        var path = Path.GetFullPath(Path.Combine(currentDir, "db", "schema.sql"));
                        if (!schemaPaths.Contains(path))
                            schemaPaths.Add(path);
                        currentDir = Path.GetDirectoryName(currentDir);
                        if (string.IsNullOrEmpty(currentDir)) break;
                    }
                    
                    string? schemaPath = null;
                    foreach (var path in schemaPaths)
                    {
                        if (File.Exists(path))
                        {
                            schemaPath = path;
                            System.Diagnostics.Debug.WriteLine($"Schema encontrado en: {path}");
                            break;
                        }
                    }
                    
                    if (schemaPath != null)
                    {
                        ExecSqlFromFile(con, schemaPath);
                        System.Diagnostics.Debug.WriteLine("Schema ejecutado correctamente");
                    }
                    else
                    {
                        var errorMsg = $"No se pudo encontrar schema.sql. Buscado en:\n{string.Join("\n", schemaPaths)}\nBaseDirectory: {AppContext.BaseDirectory}\nCurrentDirectory: {Directory.GetCurrentDirectory()}";
                        System.Diagnostics.Debug.WriteLine(errorMsg);
                        throw new FileNotFoundException(errorMsg);
                    }
                    
                    // Buscar seed.sql (mismo patrón que schema.sql)
                    var seedPaths = new List<string>();
                    currentDir = AppContext.BaseDirectory;
                    
                    for (int i = 0; i < 5; i++)
                    {
                        var path = Path.GetFullPath(Path.Combine(currentDir, "db", "seed.sql"));
                        seedPaths.Add(path);
                        currentDir = Path.GetDirectoryName(currentDir);
                        if (string.IsNullOrEmpty(currentDir)) break;
                    }
                    
                    currentDir = Directory.GetCurrentDirectory();
                    for (int i = 0; i < 5; i++)
                    {
                        var path = Path.GetFullPath(Path.Combine(currentDir, "db", "seed.sql"));
                        if (!seedPaths.Contains(path))
                            seedPaths.Add(path);
                        currentDir = Path.GetDirectoryName(currentDir);
                        if (string.IsNullOrEmpty(currentDir)) break;
                    }
                    
                    string? seedPath = null;
                    foreach (var path in seedPaths)
                    {
                        if (File.Exists(path))
                        {
                            seedPath = path;
                            System.Diagnostics.Debug.WriteLine($"Seed encontrado en: {path}");
                            break;
                        }
                    }
                    
                    if (seedPath != null)
                    {
                        ExecSqlFromFile(con, seedPath);
                        System.Diagnostics.Debug.WriteLine("Seed ejecutado correctamente");
                    }
                }
            }

            // Verificar que las tablas existen
            using var verifyCon = new SqliteConnection($"Data Source={DbPath}");
            verifyCon.Open();
            using var verifyCmd = verifyCon.CreateCommand();
            verifyCmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='estados'";
            var result = verifyCmd.ExecuteScalar();
            if (result == null)
            {
                throw new InvalidOperationException($"La base de datos se creó pero la tabla 'estados' no existe. Ruta de BD: {DbPath}");
            }

            // (Opcional) Migraciones incrementales
            ApplyMigrations();
        }

        private static void ApplyMigrations()
        {
            using var con = new SqliteConnection($"Data Source={DbPath}");
            con.Open();

            // Usamos PRAGMA user_version como número de versión
            var current = GetUserVersion(con);
            var migrationsDir = Path.Combine(AppContext.BaseDirectory, "db", "migrations");
            
            if (!Directory.Exists(migrationsDir)) return;

            var files = Directory.EnumerateFiles(migrationsDir, "*.sql")
                                 .OrderBy(f => f) // Ordena 001_, 002_, ...
                                 .ToList();

            foreach (var file in files)
            {
                // Convención: 001_xxx.sql => versión 1, 002 => versión 2, etc.
                var fileName = Path.GetFileName(file);
                if (fileName.Length >= 3 && int.TryParse(fileName.Substring(0, 3), out int target) && target > current)
                {
                    ExecSqlFromFile(con, file);
                    SetUserVersion(con, target);
                    current = target;
                }
            }
        }

        private static int GetUserVersion(SqliteConnection con)
        {
            using var cmd = con.CreateCommand();
            cmd.CommandText = "PRAGMA user_version;";
            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        private static void SetUserVersion(SqliteConnection con, int v)
        {
            using var cmd = con.CreateCommand();
            cmd.CommandText = $"PRAGMA user_version = {v};";
            cmd.ExecuteNonQuery();
        }

        private static void ExecSqlFromFile(SqliteConnection con, string path)
        {
            if (!File.Exists(path))
            {
                System.Diagnostics.Debug.WriteLine($"Archivo no encontrado: {path}");
                return;
            }

            var sql = File.ReadAllText(path);
            System.Diagnostics.Debug.WriteLine($"Ejecutando SQL desde: {path} (tamaño: {sql.Length} caracteres)");
            
            // SQLite permite ejecutar múltiples comandos usando ExecuteNonQuery directamente
            // pero necesitamos dividirlos correctamente. Usaremos un enfoque más simple:
            // ejecutar el SQL completo y luego dividir por ';' si es necesario
            
            // Primero intentar ejecutar todo el SQL de una vez
            // SQLite puede ejecutar múltiples comandos si están separados por ';'
            try
            {
                using var cmd = con.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine("SQL ejecutado exitosamente como un bloque");
                return;
            }
            catch (SqliteException ex)
            {
                // Si falla, intentar ejecutar comando por comando
                System.Diagnostics.Debug.WriteLine($"Error ejecutando SQL completo, intentando comando por comando: {ex.Message}");
            }
            
            // Si falla, dividir por ';' y ejecutar cada comando por separado
            // Filtrar comentarios primero
            var lines = sql.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var sqlLines = new List<string>();
            
            foreach (var line in lines)
            {
                var trimmed = line.Trim();
                // Saltar líneas de comentarios
                if (trimmed.StartsWith("--", StringComparison.Ordinal) || 
                    trimmed.StartsWith("/*", StringComparison.Ordinal) ||
                    string.IsNullOrWhiteSpace(trimmed))
                {
                    continue;
                }
                sqlLines.Add(trimmed);
            }
            
            var cleanSql = string.Join(" ", sqlLines);
            
            // Dividir por ';' pero solo si no está dentro de comillas
            var commands = SplitSqlCommands(cleanSql);
            
            System.Diagnostics.Debug.WriteLine($"Total de comandos a ejecutar: {commands.Count}");
            
            foreach (var command in commands)
            {
                if (string.IsNullOrWhiteSpace(command)) continue;

                using var cmd = con.CreateCommand();
                cmd.CommandText = command;
                try
                {
                    cmd.ExecuteNonQuery();
                    System.Diagnostics.Debug.WriteLine($"Comando ejecutado: {command.Substring(0, Math.Min(50, command.Length))}...");
                }
                catch (SqliteException ex)
                {
                    // Log del error pero continuar (algunos comandos pueden fallar si ya existen)
                    var errorMsg = $"Error ejecutando SQL: {command.Substring(0, Math.Min(100, command.Length))}... Error: {ex.Message}";
                    System.Diagnostics.Debug.WriteLine(errorMsg);
                    
                    // Re-lanzar si es un error crítico (no es "already exists")
                    if (!ex.Message.Contains("already exists", StringComparison.OrdinalIgnoreCase) &&
                        !ex.Message.Contains("duplicate", StringComparison.OrdinalIgnoreCase) &&
                        !ex.Message.Contains("UNIQUE constraint", StringComparison.OrdinalIgnoreCase))
                    {
                        throw new InvalidOperationException($"{errorMsg}\n\nComando completo:\n{command}", ex);
                    }
                }
            }
            
            System.Diagnostics.Debug.WriteLine("Todos los comandos SQL ejecutados");
        }
        
        private static List<string> SplitSqlCommands(string sql)
        {
            var commands = new List<string>();
            var currentCommand = new System.Text.StringBuilder();
            bool inSingleQuote = false;
            bool inDoubleQuote = false;
            
            for (int i = 0; i < sql.Length; i++)
            {
                char c = sql[i];
                char prevChar = i > 0 ? sql[i - 1] : '\0';
                
                // Manejar comillas simples
                if (c == '\'' && prevChar != '\\')
                {
                    inSingleQuote = !inSingleQuote;
                }
                // Manejar comillas dobles
                else if (c == '"' && prevChar != '\\')
                {
                    inDoubleQuote = !inDoubleQuote;
                }
                
                currentCommand.Append(c);
                
                // Si encontramos un ';' fuera de strings, es el fin de un comando
                if (c == ';' && !inSingleQuote && !inDoubleQuote)
                {
                    var command = currentCommand.ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(command))
                    {
                        commands.Add(command);
                    }
                    currentCommand.Clear();
                }
            }
            
            // Agregar el último comando si no termina con ';'
            var lastCommand = currentCommand.ToString().Trim();
            if (!string.IsNullOrWhiteSpace(lastCommand))
            {
                commands.Add(lastCommand);
            }
            
            return commands;
        }

        public static string GetConnectionString()
        {
            return $"Data Source={DbPath}";
        }
    }
}

