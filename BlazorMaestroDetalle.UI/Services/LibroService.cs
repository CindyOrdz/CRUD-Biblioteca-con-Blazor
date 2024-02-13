using BlazorMaestroDetalle.UI.DAO;
using BlazorMaestroDetalle.UI.Models;

namespace BlazorMaestroDetalle.UI.Services
{
    public class LibroService
    {
        private LibroDAO _libroDAO;


        public LibroService(LibroDAO libroDAO)
        {
            _libroDAO = libroDAO;
        }

        public Task Guardar(Libro libro)
        {
            return _libroDAO.Agregar(libro);

        }

        public Task Editar(Libro libro)
        {
            return _libroDAO.Editar(libro);

        }

        public Task<Libro> Detalles(int id)
        {
            return _libroDAO.Detalles(id);
        }

        public Task Borrar(int id)
        {
            return _libroDAO.Borrar(id);
        }

        public Task<List<Libro>> Listar()
        {
            return _libroDAO.Listar();
        }

        public Task<List<Libro>> Buscar(string valor)
        {
            return _libroDAO.Busqueda(valor);
        }

    }
}
