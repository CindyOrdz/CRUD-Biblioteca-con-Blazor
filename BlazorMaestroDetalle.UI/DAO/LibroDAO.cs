using BlazorMaestroDetalle.UI.Models;
using MySql.Data.MySqlClient;

namespace BlazorMaestroDetalle.UI.DAO
{
    public class LibroDAO
    {
        //Dependencia del servicio MySqlConnection que se registró en program
        private readonly MySqlConnection _connection;

        public LibroDAO(MySqlConnection connection)
        {
            _connection = connection;
        }


        public async Task Agregar(Libro libro)
        {
            string query = "INSERT INTO libro (titulo, autor, numpag) VALUES (@titulo, @autor, @numpag)";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            
            cmd.Parameters.AddWithValue("@titulo", libro.Titulo);
            cmd.Parameters.AddWithValue("@autor", libro.Autor);
            cmd.Parameters.AddWithValue("@numpag", libro.NumPag);
            try
            {
                await _connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar el libro: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task Borrar(int id)
        {
            string query = "DELETE FROM libro WHERE libro = @Id";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", id);
            try
            {
                await _connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al borrar el libro: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task Editar(Libro libro)
        {
            string query = "UPDATE libro SET titulo = @titulo, autor = @autor , numpag = @numpag WHERE libro = @Id";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@id", libro.Id);
            cmd.Parameters.AddWithValue("@titulo", libro.Titulo);
            cmd.Parameters.AddWithValue("@autor", libro.Autor);
            cmd.Parameters.AddWithValue("@numpag", libro.NumPag);
            try
            {
                await _connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al editar el libro: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }



        public async Task<Libro> Detalles(int id)
        {
            string query = "SELECT * FROM libro WHERE libro = @id";

            Libro libro = null;

            try
            {
                await _connection.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    if (await rdr.ReadAsync())
                    {
                        libro = new Libro();
                        libro.Id = rdr.GetInt32(0);
                        libro.Titulo = rdr.GetString(1);
                        libro.Autor = rdr.GetString(2);
                        libro.NumPag = rdr.GetInt32(3);
                    }
                }

                return libro;
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


        public async Task<List<Libro>> Listar()
        {
            string query = "SELECT * FROM libro";

            List<Libro> libros = new List<Libro>();

            try
            {
                await _connection.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, _connection);

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        Libro MiLibro = new Libro();
                        MiLibro.Id = rdr.GetInt32(0);
                        MiLibro.Titulo = rdr.GetString(1);
                        MiLibro.Autor = rdr.GetString(2);
                        MiLibro.NumPag = rdr.GetInt32(3);
                        libros.Add(MiLibro);
                    }
                }

                return libros;
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


        public async Task<List<Libro>> Busqueda(string valorBusqueda)
        {
            string query = "SELECT * FROM libro WHERE titulo LIKE @valorBusqueda";


            List<Libro> libros = new List<Libro>();

            try
            {
                await _connection.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@valorBusqueda", $"%{valorBusqueda}%");

                using (MySqlDataReader rdr = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        Libro MiLibro = new Libro();
                        MiLibro.Id = rdr.GetInt32(0);
                        MiLibro.Titulo = rdr.GetString(1);
                        MiLibro.Autor = rdr.GetString(2);
                        MiLibro.NumPag = rdr.GetInt32(3);
                        libros.Add(MiLibro);
                    }
                }

                return libros;
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
