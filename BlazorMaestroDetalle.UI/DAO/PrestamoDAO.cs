using BlazorMaestroDetalle.UI.Models;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace BlazorMaestroDetalle.UI.DAO
{
    public class PrestamoDAO
    {
        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection _connection;

        public PrestamoDAO(MySqlConnection connection)
        {
            _connection = connection;
        }


        public async Task<List<Prestamo>> HistorialPrestamos()
        {
            List<Prestamo> historialPrestamos = new List<Prestamo>();


            try
            {
                await _connection.OpenAsync();

                string consulta = "SELECT libro.libro, libro.titulo, cantidad, socio.socio, socio.nombre, socio.apellidos, fprestamo, fdevolucion FROM prestamo INNER JOIN libro ON prestamo.libro = libro.libro INNER JOIN socio ON prestamo.socio = socio.socio order by fprestamo desc";
                MySqlCommand comando = new MySqlCommand(consulta, _connection);

                using (MySqlDataReader lector = (MySqlDataReader)await comando.ExecuteReaderAsync())
                {
                    while (await lector.ReadAsync())
                    {
                        Prestamo prestamo = new Prestamo();
                        prestamo.libro = new Libro()
                        {
                            Id = lector.GetInt32(0),
                            Titulo = lector.GetString(1),
                        };

                        prestamo.cantidad = lector.GetInt32(2);
                        prestamo.socio = new Socio()
                        {
                            Id = lector.GetInt32(3),
                            Nombre = lector.GetString(4),
                            Apellidos = lector.GetString(5),
                        };

                        prestamo.fprestamo =  lector.GetDateTime(6);

                        if (lector.IsDBNull(7))
                        {
                            prestamo.fdevolucion = DateTime.MinValue;
                        }
                        else
                        {
                            prestamo.fdevolucion = lector.GetDateTime(7);
                        }
                        


                        historialPrestamos.Add(prestamo);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return historialPrestamos;
        }


        public async Task Agregar(List<Prestamo> prestamos)
        {
            string query = "INSERT INTO prestamo (libro, socio, fprestamo, cantidad) VALUES (@libro, @socio, @prestamo, @cantidad)";
            MySqlCommand cmd = new MySqlCommand(query, _connection);

            try
            {
                await _connection.OpenAsync();

                foreach (var prestamo in prestamos)
                {
                    cmd.Parameters.Clear(); // Limpia los parámetros antes de cada inserción

                    cmd.Parameters.AddWithValue("@libro", prestamo.libro.Id);
                    cmd.Parameters.AddWithValue("@socio", prestamo.socio.Id);
                    cmd.Parameters.AddWithValue("@prestamo", prestamo.fprestamo);
                    cmd.Parameters.AddWithValue("@cantidad", prestamo.cantidad);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar los préstamos: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

    }


}
