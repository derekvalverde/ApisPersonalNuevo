using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiOutBCP.Models;

namespace CapaNegocio.Servicios
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }
}
