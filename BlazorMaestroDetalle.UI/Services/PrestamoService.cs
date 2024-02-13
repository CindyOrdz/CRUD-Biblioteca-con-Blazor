using BlazorMaestroDetalle.UI.DAO;
using BlazorMaestroDetalle.UI.Models;

namespace BlazorMaestroDetalle.UI.Services
{
    public class PrestamoService
    {
        private PrestamoDAO _prestamoDAO;


        public PrestamoService(PrestamoDAO prestamoDAO)
        {
            _prestamoDAO = prestamoDAO;
        }

       

        public Task<List<Prestamo>> Listar()
        {
            return _prestamoDAO.HistorialPrestamos();
        }

        public Task Guardar(List<Prestamo> prestamos)
        {
            return _prestamoDAO.Agregar(prestamos);

        }
    }
}

