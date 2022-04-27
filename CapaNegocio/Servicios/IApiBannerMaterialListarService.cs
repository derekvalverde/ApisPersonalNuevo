using CapaDatos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public interface IApiBannerMaterialListarService
    {
        List<clsBannerMaterialListarResponse> obtenerBannerMaterial(int bnnId, string ageOficina);

        
    }
}
