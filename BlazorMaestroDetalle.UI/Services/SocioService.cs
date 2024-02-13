using BlazorMaestroDetalle.UI.DAO;
using BlazorMaestroDetalle.UI.Models;

namespace BlazorMaestroDetalle.UI.Services
{
    public class SocioService
    {
        private readonly SocioDAO _socioDAO;

        public SocioService(SocioDAO socioDAO)
        {
            _socioDAO = socioDAO;
        }

        public Task<Socio> Detalles(int id)
        {
            return _socioDAO.Detalles(id);
        }


        public Task<List<Socio>> Listar()
        {
            return _socioDAO.Listar();
        }

        public Task Borrar(int id)
        {
            return _socioDAO.Borrar(id);
        }

        public Task Editar(Socio socio)
        {
            return _socioDAO.Editar(socio);
        }

        public Task Agregar(Socio socio)
        {

            return _socioDAO.Agregar(socio);
        }

        public Task<List<int>> ListarID()
        {
            return _socioDAO.ListarID();
        }

        //public Task SeleccionAccion(Socio socio)
        //{
        //    if(socio.Id == 0)
        //    {
        //        return _socioDAO.Agregar(socio);
        //    }
        //    else
        //    {
        //        return _socioDAO.Editar(socio);
        //    }

        //}
    }
}
