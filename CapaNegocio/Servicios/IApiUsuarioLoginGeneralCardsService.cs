using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiUsuarioLoginGeneralCardsService
    {
        clsUsuarioLoginGeneralCardsResponse Authenticate2(string usuLogin, string usuPassword, int grpId);
    }
}
