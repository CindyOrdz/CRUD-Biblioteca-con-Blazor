using BlazorMaestroDetalle.UI.Models;
using MySql.Data.MySqlClient;

namespace BlazorMaestroDetalle.UI.DAO
{
    public class SocioDAO
    {
        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection _connection;

        public SocioDAO(MySqlConnection connection)
        {
            _connection = connection;
        }


        public async Task Agregar(Socio socio)
        {
            string query = "INSERT INTO socio (nombre, apellidos, telefono, correo, ingreso,foto) VALUES (@nombre, @apellidos, @telefono, @correo, @ingreso, @rutaImagen)";
            MySqlCommand cmd = new MySqlCommand(query, _connection);

            cmd.Parameters.AddWithValue("@nombre", socio.Nombre);
            cmd.Parameters.AddWithValue("@apellidos", socio.Apellidos);
            cmd.Parameters.AddWithValue("@telefono", socio.Telefono);
            cmd.Parameters.AddWithValue("@correo", socio.Correo);
            cmd.Parameters.AddWithValue("@ingreso", socio.Ingreso);
            cmd.Parameters.AddWithValue("@rutaImagen", socio.RutaImagen);  // Agrega el parámetro de la ruta de la imagen

            try
            {
                await _connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                Console.Out.WriteLine(ex.Message);
                throw new Exception("Error al agregar el socio: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task<List<Socio>> Listar()
        {
            string query = "SELECT * FROM socio";

            List<Socio> socios = new List<Socio>();

            try
            {
                await _connection.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, _connection);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        Socio MiSocio = new Socio();
                        MiSocio.Id = rdr.GetInt32(0);
                        MiSocio.Nombre = rdr.GetString(1);
                        MiSocio.Apellidos = rdr.GetString(2);
                        MiSocio.Telefono = rdr.GetString(3);
                        MiSocio.Ingreso = rdr.GetDateTime(4);
                        MiSocio.Correo = rdr.GetString(5);
                        
                        socios.Add(MiSocio);
                    }
                }

                return socios;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task<Socio> Detalles(int id)
        {
            string query = "SELECT * FROM socio WHERE socio = @id";

            Socio socio = null;

            try
            {
                await _connection.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    if (await rdr.ReadAsync())
                    {
                        socio = new Socio();
                        socio.Id = rdr.GetInt32(0);
                        socio.Nombre = rdr.GetString(1);
                        socio.Apellidos = rdr.GetString(2);
                        socio.Telefono = rdr.GetString(3);
                        socio.Ingreso = rdr.GetDateTime(4);
                        socio.Correo = rdr.GetString(5);
                        
                    }
                }

                return socio;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }


        public async Task Editar(Socio socio)
        {
            string query = "UPDATE socio SET nombre = @nombre, apellidos = @apellidos , telefono = @tel, ingreso = @ingreso, correo=@correo WHERE socio = @Id";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@id", socio.Id);
            cmd.Parameters.AddWithValue("@nombre", socio.Nombre);
            cmd.Parameters.AddWithValue("@apellidos", socio.Apellidos);
            cmd.Parameters.AddWithValue("@tel", socio.Telefono);
            cmd.Parameters.AddWithValue("@ingreso", socio.Ingreso);
            cmd.Parameters.AddWithValue("@correo", socio.Correo);

            try
            {
                await _connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al editar el socio: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task Borrar(int id)
        {
            string query = "DELETE FROM socio WHERE socio = @Id";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", id);
            try
            {
                await _connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al borrar el registro: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }


        public async Task<List<int>> ListarID()
        {
            string query = "SELECT socio FROM socio";

            List<int> IDsocios = new List<int>();

            try
            {
                await _connection.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, _connection);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        int dato = rdr.GetInt32(0);
                        IDsocios.Add(dato);
                    }
                }

                return IDsocios;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }



    }


}
